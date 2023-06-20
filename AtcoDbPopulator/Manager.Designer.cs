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
            tabPage3 = new TabPage();
            tableLayoutPanel4 = new TableLayoutPanel();
            NameBox = new TextBox();
            ControllerManagerSelector = new ComboBox();
            label3 = new Label();
            flowLayoutPanel3 = new FlowLayoutPanel();
            AggiornaButton = new Button();
            LicenziaButton = new Button();
            Cognome = new Label();
            Id = new Label();
            SurnameBox = new TextBox();
            IdBox = new TextBox();
            Centro = new Label();
            CenterComboBox = new ComboBox();
            dataGridViewHolidays = new DataGridView();
            label4 = new Label();
            flowLayoutPanel4 = new FlowLayoutPanel();
            RemoveHolidayButton = new Button();
            AddHolidayButton = new Button();
            dateTimePickerBeginHoliday = new DateTimePicker();
            dateTimePickerEndHoliday = new DateTimePicker();
            tabPage2 = new TabPage();
            tableLayoutPanel1 = new TableLayoutPanel();
            tableLayoutPanel2 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            label1 = new Label();
            numericUpDown1 = new NumericUpDown();
            label2 = new Label();
            numericUpDown2 = new NumericUpDown();
            TurnsGenerator = new Button();
            OccupancyCheckCheckBox = new CheckBox();
            flowLayoutPanel2 = new FlowLayoutPanel();
            ExportButton = new Button();
            dataGridView2 = new DataGridView();
            progressBarTurnGeneration = new ProgressBar();
            tabPage1 = new TabPage();
            tableLayoutPanel3 = new TableLayoutPanel();
            comboBox1 = new ComboBox();
            dataGridView1 = new DataGridView();
            tabSelector = new TabControl();
            tabPage3.SuspendLayout();
            tableLayoutPanel4.SuspendLayout();
            flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewHolidays).BeginInit();
            flowLayoutPanel4.SuspendLayout();
            tabPage2.SuspendLayout();
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).BeginInit();
            flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            tabPage1.SuspendLayout();
            tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabSelector.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(tableLayoutPanel4);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Padding = new Padding(3);
            tabPage3.Size = new Size(1378, 676);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Gestore Controllori";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel4
            // 
            tableLayoutPanel4.ColumnCount = 2;
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
            tableLayoutPanel4.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
            tableLayoutPanel4.Controls.Add(NameBox, 1, 1);
            tableLayoutPanel4.Controls.Add(ControllerManagerSelector, 1, 0);
            tableLayoutPanel4.Controls.Add(label3, 0, 1);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel3, 0, 0);
            tableLayoutPanel4.Controls.Add(Cognome, 0, 2);
            tableLayoutPanel4.Controls.Add(Id, 0, 3);
            tableLayoutPanel4.Controls.Add(SurnameBox, 1, 2);
            tableLayoutPanel4.Controls.Add(IdBox, 1, 3);
            tableLayoutPanel4.Controls.Add(Centro, 0, 4);
            tableLayoutPanel4.Controls.Add(CenterComboBox, 1, 4);
            tableLayoutPanel4.Controls.Add(dataGridViewHolidays, 1, 5);
            tableLayoutPanel4.Controls.Add(label4, 0, 5);
            tableLayoutPanel4.Controls.Add(flowLayoutPanel4, 1, 6);
            tableLayoutPanel4.Dock = DockStyle.Fill;
            tableLayoutPanel4.Location = new Point(3, 3);
            tableLayoutPanel4.Name = "tableLayoutPanel4";
            tableLayoutPanel4.RowCount = 7;
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle());
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 200F));
            tableLayoutPanel4.RowStyles.Add(new RowStyle(SizeType.Absolute, 20F));
            tableLayoutPanel4.Size = new Size(1372, 670);
            tableLayoutPanel4.TabIndex = 0;
            // 
            // NameBox
            // 
            NameBox.Dock = DockStyle.Fill;
            NameBox.Location = new Point(277, 38);
            NameBox.Name = "NameBox";
            NameBox.Size = new Size(1092, 23);
            NameBox.TabIndex = 2;
            // 
            // ControllerManagerSelector
            // 
            ControllerManagerSelector.Dock = DockStyle.Fill;
            ControllerManagerSelector.FormattingEnabled = true;
            ControllerManagerSelector.Location = new Point(277, 3);
            ControllerManagerSelector.Name = "ControllerManagerSelector";
            ControllerManagerSelector.Size = new Size(1092, 23);
            ControllerManagerSelector.TabIndex = 3;
            ControllerManagerSelector.SelectionChangeCommitted += this.ControllerManagerSelectorSelectionChangeCommitted;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Dock = DockStyle.Fill;
            label3.Location = new Point(3, 35);
            label3.Name = "label3";
            label3.Size = new Size(268, 29);
            label3.TabIndex = 1;
            label3.Text = "Nome:";
            label3.TextAlign = ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel3
            // 
            flowLayoutPanel3.AutoSize = true;
            flowLayoutPanel3.Controls.Add(AggiornaButton);
            flowLayoutPanel3.Controls.Add(LicenziaButton);
            flowLayoutPanel3.Dock = DockStyle.Fill;
            flowLayoutPanel3.Location = new Point(3, 3);
            flowLayoutPanel3.Name = "flowLayoutPanel3";
            flowLayoutPanel3.Size = new Size(268, 29);
            flowLayoutPanel3.TabIndex = 4;
            // 
            // AggiornaButton
            // 
            AggiornaButton.Location = new Point(3, 3);
            AggiornaButton.Name = "AggiornaButton";
            AggiornaButton.Size = new Size(75, 23);
            AggiornaButton.TabIndex = 0;
            AggiornaButton.Text = "Aggiorna";
            AggiornaButton.UseVisualStyleBackColor = true;
            AggiornaButton.Click += this.AggiornaButton_Click;
            // 
            // LicenziaButton
            // 
            LicenziaButton.Location = new Point(84, 3);
            LicenziaButton.Name = "LicenziaButton";
            LicenziaButton.Size = new Size(75, 23);
            LicenziaButton.TabIndex = 1;
            LicenziaButton.Text = "Licenzia";
            LicenziaButton.UseVisualStyleBackColor = true;
            LicenziaButton.Click += this.LicenziaButton_Click;
            // 
            // Cognome
            // 
            Cognome.AutoSize = true;
            Cognome.Dock = DockStyle.Fill;
            Cognome.Location = new Point(3, 64);
            Cognome.Name = "Cognome";
            Cognome.Size = new Size(268, 29);
            Cognome.TabIndex = 5;
            Cognome.Text = "Cognome:";
            Cognome.TextAlign = ContentAlignment.MiddleRight;
            // 
            // Id
            // 
            Id.AutoSize = true;
            Id.Dock = DockStyle.Fill;
            Id.Location = new Point(3, 93);
            Id.Name = "Id";
            Id.Size = new Size(268, 29);
            Id.TabIndex = 6;
            Id.Text = "Id:";
            Id.TextAlign = ContentAlignment.MiddleRight;
            // 
            // SurnameBox
            // 
            SurnameBox.Dock = DockStyle.Fill;
            SurnameBox.Location = new Point(277, 67);
            SurnameBox.Name = "SurnameBox";
            SurnameBox.Size = new Size(1092, 23);
            SurnameBox.TabIndex = 7;
            // 
            // IdBox
            // 
            IdBox.Dock = DockStyle.Fill;
            IdBox.Location = new Point(277, 96);
            IdBox.Name = "IdBox";
            IdBox.Size = new Size(1092, 23);
            IdBox.TabIndex = 8;
            // 
            // Centro
            // 
            Centro.AutoSize = true;
            Centro.Dock = DockStyle.Fill;
            Centro.Location = new Point(3, 122);
            Centro.Name = "Centro";
            Centro.Size = new Size(268, 29);
            Centro.TabIndex = 11;
            Centro.Text = "Centro:";
            Centro.TextAlign = ContentAlignment.MiddleRight;
            // 
            // CenterComboBox
            // 
            CenterComboBox.Dock = DockStyle.Fill;
            CenterComboBox.FormattingEnabled = true;
            CenterComboBox.Location = new Point(277, 125);
            CenterComboBox.Name = "CenterComboBox";
            CenterComboBox.Size = new Size(1092, 23);
            CenterComboBox.TabIndex = 12;
            // 
            // dataGridViewHolidays
            // 
            dataGridViewHolidays.AllowUserToAddRows = false;
            dataGridViewHolidays.AllowUserToDeleteRows = false;
            dataGridViewHolidays.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewHolidays.Dock = DockStyle.Fill;
            dataGridViewHolidays.GridColor = SystemColors.InactiveBorder;
            dataGridViewHolidays.Location = new Point(277, 154);
            dataGridViewHolidays.MultiSelect = false;
            dataGridViewHolidays.Name = "dataGridViewHolidays";
            dataGridViewHolidays.RowTemplate.Height = 25;
            dataGridViewHolidays.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewHolidays.ShowEditingIcon = false;
            dataGridViewHolidays.Size = new Size(1092, 194);
            dataGridViewHolidays.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Dock = DockStyle.Fill;
            label4.Location = new Point(3, 151);
            label4.Name = "label4";
            label4.Size = new Size(268, 200);
            label4.TabIndex = 14;
            label4.Text = "Ferie:";
            label4.TextAlign = ContentAlignment.MiddleRight;
            // 
            // flowLayoutPanel4
            // 
            flowLayoutPanel4.Controls.Add(RemoveHolidayButton);
            flowLayoutPanel4.Controls.Add(AddHolidayButton);
            flowLayoutPanel4.Controls.Add(dateTimePickerBeginHoliday);
            flowLayoutPanel4.Controls.Add(dateTimePickerEndHoliday);
            flowLayoutPanel4.Dock = DockStyle.Fill;
            flowLayoutPanel4.Location = new Point(277, 354);
            flowLayoutPanel4.Name = "flowLayoutPanel4";
            flowLayoutPanel4.Size = new Size(1092, 313);
            flowLayoutPanel4.TabIndex = 15;
            // 
            // RemoveHolidayButton
            // 
            RemoveHolidayButton.Location = new Point(3, 3);
            RemoveHolidayButton.Name = "RemoveHolidayButton";
            RemoveHolidayButton.Size = new Size(75, 23);
            RemoveHolidayButton.TabIndex = 0;
            RemoveHolidayButton.Text = "Rimuovi";
            RemoveHolidayButton.UseVisualStyleBackColor = true;
            RemoveHolidayButton.Click += this.RemoveHolidayButton_Click;
            // 
            // AddHolidayButton
            // 
            AddHolidayButton.Location = new Point(84, 3);
            AddHolidayButton.Name = "AddHolidayButton";
            AddHolidayButton.Size = new Size(75, 23);
            AddHolidayButton.TabIndex = 1;
            AddHolidayButton.Text = "Aggiungi";
            AddHolidayButton.UseVisualStyleBackColor = true;
            AddHolidayButton.Click += this.AddHolidayButton_Click;
            // 
            // dateTimePickerBeginHoliday
            // 
            dateTimePickerBeginHoliday.Location = new Point(165, 3);
            dateTimePickerBeginHoliday.MaxDate = new DateTime(2050, 1, 1, 0, 0, 0, 0);
            dateTimePickerBeginHoliday.MinDate = new DateTime(2022, 1, 1, 0, 0, 0, 0);
            dateTimePickerBeginHoliday.Name = "dateTimePickerBeginHoliday";
            dateTimePickerBeginHoliday.Size = new Size(200, 23);
            dateTimePickerBeginHoliday.TabIndex = 2;
            dateTimePickerBeginHoliday.Value = new DateTime(2022, 1, 1, 0, 0, 0, 0);
            // 
            // dateTimePickerEndHoliday
            // 
            dateTimePickerEndHoliday.Location = new Point(371, 3);
            dateTimePickerEndHoliday.MinDate = new DateTime(2022, 1, 1, 0, 0, 0, 0);
            dateTimePickerEndHoliday.Name = "dateTimePickerEndHoliday";
            dateTimePickerEndHoliday.Size = new Size(200, 23);
            dateTimePickerEndHoliday.TabIndex = 3;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(tableLayoutPanel1);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(1378, 676);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Generatore Turni";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Controls.Add(dataGridView2, 0, 2);
            tableLayoutPanel1.Controls.Add(progressBarTurnGeneration, 0, 0);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(3, 3);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1372, 670);
            tableLayoutPanel1.TabIndex = 9;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.AutoSize = true;
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 67.49634F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 32.5036621F));
            tableLayoutPanel2.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel2.Controls.Add(flowLayoutPanel2, 1, 0);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 32);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 1;
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.Size = new Size(1366, 41);
            tableLayoutPanel2.TabIndex = 12;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Controls.Add(label1);
            flowLayoutPanel1.Controls.Add(numericUpDown1);
            flowLayoutPanel1.Controls.Add(label2);
            flowLayoutPanel1.Controls.Add(numericUpDown2);
            flowLayoutPanel1.Controls.Add(TurnsGenerator);
            flowLayoutPanel1.Controls.Add(OccupancyCheckCheckBox);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(916, 35);
            flowLayoutPanel1.TabIndex = 10;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(3, 0);
            label1.Name = "label1";
            label1.Size = new Size(38, 15);
            label1.TabIndex = 2;
            label1.Text = "Mese:";
            // 
            // numericUpDown1
            // 
            numericUpDown1.Location = new Point(47, 3);
            numericUpDown1.Maximum = new decimal(new int[] { 12, 0, 0, 0 });
            numericUpDown1.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDown1.Name = "numericUpDown1";
            numericUpDown1.Size = new Size(40, 23);
            numericUpDown1.TabIndex = 0;
            numericUpDown1.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(93, 0);
            label2.Name = "label2";
            label2.Size = new Size(39, 15);
            label2.TabIndex = 3;
            label2.Text = "Anno:";
            // 
            // numericUpDown2
            // 
            numericUpDown2.Location = new Point(138, 3);
            numericUpDown2.Maximum = new decimal(new int[] { 2050, 0, 0, 0 });
            numericUpDown2.Minimum = new decimal(new int[] { 2023, 0, 0, 0 });
            numericUpDown2.Name = "numericUpDown2";
            numericUpDown2.Size = new Size(120, 23);
            numericUpDown2.TabIndex = 1;
            numericUpDown2.Value = new decimal(new int[] { 2023, 0, 0, 0 });
            // 
            // TurnsGenerator
            // 
            TurnsGenerator.Location = new Point(264, 3);
            TurnsGenerator.Name = "TurnsGenerator";
            TurnsGenerator.Size = new Size(105, 23);
            TurnsGenerator.TabIndex = 4;
            TurnsGenerator.Text = "Genera Turni";
            TurnsGenerator.UseVisualStyleBackColor = true;
            TurnsGenerator.Click += this.TurnsGenerator_Click;
            // 
            // OccupancyCheckCheckBox
            // 
            OccupancyCheckCheckBox.AutoSize = true;
            OccupancyCheckCheckBox.Checked = true;
            OccupancyCheckCheckBox.CheckState = CheckState.Checked;
            OccupancyCheckCheckBox.Location = new Point(375, 3);
            OccupancyCheckCheckBox.Name = "OccupancyCheckCheckBox";
            OccupancyCheckCheckBox.Size = new Size(151, 19);
            OccupancyCheckCheckBox.TabIndex = 8;
            OccupancyCheckCheckBox.Text = "Considera Occupazione";
            OccupancyCheckCheckBox.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            flowLayoutPanel2.AutoSize = true;
            flowLayoutPanel2.Controls.Add(ExportButton);
            flowLayoutPanel2.Dock = DockStyle.Fill;
            flowLayoutPanel2.FlowDirection = FlowDirection.RightToLeft;
            flowLayoutPanel2.Location = new Point(925, 3);
            flowLayoutPanel2.Name = "flowLayoutPanel2";
            flowLayoutPanel2.Size = new Size(438, 35);
            flowLayoutPanel2.TabIndex = 11;
            // 
            // ExportButton
            // 
            ExportButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExportButton.Location = new Point(295, 3);
            ExportButton.Name = "ExportButton";
            ExportButton.Size = new Size(140, 23);
            ExportButton.TabIndex = 7;
            ExportButton.Text = "Esporta come xlsx";
            ExportButton.UseVisualStyleBackColor = true;
            ExportButton.Click += this.ExportButton_Click;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.AllowUserToOrderColumns = true;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.ColumnHeadersVisible = false;
            dataGridView2.Dock = DockStyle.Fill;
            dataGridView2.Location = new Point(3, 79);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.ReadOnly = true;
            dataGridView2.RowHeadersVisible = false;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new Size(1366, 588);
            dataGridView2.TabIndex = 6;
            // 
            // progressBarTurnGeneration
            // 
            progressBarTurnGeneration.Dock = DockStyle.Fill;
            progressBarTurnGeneration.Location = new Point(3, 3);
            progressBarTurnGeneration.Name = "progressBarTurnGeneration";
            progressBarTurnGeneration.Size = new Size(1366, 23);
            progressBarTurnGeneration.TabIndex = 5;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tableLayoutPanel3);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1378, 676);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Controllori in Centro";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tableLayoutPanel3
            // 
            tableLayoutPanel3.ColumnCount = 1;
            tableLayoutPanel3.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Controls.Add(comboBox1, 0, 0);
            tableLayoutPanel3.Controls.Add(dataGridView1, 0, 1);
            tableLayoutPanel3.Dock = DockStyle.Fill;
            tableLayoutPanel3.Location = new Point(3, 3);
            tableLayoutPanel3.Name = "tableLayoutPanel3";
            tableLayoutPanel3.RowCount = 2;
            tableLayoutPanel3.RowStyles.Add(new RowStyle());
            tableLayoutPanel3.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel3.Size = new Size(1372, 670);
            tableLayoutPanel3.TabIndex = 2;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(3, 3);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(234, 23);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += this.ComboBox1_SelectedIndexChanged;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = true;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(3, 32);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(1366, 635);
            dataGridView1.TabIndex = 0;
            // 
            // tabSelector
            // 
            tabSelector.Controls.Add(tabPage1);
            tabSelector.Controls.Add(tabPage2);
            tabSelector.Controls.Add(tabPage3);
            tabSelector.Dock = DockStyle.Fill;
            tabSelector.Location = new Point(0, 0);
            tabSelector.Name = "tabSelector";
            tabSelector.SelectedIndex = 0;
            tabSelector.Size = new Size(1386, 704);
            tabSelector.TabIndex = 0;
            // 
            // Manager
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = SystemColors.ControlLight;
            this.ClientSize = new Size(1386, 704);
            this.Controls.Add(tabSelector);
            this.Name = "Manager";
            this.Text = "Manager";
            tabPage3.ResumeLayout(false);
            tableLayoutPanel4.ResumeLayout(false);
            tableLayoutPanel4.PerformLayout();
            flowLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridViewHolidays).EndInit();
            flowLayoutPanel4.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDown1).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericUpDown2).EndInit();
            flowLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            tabPage1.ResumeLayout(false);
            tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabSelector.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private TabPage tabPage3;
        private TableLayoutPanel tableLayoutPanel4;
        private TextBox NameBox;
        private ComboBox ControllerManagerSelector;
        private Label label3;
        private FlowLayoutPanel flowLayoutPanel3;
        private Button AggiornaButton;
        private Button LicenziaButton;
        private Label Cognome;
        private Label Id;
        private TextBox SurnameBox;
        private TextBox IdBox;
        private Label Centro;
        private ComboBox CenterComboBox;
        private DataGridView dataGridViewHolidays;
        private Label label4;
        private TabPage tabPage2;
        private TableLayoutPanel tableLayoutPanel1;
        private TableLayoutPanel tableLayoutPanel2;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label label1;
        private NumericUpDown numericUpDown1;
        private Label label2;
        private NumericUpDown numericUpDown2;
        private Button TurnsGenerator;
        /// <summary>
        /// To check if you want that occupancy is checked during the shift allocation.
        /// </summary>
        public CheckBox OccupancyCheckCheckBox;
        private FlowLayoutPanel flowLayoutPanel2;
        private Button ExportButton;
        private DataGridView dataGridView2;
        private ProgressBar progressBarTurnGeneration;
        private TabPage tabPage1;
        private TableLayoutPanel tableLayoutPanel3;
        private ComboBox comboBox1;
        private DataGridView dataGridView1;
        private TabControl tabSelector;
        private FlowLayoutPanel flowLayoutPanel4;
        private Button RemoveHolidayButton;
        private Button AddHolidayButton;
        private DateTimePicker dateTimePickerBeginHoliday;
        private DateTimePicker dateTimePickerEndHoliday;
    }
}