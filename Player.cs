using AtcoDbPopulator.Models;

namespace AtcoDbPopulator;

public class Player
{
    
    public Player(MainForm mf)
    {
        Cont = false;
        ActualDateTime = DateTime.Now;
        this.Mf = mf;
    }

    private MainForm Mf { get; set; }

    readonly Random _random = new Random();
    readonly NormalDistribution _normalDistribution = new NormalDistribution();
    private IList<Percorrenza> _futureOverpass = new List<Percorrenza>();

    public void UpdateTill(DateTime selectedDateTime)
    {
        ActualDateTime = selectedDateTime;
        using var dbContext = new AtctablesContext();
        IList<Stimati> passedEstimates = dbContext.Stimatis.ToList();
        foreach (var estimate in passedEstimates)
        {
            var overTime =
                estimate.OrarioStimato.Add(new TimeSpan(0, 0,
                    (int)_normalDistribution.GenerateNormalRandomNumber(120, 10)));
            var newPassed = new Percorrenza()
            {
                Callsign = estimate.Callsign,
                Dof = estimate.Dof,
                NomePunto = estimate.NomePunto,
                OrarioDiSorvolo = overTime
            };
            _futureOverpass.Add(newPassed);

        }
        UpdatePassed(dbContext);
        foreach (var firstPoint in dbContext.Percorrenzas.GroupBy(f => new { f.Dof, f.Callsign })
                     .Select(g => g.OrderBy(f => f.OrarioDiSorvolo).First())
                     .ToList())
        {
            dbContext.Pianodivolos.Find(firstPoint.Callsign, firstPoint.Dof)!.OrarioDecollo =
                firstPoint.OrarioDiSorvolo.Subtract(new TimeSpan(0, _random.Next(10, 30), _random.Next(0, 60)));
        }

        dbContext.SaveChanges();
        foreach (var flightPlan in dbContext.Pianodivolos.ToList())
        {
            if (dbContext.Stimatis.Count(s => s.Dof == flightPlan.Dof && s.Callsign == flightPlan.Callsign) ==
                dbContext.Percorrenzas.Count(s => s.Dof == flightPlan.Dof && s.Callsign == flightPlan.Callsign))
            {
                var stimatoAtterraggio = dbContext.Percorrenzas
                    .Where(p => p.Dof == flightPlan.Dof && p.Callsign == flightPlan.Callsign)
                    .Max(p => p.OrarioDiSorvolo).AddSeconds(_random.Next(100, 1000));
                if (stimatoAtterraggio < ActualDateTime)
                {
                    flightPlan.OrarioAtterraggio = stimatoAtterraggio;
                }
            }
        }

        dbContext.SaveChanges();
    }

    private void UpdatePassed(AtctablesContext dbContext)
    {
        var passed = _futureOverpass.Where(p => p.OrarioDiSorvolo < ActualDateTime);
        _futureOverpass = _futureOverpass.Where(p => p.OrarioDiSorvolo >= ActualDateTime).ToList();
        dbContext.Percorrenzas.AddRange(passed);
        dbContext.SaveChanges();
    }


    public void Play(int speed)
    {
        Cont = true;
        
        new Thread(() =>
        {
            while (Cont)
            {
                ActualDateTime= ActualDateTime.AddSeconds(speed);
                using (AtctablesContext dbContext = new AtctablesContext())
                {
                    UpdatePassed(dbContext);
                    //TODO update also airports
                }

                Mf.BeginInvoke(() =>
                {
                    Mf.dateTimePicker1.Value = ActualDateTime;
                    Mf.HourPicker.Value = ActualDateTime.Hour;
                    Mf.MinutePicker.Value = ActualDateTime.Minute;
                });


                Thread.Sleep(1000);
            }
        }).Start();
    }

    public void Pause()
    {
        Cont = false;
    }

    private bool Cont { get; set; }
    private DateTime ActualDateTime { get; set; }
}