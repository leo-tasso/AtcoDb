using AtcoDbPopulator.Models;
using ScottPlot;
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
    public partial class Supervisor : Form
    {
        private MainForm mf;

        public Supervisor(MainForm mf)
        {
            InitializeComponent();
            this.mf = mf;
            using var dbContext = new AtctablesContext();
            running = true;
            this.mf = mf;
            this.ControllersUtils = new ControllersUtils();
            UpdateView();
            comboBoxCenter.DataSource = dbContext.Centros.Select(c => c.NomeCentro).ToList();
            this.continuousThread = new Thread(() =>
            {
                while (this.running)
                {
                    try
                    { this.Invoke(() =>
                        {

                            using var dbContext = new AtctablesContext();
                            this.labelActualTime.Text =
                                this.mf.ActualTime.ToString(System.Globalization.CultureInfo.CurrentCulture);
                            foreach (TableLayoutPanel table in flowLayoutPanelGraphs.Controls)
                            {
                                FormsPlot plot = (FormsPlot)table.GetControlFromPosition(0, 1);
                                plot.Plot.Clear();
                                plot.Plot.AddSignal(DataGen.Sin(mf.ActualTime.Second + 2));
                                plot.Refresh();
                            }
                        });
                    }
                    catch (System.ObjectDisposedException e) {}
                    catch(System.InvalidOperationException e){}

                    
                    Thread.Sleep(200);
                }
            });
            this.continuousThread.Start();

        }

        private ControllersUtils ControllersUtils { get; }

        private Thread? continuousThread { get; set; }
        private volatile bool running = false;
        public TableLayoutPanel CreatePositionTable(string positionName)
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
            label.Text = positionName + ": " + string.Join(", ", dbContext.Settores.Where(s => s.IdPostaziones.Contains(dbContext.Postaziones.Find(positionName))).Select(s => s.IdSettore)
                .ToList());
            flowLayoutPanel.Controls.Add(label);

            ComboBox comboBox = new ComboBox();
            // Fill box with controllers on duty.
            comboBox.DataSource = dbContext.Turnos.Where(t =>
                t.Data.Equals(mf.ActualTime.Date)
                && t.Slot == PositionsUtils.SlotOfTime(mf.ActualTime.TimeOfDay)
                && (t.CentroStandBy.Equals(comboBoxCenter.SelectedItem) || t.IdPostazioneNavigation.NomeCentro.Equals(comboBoxCenter.SelectedItem)))
                .Select(t => t.IdControlloreNavigation.IdControllore + " " + t.IdControlloreNavigation.Nome + " " + t.IdControlloreNavigation.Cognome)
                .ToList();
            flowLayoutPanel.Controls.Add(comboBox);
            CheckBox checkBox = new CheckBox();
            checkBox.Text = "Attivo";

            // Fill checkbox if position is active.
            checkBox.Checked = dbContext.Turnos.Any(t =>
                t.Data.Equals(mf.ActualTime.Date)
                && t.Slot == PositionsUtils.SlotOfTime(mf.ActualTime.TimeOfDay)
                && t.IdPostazione.Equals(positionName));
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

        private void comboBoxCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateView();
        }

        private void UpdateView()
        {
            using var dbContext = new AtctablesContext();
            flowLayoutPanelGraphs.Controls.Clear();
            var positionSelected = dbContext.Postaziones.Where(p => p.NomeCentro.Equals(comboBoxCenter.SelectedItem)).ToList();
            foreach (var position in positionSelected)
            {
                flowLayoutPanelGraphs.Controls.Add(this.CreatePositionTable(position.IdPostazione));
            }
        }
        
        private void Supervisor_FormClosing(object sender, FormClosingEventArgs e)
        {
            running = false;
            if (continuousThread != null && continuousThread.IsAlive)
            {
                //continuousThread.Join();
            }
        }
    }
}
