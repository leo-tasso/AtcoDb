using AtcoDbPopulator.Models;

namespace AtcoDbPopulator;

public class Player
{
    
    public Player(MainForm mf)
    {
        cont = false;
        ActualDateTime = DateTime.Now;
        this.mf = mf;
    }

    public MainForm mf { get; set; }

    Random random = new Random();
    NormalDistribution normalDistribution = new NormalDistribution();
    IList<Percorrenza> futurePercorrenzas = new List<Percorrenza>();

    public void UpdateTill(DateTime selecteDateTime)
    {
        ActualDateTime = selecteDateTime;
        using (AtctablesContext dbContext = new AtctablesContext())
        {
            IList<Stimati> passedExtimates = dbContext.Stimatis.ToList();
            foreach (var extimate in passedExtimates)
            {
                var overTime =
                    extimate.OrarioStimato.Add(new TimeSpan(0, 0,
                        (int)normalDistribution.GenerateNormalRandomNumber(120, 10)));
                var newPassed = new Percorrenza()
                    {
                        Callsign = extimate.Callsign,
                        Dof = extimate.Dof,
                        NomePunto = extimate.NomePunto,
                        OrarioDiSorvolo = overTime
                    };
                futurePercorrenzas.Add(newPassed);

            }
            UpdatePassed(dbContext);
            foreach (var firstPoint in dbContext.Percorrenzas.GroupBy(f => new { f.Dof, f.Callsign })
                         .Select(g => g.OrderBy(f => f.OrarioDiSorvolo).First())
                         .ToList())
            {
                dbContext.Pianodivolos.Find(firstPoint.Callsign, firstPoint.Dof).OrarioDecollo =
                    firstPoint.OrarioDiSorvolo.Subtract(new TimeSpan(0, random.Next(10, 30), random.Next(0, 60)));
            }

            dbContext.SaveChanges();
            foreach (var flightPlan in dbContext.Pianodivolos.ToList())
            {
                if (dbContext.Stimatis.Count(s => s.Dof == flightPlan.Dof && s.Callsign == flightPlan.Callsign) ==
                    dbContext.Percorrenzas.Count(s => s.Dof == flightPlan.Dof && s.Callsign == flightPlan.Callsign))
                {
                    var stimatoAtterraggio = dbContext.Percorrenzas
                        .Where(p => p.Dof == flightPlan.Dof && p.Callsign == flightPlan.Callsign)
                        .Max(p => p.OrarioDiSorvolo).AddSeconds(random.Next(100, 1000));
                    if (stimatoAtterraggio < ActualDateTime)
                    {
                        flightPlan.OrarioAtterraggio = stimatoAtterraggio;
                    }
                }
            }

            dbContext.SaveChanges();
        }
    }

    private void UpdatePassed(AtctablesContext dbContext)
    {
        var passed = futurePercorrenzas.Where(p => p.OrarioDiSorvolo < ActualDateTime);
        futurePercorrenzas = futurePercorrenzas.Where(p => p.OrarioDiSorvolo >= ActualDateTime).ToList();
        dbContext.Percorrenzas.AddRange(passed);
        dbContext.SaveChanges();
    }


    public void play(int speed)
    {
        cont = true;
        
        new Thread(() =>
        {
            while (cont)
            {
                ActualDateTime= ActualDateTime.AddSeconds(speed);
                using (AtctablesContext dbContext = new AtctablesContext())
                {
                    UpdatePassed(dbContext);
                    //TODO update also airports
                }

                mf.BeginInvoke(() =>
                {
                    mf.dateTimePicker1.Value = ActualDateTime;
                    mf.HourPicker.Value = ActualDateTime.Hour;
                    mf.MinutePicker.Value = ActualDateTime.Minute;
                });


                Thread.Sleep(1000);
            }
        }).Start();
    }

    public void pause()
    {
        cont = false;
    }

    public bool cont { get; set; }
    public DateTime ActualDateTime { get; set; }
}