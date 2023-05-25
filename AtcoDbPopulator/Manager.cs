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
    public partial class Manager : Form
    {
        public Manager()
        {
            InitializeComponent();
            using var dbContext = new AtctablesContext();
            comboBox1.DataSource = dbContext.Centros.Select(c => c.NomeCentro).ToList();
            dataGridView1.DataSource = DataSource();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = DataSource();
        }

        private List<ControlloreViewModel> DataSource()
        {
            using var dbContext = new AtctablesContext();
            return dbContext.Controllores.Where(c => c.NomeCentro.Equals(comboBox1.SelectedItem.ToString())).Select(s => new ControlloreViewModel
            {
                Nome = s.Nome,
                Cognome = s.Cognome,
                Id = s.IdControllore,
            }).ToList();
        }

        private void TurnsGenerator_Click(object sender, EventArgs e)
        {
            using var dbContext = new AtctablesContext();
            foreach (var center in dbContext.Centros)
            {
                CenterTurns.CenterTurnsGenerator(center, (int)numericUpDown1.Value, (int)numericUpDown2.Value);
            }
        }
    }
    public class ControlloreViewModel
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Cognome { get; set; }

    }
}
