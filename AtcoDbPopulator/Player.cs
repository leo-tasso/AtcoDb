// <copyright file="Player.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator;
using Models;

/// <summary>
/// Player class, to play the real time scenery of evolving traffic.
/// </summary>
public class Player : IPlayer
{
    private readonly IDictionary<(string, DateTime), DateTime> futureTakeOffTimes = new Dictionary<(string, DateTime), DateTime>();
    private readonly IDictionary<(string, DateTime), DateTime> futureLandingsTimes = new Dictionary<(string, DateTime), DateTime>();
    private readonly NormalDistribution normalDistribution = new NormalDistribution();
    private List<Percorrenza> futureOverpass = new List<Percorrenza>();
    private Thread? playThread;

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
        this.AddPlanes(dbContext.Stimatis.ToList());
        this.UpdateSystem(dbContext);
    }

    /// <inheritdoc/>
    public void Play(int speed)
    {
        this.Cont = true;

        this.playThread = new Thread(() =>
        {
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();
            while (this.Cont)
            {
                stopwatch.Restart(); // Start measuring elapsed time

                this.ActualDateTime = this.ActualDateTime.AddSeconds(speed / 5.0);
                using (AtctablesContext dbContext = new AtctablesContext())
                {
                    this.UpdateSystem(dbContext);
                }

                this.Mf.BeginInvoke(() =>
                {
                    this.Mf.dateTimePicker.Value = this.ActualDateTime;
                    this.Mf.hourPicker.Value = this.ActualDateTime.Hour;
                    this.Mf.minutePicker.Value = this.ActualDateTime.Minute;
                });

                stopwatch.Stop(); // Stop measuring elapsed time

                // Calculate the remaining sleep time to reach the desired 200ms duration
                int sleepTime = 200 - (int)stopwatch.ElapsedMilliseconds;
                if (sleepTime > 0)
                {
                    Thread.Sleep(sleepTime);
                }

                // If the elapsed time exceeds 200ms, the loop will continue immediately
            }
        });
        this.playThread.Start();
    }

    /// <inheritdoc/>
    public void Pause()
    {
        this.Cont = false;
        if (this.playThread is { IsAlive: true })
        {
            this.playThread?.Join();
        }
    }

    /// <summary>
    /// Method to add one or more planes to the system, by giving his estimates (Flight plan must be already created),
    /// hidden overflights, TO and Landing times will be generated and stored until reached.
    /// </summary>
    /// <param name="estimates">The list of new estimates.</param>
    public void AddPlanes(IList<Stimati> estimates)
    {
        // Create a list of future hidden overpasses.
        IList<Percorrenza> newFutureOverpasses = new List<Percorrenza>();
        foreach (var estimate in estimates)
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
            newFutureOverpasses.Add(newPassed);
        }

        this.futureOverpass.AddRange(newFutureOverpasses);

        // Create a TakeOff Time
        foreach (var firstNewOverpass in newFutureOverpasses.GroupBy(f => new { f.Dof, f.Callsign })
                     .Select(g => g.OrderBy(f => f.OrarioDiSorvolo).First())
                     .ToList())
        {
            this.futureTakeOffTimes.Add((firstNewOverpass.Callsign, firstNewOverpass.Dof), firstNewOverpass.OrarioDiSorvolo.Subtract(new TimeSpan(
                0,
                0,
                (int)this.normalDistribution.GenerateNormalRandomNumber(120, 10))));
        }

        // Create a Landing Time
        foreach (var latestNewOverpass in newFutureOverpasses.GroupBy(f => new { f.Dof, f.Callsign })
                     .Select(g => g.OrderByDescending(f => f.OrarioDiSorvolo).First())
                     .ToList())
        {
            this.futureLandingsTimes.Add((latestNewOverpass.Callsign, latestNewOverpass.Dof), latestNewOverpass.OrarioDiSorvolo.AddSeconds((int)this.normalDistribution.GenerateNormalRandomNumber(120, 10)));
        }
    }

    private void UpdateSystem(AtctablesContext dbContext)
    {
        // Update Overflights.
        var passed = this.futureOverpass.Where(p => p.OrarioDiSorvolo < this.ActualDateTime);
        this.futureOverpass = this.futureOverpass.Where(p => p.OrarioDiSorvolo >= this.ActualDateTime).ToList();
        dbContext.Percorrenzas.AddRange(passed);

        // Update TOs.
        var tookOff = this.futureTakeOffTimes.Where(p => p.Value < this.ActualDateTime).ToList();
        foreach (var ongoingTakeOff in tookOff)
        {
            dbContext.Pianodivolos.Find(ongoingTakeOff.Key.Item1, ongoingTakeOff.Key.Item2) !.OrarioDecollo =
                ongoingTakeOff.Value;
            this.futureTakeOffTimes.Remove(ongoingTakeOff);
        }

        // Update Landings.
        var landed = this.futureLandingsTimes.Where(p => p.Value < this.ActualDateTime).ToList();
        foreach (var ongoingLanding in landed)
        {
            dbContext.Pianodivolos.Find(ongoingLanding.Key.Item1, ongoingLanding.Key.Item2) !.OrarioAtterraggio =
                ongoingLanding.Value;
            this.futureLandingsTimes.Remove(ongoingLanding);
        }

        dbContext.SaveChanges();
    }
}