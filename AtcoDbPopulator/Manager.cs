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

        private void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.DataSource();
        }

        private List<ControlloreViewModel> DataSource()
        {
            using var dbContext = new AtctablesContext();
            return dbContext.Controllores.Where(c => c.NomeCentro.Equals(this.comboBox1.SelectedItem.ToString())).Select(s => new ControlloreViewModel
            {
                Nome = s.Nome,
                Cognome = s.Cognome,
                Id = s.IdControllore,
            }).ToList();
        }

        private void TurnsGenerator_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();
            foreach (var center in dbContext.Centros.Where(c => c.Postaziones.Count == 1)) // TODO expand with all centers
            {
                this.centerShifts.CenterTurnsGenerator(center, (int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
            }
        }
    }

    /// <summary>
    /// Short Controller view.
    /// </summary>
    public class ControlloreViewModel
    {
        public string Id { get; set; }

        public string Nome { get; set; }

        public string Cognome { get; set; }
    }
}
