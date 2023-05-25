// <copyright file="ControllerFactory.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator.Factories;

/// <summary>
/// Factory of controllers, with licence and holidays.
/// </summary>
public class ControllerFactory
{
    private const int MaxHoliday = 15;
    private const int MinHoliday = 2;
    private readonly Random random = new Random();

    /// <summary>
    /// Initializes a new instance of the <see cref="ControllerFactory"/> class.
    /// </summary>
    /// <param name="firstId">The id to begin with.</param>
    public ControllerFactory(int firstId)
    {
        this.LastId = firstId;
    }

    private int LastId { get; set; }

    /// <summary>
    /// Method to generate a new controller.
    /// </summary>
    /// <param name="center">The relative center.</param>
    /// <param name="dbContext">The relative db.</param>
    public void Create(AtcoDbPopulator.Models.Centro center, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        IList<string> names = AtcoDbPopulator.Utils.FileToList.ReadFileToList("Models/Names.txt");
        IList<string> surnames = AtcoDbPopulator.Utils.FileToList.ReadFileToList("Models/Surnames.txt");
        var newController = new AtcoDbPopulator.Models.Controllore()
        {
            IdControllore = this.LastId.ToString(),
            Nome = names[this.random.Next(0, names.Count)],
            Cognome = surnames[this.random.Next(0, surnames.Count)],
            NomeCentro = center.NomeCentro,
        };
        var newLicence = new AtcoDbPopulator.Models.Abilitazione()
        {
            MatricolaAbilitazione = this.LastId,
            IdControllore = newController.IdControllore,
            IdSettores = SectorsInCenter(dbContext.Centros.Find(newController.NomeCentro) !),
        };
        var startDate = new DateTime(DateTime.Now.Year, this.random.Next(1, 13), this.random.Next(1, 28));
        var newHoliday = new AtcoDbPopulator.Models.Ferie()
        {
            IdControllore = this.LastId.ToString(),
            Inizio = startDate,
            Fine = startDate.AddDays(this.random.Next(MinHoliday, MaxHoliday)),
        };
        dbContext.Feries.Add(newHoliday);
        newController.Abilitaziones.Add(newLicence);
        dbContext.Controllores.Add(newController);
        dbContext.Abilitaziones.Add(newLicence);
        this.LastId++;
    }

    private static IList<AtcoDbPopulator.Models.Settore> SectorsInCenter(AtcoDbPopulator.Models.Centro c)
    {
        return c.Postaziones.SelectMany(p => p.IdSettores).ToList();
    }
}