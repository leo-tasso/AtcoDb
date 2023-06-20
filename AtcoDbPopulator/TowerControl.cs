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

        /// <summary>
        /// Initializes a new instance of the <see cref="TowerControl"/> class.
        /// </summary>
        /// <param name="mf">The MainForm Launcher.</param>
        public TowerControl(MainForm mf)
        {
            this.InitializeComponent();
            using var dbContext = new AtctablesContext();
            this.comboBoxAirports.DataSource = dbContext.Postaziones.Where(p => p.IdSettores.Any(s => s.CodAd != null)).Select(p => p.IdPostazione).ToList();
            this.ControllersUtils = new ControllersUtils();
            this.RefreshControllerBox();
            this.mf = mf;
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
                .Select(s => s.CodAd).First() !;

            this.Running = true;
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
                    this.dataGridViewArrivals.DataSource = dbContext.Pianodivolos.Where(p => p.CodAdAtterraggio.Equals(aptPos)
                        && p.OrarioAtterraggio == null
                        && p.Dof >= this.mf.ActualTime.Date.AddDays(-2)
                        && p.Dof <= this.mf.ActualTime.Date.AddDays(1)).OrderByDescending(p => p.OrarioDecollo).ToList();
                });
                Thread.Sleep(1000);
            }
        }

        private void TowerControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Running = false;
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
        }
    }
}
