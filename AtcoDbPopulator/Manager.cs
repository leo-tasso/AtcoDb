// <copyright file="Manager.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using AtcoDbPopulator.Models;
    using ClosedXML.Excel;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Window to manage the controllers.
    /// </summary>
    public partial class Manager : Form
    {
        private const int MaxHolidayDays = 30;

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        public Manager()
        {
            this.InitializeComponent();
            using var dbContext = new AtctablesContext();
            this.comboBox1.DataSource = dbContext.Centros.Select(c => c.NomeCentro).ToList();
            this.dataGridView1.DataSource = this.DataSource();
            this.UpdateControllerManagerList();
            this.CenterComboBox.DataSource = dbContext.Centros.Select(c => c.NomeCentro).ToList();
        }

        private static int GetTotalHolidayDays(string controllerId)
        {
            using var dbContext = new AtctablesContext();
            return dbContext.Feries.Where(f => f.IdControllore.Equals(controllerId))
                .ToList()
                .Aggregate(0, (sum, holiday) => sum + (holiday.Fine.Date - holiday.Inizio.Date).Days);
        }

        private static bool CompatibleHoliday(string controllerId, DateTime begin, DateTime end)
        {
            using var dbContext = new AtctablesContext();
            var holidays = dbContext.Feries.Where(f => f.IdControllore.Equals(controllerId));
            return !holidays.Any(h => (h.Inizio > begin && h.Inizio < end) || (h.Fine > begin && h.Fine < end) || (begin < h.Inizio && end > h.Fine));
        }

        private void UpdateControllerManagerList()
        {
            using var dbContext = new AtctablesContext();
            var controllersDropDown =
                dbContext.Controllores.Select(c => c.IdControllore + " " + c.Cognome + " " + c.Nome).ToList();
            controllersDropDown.Insert(0, "Assumi nuovo");
            this.ControllerManagerSelector.DataSource = controllersDropDown;
            if (this.IdBox.Text != string.Empty)
            {
                var controller = dbContext.Controllores.Find(this.IdBox.Text);
                if (controller != null)
                {
                    this.ControllerManagerSelector.SelectedItem = controller.IdControllore + " " + controller.Cognome + " " + controller.Nome;
                }
            }
        }

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.DataSource();
        }

        private List<ControlloreViewModel> DataSource()
        {
            using var dbContext = new AtctablesContext();
            return dbContext.Controllores.Where(c => c.NomeCentro.Equals(this.comboBox1.SelectedItem.ToString()))
                .Select(s => new ControlloreViewModel
                {
                    Nome = s.Nome,
                    Cognome = s.Cognome,
                    Id = s.IdControllore,
                    TurniLavorati = dbContext.Turnos.Count(t => t.IdControllore.Equals(s.IdControllore)),
                }).ToList();
        }

        private void TurnsGenerator_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();

            // wipe previous data
            dbContext.RemoveRange(dbContext.Turnos.Where(t => t.Data.Month == (int)this.numericUpDown1.Value && t.Data.Year == (int)this.numericUpDown2.Value));
            dbContext.SaveChanges();
            var totCenters = dbContext.Centros.Count();
            int done = 0;
            var centerTurns = new CenterTurns();
            foreach (var center in dbContext.Centros)
            {
                this.progressBarTurnGeneration.Value = done * 100 / totCenters;
                centerTurns.CenterTurnsGenerator(
                    center,
                    (int)this.numericUpDown1.Value,
                    (int)this.numericUpDown2.Value,
                    this.OccupancyCheckCheckBox.CheckState == CheckState.Checked);
                done++;
            }

            this.progressBarTurnGeneration.Value = 0;

            var dataTable = new ShiftsTableFactory().CreateDataTable((int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);

            this.dataGridView2.DataSource = dataTable;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;
            this.dataGridView2.Columns[0].Frozen = true;
            this.dataGridView2.Rows[0].Frozen = true;

            for (int i = 0; i < this.dataGridView2.Columns.Count - 1; i++)
            {
                Color columnColor = i % 3 == 0 ? Color.LightBlue : i % 3 == 1 ? Color.LightGreen : Color.LightPink;
                this.dataGridView2.Columns[i + 1].DefaultCellStyle.BackColor = columnColor;
            }

            for (int i = 1; i < this.dataGridView2.Columns.Count; i++)
            {
                this.dataGridView2.Columns[i].SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            this.dataGridView2.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            Thread exportThread = new Thread(() =>
            {
                var saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = @"Excel Files|*.xlsx";
                saveFileDialog.Title = @"Save Excel File";
                saveFileDialog.FileName = "output.xlsx";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName;

                    // Export the data to Excel
                    var workbook = new XLWorkbook();
                    var worksheet =
                        workbook.Worksheets.Add("Turni " + this.numericUpDown2.Value + " " + this.numericUpDown2.Value);
                    /*not needed, copied header on first row

                    // Copy the first row with dates if de-commenting  set "i + 2" in worksheet.cell(...
                    for (int j = 0; j < this.dataGridView2.Columns.Count; j++)
                    {
                        worksheet.Cell(1, j + 1).Value = this.dataGridView2.Columns[j].HeaderText;
                    }
                    */

                    // Set the column colors
                    for (int j = 1; j <= this.dataGridView2.Columns.Count; j++)
                    {
                        var color = this.dataGridView2.Columns[j - 1].DefaultCellStyle.BackColor;
                        worksheet.Column(j).Style.Fill.BackgroundColor = XLColor.FromColor(color);
                    }

                    // Fill the worksheet with data from the DataGridView
                    for (int i = 0; i < this.dataGridView2.Rows.Count; i++)
                    {
                        for (int j = 0; j < this.dataGridView2.Columns.Count; j++)
                        {
                            var cellValue = this.dataGridView2.Rows[i].Cells[j].Value;
                            worksheet.Cell(i + 1, j + 1).Value =
                                (cellValue != null) ? cellValue.ToString() : string.Empty;
                        }
                    }

                    // Apply borders to all cells
                    var range = worksheet.RangeUsed();
                    var borders = range.Style.Border;
                    borders.OutsideBorder = XLBorderStyleValues.Thin;
                    borders.InsideBorder = XLBorderStyleValues.Thin;

                    // Auto-fit columns
                    worksheet.Columns().AdjustToContents();

                    // Save the Excel file
                    workbook.SaveAs(filePath);
                }
            });
            exportThread.SetApartmentState(ApartmentState.STA);
            exportThread.Start();
        }

        private void ControllerManagerSelectorSelectionChangeCommitted(object sender, EventArgs e)
        {
            this.RefreshController();
        }

        private void RefreshController()
        {
            var selected = this.ControllerManagerSelector.SelectedItem.ToString() !.Split(' ').ToArray()[0];
            using var dbContext = new AtctablesContext();
            var controller = dbContext.Controllores.Find(selected);
            if (controller != null)
            {
                this.IdBox.Text = controller.IdControllore;
                this.IdBox.Enabled = false;
                this.NameBox.Text = controller.Nome;
                this.SurnameBox.Text = controller.Cognome;
                this.CenterComboBox.SelectedItem = controller.NomeCentro;
                this.dataGridViewHolidays.DataSource =
                    dbContext.Feries.Where(f => f.IdControllore.Equals(controller.IdControllore))
                        .Select(f => new { f.Inizio, f.Fine }).ToList();
            }
            else
            {
                this.FlushFields();
            }
        }

        private void FlushFields()
        {
            this.IdBox.Text = string.Empty;
            this.IdBox.Enabled = true;
            this.NameBox.Text = string.Empty;
            this.SurnameBox.Text = string.Empty;
            this.dataGridViewHolidays.DataSource = null;
        }

        private void AggiornaButton_Click(object sender, EventArgs e)
        {
            var selected = this.ControllerManagerSelector.SelectedItem.ToString() !.Split(' ').ToArray()[0];
            using var dbContext = new AtctablesContext();
            Controllore? controller = dbContext.Controllores.Find(selected);
            if (controller != null)
            {
                controller.Nome = this.NameBox.Text;
                controller.Cognome = this.SurnameBox.Text;
                controller.NomeCentro = this.CenterComboBox.SelectedItem.ToString();
                dbContext.Update(controller);
            }
            else
            {
                var newController = new Controllore()
                {
                    IdControllore = this.IdBox.Text,
                    Nome = this.NameBox.Text,
                    Cognome = this.SurnameBox.Text,
                    NomeCentro = this.CenterComboBox.SelectedItem.ToString(),
                };
                dbContext.Controllores.Add(newController);
                dbContext.SaveChanges();
            }

            dbContext.SaveChanges();
            this.UpdateControllerManagerList();
            this.RefreshController();
        }

        private void LicenziaButton_Click(object sender, EventArgs e)
        {
            var selected = this.ControllerManagerSelector.SelectedItem.ToString() !.Split(' ').ToArray()[0];
            using var dbContext = new AtctablesContext();
            var controller = dbContext.Controllores.Find(selected);
            if (controller != null)
            {
                dbContext.Database.ExecuteSqlRaw("DELETE FROM AbilitazioneSettori WHERE MatricolaAbilitazione =" + controller.IdControllore);
                dbContext.Abilitaziones.RemoveRange(
                    dbContext.Abilitaziones.Where(a => a.IdControllore.Equals(controller.IdControllore)));
                dbContext.Feries.RemoveRange(
                    dbContext.Feries.Where(a => a.IdControllore.Equals(controller.IdControllore)));
                dbContext.Controllores.Remove(controller);
                dbContext.SaveChanges();
                this.FlushFields();
                this.UpdateControllerManagerList();
            }
        }

        private void AddHolidayButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewHolidays.DataSource != null)
            {
                var selected = this.ControllerManagerSelector.SelectedItem.ToString() !.Split(' ').ToArray()[0];
                using var dbContext = new AtctablesContext();
                var controller = dbContext.Controllores.Find(selected);
                if (controller != null)
                {
                    int newDays = (this.dateTimePickerEndHoliday.Value - this.dateTimePickerBeginHoliday.Value).Days;
                    if (newDays + Manager.GetTotalHolidayDays(controller.IdControllore) < MaxHolidayDays &&
                        Manager.CompatibleHoliday(
                            controller.IdControllore,
                            this.dateTimePickerEndHoliday.Value,
                            this.dateTimePickerBeginHoliday.Value))
                    {
                        var newHoliday = new Ferie()
                        {
                            IdControllore = controller.IdControllore,
                            Inizio = this.dateTimePickerBeginHoliday.Value.Date,
                            Fine = this.dateTimePickerEndHoliday.Value.Date,
                        };
                        dbContext.Feries.Add(newHoliday);
                        dbContext.SaveChanges();
                        this.RefreshController();
                    }
                    else
                    {
                        throw new OverflowException("Incompatible holiday days");
                    }
                }
            }
        }

        private void RemoveHolidayButton_Click(object sender, EventArgs e)
        {
            if (this.dataGridViewHolidays.DataSource != null)
            {
                var selected = this.ControllerManagerSelector.SelectedItem.ToString() !.Split(' ').ToArray()[0];
                using var dbContext = new AtctablesContext();
                var controller = dbContext.Controllores.Find(selected);
                if (controller != null)
                {
                    var selectedHoliday = this.dataGridViewHolidays.SelectedCells;
                    dbContext.Feries.Remove(dbContext.Feries.Find(
                        controller.IdControllore,
                        DateTime.Parse(selectedHoliday[0].Value.ToString() !)) !);
                    dbContext.SaveChanges();
                    this.RefreshController();
                }
            }
        }
    }
}
