using AtcoDbPopulator.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AtcoDbPopulator
{
    public partial class TowerControl : Form
    {
        private MainForm mf;
        public TowerControl(MainForm mf)
        {
            InitializeComponent();
            using var dbContext = new AtctablesContext();
            this.comboBoxAirports.DataSource = dbContext.Postaziones.Where(p => p.IdSettores.Any(s => s.CodAd != null)).Select(p => p.IdPostazione).ToList();
            this.controllersUtils = new ControllersUtils();
            RefreshControllerBox();
            this.mf=mf;
        }


        private void comboBoxAirports_SelectionChangeCommitted(object sender, EventArgs e)
        {
            RefreshControllerBox();
        }

        private void RefreshControllerBox()
        {
            using var dbContext = new AtctablesContext();
            this.comboBoxControllers.DataSource = dbContext.Controllores.ToList()
                .Where(c => controllersUtils.ControllerIsAble(c,
                    dbContext.Settores
                        .Where(s => s.IdPostaziones.Any(p => p.IdPostazione.Equals(comboBoxAirports.SelectedItem)))
                        .Select(s => s.IdSettore).ToList()
                    , dbContext))
                .Select(c => c.IdControllore + " " + c.Cognome + " " + c.Nome).ToList();
        }

        private void buttonLogIn_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();

            var apt = dbContext.Settores
                .Where(s => s.IdPostaziones.Any(p => p.IdPostazione.Equals(comboBoxAirports.SelectedItem)))
                .Select(s => s.CodAd).First();


            running = true;
            this.comboBoxAirports.Enabled = false;
            this.comboBoxControllers.Enabled = false;
            CyclicCkecker = new Thread(() => CyclicCheckTraffic(apt));
            CyclicCkecker.Start();
        }

        public Thread CyclicCkecker { get; set; }

        private void CyclicCheckTraffic(string aptPos)
        {
            while (running)
            {
                this.Invoke(new Action(() =>
                {
                    using var dbContext = new AtctablesContext();
                    DateTimeLabel.Text = mf.ActualTime.ToString();
                    dataGridViewArrivals.DataSource = dbContext.Pianodivolos.Where(p => p.CodAdAtterraggio.Equals(aptPos)).ToList();
                }));
                Thread.Sleep(1000);
            }

        }

        private void TowerControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            if (CyclicCkecker != null) CyclicCkecker.Join();
        }

        private void buttonLogOut_Click(object sender, EventArgs e)
        {
            running = false;
            this.comboBoxAirports.Enabled = true;
            this.comboBoxControllers.Enabled = true;
            DateTimeLabel.Text = string.Empty;
            if (CyclicCkecker != null) CyclicCkecker.Join();
            this.dataGridViewArrivals.DataSource=null;
            this.dataGridViewDepartures.DataSource=null;
        }

        public ControllersUtils controllersUtils { get; }
        public bool running { get; set; }

    }

}
