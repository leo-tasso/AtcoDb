namespace AtcoDbPopulator
{
    partial class Supervisor
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
            CenterLabel = new Label();
            comboBoxCenter = new ComboBox();
            buttonApply = new Button();
            labelActualTime = new Label();
            flowLayoutPanelGraphs = new FlowLayoutPanel();
            tableLayoutPanel1.SuspendLayout();
            flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            tableLayoutPanel1.AutoSize = true;
            tableLayoutPanel1.ColumnCount = 1;
            tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Controls.Add(flowLayoutPanel1, 0, 0);
            tableLayoutPanel1.Controls.Add(flowLayoutPanelGraphs, 0, 1);
            tableLayoutPanel1.Dock = DockStyle.Fill;
            tableLayoutPanel1.Location = new Point(0, 0);
            tableLayoutPanel1.Name = "tableLayoutPanel1";
            tableLayoutPanel1.RowCount = 2;
            tableLayoutPanel1.RowStyles.Add(new RowStyle());
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tableLayoutPanel1.Size = new Size(794, 473);
            tableLayoutPanel1.TabIndex = 0;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.AutoSize = true;
            flowLayoutPanel1.Controls.Add(CenterLabel);
            flowLayoutPanel1.Controls.Add(comboBoxCenter);
            flowLayoutPanel1.Controls.Add(buttonApply);
            flowLayoutPanel1.Controls.Add(labelActualTime);
            flowLayoutPanel1.Dock = DockStyle.Fill;
            flowLayoutPanel1.Location = new Point(3, 3);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new Size(788, 37);
            flowLayoutPanel1.TabIndex = 0;
            // 
            // CenterLabel
            // 
            CenterLabel.AutoSize = true;
            CenterLabel.Location = new Point(3, 0);
            CenterLabel.Name = "CenterLabel";
            CenterLabel.Size = new Size(46, 15);
            CenterLabel.TabIndex = 0;
            CenterLabel.Text = "Centro:";
            // 
            // comboBoxCenter
            // 
            comboBoxCenter.FormattingEnabled = true;
            comboBoxCenter.Location = new Point(55, 3);
            comboBoxCenter.Name = "comboBoxCenter";
            comboBoxCenter.Size = new Size(234, 23);
            comboBoxCenter.TabIndex = 1;
            comboBoxCenter.SelectedIndexChanged += this.ComboBoxCenter_SelectedIndexChanged;
            // 
            // buttonApply
            // 
            buttonApply.Location = new Point(295, 3);
            buttonApply.Name = "buttonApply";
            buttonApply.Size = new Size(75, 23);
            buttonApply.TabIndex = 2;
            buttonApply.Text = "Applica";
            buttonApply.UseVisualStyleBackColor = true;
            // 
            // labelActualTime
            // 
            labelActualTime.AutoSize = true;
            labelActualTime.Font = new Font("Segoe UI", 20F, FontStyle.Regular, GraphicsUnit.Point);
            labelActualTime.Location = new Point(376, 0);
            labelActualTime.Name = "labelActualTime";
            labelActualTime.Size = new Size(0, 37);
            labelActualTime.TabIndex = 3;
            // 
            // flowLayoutPanelGraphs
            // 
            flowLayoutPanelGraphs.Dock = DockStyle.Fill;
            flowLayoutPanelGraphs.Location = new Point(3, 46);
            flowLayoutPanelGraphs.Name = "flowLayoutPanelGraphs";
            flowLayoutPanelGraphs.Size = new Size(788, 424);
            flowLayoutPanelGraphs.TabIndex = 1;
            // 
            // Supervisor
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.ClientSize = new Size(794, 473);
            this.Controls.Add(tableLayoutPanel1);
            this.Name = "Supervisor";
            this.Text = "Supervisor";
            this.FormClosing += this.Supervisor_FormClosing;
            tableLayoutPanel1.ResumeLayout(false);
            tableLayoutPanel1.PerformLayout();
            flowLayoutPanel1.ResumeLayout(false);
            flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private TableLayoutPanel tableLayoutPanel1;
        private FlowLayoutPanel flowLayoutPanel1;
        private Label CenterLabel;
        private ComboBox comboBoxCenter;
        private Button buttonApply;
        private FlowLayoutPanel flowLayoutPanelGraphs;
        private Label labelActualTime;
    }
}