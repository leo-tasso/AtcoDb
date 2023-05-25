namespace AtcoDbPopulator
{
    partial class Manager
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabControl1 = new TabControl();
            this.tabPage1 = new TabPage();
            this.comboBox1 = new ComboBox();
            this.dataGridView1 = new DataGridView();
            this.tabPage2 = new TabPage();
            this.TurnsGenerator = new Button();
            this.label2 = new Label();
            this.label1 = new Label();
            this.numericUpDown2 = new NumericUpDown();
            this.numericUpDown1 = new NumericUpDown();
            this.tabPage3 = new TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.dataGridView1).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDown2).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDown1).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new Size(1363, 680);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Controls.Add(this.dataGridView1);
            this.tabPage1.Location = new Point(4, 24);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new Padding(3);
            this.tabPage1.Size = new Size(1355, 652);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new Point(127, 6);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new Size(121, 23);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += this.ComboBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new Point(3, 33);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.Size = new Size(827, 616);
            this.dataGridView1.TabIndex = 0;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.TurnsGenerator);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.numericUpDown2);
            this.tabPage2.Controls.Add(this.numericUpDown1);
            this.tabPage2.Location = new Point(4, 24);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new Padding(3);
            this.tabPage2.Size = new Size(1355, 652);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Generatore Turni";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // TurnsGenerator
            // 
            this.TurnsGenerator.Location = new Point(297, 15);
            this.TurnsGenerator.Name = "TurnsGenerator";
            this.TurnsGenerator.Size = new Size(105, 23);
            this.TurnsGenerator.TabIndex = 4;
            this.TurnsGenerator.Text = "Genera Turni";
            this.TurnsGenerator.UseVisualStyleBackColor = true;
            this.TurnsGenerator.Click += this.TurnsGenerator_Click;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(110, 17);
            this.label2.Name = "label2";
            this.label2.Size = new Size(39, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Anno:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(11, 17);
            this.label1.Name = "label1";
            this.label1.Size = new Size(38, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Mese:";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new Point(155, 15);
            this.numericUpDown2.Maximum = new decimal(new int[] { 2050, 0, 0, 0 });
            this.numericUpDown2.Minimum = new decimal(new int[] { 2023, 0, 0, 0 });
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new Size(120, 23);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.Value = new decimal(new int[] { 2023, 0, 0, 0 });
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new Point(55, 15);
            this.numericUpDown1.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            this.numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new Size(40, 23);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new Point(4, 24);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new Padding(3);
            this.tabPage3.Size = new Size(1355, 652);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "tabPage3";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.BackColor = SystemColors.ControlLight;
            this.ClientSize = new Size(1386, 704);
            this.Controls.Add(this.tabControl1);
            this.Name = "Manager";
            this.Text = "Manager";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)this.dataGridView1).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDown2).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.numericUpDown1).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private DataGridView dataGridView1;
        private TabPage tabPage3;
        private ComboBox comboBox1;
        private Button TurnsGenerator;
        private Label label2;
        private Label label1;
        private NumericUpDown numericUpDown2;
        private NumericUpDown numericUpDown1;
    }
}