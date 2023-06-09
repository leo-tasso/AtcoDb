﻿// <copyright file="CenterTurns.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

// ReSharper disable CanSimplifyDictionaryLookupWithTryAdd
// It slows down the whole thing.
namespace AtcoDbPopulator;

/// <summary>
/// Class to generate shifts.
/// </summary>
public class CenterTurns
{
    /// <summary>
    /// Standard unified pay for each shift.
    /// </summary>
    public const int StandardPay = 80;

    private const int NumTurns = 3;
    private const float StandByRate = 0.3f;

    /// <summary>
    /// Initializes a new instance of the <see cref="CenterTurns"/> class.
    /// </summary>
    public CenterTurns()
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        this.ControllersUtils = new ControllersUtils();
        this.Shifts = dbContext.Turnos.ToHashSet();
        this.PositionsUtils = new PositionsUtils(this);
    }

    /// <summary>
    /// Gets the shifts of this instance.
    /// </summary>
    public ISet<AtcoDbPopulator.Models.Turno> Shifts { get; }

    /// <summary>
    /// Gets the ControllerUtils of this instance.
    /// </summary>
    public ControllersUtils ControllersUtils { get; }

    /// <summary>
    /// Gets property to get the positionUtil of this instance.
    /// </summary>
    private PositionsUtils PositionsUtils { get; }

    /// <summary>
    /// Method to generate the shifts.
    /// </summary>
    /// <param name="center">The relative center.</param>
    /// <param name="month">The relative Month.</param>
    /// <param name="year">The relative year.</param>
    /// <param name="checkOccupation">If the algorithm should analyze occupation and fill controllers accordingly.</param>
    public void CenterTurnsGenerator(AtcoDbPopulator.Models.Centro center, int month, int year, bool checkOccupation)
    {
        var i = new DateTime(year, month, 1);
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        while (i.Month == month)
        {
            for (var slot = 1; slot <= NumTurns; slot++)
            {
                ISet<AtcoDbPopulator.Models.Postazione>? ponderedPosition = this.PositionsUtils.PonderedPositions(center, year, month, i.Day, slot, dbContext, checkOccupation);

                if (ponderedPosition == null || ponderedPosition.Count == 0)
                {
                    throw new Exception("No set of position is valid");
                }

                this.PositionsUtils.PopulatePositions(dbContext, i, slot, ponderedPosition);

                this.PopulateStandbyCenter(dbContext, center.NomeCentro, (int)(ponderedPosition.Count * StandByRate) + 1, i, slot);
            }

            dbContext.SaveChanges();
            i = i.AddDays(1);
        }
    }

    private void PopulateStandbyCenter(AtcoDbPopulator.Models.AtctablesContext dbContext, string center, int requiredPositions, DateTime date, int slot)
    {
        for (int i = 0; i < requiredPositions; i++)
        {
            var suitableController = this.ControllersUtils.GetSuitableController(center, date, slot, dbContext) ?? throw new InvalidOperationException("No Controller available for the shift."
                + center
                + date
                + slot);

            // create shift
            var newShift = new AtcoDbPopulator.Models.Turno()
            {
                IdControllore = suitableController.IdControllore,
                Retribuzione = StandardPay,
                Data = date,
                Slot = slot,
                IdPostazione = null,
                CentroStandBy = center,
            };
            this.Shifts.Add(newShift);
            dbContext.Turnos.Add(newShift);
            this.ControllersUtils.ControllersShifts[newShift.IdControllore].Add(newShift);
        }
    }

    // It would be better to add the methods to the controller class, but since it's autogenerated i preferred to make those method in a static way.
}