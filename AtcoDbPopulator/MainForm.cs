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
        private const int NumControllersEachCenter = 40;
        private readonly Player player;
        private readonly Random random = new Random();
        private readonly string fullPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, FilePath);
        private readonly AtcoDbPopulator.Factories.AptFactory aptFactory;
        private readonly AtcoDbPopulator.Factories.ControllerFactory controllerFactory;
        private readonly AtcoDbPopulator.Factories.CenterFactory centerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            this.InitializeComponent();
            this.player = new Player(this);
            this.controllerFactory = new AtcoDbPopulator.Factories.ControllerFactory(1);
            this.aptFactory = new AtcoDbPopulator.Factories.AptFactory();
            this.centerFactory = new AtcoDbPopulator.Factories.CenterFactory();
        }

        /// <summary>
        /// Gets the actual time of the simulation.
        /// </summary>
        public DateTime ActualTime => this.player.ActualDateTime;

        /// <inheritdoc/>
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            this.player.Pause();
        }

        private void InitializeApts()
        {
            using var dbContext = new AtctablesContext();
            foreach (var airport in new AtcoDbPopulator.Utils.AirportFetcher().FetchAirportInfo(this.fullPath))
            {
                var newCenter = this.aptFactory.Create(airport, dbContext);

                // For each center some controllers are created each with a licence valid for each sector of the center.
                for (int i = 1; i < NumControllersEachCenter; i++)
                {
                    this.controllerFactory.Create(newCenter, dbContext);
                }
            }

            dbContext.SaveChanges();
        }

        private void InitializeCenters()
        {
            IList<string> centersNames = AtcoDbPopulator.Utils.FileToList.ReadFileToList("Models/Centers.txt");
            foreach (string s in centersNames)
            {
                using var dbContext = new AtctablesContext();
                var newCenter = this.centerFactory.Create(s, dbContext);

                // For each center some controllers are created each with a licence valid for each sector of the center.
                for (int i = 1; i < NumControllersEachCenter; i++)
                {
                    this.controllerFactory.Create(newCenter, dbContext);
                }

                dbContext.SaveChanges();
            }
        }

        private void PopulateControllersButtonClick(object sender, EventArgs e)
        {
            this.populateControllersButton.Enabled = false;
            this.controllerNum.Enabled = false;
            using var dbContext = new AtctablesContext();
            for (int i = 0; i < this.controllerNum.Value; i++)
            {
                this.progressBar1.Value = (int)(i / this.controllerNum.Value * 100);
                this.controllerFactory.Create(dbContext.Centros.ToArray()[this.random.Next(0, dbContext.Centros.Count())], dbContext);
            }

            this.progressBar1.Value = 0;

            dbContext.SaveChanges();
        }

        private void CentersPopulateButtonClick(object sender, EventArgs e)
        {
            this.centersPopulateButton.Enabled = false;
            this.InitializeCenters();
            this.InitializeApts();
            this.populateControllersButton.Enabled = true;
            this.controllerNum.Enabled = true;
            this.trafficPopulatorButton.Enabled = true;
            this.trafficNum.Enabled = true;
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
            this.hourPicker.Enabled = false;
            this.minutePicker.Enabled = false;
            this.controllerNum.Enabled = false;
            this.trafficNum.Enabled = false;
            this.dateTimePicker.Enabled = false;
        }

        private void TrafficPopulatorButtonClick(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            Application.DoEvents();
            this.populateControllersButton.Enabled = false;
            this.trafficPopulatorButton.Enabled = false;
            this.trafficNum.Enabled = false;
            this.controllerNum.Enabled = false;
            new TrafficPopulator().PopulateTraffic((int)this.trafficNum.Value, this.progressBar1, DateTime.Now);
            Cursor.Current = Cursors.Default;
            this.randomstateButton.Enabled = true;
            this.dateTimePicker.Enabled = true;
            this.hourPicker.Enabled = true;
            this.minutePicker.Enabled = true;
        }

        private void RandomStateButton_Click(object sender, EventArgs e)
        {
            this.randomstateButton.Enabled = false;
            this.dateTimePicker.Enabled = false;
            this.hourPicker.Enabled = false;
            this.minutePicker.Enabled = false;
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
            this.wipeButton.Enabled = true;
        }

        private void PlayButton_Click(object sender, EventArgs e)
        {
            this.player.Play(this.speedBar.Value);
            this.pauseButton.Enabled = true;
            this.playButton.Enabled = false;
            this.speedBar.Enabled = false;
            this.wipeButton.Enabled = false;
        }

        private void LaunchManagerButton_Click(object sender, EventArgs e)
        {
            new Thread(() => Application.Run(new Manager(this))).Start();
        }

        private void LaunchTwr_Click(object sender, EventArgs e)
        {
            new Thread(() => Application.Run(new TowerControl(this, this.player))).Start();
        }

        private void ButtonSupervisorPanel_Click(object sender, EventArgs e)
        {
            new Thread(() => Application.Run(new Supervisor(this))).Start();
        }
    }
}