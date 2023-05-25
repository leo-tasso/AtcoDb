﻿// <copyright file="Manager.cs" company="Leonardo Tassinari">
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
            var totCenters = dbContext.Centros.Count(c => c.Postaziones.Count == 1);
            int done = 0;
            /* TODO expand with all centers */

            foreach (var center in dbContext.Centros.Where(c => c.Postaziones.Count == 1))
            {
                this.progressBarTurnGeneration.Value = done * 100 / totCenters;
                this.centerShifts.CenterTurnsGenerator(center, (int)this.numericUpDown1.Value, (int)this.numericUpDown2.Value);
                done++;
            }

            this.progressBarTurnGeneration.Value = 0;
        }
    }
}
