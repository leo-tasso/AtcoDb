// <copyright file="Supervisor.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using AtcoDbPopulator.Models;
    using ScottPlot;

    /// <summary>
    /// Class to manage a supervisor window.
    /// </summary>
    public partial class Supervisor : Form
    {
        private readonly MainForm mf;
        private volatile bool running;
        private ISet<Stimati> estim = new HashSet<Stimati>();
        private IList<IList<string>> sectorsList = new List<IList<string>>();
        private IList<string> positionList = new List<string>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Supervisor"/> class.
        /// </summary>
        /// <param name="mf">The calling MainForm.</param>
        public Supervisor(MainForm mf)
        {
            this.InitializeComponent();
            this.mf = mf;
            using var dbContext = new AtctablesContext();
            estim = dbContext.Stimatis.ToHashSet();
            this.running = true;
            this.mf = mf;
            this.UpdateView();
            this.comboBoxCenter.DataSource = dbContext.Centros.Where(c => c.Postaziones.Count > 1).Select(c => c.NomeCentro).ToList();
            this.ContinuousThread = new Thread(() =>
            {
                int minOld = 0;
                while (this.running)
                {

                    try
                    {
                        this.Invoke(() =>
                        {
                            this.labelActualTime.Text =
                                this.mf.ActualTime.ToString(System.Globalization.CultureInfo.CurrentCulture);
                            if (mf.ActualTime.Minute != minOld)
                            {
                                UpdatePlots();
                                minOld = mf.ActualTime.Minute;
                            }
                        });
                    }
                    catch (System.ObjectDisposedException)
                    {
                    }
                    catch (System.InvalidOperationException)
                    {
                    }

                    Thread.Sleep(200);
                }
            });
            this.ContinuousThread.Start();
        }

        private void UpdatePlots()
        {
            int index = 0;
            foreach (TableLayoutPanel table in this.flowLayoutPanelGraphs.Controls)
            {
                FormsPlot plot = (FormsPlot)table.GetControlFromPosition(0, 1)!;
                plot.Plot.Clear();
                DateTime[] x = new DateTime[10];
                for (int i = 0; i < 10; i++)
                {
                    x[i] = mf.ActualTime.AddHours(i);
                }

                int[] y = new int[10];
                for (int i = 0; i < 10; i++)
                {
                    y[i] = occupationInSector(x[i], sectorsList[index]);
                }

                plot.Plot.AddScatter(x.Select(x => (double)x.Hour).ToArray(),
                    y.Select(y => (double)y).ToArray());
                plot.Plot.AddHorizontalLine(PositionsUtils.PositionCapacityWithSectors(sectorsList[index].Count) / (24 / PositionsUtils.ShiftsInDays), color: System.Drawing.Color.Red);

                plot.Refresh();
                index++;
            }
        }

        private Thread? ContinuousThread { get; set; }

        private int occupationInSector(DateTime time, ICollection<string> sector)
        {
            using var dbContext = new AtctablesContext();
            return estim.Count(s =>
         s.OrarioStimato.Date.Equals(time.Date)
         && s.OrarioStimato.Hour == time.Hour
         && sector.Contains(dbContext.Puntos.Find(s.NomePunto).IdSettore));
        }
        private TableLayoutPanel CreatePositionTable(string positionName)
        {
            using var dbContext = new AtctablesContext();

            // Create the table layout panel
            TableLayoutPanel tableLayoutPanel = new TableLayoutPanel();
            tableLayoutPanel.RowCount = 2;
            tableLayoutPanel.RowStyles.Clear();
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel.Dock = DockStyle.Fill;

            // Create the controls for the first row
            FlowLayoutPanel flowLayoutPanel = new FlowLayoutPanel();
            flowLayoutPanel.FlowDirection = FlowDirection.LeftToRight;
            flowLayoutPanel.AutoSize = true;

            Label label = new Label();
            label.AutoSize = true;
            label.Text = positionName + @": " + string.Join(", ", dbContext.Settores.Where(s => s.IdPostaziones.Contains(dbContext.Postaziones.Find(positionName)!)).Select(s => s.IdSettore)
                .ToList());
            sectorsList.Add(new List<string>(dbContext.Settores.Where(s => s.IdPostaziones.Contains(dbContext.Postaziones.Find(positionName)!)).Select(s => s.IdSettore)
                .ToList()));
            positionList.Add(positionName);
            flowLayoutPanel.Controls.Add(label);

            ComboBox comboBox = new ComboBox();

            // Fill box with controllers on duty.
            var source = dbContext.Turnos.Where(t =>
                    t.Data.Equals(this.mf.ActualTime.Date)
                    && t.Slot == PositionsUtils.SlotOfTime(this.mf.ActualTime.TimeOfDay)
                    && (t.CentroStandBy!.Equals(this.comboBoxCenter.SelectedItem) || t.IdPostazioneNavigation!.NomeCentro.Equals(this.comboBoxCenter.SelectedItem)))
                .Select(t => t.IdControlloreNavigation.IdControllore + " " + t.IdControlloreNavigation.Nome + " " + t.IdControlloreNavigation.Cognome)
                .ToList();

            var shift = dbContext.Turnos.FirstOrDefault(t => t.Data.Equals(mf.ActualTime.Date) && t.Slot.Equals(PositionsUtils.SlotOfTime(this.mf.ActualTime.TimeOfDay)) && t.IdPostazione.Equals(positionName));
            string? contrString = null;
            if (shift != null)
            {
                var shiftController = dbContext.Controllores.Find(shift.IdControllore);
                contrString = shiftController.IdControllore + " " + shiftController.Nome + " " +
                                  shiftController.Cognome;
                source.Remove(contrString);
                source.Insert(0, contrString);
            }

            comboBox.DataSource = source;
            comboBox.AutoSize = true;
            flowLayoutPanel.Controls.Add(comboBox);

            CheckBox checkBox = new CheckBox();
            checkBox.Text = @"Attivo";

            // Fill checkbox if position is active.
            checkBox.Checked = dbContext.Turnos.Any(t =>
                t.Data.Equals(this.mf.ActualTime.Date)
                && t.Slot == PositionsUtils.SlotOfTime(this.mf.ActualTime.TimeOfDay)
                && t.IdPostazione!.Equals(positionName));
            flowLayoutPanel.Controls.Add(checkBox);

            // Create the ScottPlot control for the second row
            FormsPlot scottPlotControl = new FormsPlot();
            scottPlotControl.Dock = DockStyle.Fill;
            scottPlotControl.Plot.AddSignal(DataGen.Sin(50));

            // Add the controls to the table layout panel
            tableLayoutPanel.Controls.Add(flowLayoutPanel, 0, 0);
            tableLayoutPanel.Controls.Add(scottPlotControl, 0, 1);

            // Set sizes for the rows
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.AutoSize));
            tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // Set preferred size for the table layout panel
            tableLayoutPanel.Size = new Size(500, 300); // Adjust the size as needed
            tableLayoutPanel.AutoSize = true;
            scottPlotControl.Refresh();
            return tableLayoutPanel;
        }

        private void ComboBoxCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.UpdateView();
        }

        private void UpdateView()
        {
            using var dbContext = new AtctablesContext();
            this.flowLayoutPanelGraphs.Controls.Clear();
            var positionSelected = dbContext.Postaziones
                .Where(p => p.NomeCentro.Equals(this.comboBoxCenter.SelectedItem)).ToList();
            sectorsList.Clear();
            positionList.Clear();
            foreach (var position in positionSelected)
            {
                this.flowLayoutPanelGraphs.Controls.Add(this.CreatePositionTable(position.IdPostazione));
            }
            UpdatePlots();

        }

        private void Supervisor_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.running = false;
        }

        private void buttonApply_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();
            int index = 0;
            var shift = dbContext.Turnos.Where(t => t.Data.Equals(mf.ActualTime.Date) && t.Slot.Equals(PositionsUtils.SlotOfTime(this.mf.ActualTime.TimeOfDay)) && positionList.Contains(t.IdPostazione));
            dbContext.Turnos.RemoveRange(shift);
            foreach (TableLayoutPanel table in this.flowLayoutPanelGraphs.Controls)
            {
                Controllore controller = dbContext.Controllores.Find((((((FlowLayoutPanel)table.GetControlFromPosition(0, 0)!)).Controls[1] as ComboBox)!).SelectedItem.ToString().Split(" ")[0]);
                if ((((((FlowLayoutPanel)table.GetControlFromPosition(0, 0)!)).Controls[2] as CheckBox) !).Checked)
                {
                    // Remove stand by shift if present

                    var sBShift = dbContext.Turnos.Where(t =>
                        t.Data.Equals(mf.ActualTime.Date) &&
                        t.Slot.Equals(PositionsUtils.SlotOfTime(this.mf.ActualTime.TimeOfDay)) &&
                        t.IdControllore.Equals(controller.IdControllore));
                    dbContext.Turnos.RemoveRange(sBShift);
                    var newShift = new Turno()
                    {
                        IdControllore = controller.IdControllore,
                        Retribuzione = CenterTurns.StandardPay,
                        Data = mf.ActualTime.Date,
                        Slot = PositionsUtils.SlotOfTime(this.mf.ActualTime.TimeOfDay),
                        IdPostazione = positionList[index],
                    };
                    dbContext.Turnos.Add(newShift);
                }
                index++;
            }

            dbContext.SaveChanges();
            UpdateView();
        }
    }
}
