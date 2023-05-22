using AtcoDbPopulator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Linq;
using MySqlX.XDevAPI;

namespace AtcoDbPopulator
{
    public partial class MainForm : Form
    {
        Random random = new Random();
        static string filePath = "Models/Airports.txt";
        string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
        public static IList<string> NomiCentri = new List<string>()
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
            "AmendolaCTR"
        };

        public MainForm()
        {
            InitializeComponent();
        }

        private const int NUMPOS = 3;

        private void InitializeCenters()
        {
            foreach (string s in NomiCentri)
            {
                using (var dbContext = new AtctablesContext())
                {
                    var newCenter = new Centro()
                    {
                        NomeCentro = s
                    };
                    dbContext.Centros.Add(newCenter);
                    var globalPos = new Postazione()
                    {
                        IdPostazione = s + "Pos" + NUMPOS,
                        NomeCentro = s,
                    };
                    dbContext.Postaziones.Add(globalPos);
                    IList<Settore> newSettores = new List<Settore>();
                    for (int i = 1; i < NUMPOS; i++)
                    {
                        var newPos = new Postazione()
                        {
                            IdPostazione = s + "Pos" + i,
                            NomeCentro = s
                        };
                        dbContext.Postaziones.Add(newPos);
                        var newSettore = SectorFactory(dbContext,s + "Sec" + i, new[] { newPos, globalPos },null);
                        newSettores.Add(newSettore);
                    }
                    dbContext.SaveChanges();
                    Console.WriteLine("New center added successfully.");
                    for (int i = 1; i < NUMCONTROLLERSEACHCENTER; i++)
                    {
                        this.ControllerFactory(MaxControllerId(dbContext) + 1, newCenter, dbContext);
                    }
                }


            }
        }

        private Settore SectorFactory(AtctablesContext dbContext,string id, Postazione[] postazioni, string? codAd)
        {
            Postazione newPos;
            Postazione globalPos;
            string s;
            int i;
            var newSettore = new Settore()
            {
                IdPostaziones = postazioni,
                IdSettore = id,
                CodAd = codAd
            };
            dbContext.Settores.Add(newSettore);
            if (codAd == null)
            {
                for (int j = 0; j < POINTSPERCENTER; j++)
                {
                    Punto newWaypoint = new Punto()
                    {
                        NomePunto = GenerateRandomString(5),
                        PosLatitudine = Math.Round(random.NextDouble() * 12 + 35.5, 4).ToString(),
                        PosLongitudine = Math.Round(random.NextDouble() * 12.5 + 6.6, 4).ToString(),
                        IdSettore = newSettore.IdSettore
                    };
                    dbContext.Puntos.Add(newWaypoint);
                }
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

        private const int POINTSPERCENTER = 30;

        private static int MaxControllerId(AtctablesContext dbContext)
        {
            return dbContext.Controllores.Any() ? dbContext.Controllores.ToList().Max(c => int.Parse(c.IdControllore)) : 0;
        }

        private const int NUMCONTROLLERSEACHCENTER = 10;


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

                    var newController = ControllerFactory(i + 1, dbContext.Centros.ToArray()[random.Next(0, NomiCentri.Count)], dbContext);

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
            InitializeCenters();
            InitializeApts();
        }

        private void InitializeApts()
        {
            using (var dbContext = new AtctablesContext()) // Replace with the name of your generated DbContext class.
            {
                foreach (var Airport in FetchAirportInfo(fullPath))
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

        public List<(string, string, string)> FetchAirportInfo(string filePath)
        {
            List<(string, string, string)> airportInfoList = new List<(string, string, string)>();

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] parts = line.Split(',');
                        if (parts.Length >= 3)
                        {
                            string airportName = parts[0].Trim();
                            string iataCode = parts[1].Trim();
                            string icaoCode = parts[2].Trim();

                            airportInfoList.Add((airportName, iataCode, icaoCode));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
            }

            return airportInfoList;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

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
        }
    }
}