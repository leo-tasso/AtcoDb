// <copyright file="SectorFactory.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator.Factories;

/// <summary>
/// Factory for sectors.
/// </summary>
public class SectorFactory
{
    private const int PointsPerCenter = 30;
    private readonly List<AtcoDbPopulator.Models.Punto> generalPoints = new List<AtcoDbPopulator.Models.Punto>();
    private readonly Random random = new Random();

    /// <summary>
    /// Method to generate a sector.
    /// </summary>
    /// <param name="dbContext">The reference db.</param>
    /// <param name="id">The Name of the new sector.</param>
    /// <param name="positions">The position that will contain the new sector.</param>
    /// <param name="codAd">The code of the ad if it's an apt.</param>
    /// <returns>A generated sector with the required specs.</returns>
    public AtcoDbPopulator.Models.Settore Create(AtcoDbPopulator.Models.AtctablesContext dbContext, string id, AtcoDbPopulator.Models.Postazione[] positions, string? codAd)
    {
        var newSettore = new AtcoDbPopulator.Models.Settore()
        {
            IdPostaziones = positions,
            IdSettore = id,
            CodAd = codAd,
        };
        dbContext.Settores.Add(newSettore);

        // if the code is null points have to be created.
        if (codAd != null)
        {
            return newSettore;
        }

        IList<AtcoDbPopulator.Models.Punto> newPoints = new List<AtcoDbPopulator.Models.Punto>();
        for (int j = 0; j < PointsPerCenter; j++)
        {
            AtcoDbPopulator.Models.Punto newWaypoint = new AtcoDbPopulator.Models.Punto()
            {
                NomePunto = RandomStringGenerator.GenerateRandomString(5),
                PosLatitudine = Math.Round((this.random.NextDouble() * 12) + 35.5, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
                PosLongitudine = Math.Round((this.random.NextDouble() * 12.5) + 6.6, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
                IdSettore = newSettore.IdSettore,
            };
            if (!newPoints.Any(p => p.NomePunto.Equals(newWaypoint.NomePunto)) && !this.generalPoints.Any(p => p.NomePunto.Equals(newWaypoint.NomePunto)))
            {
                newPoints.Add(newWaypoint);
            }
        }

        this.generalPoints.AddRange(newPoints);
        dbContext.Puntos.AddRange(newPoints);

        return newSettore;
    }
}