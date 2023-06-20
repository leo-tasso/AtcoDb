// <copyright file="ControllersUtils.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

// ReSharper disable CanSimplifyDictionaryLookupWithTryAdd
// This way is more efficient.
namespace AtcoDbPopulator;

/// <summary>
/// Class to manage Controllers during the creation of a schedule.
/// </summary>
public class ControllersUtils
{
    private const int MandatoryOffShifts = 3;
    private const int MaxShiftsPerYear = 300;
    private readonly Dictionary<string, ICollection<string>> controllersSkills = new ();
    private readonly Dictionary<string, ICollection<AtcoDbPopulator.Models.Turno>> controllersShifts = new ();
    private readonly ISet<AtcoDbPopulator.Models.Controllore> controllers;

    /// <summary>
    /// Initializes a new instance of the <see cref="ControllersUtils"/> class.
    /// </summary>
    public ControllersUtils()
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        this.controllers = dbContext.Controllores.ToHashSet();
    }

    /// <summary>
    /// Gets method the collection of shifts.
    /// </summary>
    public Dictionary<string, ICollection<AtcoDbPopulator.Models.Turno>> ControllersShifts => this.controllersShifts;

    private ISet<AtcoDbPopulator.Models.Controllore> Controllers => this.controllers;

    /// <summary>
    /// Method (for standby shifts) that given a time and a center it returns the controller with the minimum amount of shift worked.
    /// </summary>
    /// <param name="centerName">The stand by center.</param>
    /// <param name="date">The date.</param>
    /// <param name="shift">The slot.</param>
    /// <param name="dbContext">The relative db.</param>
    /// <returns>The best controller, if present.</returns>
    public AtcoDbPopulator.Models.Controllore? GetSuitableController(string centerName, DateTime date, int shift, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var suitableControllers = this.Controllers.Where(c =>
            c.NomeCentro.Equals(centerName)
            && this.ControllerIsNotTired(c, date, shift, dbContext));

        return suitableControllers.MinBy(this.ShiftsWorked);
    }

    /// <summary>
    /// Method that given the sectors, returns the best controller. (With the minimum amount of shift worked.)
    /// </summary>
    /// <param name="sectors">The required sectors.</param>
    /// <param name="date">The date.</param>
    /// <param name="shift">The slot.</param>
    /// <param name="dbContext">The db.</param>
    /// <returns>The best controller, if present.</returns>
    public AtcoDbPopulator.Models.Controllore? GetSuitableController(
        ICollection<string> sectors,
        DateTime date,
        int shift,
        AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var suitableControllers = this.controllers.Where(c =>
            this.ControllerIsAble(c, sectors, dbContext)
            && this.ControllerIsNotTired(c, date, shift, dbContext));

// a bit of dry not respected with the previous method. Maybe refactor in future.
        return suitableControllers.MinBy(this.ShiftsWorked);
    }

    /// <summary>
    /// Checks if the controller has the licence for the required sectors.
    /// </summary>
    /// <param name="controller">The testing Controller.</param>
    /// <param name="sectors">The required sectors.</param>
    /// <param name="dbContext">The relative db.</param>
    /// <returns>If the controller is able.</returns>
    public bool ControllerIsAble(
        AtcoDbPopulator.Models.Controllore controller,
        ICollection<string> sectors,
        AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var controllerSkills = this.ControllerSectors(controller, dbContext);
        return sectors.All(s => controllerSkills.Contains(s));
    }

    private List<string> ControllerSectors(
        AtcoDbPopulator.Models.Controllore controller,
        AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        if (!this.controllersSkills.ContainsKey(controller.IdControllore))
        {
            this.controllersSkills.Add(controller.IdControllore, dbContext.Abilitaziones
                .Where(a => a.IdControllore == controller.IdControllore)
                .SelectMany(a => a.IdSettores)
                .Select(s => s.IdSettore).ToList());
        }

        return this.controllersSkills[controller.IdControllore].ToList();
    }

    private bool ControllerIsNotTired(
        AtcoDbPopulator.Models.Controllore controller,
        DateTime date,
        int shift,
        AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var controllerShifts = this.GetControllerShifts(controller, dbContext);
        if (controllerShifts.Count >= MaxShiftsPerYear)
        {
            return false;
        }

        var shiftDate = date.AddDays(Math.Truncate(MandatoryOffShifts / 3f));
        var shiftNumber = ((shift + MandatoryOffShifts) % MandatoryOffShifts) + 1;
        for (var i = 0; i <= (MandatoryOffShifts * 2) + 1; i++)
        {
            if (shiftNumber < 1)
            {
                shiftNumber = PositionsUtils.ShiftsInDays;
                shiftDate = shiftDate.AddDays(-1);
            }

            if (controllerShifts.Any(s => s.Slot == shiftNumber && s.Data.Equals(shiftDate)))
            {
                return false;
            }

            shiftNumber--;
        }

        return true;
    }

    private IList<AtcoDbPopulator.Models.Turno> GetControllerShifts(
        AtcoDbPopulator.Models.Controllore controller,
        AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        if (!this.controllersShifts.ContainsKey(controller.IdControllore))
        {
            this.controllersShifts.Add(
                controller.IdControllore,
                dbContext.Turnos.Where(t => t.IdControllore.Equals(controller.IdControllore)).ToList());
        }

        return this.controllersShifts[controller.IdControllore].ToList();
    }

    /// <summary>
    /// Method to get the number of shifts already worked by a controller.
    /// </summary>
    /// <param name="controller">The controller to test.</param>
    /// <returns>The number of shifts worked.</returns>
    private int ShiftsWorked(
        AtcoDbPopulator.Models.Controllore controller)
    {
        return this.controllersShifts[controller.IdControllore].Count;
    }
}