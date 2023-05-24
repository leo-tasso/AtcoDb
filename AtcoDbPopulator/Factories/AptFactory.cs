// <copyright file="AptFactory.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator.Factories;

/// <summary>
/// Factory for airports, adds it to the db but does not save.
/// </summary>
public class AptFactory
{
    private readonly Random random = new Random();

    /// <summary>
    /// Method to create a new airport.
    /// </summary>
    /// <param name="airport">The name of the airport, Iata code, Icao code.</param>
    /// <param name="dbContext">The relative db.</param>
    /// <returns>The Created center of the airport.</returns>
    public AtcoDbPopulator.Models.Centro Create((string, string, string) airport, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        var newAirport = new AtcoDbPopulator.Models.Aerodromo()
        {
            AdLatitudine = Math.Round((this.random.NextDouble() * 12) + 35.5, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
            AdLongitudine = Math.Round((this.random.NextDouble() * 12.5) + 6.6, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
            CodiceIata = airport.Item2,
            CodiceIcao = airport.Item3,
        };
        AtcoDbPopulator.Models.Pistum newRunway = new AtcoDbPopulator.Models.Pistum()
        {
            CodAd = newAirport.CodiceIcao,
            Orientamento = this.random.Next(0, 19).ToString(),
            Lunghezza = this.random.Next(999),
        };
        dbContext.Aerodromos.Add(newAirport);
        dbContext.Pista.Add(newRunway);
        var newCenter = new AtcoDbPopulator.Models.Centro()
        {
            NomeCentro = airport.Item1 + "APT",
        };
        var newSector = new AtcoDbPopulator.Models.Settore()
        {
            IdSettore = airport.Item1 + "APT" + "Sec",
            CodAd = airport.Item3,
        };
        var newPosition = new AtcoDbPopulator.Models.Postazione()
        {
            IdPostazione = airport.Item1 + "APT" + "TWR",
            IdSettores = new[] { newSector },
            NomeCentro = newCenter.NomeCentro,
        };
        dbContext.Postaziones.Add(newPosition);
        dbContext.Settores.Add(newSector);
        dbContext.Centros.Add(newCenter);
        return newCenter;
    }
}