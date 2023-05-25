// <copyright file="Manager.cs" company="Leonardo Tassinari">
// Copyright (c) Leonardo Tassinari. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
// </copyright>

namespace AtcoDbPopulator
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Windows.Forms;
    using AtcoDbPopulator.Models;
    using ClosedXML.Excel;

    /// <summary>
    /// Window to manage the controllers.
    /// </summary>
    public partial class Manager : Form
    {
        private readonly CenterTurns centerShifts = new CenterTurns();

        /// <summary>
        /// Initializes a new instance of the <see cref="Manager"/> class.
        /// </summary>
        public Manager()
        {
            this.InitializeComponent();
            using var dbContext = new AtctablesContext();
            this.comboBox1.DataSource = dbContext.Centros.Select(c => c.NomeCentro).ToList();
            this.dataGridView1.DataSource = this.DataSource();
        }

        private static Controllore? FindController(string idController)
        {
            using var dbContext = new AtctablesContext();
            return dbContext.Controllores.Find(idController);
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
                }).ToList();
        }

        private void TurnsGenerator_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();
            var totCenters = dbContext.Centros.Count(c => c.Postaziones.Count == 1);
            int done = 0;
            /* TODO expand with all centers */

            foreach (var center in dbContext.Centros.Where(c => c.Postaziones.Count == 1))
            {
                this.progressBarTurnGeneration.Value = done * 100 / totCenters;
                this.centerShifts.CenterTurnsGenerator(
                    center,
                    (int)this.numericUpDown1.Value,
                    (int)this.numericUpDown2.Value);
                done++;
            }

            this.progressBarTurnGeneration.Value = 0;

            var positions = dbContext.Postaziones.Select(p => p.IdPostazione);

            // TODO add also standby centers
            List<string> dates = new List<string>();
            DateTime startDate = new DateTime((int)this.numericUpDown2.Value, (int)this.numericUpDown1.Value, 1);
            var shifts = dbContext.Turnos
                .Where(t =>
                    t.Data.Year == (int)this.numericUpDown2.Value && t.Data.Month == (int)this.numericUpDown1.Value)
                .ToList();

            while (startDate.Month == (int)this.numericUpDown1.Value)
            {
                for (int i = 1; i <= CenterTurns.ShiftsInDays; i++)
                {
                    dates.Add(startDate.ToShortDateString() + ", turno " + i);
                }

                startDate = startDate.AddDays(1);
            }

            var dataTable = new DataTable();
            dataTable.Columns.Add("Position", typeof(string));
            foreach (var date in dates)
            {
                dataTable.Columns.Add(date);
            }

            foreach (var position in positions)
            {
                var row = dataTable.NewRow();
                row["Position"] = position;

                // Fill the row with data for each date
                foreach (var date in dates)
                {
                    var turno = shifts.FirstOrDefault(t =>
                        t.IdPostazione == position &&
                        date == t.Data.ToShortDateString() + ", turno " + t.Slot);
                    if (turno != null)
                    {
                        Controllore? controller = Manager.FindController(turno.IdControllore);
                        row[date] = controller != null ? turno.IdControllore + " " + controller.Nome + " " + controller.Cognome : string.Empty;
                    }
                    else
                    {
                        row[date] = string.Empty;
                    }
                }

                dataTable.Rows.Add(row);
            }

            this.dataGridView2.DataSource = dataTable;
            this.dataGridView2.Columns[1].SortMode = DataGridViewColumnSortMode.NotSortable;

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

                    // Copy the first row with dates
                    for (int j = 0; j < this.dataGridView2.Columns.Count; j++)
                    {
                        worksheet.Cell(1, j + 1).Value = this.dataGridView2.Columns[j].HeaderText;
                    }

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
                            worksheet.Cell(i + 2, j + 1).Value =
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
    }
}
