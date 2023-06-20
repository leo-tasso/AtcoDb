namespace AtcoDbPopulator
{
    partial class TowerControl
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
            tableLayoutPanel1 = new TableLayoutPanel();
            flowLayoutPanel1 = new FlowLayoutPanel();
            labelAirport = new Label();
            comboBoxAirports = new ComboBox();
            labelId = new Label();
            comboBoxControllers = new ComboBox();
            buttonLogIn = new Button();
            buttonLogOut = new Button();
            DateTimeLabel = new Label();
            tableLayoutPanel2 = new TableLayoutPanel();
            dataGridViewArrivals = new DataGridView();
            dataGridViewDepartures = new DataGridView();
            labelArrivals = new Label();
            labelDepartures = new Label();
            buttonLanded = new Button();
            buttonTookOff = new Button();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrivals).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDepartures).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(tableLayoutPanel2, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(1682, 748);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(labelAirport);
            flowLayoutPanel1.Controls.Add(comboBoxAirports);
            flowLayoutPanel1.Controls.Add(labelId);
            flowLayoutPanel1.Controls.Add(comboBoxControllers);
            flowLayoutPanel1.Controls.Add(buttonLogIn);
            flowLayoutPanel1.Controls.Add(buttonLogOut);
            flowLayoutPanel1.Controls.Add(DateTimeLabel);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(1676, 37);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // labelAirport
            // 
            labelAirport.AutoSize = true;
            labelAirport.Location = new Point(3, 0);
            labelAirport.Name = "labelAirport";
            labelAirport.Size = new Size(47, 15);
            labelAirport.TabIndex = 0;
            labelAirport.Text = "Airport:";
            labelAirport.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // comboBoxAirports
            // 
            comboBoxAirports.FormattingEnabled = true;
            comboBoxAirports.Location = new Point(56, 3);
            comboBoxAirports.Name = "comboBoxAirports";
            comboBoxAirports.Size = new Size(316, 23);
            comboBoxAirports.TabIndex = 1;
            comboBoxAirports.SelectionChangeCommitted += this.ComboBoxAirports_SelectionChangeCommitted;
            // 
            // labelId
            // 
            labelId.AutoSize = true;
            labelId.Location = new Point(378, 0);
            labelId.Name = "labelId";
            labelId.Size = new Size(83, 15);
            labelId.TabIndex = 2;
            labelId.Text = "Id Controllore:";
            labelId.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // comboBoxControllers
            // 
            comboBoxControllers.FormattingEnabled = true;
            comboBoxControllers.Location = new Point(467, 3);
            comboBoxControllers.Name = "comboBoxControllers";
            comboBoxControllers.Size = new Size(207, 23);
            comboBoxControllers.TabIndex = 3;
            // 
            // buttonLogIn
            // 
            buttonLogIn.Location = new Point(680, 3);
            buttonLogIn.Name = "buttonLogIn";
            buttonLogIn.Size = new Size(75, 23);
            buttonLogIn.TabIndex = 4;
            buttonLogIn.Text = "LogIn";
            buttonLogIn.UseVisualStyleBackColor = true;
            buttonLogIn.Click += this.ButtonLogIn_Click;
            // 
            // buttonLogOut
            // 
            buttonLogOut.Location = new Point(761, 3);
            buttonLogOut.Name = "buttonLogOut";
            buttonLogOut.Size = new Size(75, 23);
            buttonLogOut.TabIndex = 5;
            buttonLogOut.Text = "LogOut";
            buttonLogOut.UseVisualStyleBackColor = true;
            buttonLogOut.Click += this.ButtonLogOut_Click;
            // 
            // DateTimeLabel
            // 
            DateTimeLabel.AutoSize = true;
            DateTimeLabel.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            DateTimeLabel.Location = new Point(842, 0);
            DateTimeLabel.Name = "DateTimeLabel";
            DateTimeLabel.Size = new Size(0, 37);
            DateTimeLabel.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            tableLayoutPanel2.ColumnCount = 2;
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
            tableLayoutPanel2.Controls.Add(dataGridViewArrivals, 0, 1);
            tableLayoutPanel2.Controls.Add(dataGridViewDepartures, 1, 1);
            tableLayoutPanel2.Controls.Add(labelArrivals, 0, 0);
            tableLayoutPanel2.Controls.Add(labelDepartures, 1, 0);
            tableLayoutPanel2.Controls.Add(buttonLanded, 0, 2);
            tableLayoutPanel2.Controls.Add(buttonTookOff, 1, 2);
            tableLayoutPanel2.Dock = DockStyle.Fill;
            tableLayoutPanel2.Location = new Point(3, 46);
            tableLayoutPanel2.Name = "tableLayoutPanel2";
            tableLayoutPanel2.RowCount = 3;
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel2.RowStyles.Add(new RowStyle());
            tableLayoutPanel2.Size = new Size(1676, 699);
            tableLayoutPanel2.TabIndex = 1;
            // 
            // dataGridViewArrivals
            // 
            dataGridViewArrivals.AllowUserToAddRows = false;
            dataGridViewArrivals.AllowUserToDeleteRows = false;
            dataGridViewArrivals.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewArrivals.Dock = DockStyle.Fill;
            dataGridViewArrivals.Location = new Point(3, 18);
            dataGridViewArrivals.Name = "dataGridViewArrivals";
            dataGridViewArrivals.RowTemplate.Height = 25;
            dataGridViewArrivals.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewArrivals.Size = new Size(832, 649);
            dataGridViewArrivals.TabIndex = 0;
            // 
            // dataGridViewDepartures
            // 
            dataGridViewDepartures.AllowUserToAddRows = false;
            dataGridViewDepartures.AllowUserToDeleteRows = false;
            dataGridViewDepartures.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewDepartures.Dock = DockStyle.Fill;
            dataGridViewDepartures.Location = new Point(841, 18);
            dataGridViewDepartures.Name = "dataGridViewDepartures";
            dataGridViewDepartures.RowTemplate.Height = 25;
            dataGridViewDepartures.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewDepartures.Size = new Size(832, 649);
            dataGridViewDepartures.TabIndex = 1;
            // 
            // labelArrivals
            // 
            labelArrivals.AutoSize = true;
            labelArrivals.Location = new Point(3, 0);
            labelArrivals.Name = "labelArrivals";
            labelArrivals.Size = new Size(38, 15);
            labelArrivals.TabIndex = 2;
            labelArrivals.Text = "Arrivi:";
            // 
            // labelDepartures
            // 
            labelDepartures.AutoSize = true;
            labelDepartures.Location = new Point(841, 0);
            labelDepartures.Name = "labelDepartures";
            labelDepartures.Size = new Size(55, 15);
            labelDepartures.TabIndex = 3;
            labelDepartures.Text = "Partenze:";
            // 
            // buttonLanded
            // 
            buttonLanded.Dock = DockStyle.Fill;
            buttonLanded.Location = new Point(3, 673);
            buttonLanded.Name = "buttonLanded";
            buttonLanded.Size = new Size(832, 23);
            buttonLanded.TabIndex = 4;
            buttonLanded.Text = "Segna come Atterrato";
            buttonLanded.UseVisualStyleBackColor = true;
            // 
            // buttonTookOff
            // 
            buttonTookOff.Dock = DockStyle.Fill;
            buttonTookOff.Location = new Point(841, 673);
            buttonTookOff.Name = "buttonTookOff";
            buttonTookOff.Size = new Size(832, 23);
            buttonTookOff.TabIndex = 5;
            buttonTookOff.Text = "Segna Come Decollato";
            buttonTookOff.UseVisualStyleBackColor = true;
            // 
            // TowerControl
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(1682, 748);
            this.Controls.Add(tableLayoutPanel1);
            this.Name = "TowerControl";
            this.Text = "TowerControl";
            this.FormClosing += this.TowerControl_FormClosing;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            tableLayoutPanel2.ResumeLayout(false);
            tableLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewArrivals).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewDepartures).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label labelAirport;
        private ComboBox comboBoxAirports;
        private Label labelId;
        private ComboBox comboBoxControllers;
        private Button buttonLogIn;
        private Button buttonLogOut;
        private TableLayoutPanel tableLayoutPanel2;
        private DataGridView dataGridViewArrivals;
        private DataGridView dataGridViewDepartures;
        private Label labelArrivals;
        private Label labelDepartures;
        private Label DateTimeLabel;
        private Button buttonLanded;
        private Button buttonTookOff;
    }
}