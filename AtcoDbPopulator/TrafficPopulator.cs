// <copyright file="TrafficPopulator.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;

/// <summary>
/// Class to populate traffic.
/// </summary>
public class TrafficPopulator
{
    private const int LongestFlightSectors = 10;
    private readonly Random random = new Random();
    private readonly ISet<AtcoDbPopulator.Models.Settore> airBornSectors;
    private readonly Dictionary<string, ICollection<AtcoDbPopulator.Models.Punto>> pointsInSector = new ();

    /// <summary>
    /// Initializes a new instance of the <see cref="TrafficPopulator"/> class.
    /// </summary>
    public TrafficPopulator()
    {
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        this.airBornSectors = dbContext.Settores.Where(s => s.CodAd == null).ToHashSet();
    }

    /// <summary>
    /// Method to populate traffic.
    /// </summary>
    /// <param name="trafficNumValue">The number of traffic to create.</param>
    /// <param name="progressBar">The progressbar to update.</param>
    /// <param name="date">The relevant date.</param>
    public void PopulateTraffic(decimal trafficNumValue, ProgressBar progressBar, DateTime date)
    {
        IList<string> types = AtcoDbPopulator.Utils.FileToList.ReadFileToList("Models/Aircrafts.txt");
        IList<string> companies = AtcoDbPopulator.Utils.FileToList.ReadFileToList("Models/Airlines.txt");
        DateTime startDate = new DateTime(date.Year, date.Month, 1);
        DateTime endDate = new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));

        TimeSpan timeSpan = endDate - startDate;
        int totalDays = timeSpan.Days;
        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();
        var aerodromos = dbContext.Aerodromos.ToList();
        for (int i = 0; i < trafficNumValue; i++)
        {
            progressBar.Value = (int)(i / trafficNumValue * 100);
            Application.DoEvents();
            var adTakeOff = aerodromos[this.random.Next(0, aerodromos.Count)];
            var adLanding = aerodromos[this.random.Next(0, aerodromos.Count)];

            var newPlane = new AtcoDbPopulator.Models.Aereomobile()
            {
                Tipo = types[this.random.Next(0, types.Count)],
                NumeroDiCoda = AtcoDbPopulator.Utils.RandomStringGenerator.GenerateRandomString(1) + "-" +
                               AtcoDbPopulator.Utils.RandomStringGenerator.GenerateRandomString(4),
            };
            var newFlightPlan = new AtcoDbPopulator.Models.Pianodivolo()
            {
                Callsign = this.random.NextDouble() > 0.8
                    ? newPlane.NumeroDiCoda
                    : (companies[this.random.Next(0, companies.Count)] + this.random.Next(100, 10000).ToString()),
                Dof = startDate.AddDays(this.random.Next(totalDays)),
                NumeroDiCoda = newPlane.NumeroDiCoda,
                CodAdDecollo = adTakeOff.CodiceIcao,
                CodAdAtterraggio = adLanding.CodiceIcao,
                OrientamentoPistaDecollo =
                    dbContext.Pista.First(p => p.CodAd.Equals(adTakeOff.CodiceIcao)).Orientamento,
                OrientamentoPistaAtterraggio =
                    dbContext.Pista.First(p => p.CodAd.Equals(adLanding.CodiceIcao)).Orientamento,
            };
            if (dbContext.Aereomobiles.Find(newPlane.NumeroDiCoda) != null || dbContext.Pianodivolos.Find(newFlightPlan.Callsign, newFlightPlan.Dof) != null)
            {
                continue;
            }

            dbContext.Aereomobiles.Add(newPlane);
            dbContext.Pianodivolos.Add(newFlightPlan);
            foreach (var crossingSector in this.airBornSectors.OrderBy(_ => Guid.NewGuid()).Take(this.random.Next(LongestFlightSectors / 2, LongestFlightSectors)))
            {
                var pointsInCurrentSector = this.PointsInSector(crossingSector.IdSettore).Count;
                IList<AtcoDbPopulator.Models.Stimati> newEstimates = new List<AtcoDbPopulator.Models.Stimati>();
                IList<AtcoDbPopulator.Models.Punto> newEstimatesNames = this.PointsInSector(crossingSector.IdSettore)
                    .Take(this.random.Next(pointsInCurrentSector / 2))
                    .ToList();

                for (int k = 0; k < newEstimatesNames.Count; k++)
                {
                    string newEstimatesName = newEstimatesNames[0].NomePunto;
                    newEstimatesNames.RemoveAt(0);
                    var newEstimate = new AtcoDbPopulator.Models.Stimati()
                    {
                        Callsign = newFlightPlan.Callsign,
                        Dof = newFlightPlan.Dof,
                        NomePunto = newEstimatesName,
                        OrarioStimato = newEstimates.Any()
                            ? newEstimates.Max(s => s.OrarioStimato).AddSeconds(this.random.Next(100, 1000))
                            : newFlightPlan.Dof.AddSeconds(this.random.Next(86400)),
                    };
                    newEstimates.Add(newEstimate);
                }

                dbContext.Stimatis.AddRange(newEstimates);
            }
        }

        dbContext.SaveChanges();

        progressBar.Value = 0;
    }

    private ICollection<AtcoDbPopulator.Models.Punto> PointsInSector(string sector)
    {
        if (this.pointsInSector.TryGetValue(sector, out var dictResult))
        {
            return dictResult;
        }

        using var dbContext = new AtcoDbPopulator.Models.AtctablesContext();

        dictResult = dbContext.Puntos.Where(p => p.IdSettore.Equals(sector)).ToList();
        this.pointsInSector.Add(sector, dictResult);

        return dictResult;
    }
}