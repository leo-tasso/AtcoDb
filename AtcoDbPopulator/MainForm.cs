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

        private static readonly IList<string> CentersNames = new List<string>()
        {
            "PadovaACC",
            "MilanoACC",
            "RomaACC",
            "BrindisiACC",
            "RomaCTR",
            "MilanoCTR",
            "TorinoCTR",
            "NapoliCTR",
            "VeneziaCTR",
            "PalermoCTR",
            "CataniaCTR",
            "BolognaCTR",
            "FirenzeCTR",
            "GenovaCTR",
            "PisaCTR",
            "VeronaCTR",
            "BergamoCTR",
            "CagliariCTR",
            "AnconaCTR",
            "PerugiaCTR",
            "TriesteCTR",
            "LameziaCTR",
            "BrindisiCTR",
            "ReggioCTR",
            "TorrejónCTR",
            "TrevisoCTR",
            "PescaraCTR",
            "RiminiCTR",
            "LampedusaCTR",
            "ComisoCTR",
            "GrossetoCTR",
            "TrapaniCTR",
            "SalernoCTR",
            "BolzanoCTR",
            "AlgheroCTR",
            "VicenzaCTR",
            "AvianoCTR",
            "LatinaCTR",
            "SigonellaCTR",
            "DecimomannuCTR",
            "AmendolaCTR",
        };

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

        private void InitializeCenters()
        {
            foreach (string s in CentersNames)
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
                for (int i = 1; i < Numcontrollerseachcenter; i++)
                {
                    this.ControllerFactory(MaxControllerId(dbContext) + 1, newCenter, dbContext);
                }
            }
        }

        private Settore SectorFactory(AtctablesContext dbContext, string id, Postazione[] positions, string? codAd)
        {
            Postazione newPos;
            Postazione globalPos;
            string s;
            int i;
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
                        NomePunto = GenerateRandomString(5),
                        PosLatitudine = Math.Round(random.NextDouble() * 12 + 35.5, 4).ToString(),
                        PosLongitudine = Math.Round(random.NextDouble() * 12.5 + 6.6, 4).ToString(),
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

        private string GenerateRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        private static int MaxControllerId(AtctablesContext dbContext)
        {
            return dbContext.Controllores.Any() ? dbContext.Controllores.ToList().Max(c => int.Parse(c.IdControllore)) : 0;
        }

        private const int Numcontrollerseachcenter = 10;


        static List<string> ReadFileToList(string filePath)
        {
            List<string> strings = new List<string>();

            try
            {
                // Read all lines from the file and add them to the list
                strings.AddRange(File.ReadAllLines(filePath));
            }
            catch (IOException e)
            {
                Console.WriteLine("An error occurred while reading the file: " + e.Message);
            }

            return strings;
        }

        private void PopulateControllersClick(object sender, EventArgs e)
        {
            PopulateControllers.Enabled = false;
            int newId = 0;
            using (AtctablesContext
                   dbContext = new AtctablesContext()) // Replace with the name of your generated DbContext class.
            {
                newId = MaxControllerId(dbContext);
            }

            for (int i = newId; i < ControllerNum.Value + newId; i++)
            {
                using (var dbContext = new AtctablesContext()) // Replace with the name of your generated DbContext class.
                {

                    var newController = ControllerFactory(i + 1, dbContext.Centros.ToArray()[random.Next(0, CentersNames.Count)], dbContext);

                    var inizioDate = new DateTime(DateTime.Now.Year, random.Next(1, 13), random.Next(1, 28));
                    var newFerie = new Ferie()
                    {
                        IdControllore = (i + 1).ToString(),
                        Inizio = inizioDate,
                        Fine = inizioDate.AddDays(15)
                    };

                    dbContext.Feries.Add(newFerie);
                    dbContext.SaveChanges();

                    Console.WriteLine("New Controller added successfully.");
                }
            }
        }

        private Controllore ControllerFactory(int Id, Centro center, AtctablesContext dbContext)
        {
            IList<string> names = ReadFileToList("Models/Nomi.txt");
            IList<string> surnames = ReadFileToList("Models/Cognomi.txt");
            var newController = new Controllore()
            {
                IdControllore = (Id).ToString(),
                Nome = names[random.Next(0, names.Count)],
                Cognome = surnames[random.Next(0, surnames.Count)],
                NomeCentro = center.NomeCentro
            };
            var newAbilitazione = new Abilitazione()
            {
                MatricolaAbilitazione = dbContext.Abilitaziones.Any() ? dbContext.Abilitaziones.Max(a => a.MatricolaAbilitazione) + 1 : 1,
                IdControllore = newController.IdControllore,
                IdSettores = SectorsInCenter(dbContext.Centros.Find(newController.NomeCentro))
            };
            newController.Abilitaziones.Add(newAbilitazione);
            dbContext.Controllores.Add(newController);
            dbContext.Abilitaziones.Add(newAbilitazione);
            dbContext.SaveChanges();
            return newController;
        }

        private IList<Settore> SectorsInCenter(Centro c)
        {
            return c.Postaziones.SelectMany(p => p.IdSettores).ToList();
        }
        private void CentersPopulateClick(object sender, EventArgs e)
        {
            CentersPopulate.Enabled = false;
            InitializeCenters();
            InitializeApts();
            PopulateControllers.Enabled = true;
            TrafficPopulatorbutton.Enabled = true;
        }

        private void InitializeApts()
        {
            using (var dbContext = new AtctablesContext())
            {
                foreach (var Airport in new AirportFetcher().FetchAirportInfo(fullPath))
                {
                    AerodromoFactory(Airport, dbContext);
                    var newCenter = new Centro()
                    {
                        NomeCentro = Airport.Item1 + "APT"
                    };
                    var newSector = new Settore()
                    {
                        IdSettore = Airport.Item1 + "APT" + "Sec",
                        CodAd = Airport.Item3

                    };
                    var newPosition = new Postazione()
                    {
                        IdPostazione = Airport.Item1 + "APT" + "TWR",
                        IdSettores = new[] { newSector },
                        NomeCentro = newCenter.NomeCentro
                    };
                    dbContext.Postaziones.Add(newPosition);
                    dbContext.Settores.Add(newSector);
                    dbContext.Centros.Add(newCenter);
                    ControllerFactory(MaxControllerId(dbContext) + 1, newCenter, dbContext);
                    dbContext.SaveChanges();
                }
            }
        }

        private Aerodromo AerodromoFactory((string, string, string) Airport, AtctablesContext dbContext)
        {
            Aerodromo NewAirport = new Aerodromo()
            {
                AdLatitudine = Math.Round(random.NextDouble() * 12 + 35.5, 4).ToString(),
                AdLongitudine = Math.Round(random.NextDouble() * 12.5 + 6.6, 4).ToString(),
                CodiceIata = Airport.Item2,
                CodiceIcao = Airport.Item3
            };
            Pistum newPistum = new Pistum()
            {
                CodAd = NewAirport.CodiceIcao,
                Orientamento = random.Next(0, 19).ToString(),
                Lunghezza = random.Next(999),
            };
            dbContext.Aerodromos.Add(NewAirport);
            dbContext.Pista.Add(newPistum);
            return NewAirport;
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
            CentersPopulate.Enabled = true;
            PopulateControllers.Enabled = false;
            TrafficPopulatorbutton.Enabled = false;
            RandomstateButton.Enabled = false;
            playButton.Enabled = false;
            pauseButton.Enabled = false;
        }

        private void TrafficPopulatorbuttonClick(object sender, EventArgs e)
        {
            PopulateControllers.Enabled = false;
            TrafficPopulatorbutton.Enabled = false;
            IList<string> Types = ReadFileToList("Models/Aircrafts.txt");
            IList<string> Companies = ReadFileToList("Models/Airlines.txt");
            DateTime startDate = new DateTime(DateTime.Now.Year, 1, 1);
            DateTime endDate = new DateTime(DateTime.Now.Year, 12, 31);

            TimeSpan timeSpan = endDate - startDate;
            int totalDays = timeSpan.Days;
            using (var dbContext = new AtctablesContext())
            {
                for (int i = 0; i < TrafficCounter.Value; i++)
                {
                    var adTakeOff = dbContext.Aerodromos.ToList()[random.Next(0, dbContext.Aerodromos.Count())];
                    var adLanding = dbContext.Aerodromos.ToList()[random.Next(0, dbContext.Aerodromos.Count())];

                    var newPlane = new Aereomobile()
                    {
                        Tipo = Types[random.Next(0, Types.Count)],
                        NumeroDiCoda = GenerateRandomString(1) + "-" + GenerateRandomString(4)
                    };
                    var newFlightPlan = new Pianodivolo()
                    {
                        Callsign = random.NextDouble() > 0.8
                            ? newPlane.NumeroDiCoda
                            : (Companies[random.Next(0, Companies.Count)] + random.Next(100, 10000).ToString()),
                        Dof = startDate.AddDays(random.Next(totalDays)),
                        NumeroDiCoda = newPlane.NumeroDiCoda,
                        CodAdDecollo = adTakeOff.CodiceIcao,
                        CodAdAtterraggio = adLanding.CodiceIcao,
                        OrientamentoPistaDecollo = dbContext.Pista.First(p => p.CodAd.Equals(adTakeOff.CodiceIcao)).Orientamento,
                        OrientamentoPistaAtterraggio = dbContext.Pista.First(p => p.CodAd.Equals(adLanding.CodiceIcao)).Orientamento,
                    };
                    dbContext.Aereomobiles.Add(newPlane);
                    dbContext.Pianodivolos.Add(newFlightPlan);
                    dbContext.SaveChanges();

                    foreach (var CrossingSector in dbContext.Settores.Where(s => s.CodAd == null).Take(random.Next(LONGESTFLIGHTSECTORS / 2, LONGESTFLIGHTSECTORS)).ToList())
                    {
                        var PointsInSector = dbContext.Puntos.Count(p => p.IdSettore.Equals(CrossingSector.IdSettore));
                        IList<Punto> NewExtimatesNames = new List<Punto>();
                        IList<Stimati> NewExtimates = new List<Stimati>();
                        NewExtimatesNames = dbContext.Puntos
                            .Where(p => p.IdSettore.Equals(CrossingSector.IdSettore))
                            .Take(random.Next(PointsInSector / 2))
                            .ToList();

                        for (int k = 0; k < NewExtimatesNames.Count; k++)
                        {
                            string NewExtimatesName = NewExtimatesNames[0].NomePunto;
                            NewExtimatesNames.RemoveAt(0);
                            var NewExtimate = new Stimati()
                            {
                                Callsign = newFlightPlan.Callsign,
                                Dof = newFlightPlan.Dof,
                                NomePunto = NewExtimatesName,
                                OrarioStimato = NewExtimates.Any() ? NewExtimates.Max(s => s.OrarioStimato).AddSeconds(random.Next(100, 1000)) : newFlightPlan.Dof.AddSeconds(random.Next(86400))
                            };
                            NewExtimates.Add(NewExtimate);
                        }
                        dbContext.Stimatis.AddRange(NewExtimates);
                        dbContext.SaveChanges();
                    }
                }
            }

            RandomstateButton.Enabled = true;
        }

        private const int CROSSINGPOINTSPERSECTOR = 10;

        private const int LONGESTFLIGHTSECTORS = 10;

        private void RandomstateButton_Click(object sender, EventArgs e)
        {
            RandomstateButton.Enabled = false;
            DateTime selecteDateTime = dateTimePicker1.Value;
            selecteDateTime = selecteDateTime.Add(new TimeSpan(0, (int)HourPicker.Value, (int)MinutePicker.Value));

            player.UpdateTill(selecteDateTime);
            playButton.Enabled = true;
            SpeedBar.Enabled = true;

        }

        private void PauseButtonClick(object sender, EventArgs e)
        {
            player.Pause();
            pauseButton.Enabled = false;
            playButton.Enabled = true;
            SpeedBar.Enabled = true;

        }

        private void playButton_Click(object sender, EventArgs e)
        {
            player.Play(SpeedBar.Value);
            pauseButton.Enabled = true;
            playButton.Enabled = false;
            SpeedBar.Enabled = false;
        }


    }
}