// <copyright file="Player.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace AtcoDbPopulator;
using Models;

/// <summary>
/// Player class, to play the real time scenery of evolving traffic.
/// </summary>
public class Player : IPlayer
{
    private readonly Random random = new Random();
    private readonly NormalDistribution normalDistribution = new NormalDistribution();
    private IList<Percorrenza> futureOverpass = new List<Percorrenza>();

    /// <summary>
    /// Initializes a new instance of the <see cref="Player"/> class.
    /// </summary>
    /// <param name="mf">the MainForm to make the watches dynamic.</param>
    public Player(MainForm mf)
    {
        this.Cont = false;
        this.ActualDateTime = DateTime.Now;
        this.Mf = mf;
    }

    private MainForm Mf { get; set; }

    private bool Cont { get; set; }

    private DateTime ActualDateTime { get; set; }

    /// <inheritdoc/>
    public void UpdateTill(DateTime selectedDateTime)
    {
        this.ActualDateTime = selectedDateTime;
        using var dbContext = new AtctablesContext();
        IList<Stimati> passedEstimates = dbContext.Stimatis.ToList();
        foreach (var estimate in passedEstimates)
        {
            var overTime =
                estimate.OrarioStimato.Add(new TimeSpan(
                    0,
                    0,
                    (int)this.normalDistribution.GenerateNormalRandomNumber(120, 10)));
            var newPassed = new Percorrenza()
            {
                Callsign = estimate.Callsign,
                Dof = estimate.Dof,
                NomePunto = estimate.NomePunto,
                OrarioDiSorvolo = overTime,
            };
            this.futureOverpass.Add(newPassed);
        }

        this.UpdatePassed(dbContext);
        foreach (var firstPoint in dbContext.Percorrenzas.GroupBy(f => new { f.Dof, f.Callsign })
                     .Select(g => g.OrderBy(f => f.OrarioDiSorvolo).First())
                     .ToList())
        {
            dbContext.Pianodivolos.Find(firstPoint.Callsign, firstPoint.Dof) !.OrarioDecollo =
                firstPoint.OrarioDiSorvolo.Subtract(new TimeSpan(0, this.random.Next(10, 30), this.random.Next(0, 60)));
        }

        dbContext.SaveChanges();
        foreach (var flightPlan in dbContext.Pianodivolos.ToList())
        {
            if (dbContext.Stimatis.Count(s => s.Dof == flightPlan.Dof && s.Callsign == flightPlan.Callsign) ==
                dbContext.Percorrenzas.Count(s => s.Dof == flightPlan.Dof && s.Callsign == flightPlan.Callsign))
            {
                var stimatoAtterraggio = dbContext.Percorrenzas
                    .Where(p => p.Dof == flightPlan.Dof && p.Callsign == flightPlan.Callsign)
                    .Max(p => p.OrarioDiSorvolo).AddSeconds(this.random.Next(100, 1000));
                if (stimatoAtterraggio < this.ActualDateTime)
                {
                    flightPlan.OrarioAtterraggio = stimatoAtterraggio;
                }
            }
        }

        dbContext.SaveChanges();
    }

    /// <inheritdoc/>
    public void Play(int speed)
    {
        this.Cont = true;

        new Thread(() =>
        {
            while (this.Cont)
            {
                this.ActualDateTime = this.ActualDateTime.AddSeconds(speed);
                using (AtctablesContext dbContext = new AtctablesContext())
                {
                    this.UpdatePassed(dbContext);

                    // TODO update also airports
                }

                this.Mf.BeginInvoke(() =>
                {
                    this.Mf.dateTimePicker1.Value = this.ActualDateTime;
                    this.Mf.HourPicker.Value = this.ActualDateTime.Hour;
                    this.Mf.MinutePicker.Value = this.ActualDateTime.Minute;
                });

                Thread.Sleep(1000);
            }
        }).Start();
    }

    /// <inheritdoc/>
    public void Pause()
    {
        this.Cont = false;
    }

    private void UpdatePassed(AtctablesContext dbContext)
    {
        var passed = this.futureOverpass.Where(p => p.OrarioDiSorvolo < this.ActualDateTime);
        this.futureOverpass = this.futureOverpass.Where(p => p.OrarioDiSorvolo >= this.ActualDateTime).ToList();
        dbContext.Percorrenzas.AddRange(passed);
        dbContext.SaveChanges();
    }
}