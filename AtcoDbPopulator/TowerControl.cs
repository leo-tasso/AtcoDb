// <copyright file="TowerControl.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using AtcoDbPopulator.Models;

    /// <summary>
    /// Class to manage a tower.
    /// </summary>
    public partial class TowerControl : Form
    {
        private readonly MainForm mf;
        private readonly Player player;

        /// <summary>
        /// Initializes a new instance of the <see cref="TowerControl"/> class.
        /// </summary>
        /// <param name="mf">The MainForm Launcher.</param>
        /// <param name="player">The Player Class.</param>
        public TowerControl(MainForm mf, Player player)
        {
            this.InitializeComponent();
            using var dbContext = new AtctablesContext();
            this.comboBoxAirports.DataSource = dbContext.Postaziones.Where(p => p.IdSettores.Any(s => s.CodAd != null)).Select(p => p.IdPostazione).ToList();
            this.ControllersUtils = new ControllersUtils();
            this.RefreshControllerBox();
            this.buttonLogOut.Enabled = false;
            this.mf = mf;
            this.player = player;
        }


        private ControllersUtils ControllersUtils { get; }

        private bool Running { get; set; }

        private Thread? CyclicChecker { get; set; }

        private void ComboBoxAirports_SelectionChangeCommitted(object sender, EventArgs e)
        {
            this.RefreshControllerBox();
        }

        private void RefreshControllerBox()
        {
            using var dbContext = new AtctablesContext();
            this.comboBoxControllers.DataSource = dbContext.Controllores.ToList()
                .Where(c => this.ControllersUtils.ControllerIsAble(
                    c,
                    dbContext.Settores
                        .Where(s => s.IdPostaziones.Any(p => p.IdPostazione.Equals(this.comboBoxAirports.SelectedItem)))
                        .Select(s => s.IdSettore).ToList(),
                    dbContext))
                .Select(c => c.IdControllore + " " + c.Cognome + " " + c.Nome).ToList();
        }

        private void ButtonLogIn_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();

            string apt = dbContext.Settores
                .Where(s => s.IdPostaziones.Any(p => p.IdPostazione.Equals(this.comboBoxAirports.SelectedItem)))
                .Select(s => s.CodAd).First()!;
            this.buttonLogOut.Enabled = true;
            this.buttonLogIn.Enabled = false;
            this.Running = true;
            this.player.ActiveTwr = dbContext.Settores
                .Where(s => s.IdPostaziones.Any(p => p.IdPostazione.Equals(this.comboBoxAirports.SelectedItem)))
                .Select(s => s.CodAd).First()!;
            this.comboBoxAirports.Enabled = false;
            this.comboBoxControllers.Enabled = false;
            this.CyclicChecker = new Thread(() => this.CyclicCheckTraffic(apt));
            this.CyclicChecker.Start();
        }

        private void CyclicCheckTraffic(string aptPos)
        {
            while (this.Running)
            {
                this.Invoke(() =>
                {
                    using var dbContext = new AtctablesContext();
                    this.DateTimeLabel.Text = this.mf.ActualTime.ToString(System.Globalization.CultureInfo.CurrentCulture);


                    this.UpdateTables(aptPos, dbContext);
                });
                Thread.Sleep(200);
            }
        }

        private void UpdateTables(string aptPos, AtctablesContext dbContext)
        {
            int selectedIndex = 0;
            int firstDisplayedScrollingColumnIndex = 0;
            if (this.dataGridViewArrivals.DataSource != null && this.dataGridViewArrivals.RowCount > 0)
            {
                selectedIndex = this.dataGridViewArrivals.CurrentRow.Index;
                firstDisplayedScrollingColumnIndex = this.dataGridViewArrivals.FirstDisplayedScrollingColumnIndex;
            }

            this.dataGridViewArrivals.DataSource = dbContext.Pianodivolos.Where(p => p.CodAdAtterraggio.Equals(aptPos)
                    && p.OrarioAtterraggio == null
                    && p.Dof >=
                    this.mf.ActualTime.Date.AddDays(-2)
                    && p.Dof <=
                    this.mf.ActualTime.Date.AddDays(1))
                .OrderByDescending(p => p.OrarioDecollo).Select(r => new { r.OrarioDecollo, r.Callsign, r.Dof, r.NumeroDiCoda, r.OrientamentoPistaAtterraggio, r.CodAdDecollo }).ToList();
            if (selectedIndex >= 0 && selectedIndex < this.dataGridViewArrivals.Rows.Count)
            {
                this.dataGridViewArrivals.CurrentCell = this.dataGridViewArrivals.Rows[selectedIndex].Cells[0];
            }

            if (firstDisplayedScrollingColumnIndex >= 0 &&
                firstDisplayedScrollingColumnIndex < this.dataGridViewArrivals.ColumnCount)
            {
                this.dataGridViewArrivals.FirstDisplayedScrollingColumnIndex = firstDisplayedScrollingColumnIndex;
            }



            selectedIndex = 0;
            firstDisplayedScrollingColumnIndex = 0;
            if (this.dataGridViewDepartures.DataSource != null && this.dataGridViewDepartures.RowCount > 0)
            {
                selectedIndex = this.dataGridViewDepartures.CurrentRow.Index;
                firstDisplayedScrollingColumnIndex = this.dataGridViewDepartures.FirstDisplayedScrollingColumnIndex;
            }

            this.dataGridViewDepartures.DataSource = dbContext.Pianodivolos.Where(p => p.CodAdDecollo.Equals(aptPos)
                    && p.OrarioDecollo == null
                    && p.Dof >=
                    this.mf.ActualTime.Date.AddDays(-2)
                    && p.Dof <=
                    this.mf.ActualTime.Date.AddDays(2))
                .OrderBy(p => p.Dof).Select(r => new { r.Callsign, r.Dof, r.NumeroDiCoda, r.OrientamentoPistaDecollo, r.CodAdAtterraggio }).ToList();
            if (selectedIndex >= 0 && selectedIndex < this.dataGridViewDepartures.Rows.Count)
            {
                this.dataGridViewDepartures.CurrentCell = this.dataGridViewDepartures.Rows[selectedIndex].Cells[0];
            }

            if (firstDisplayedScrollingColumnIndex >= 0 &&
                firstDisplayedScrollingColumnIndex < this.dataGridViewDepartures.ColumnCount)
            {
                this.dataGridViewDepartures.FirstDisplayedScrollingColumnIndex = firstDisplayedScrollingColumnIndex;
            }
        }

        private void TowerControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Running = false;
            this.player.ActiveTwr = null;
            this.CyclicChecker?.Join();
        }

        private void ButtonLogOut_Click(object sender, EventArgs e)
        {
            this.Running = false;
            this.comboBoxAirports.Enabled = true;
            this.comboBoxControllers.Enabled = true;
            this.DateTimeLabel.Text = string.Empty;
            this.CyclicChecker?.Join();
            this.dataGridViewArrivals.DataSource = null;
            this.dataGridViewDepartures.DataSource = null;
            this.buttonLogOut.Enabled = false;
            this.buttonLogIn.Enabled = true;

        }

        private void buttonLanded_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewArrivals.SelectedCells.Count != 0)
            {
                string callsign = this.dataGridViewArrivals.SelectedCells[1].Value.ToString();
                DateTime dof = DateTime.Parse(this.dataGridViewArrivals.SelectedCells[2].Value.ToString());
                using var dbContext = new AtctablesContext();
                var flightPlan = dbContext.Pianodivolos.Find(callsign, dof);
                if (flightPlan.OrarioDecollo != null)
                {
                    flightPlan.OrarioAtterraggio = this.mf.ActualTime;
                    dbContext.SaveChanges();
                }
            }
        }

        private void buttonTookOff_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewDepartures.SelectedCells.Count != 0)
            {
                string callSign = this.dataGridViewDepartures.SelectedCells[0].Value.ToString();
                DateTime dof = DateTime.Parse(this.dataGridViewDepartures.SelectedCells[1].Value.ToString());
                using var dbContext = new AtctablesContext();
                var flightPlan = dbContext.Pianodivolos.Find(callSign, dof);
                flightPlan.OrarioDecollo = this.mf.ActualTime;
                dbContext.SaveChanges();
            }
        }
    }
}
