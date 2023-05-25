﻿// <copyright file="CenterTurns.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

using Microsoft.EntityFrameworkCore;

namespace AtcoDbPopulator;

/// <summary>
/// Class to generate shifts.
/// </summary>
public class CenterTurns
{
    private const int ShiftsInDays = 3;
    private const int MandatoryOffShifts = 3;
    private const int MaxShiftsPerYear = 300;
    private const int StandardPay = 80;
    private const int NumTurns = 3;

    private readonly Dictionary<string, ICollection<string>> controllersSkills = new Dictionary<string, ICollection<string>>();
    private readonly Dictionary<string, ICollection<AtcoDbPopulator.Models.Turno>> controllersShifts = new Dictionary<string, ICollection<AtcoDbPopulator.Models.Turno>>();
    private readonly List<AtcoDbPopulator.Models.Controllore> controllers;
    private ICollection<AtcoDbPopulator.Models.Turno> Shifts = new List<AtcoDbPopulator.Models.Turno>();

    /// <summary>
    /// Initializes a new instance of the <see cref="CenterTurns"/> class.
    /// </summary>
    public CenterTurns()
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        this.controllers = dbContext.Controllores.ToList();
        this.Shifts = dbContext.Turnos.ToList();
    }

    /// <summary>
    /// Method to generate the shifts.
    /// </summary>
    /// <param name="center">The relative center.</param>
    /// <param name="month">The relative Month.</param>
    /// <param name="year">The relative year.</param>
    public void CenterTurnsGenerator(AtcoDbPopulator.Models.Centro center, int month, int year)
    {


        DateTime i = new DateTime(year, month, 1);
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        while (i.Month == month)
        {
            var positions = dbContext.Postaziones
                .Where(p => p.NomeCentro.Equals(center.NomeCentro))
                .Distinct()
                .ToList();
            for (int j = 1; j <= NumTurns; j++)
            {
                // TODO allocating only airports, expand with centers, evaluate positions.
                this.PopulatePositions(dbContext, i, j, positions);

                // TODO addStandby
            }

            i = i.AddDays(1);
        }

        dbContext.SaveChanges();
    }

    private void PopulatePositions(AtcoDbPopulator.Models.AtctablesContext dbContext, DateTime date, int shift, ICollection<AtcoDbPopulator.Models.Postazione> positions)
    {
        foreach (var position in positions)
        {
            if(Shifts.Any(s=>s.Data.Equals(date)&&s.Slot==shift&&s.IdPostazione.Equals(position.IdPostazione))) break;
            var involvedSectors = dbContext.Settores
                .Where(s => s.IdPostaziones.Contains(position))
                .Select(s => s.IdSettore).ToList();
            AtcoDbPopulator.Models.Controllore? suitableController = this.GetSuitableController(involvedSectors, date, shift, dbContext);
            if (suitableController == null)
            {
                throw new InvalidOperationException("No Controller available for the shift." + involvedSectors + date + shift);
            }

            // create shift
            AtcoDbPopulator.Models.Turno newShift = new AtcoDbPopulator.Models.Turno()
            {
                IdControllore = suitableController.IdControllore,
                Retribuzione = StandardPay,
                Data = date,
                Slot = shift,
                IdPostazione = position.IdPostazione,
                CentroStandBy = null,
            };
            Shifts.Add(newShift);
            dbContext.Turnos.Add(newShift);
            this.controllersShifts[newShift.IdControllore].Add(newShift);
        }
    }

    private AtcoDbPopulator.Models.Controllore? GetSuitableController(
        ICollection<string> sectors,
        DateTime date,
        int shift,
        AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var suitableControllers = this.controllers.Where(c =>
                this.ControllerIsAble(c, sectors, dbContext)
                && this.ControllerIsNotTired(c, date, shift, dbContext));

        return suitableControllers.MinBy(c => this.ShiftsWorked(c, dbContext));
    }

    // It would be better to add the methods to the controller class, but since it's autogenerated i preferred to make those method in a static way.

    /// <summary>
    /// Checks if the controller has the licence for the required sectors.
    /// </summary>
    /// <param name="controller">The testing Controller.</param>
    /// <param name="sectors">The required sectors.</param>
    /// <param name="dbContext">The relative db.</param>
    /// <returns>If the controller is able.</returns>
    public bool ControllerIsAble(AtcoDbPopulator.Models.Controllore controller, ICollection<string> sectors, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var controllerSkills = this.ControllerSectors(controller, dbContext);
        return sectors.All(s => controllerSkills.Contains(s));
    }

    private List<string> ControllerSectors(AtcoDbPopulator.Models.Controllore controller, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {

        if (!this.controllersSkills.ContainsKey(controller.IdControllore))
        {
            this.controllersSkills.Add(controller.IdControllore, dbContext.Abilitaziones
                .Where(a => a.IdControllore == controller.IdControllore)
                .SelectMany(a => a.IdSettores)
                .Select(s=>s.IdSettore).ToList());
        }

        return this.controllersSkills[controller.IdControllore].ToList();
    }

    public bool ControllerIsNotTired(AtcoDbPopulator.Models.Controllore controller, DateTime date, int shift, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {

        var controllerShifts = getControllerShifts(controller,dbContext); 
        if (controllerShifts.Count() >= MaxShiftsPerYear)
        {
            return false;
        }

        var shiftDate = date.AddDays(Math.Truncate((double)(CenterTurns.MandatoryOffShifts/3)));
        int shiftNumber = (shift + MandatoryOffShifts) % CenterTurns.MandatoryOffShifts + 1 ;
        for (int i = 0; i <= MandatoryOffShifts*2+1; i++)
        {
            if (shiftNumber < 1)
            {
                shiftNumber = ShiftsInDays;
                shiftDate.AddDays(-1);
            }

            if (controllerShifts.Any(s => s.Slot == shiftNumber && s.Data.Equals(shiftDate))) return false;
            shiftNumber--;
        }

        return true;
    }

    private IList<AtcoDbPopulator.Models.Turno> getControllerShifts(AtcoDbPopulator.Models.Controllore controller, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        if (!controllersShifts.ContainsKey(controller.IdControllore))
        {
            controllersShifts.Add(controller.IdControllore,dbContext.Turnos.Where(t => t.IdControllore.Equals(controller.IdControllore)).ToList());
        }

        return controllersShifts[controller.IdControllore].ToList();
    }

    public int ShiftsWorked(AtcoDbPopulator.Models.Controllore controller, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        return this.controllersShifts[controller.IdControllore].Count;
    }
}