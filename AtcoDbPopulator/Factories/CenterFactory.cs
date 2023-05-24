// <copyright file="CenterFactory.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator.Factories;

/// <summary>
/// Factory of centers.
/// </summary>
public class CenterFactory
{
    private const int NumPositionsPerCenter = 3;
    private readonly SectorFactory sectorFactory;

    /// <summary>
    /// Initializes a new instance of the <see cref="CenterFactory"/> class.
    /// </summary>
    public CenterFactory()
    {
        this.sectorFactory = new SectorFactory();
    }

    /// <summary>
    /// Method to create a new center.
    /// </summary>
    /// <param name="name">The center name.</param>
    /// <param name="dbContext">The relative db.</param>
    /// <returns>The created Center.</returns>
    public AtcoDbPopulator.Models.Centro Create(string name, AtcoDbPopulator.Models.AtctablesContext dbContext)
    {
        // For each center create a position for each sector and a general one merging all sectors.
        var newCenter = new AtcoDbPopulator.Models.Centro()
        {
            NomeCentro = name,
        };
        dbContext.Centros.Add(newCenter);
        var globalPos = new AtcoDbPopulator.Models.Postazione()
        {
            IdPostazione = $"{name}Pos{NumPositionsPerCenter}",
            NomeCentro = name,
        };
        dbContext.Postaziones.Add(globalPos);
        for (int i = 1; i < NumPositionsPerCenter; i++)
        {
            var newPos = new AtcoDbPopulator.Models.Postazione()
            {
                IdPostazione = name + "Pos" + i,
                NomeCentro = name,
            };
            dbContext.Postaziones.Add(newPos);
            this.sectorFactory.Create(dbContext, name + "Sec" + i, new[] { newPos, globalPos }, null);
        }

        return newCenter;
    }
}