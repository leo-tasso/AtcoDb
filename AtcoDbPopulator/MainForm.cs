// <copyright file="MainForm.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator
{
    using System.Linq;
    using AtcoDbPopulator.Models;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// MainForm of the Populator.
    /// </summary>
    public partial class MainForm : Form
    {
        private const string FilePath = "Models/Airports.txt";
        private const int NumPositionsPerCenter = 3;
        private const int PointsPerCenter = 30;
        private const int NumControllersEachCenter = 10;
        private const int LongestFlightSectors = 10;
        private readonly Player player;
        private readonly Random random = new Random();
        private readonly string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.player = new Player(this);
        }

        private static int MaxControllerId(AtctablesContext dbContext)
        {
            return dbContext.Controllores.Any() ? dbContext.Controllores.ToList().Max(c => int.Parse(c.IdControllore)) : 0;
        }

        private void InitializeCenters()
        {
            IList<string> centersNames = FileToList.ReadFileToList("Models/Centers.txt");
            foreach (string s in centersNames)
            {
                using var dbContext = new AtctablesContext();
                var newCenter = new Centro()
                {
                    NomeCentro = s,
                };
                dbContext.Centros.Add(newCenter);
                var globalPos = new Postazione()
                {
                    IdPostazione = $"{s}Pos{NumPositionsPerCenter}",
                    NomeCentro = s,
                };
                dbContext.Postaziones.Add(globalPos);
                IList<Settore> newSectors = new List<Settore>();
                if (newSectors == null)
                {
                    throw new ArgumentNullException(nameof(newSectors));
                }

                for (int i = 1; i < NumPositionsPerCenter; i++)
                {
                    var newPos = new Postazione()
                    {
                        IdPostazione = s + "Pos" + i,
                        NomeCentro = s,
                    };
                    dbContext.Postaziones.Add(newPos);
                    var newSettore = this.SectorFactory(dbContext, s + "Sec" + i, new[] { newPos, globalPos }, null);
                    newSectors.Add(newSettore);
                }

                dbContext.SaveChanges();
                for (int i = 1; i < NumControllersEachCenter; i++)
                {
                    this.ControllerFactory(MaxControllerId(dbContext) + 1, newCenter, dbContext);
                }
            }
        }

        private Settore SectorFactory(AtctablesContext dbContext, string id, Postazione[] positions, string? codAd)
        {
            var newSettore = new Settore()
            {
                IdPostaziones = positions,
                IdSettore = id,
                CodAd = codAd,
            };
            dbContext.Settores.Add(newSettore);
            if (codAd == null)
            {
                IList<Punto> newPoints = new List<Punto>();
                for (int j = 0; j < PointsPerCenter; j++)
                {
                    Punto newWaypoint = new Punto()
                    {
                        NomePunto = RandomStringGenerator.GenerateRandomString(5),
                        PosLatitudine = Math.Round((this.random.NextDouble() * 12) + 35.5, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
                        PosLongitudine = Math.Round((this.random.NextDouble() * 12.5) + 6.6, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
                        IdSettore = newSettore.IdSettore,
                    };
                    if (!newPoints.Any(p => p.NomePunto.Equals(newWaypoint.NomePunto)) && dbContext.Puntos.Find(newWaypoint.NomePunto) == null)
                    {
                        newPoints.Add(newWaypoint);
                    }
                }

                dbContext.Puntos.AddRange(newPoints);
            }

            return newSettore;
        }

        private void PopulateControllersButtonClick(object sender, EventArgs e)
        {
            this.populateControllersButton.Enabled = false;
            int newId;
            using (AtctablesContext
                   dbContext = new AtctablesContext()) // Replace with the name of your generated DbContext class.
            {
                newId = MaxControllerId(dbContext);
            }

            for (int i = newId; i < this.controllerNum.Value + newId; i++)
            {
                using var dbContext = new AtctablesContext();
                this.ControllerFactory(i + 1, dbContext.Centros.ToArray()[this.random.Next(0, dbContext.Centros.Count())], dbContext);

                var startDate = new DateTime(DateTime.Now.Year, this.random.Next(1, 13), this.random.Next(1, 28));
                var newHoliday = new Ferie()
                {
                    IdControllore = (i + 1).ToString(),
                    Inizio = startDate,
                    Fine = startDate.AddDays(15),
                };
                dbContext.Feries.Add(newHoliday);
                dbContext.SaveChanges();
            }
        }

        private void ControllerFactory(int id, Centro center, AtctablesContext dbContext)
        {
            IList<string> names = FileToList.ReadFileToList("Models/Names.txt");
            IList<string> surnames = FileToList.ReadFileToList("Models/Surnames.txt");
            var newController = new Controllore()
            {
                IdControllore = id.ToString(),
                Nome = names[this.random.Next(0, names.Count)],
                Cognome = surnames[this.random.Next(0, surnames.Count)],
                NomeCentro = center.NomeCentro,
            };
            var newLicence = new Abilitazione()
            {
                MatricolaAbilitazione = dbContext.Abilitaziones.Any() ? dbContext.Abilitaziones.Max(a => a.MatricolaAbilitazione) + 1 : 1,
                IdControllore = newController.IdControllore,
                IdSettores = this.SectorsInCenter(dbContext.Centros.Find(newController.NomeCentro) !),
            };
            newController.Abilitaziones.Add(newLicence);
            dbContext.Controllores.Add(newController);
            dbContext.Abilitaziones.Add(newLicence);
            dbContext.SaveChanges();
        }

        private IList<Settore> SectorsInCenter(Centro c)
        {
            return c.Postaziones.SelectMany(p => p.IdSettores).ToList();
        }

        private void CentersPopulateButtonClick(object sender, EventArgs e)
        {
            this.centersPopulateButton.Enabled = false;
            this.InitializeCenters();
            this.InitializeApts();
            this.populateControllersButton.Enabled = true;
            this.trafficPopulatorButton.Enabled = true;
        }

        private void InitializeApts()
        {
            using var dbContext = new AtctablesContext();
            foreach (var airport in new AirportFetcher().FetchAirportInfo(this.fullPath))
            {
                this.AptFactory(airport, dbContext);
                var newCenter = new Centro()
                {
                    NomeCentro = airport.Item1 + "APT",
                };
                var newSector = new Settore()
                {
                    IdSettore = airport.Item1 + "APT" + "Sec",
                    CodAd = airport.Item3,
                };
                var newPosition = new Postazione()
                {
                    IdPostazione = airport.Item1 + "APT" + "TWR",
                    IdSettores = new[] { newSector },
                    NomeCentro = newCenter.NomeCentro,
                };
                dbContext.Postaziones.Add(newPosition);
                dbContext.Settores.Add(newSector);
                dbContext.Centros.Add(newCenter);
                this.ControllerFactory(MaxControllerId(dbContext) + 1, newCenter, dbContext);
                dbContext.SaveChanges();
            }
        }

        private void AptFactory((string, string, string) airport, AtctablesContext dbContext)
        {
            var newAirport = new Aerodromo()
            {
                AdLatitudine = Math.Round((this.random.NextDouble() * 12) + 35.5, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
                AdLongitudine = Math.Round((this.random.NextDouble() * 12.5) + 6.6, 4).ToString(System.Globalization.CultureInfo.InvariantCulture),
                CodiceIata = airport.Item2,
                CodiceIcao = airport.Item3,
            };
            Pistum newRunway = new Pistum()
            {
                CodAd = newAirport.CodiceIcao,
                Orientamento = this.random.Next(0, 19).ToString(),
                Lunghezza = this.random.Next(999),
            };
            dbContext.Aerodromos.Add(newAirport);
            dbContext.Pista.Add(newRunway);
        }

        private void WipeButtonClick(object sender, EventArgs e)
        {
            using (var dbContext = new AtctablesContext())
            {
                // Get the list of table names in the database
                var tableNames = dbContext.Model.GetEntityTypes()
                    .Select(t => t.GetTableName())
                    .ToList();

                // Disable foreign key constraint checks
                dbContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 0");

                // Delete all records from each table using raw SQL queries
                foreach (var tableName in tableNames)
                {
                    var deleteSql = $"DELETE FROM {tableName}";
                    dbContext.Database.ExecuteSqlRaw(deleteSql);
                }

                // Enable foreign key constraint checks
                dbContext.Database.ExecuteSqlRaw("SET FOREIGN_KEY_CHECKS = 1");

                // Save the changes to the database
                dbContext.SaveChanges();
            }

            this.centersPopulateButton.Enabled = true;
            this.populateControllersButton.Enabled = false;
            this.trafficPopulatorButton.Enabled = false;
            this.randomstateButton.Enabled = false;
            this.playButton.Enabled = false;
            this.pauseButton.Enabled = false;
        }

        private void TrafficPopulatorButtonClick(object sender, EventArgs e)
        {
            this.populateControllersButton.Enabled = false;
            this.trafficPopulatorButton.Enabled = false;
            IList<string> types = FileToList.ReadFileToList("Models/Aircrafts.txt");
            IList<string> companies = FileToList.ReadFileToList("Models/Airlines.txt");
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);

            TimeSpan timeSpan = endDate - startDate;
            int totalDays = timeSpan.Days;
            using (var dbContext = new AtctablesContext())
            {
                for (int i = 0; i < this.trafficNum.Value; i++)
                {
                    var adTakeOff = dbContext.Aerodromos.ToList()[this.random.Next(0, dbContext.Aerodromos.Count())];
                    var adLanding = dbContext.Aerodromos.ToList()[this.random.Next(0, dbContext.Aerodromos.Count())];

                    var newPlane = new Aereomobile()
                    {
                        Tipo = types[this.random.Next(0, types.Count)],
                        NumeroDiCoda = RandomStringGenerator.GenerateRandomString(1) + "-" + RandomStringGenerator.GenerateRandomString(4),
                    };
                    var newFlightPlan = new Pianodivolo()
                    {
                        Callsign = this.random.NextDouble() > 0.8
                            ? newPlane.NumeroDiCoda
                            : (companies[this.random.Next(0, companies.Count)] + this.random.Next(100, 10000).ToString()),
                        Dof = startDate.AddDays(this.random.Next(totalDays)),
                        NumeroDiCoda = newPlane.NumeroDiCoda,
                        CodAdDecollo = adTakeOff.CodiceIcao,
                        CodAdAtterraggio = adLanding.CodiceIcao,
                        OrientamentoPistaDecollo = dbContext.Pista.First(p => p.CodAd.Equals(adTakeOff.CodiceIcao)).Orientamento,
                        OrientamentoPistaAtterraggio = dbContext.Pista.First(p => p.CodAd.Equals(adLanding.CodiceIcao)).Orientamento,
                    };
                    dbContext.Aereomobiles.Add(newPlane);
                    dbContext.Pianodivolos.Add(newFlightPlan);
                    dbContext.SaveChanges();

                    foreach (var crossingSector in dbContext.Settores.Where(s => s.CodAd == null).Take(this.random.Next(LongestFlightSectors / 2, LongestFlightSectors)).ToList())
                    {
                        var pointsInSector = dbContext.Puntos.Count(p => p.IdSettore.Equals(crossingSector.IdSettore));
                        IList<Stimati> newEstimates = new List<Stimati>();
                        IList<Punto> newEstimatesNames = dbContext.Puntos
                            .Where(p => p.IdSettore.Equals(crossingSector.IdSettore))
                            .Take(this.random.Next(pointsInSector / 2))
                            .ToList();

                        for (int k = 0; k < newEstimatesNames.Count; k++)
                        {
                            string newEstimatesName = newEstimatesNames[0].NomePunto;
                            newEstimatesNames.RemoveAt(0);
                            var newEstimate = new Stimati()
                            {
                                Callsign = newFlightPlan.Callsign,
                                Dof = newFlightPlan.Dof,
                                NomePunto = newEstimatesName,
                                OrarioStimato = newEstimates.Any() ? newEstimates.Max(s => s.OrarioStimato).AddSeconds(this.random.Next(100, 1000)) : newFlightPlan.Dof.AddSeconds(this.random.Next(86400)),
                            };
                            newEstimates.Add(newEstimate);
                        }

                        dbContext.Stimatis.AddRange(newEstimates);
                        dbContext.SaveChanges();
                    }
                }
            }

            this.randomstateButton.Enabled = true;
        }

        private void RandomStateButton_Click(object sender, EventArgs e)
        {
            this.randomstateButton.Enabled = false;
            DateTime selectedDateTime = this.dateTimePicker.Value;
            selectedDateTime = selectedDateTime.Add(new TimeSpan(0, (int)this.hourPicker.Value, (int)this.minutePicker.Value));

            this.player.UpdateTill(selectedDateTime);
            this.playButton.Enabled = true;
            this.speedBar.Enabled = true;
        }

        private void PauseButtonClick(object sender, EventArgs e)
        {
            this.player.Pause();
            this.pauseButton.Enabled = false;
            this.playButton.Enabled = true;
            this.speedBar.Enabled = true;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.player.Play(this.speedBar.Value);
            this.pauseButton.Enabled = true;
            this.playButton.Enabled = false;
            this.speedBar.Enabled = false;
        }
    }
}