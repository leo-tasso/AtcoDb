// <copyright file="PositionsUtils.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;

/// <summary>
/// Class to manage positions.
/// </summary>
public class PositionsUtils
{
    /// <summary>
    /// The number of shifts within a day.
    /// </summary>
    public const int ShiftsInDays = 3;

    private const int BaseCapacity = 30;

    private readonly Dictionary<string, ICollection<string>> sectorsInPosition = new ();
    private readonly CenterTurns centerTurns;

    /// <summary>
    /// Initializes a new instance of the <see cref="PositionsUtils"/> class.
    /// </summary>
    /// <param name="centerTurns"> the caller class that manages shifts.</param>
    public PositionsUtils(CenterTurns centerTurns)
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        this.centerTurns = centerTurns;
    }

    /// <summary>
    /// Class to find which positions have to be filled.
    /// </summary>
    /// <param name="center">Center name.</param>
    /// <param name="year">The year.</param>
    /// <param name="month">The month.</param>
    /// <param name="day">The day.</param>
    /// <param name="slot">The shift.</param>
    /// <param name="dbContext">The relative db.</param>
    /// <param name="checkOccupation">If it has to check the occupation.</param>
    /// <returns>The list of positions to fill.</returns>
    public ISet<AtcoDbPopulator.Models.Postazione>? PonderedPositions(
        AtcoDbPopulator.Models.Centro center,
        int year,
        int month,
        int day,
        int slot,
        AtcoDbPopulator.Models.AtctablesContext dbContext,
        bool checkOccupation)
    {
        var positions = dbContext.Postaziones
            .Where(p => p.NomeCentro.Equals(center.NomeCentro))
            .ToHashSet();

        if (positions.Count == 1)
        {
            if (checkOccupation && this.PositionOverCapacity(positions.First(), year, month, day, slot))
            {
                return null;
            }

            return positions;
        }

        var sectorsInCenter = dbContext.Postaziones
            .Where(p => p.NomeCentro.Equals(center.NomeCentro)).SelectMany(p => p.IdSettores).Distinct();
        if (checkOccupation)
        {
            positions = positions.Where(p => !this.PositionOverCapacity(p, year, month, day, slot)).ToHashSet();
        }

        return this.RecursivePositionSearch(positions, sectorsInCenter.Select(s => s.IdSettore).ToHashSet());
    }

    /// <summary>
    /// Method to populate the given positions in the best way.
    /// </summary>
    /// <param name="dbContext"> The relative db.</param>
    /// <param name="date">The relative date.</param>
    /// <param name="shift">The relative shift.</param>
    /// <param name="positions">The positions to populate.</param>
    /// <exception cref="InvalidOperationException">Exception thrown if no solution is found.</exception>
    public void PopulatePositions(
        AtcoDbPopulator.Models.AtctablesContext dbContext,
        DateTime date,
        int shift,
        ICollection<AtcoDbPopulator.Models.Postazione> positions)
    {
        foreach (var position in positions)
        {
            if (this.centerTurns.Shifts.Any(s =>
                    s.Data.Equals(date) && s.Slot == shift && s.IdPostazione != null && s.IdPostazione.Equals(position.IdPostazione)))
            {
                break;
            }

            var involvedSectors = this.SectorsInPosition(position.IdPostazione);
            var suitableController = this.centerTurns.ControllersUtils.GetSuitableController(involvedSectors, date, shift, dbContext) ?? throw new InvalidOperationException("No Controller available for the shift."
                + involvedSectors
                + date
                + shift);

            // create shift
            var newShift = new AtcoDbPopulator.Models.Turno()
            {
                IdControllore = suitableController.IdControllore,
                Retribuzione = CenterTurns.StandardPay,
                Data = date,
                Slot = shift,
                IdPostazione = position.IdPostazione,
                CentroStandBy = null,
            };
            this.centerTurns.Shifts.Add(newShift);
            dbContext.Turnos.Add(newShift);
            this.centerTurns.ControllersUtils.ControllersShifts[newShift.IdControllore].Add(newShift);
        }
    }

    private ISet<AtcoDbPopulator.Models.Postazione>? RecursivePositionSearch(ISet<AtcoDbPopulator.Models.Postazione>? positions, ISet<string> remainingSectors)
    {
        if (remainingSectors.Count == 0)
        {
            // Base case: all sectors have been covered, return the current list of positions
            return new HashSet<AtcoDbPopulator.Models.Postazione>();
        }

        // Initialize variables to track the best combination of positions
        ISet<AtcoDbPopulator.Models.Postazione>? bestCombination = null;
        int minPositionsCount = int.MaxValue;

        foreach (var position in positions!)
        {
            var newPositions = new HashSet<AtcoDbPopulator.Models.Postazione>(positions);
            newPositions.Remove(position);

            var sectorsInCurrentPosition = this.SectorsInPosition(position.IdPostazione);
            if (!sectorsInCurrentPosition.All(remainingSectors.Contains))
            {
                continue;
            }

            var remainingSectorsForPosition = remainingSectors.Except(sectorsInCurrentPosition).ToHashSet();

            var recursiveCombination = this.RecursivePositionSearch(newPositions, remainingSectorsForPosition);
            recursiveCombination?.Add(position);
            if (recursiveCombination != null && recursiveCombination.Count < minPositionsCount)
            {
                // Found a better combination of positions
                bestCombination = recursiveCombination;
                minPositionsCount = recursiveCombination.Count;
            }
        }

        return bestCombination;
    }

    private ICollection<string> SectorsInPosition(string position)
    {
        if (this.sectorsInPosition.TryGetValue(position, out ICollection<string>? dictResult))
        {
            return dictResult;
        }

        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();

        dictResult = dbContext.Settores
            .Where(s => s.IdPostaziones.Select(p => p.IdPostazione).Contains(position)).Select(s => s.IdSettore)
            .ToList();
        this.sectorsInPosition.Add(position, dictResult);

        return dictResult;
    }

    private bool PositionOverCapacity(AtcoDbPopulator.Models.Postazione position, int year, int month, int day, int slot)
    {
        return this.PositionLoad(position, year, month, day, slot) >= this.PositionCapacity(position);
    }

    private int PositionLoad(AtcoDbPopulator.Models.Postazione position, int year, int month, int day, int slot)
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();

        // Query the estimates table to count the number of matching records
        // ReSharper disable once RemoveToList.2
        // It's not possible to remove to list, it's not able to create a query otherwise.
        int estimatesCount = dbContext.Stimatis
            .Where(s => s.OrarioStimato.Month == month && s.OrarioStimato.Year == year && s.OrarioStimato.Day == day &&
                        s.NomePuntoNavigation.IdSettoreNavigation.IdPostaziones.Select(p => p.IdPostazione).Contains(position.IdPostazione))
            .ToList()
            .Count(s => this.SlotOfTime(s.OrarioStimato.TimeOfDay) == slot);

        return estimatesCount;
    }

    private int SlotOfTime(TimeSpan time)
    {
        int totalMinutes = (time.Hours * 60) + time.Minutes;
        int minutesPerPart = 24 * 60 / ShiftsInDays;

        int partIndex = totalMinutes / minutesPerPart;

        return partIndex;
    }

    private int PositionCapacity(AtcoDbPopulator.Models.Postazione position)
    {
        // TODO might be adjusted
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        int sectorsInCurrentPosition = this.SectorsInPosition(position.IdPostazione).Count;
        if (sectorsInCurrentPosition > 1)
        {
            return 1;
        }

        return (int)Math.Truncate(BaseCapacity / Math.Pow(3, sectorsInCurrentPosition - 1));
    }
}