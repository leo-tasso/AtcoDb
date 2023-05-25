namespace AtcoDbPopulator
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.populateControllersButton = new Button();
            this.controllerNum = new NumericUpDown();
            this.centersPopulateButton = new Button();
            this.playButton = new Button();
            this.pauseButton = new Button();
            this.wipeButton = new Button();
            this.trafficNum = new NumericUpDown();
            this.trafficPopulatorButton = new Button();
            this.randomstateButton = new Button();
            this.dateTimePicker = new DateTimePicker();
            this.hourPicker = new NumericUpDown();
            this.minutePicker = new NumericUpDown();
            this.speedBar = new TrackBar();
            this.label1 = new Label();
            this.label2 = new Label();
            this.label3 = new Label();
            this.progressBar1 = new ProgressBar();
            this.launchManagerButton = new Button();
            this.hourLabel = new Label();
            this.minuteLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)this.controllerNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.trafficNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.hourPicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.minutePicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)this.speedBar).BeginInit();
            this.SuspendLayout();
            // 
            // populateControllersButton
            // 
            this.populateControllersButton.Enabled = false;
            this.populateControllersButton.Location = new Point(11, 49);
            this.populateControllersButton.Name = "populateControllersButton";
            this.populateControllersButton.Size = new Size(151, 23);
            this.populateControllersButton.TabIndex = 0;
            this.populateControllersButton.Text = "Populate Controllers";
            this.populateControllersButton.UseVisualStyleBackColor = true;
            this.populateControllersButton.Click += this.PopulateControllersButtonClick;
            // 
            // controllerNum
            // 
            this.controllerNum.Enabled = false;
            this.controllerNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            this.controllerNum.Location = new Point(185, 49);
            this.controllerNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.controllerNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.controllerNum.Name = "controllerNum";
            this.controllerNum.Size = new Size(49, 23);
            this.controllerNum.TabIndex = 1;
            this.controllerNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // centersPopulateButton
            // 
            this.centersPopulateButton.Enabled = false;
            this.centersPopulateButton.Location = new Point(268, 12);
            this.centersPopulateButton.Name = "centersPopulateButton";
            this.centersPopulateButton.Size = new Size(241, 23);
            this.centersPopulateButton.TabIndex = 2;
            this.centersPopulateButton.Text = "Populate Centers";
            this.centersPopulateButton.UseVisualStyleBackColor = true;
            this.centersPopulateButton.Click += this.CentersPopulateButtonClick;
            // 
            // playButton
            // 
            this.playButton.Enabled = false;
            this.playButton.Location = new Point(96, 109);
            this.playButton.Name = "playButton";
            this.playButton.Size = new Size(66, 23);
            this.playButton.TabIndex = 4;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += this.PlayButton_Click;
            // 
            // pauseButton
            // 
            this.pauseButton.Enabled = false;
            this.pauseButton.Location = new Point(11, 109);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new Size(59, 23);
            this.pauseButton.TabIndex = 5;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += this.PauseButtonClick;
            // 
            // wipeButton
            // 
            this.wipeButton.Location = new Point(12, 12);
            this.wipeButton.Name = "wipeButton";
            this.wipeButton.Size = new Size(244, 23);
            this.wipeButton.TabIndex = 6;
            this.wipeButton.Text = "Wipe DB";
            this.wipeButton.UseVisualStyleBackColor = true;
            this.wipeButton.Click += this.WipeButtonClick;
            // 
            // trafficNum
            // 
            this.trafficNum.Enabled = false;
            this.trafficNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            this.trafficNum.Location = new Point(425, 49);
            this.trafficNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            this.trafficNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            this.trafficNum.Name = "trafficNum";
            this.trafficNum.Size = new Size(83, 23);
            this.trafficNum.TabIndex = 8;
            this.trafficNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // trafficPopulatorButton
            // 
            this.trafficPopulatorButton.Enabled = false;
            this.trafficPopulatorButton.Location = new Point(268, 49);
            this.trafficPopulatorButton.Name = "trafficPopulatorButton";
            this.trafficPopulatorButton.Size = new Size(151, 23);
            this.trafficPopulatorButton.TabIndex = 7;
            this.trafficPopulatorButton.Text = "Populate Traffic";
            this.trafficPopulatorButton.UseVisualStyleBackColor = true;
            this.trafficPopulatorButton.Click += this.TrafficPopulatorButtonClick;
            // 
            // randomstateButton
            // 
            this.randomstateButton.Enabled = false;
            this.randomstateButton.Location = new Point(11, 80);
            this.randomstateButton.Name = "randomstateButton";
            this.randomstateButton.Size = new Size(151, 23);
            this.randomstateButton.TabIndex = 9;
            this.randomstateButton.Text = "Random State Traffic";
            this.randomstateButton.UseVisualStyleBackColor = true;
            this.randomstateButton.Click += this.RandomStateButton_Click;
            // 
            // dateTimePicker
            // 
            this.dateTimePicker.Enabled = false;
            this.dateTimePicker.Location = new Point(185, 80);
            this.dateTimePicker.MaxDate = new DateTime(2023, 12, 31, 0, 0, 0, 0);
            this.dateTimePicker.MinDate = new DateTime(2023, 1, 1, 0, 0, 0, 0);
            this.dateTimePicker.Name = "dateTimePicker";
            this.dateTimePicker.Size = new Size(200, 23);
            this.dateTimePicker.TabIndex = 10;
            // 
            // hourPicker
            // 
            this.hourPicker.Location = new Point(411, 80);
            this.hourPicker.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            this.hourPicker.Name = "hourPicker";
            this.hourPicker.Size = new Size(35, 23);
            this.hourPicker.TabIndex = 11;
            this.hourPicker.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // minutePicker
            // 
            this.minutePicker.Location = new Point(473, 82);
            this.minutePicker.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            this.minutePicker.Name = "minutePicker";
            this.minutePicker.Size = new Size(35, 23);
            this.minutePicker.TabIndex = 12;
            // 
            // speedBar
            // 
            this.speedBar.Enabled = false;
            this.speedBar.Location = new Point(184, 109);
            this.speedBar.Maximum = 86400;
            this.speedBar.Minimum = 1;
            this.speedBar.Name = "speedBar";
            this.speedBar.Size = new Size(324, 45);
            this.speedBar.TabIndex = 13;
            this.speedBar.Value = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new Point(185, 139);
            this.label1.Name = "label1";
            this.label1.Size = new Size(19, 15);
            this.label1.TabIndex = 14;
            this.label1.Text = "x1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new Point(453, 139);
            this.label2.Name = "label2";
            this.label2.Size = new Size(46, 15);
            this.label2.TabIndex = 15;
            this.label2.Text = "x86'400";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new Point(330, 139);
            this.label3.Name = "label3";
            this.label3.Size = new Size(46, 15);
            this.label3.TabIndex = 16;
            this.label3.Text = "x43'200";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new Point(11, 160);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new Size(497, 23);
            this.progressBar1.TabIndex = 17;
            // 
            // launchManagerButton
            // 
            this.launchManagerButton.Location = new Point(11, 200);
            this.launchManagerButton.Name = "launchManagerButton";
            this.launchManagerButton.Size = new Size(106, 23);
            this.launchManagerButton.TabIndex = 18;
            this.launchManagerButton.Text = "Launch Manager";
            this.launchManagerButton.UseVisualStyleBackColor = true;
            this.launchManagerButton.Click += this.LaunchManagerButton_Click;
            // 
            // hourLabel
            // 
            this.hourLabel.AutoSize = true;
            this.hourLabel.Location = new Point(391, 82);
            this.hourLabel.Name = "hourLabel";
            this.hourLabel.Size = new Size(14, 15);
            this.hourLabel.TabIndex = 19;
            this.hourLabel.Text = "h";
            // 
            // minuteLabel
            // 
            this.minuteLabel.AutoSize = true;
            this.minuteLabel.Location = new Point(453, 84);
            this.minuteLabel.Name = "minuteLabel";
            this.minuteLabel.Size = new Size(18, 15);
            this.minuteLabel.TabIndex = 20;
            this.minuteLabel.Text = "m";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ClientSize = new Size(521, 237);
            this.Controls.Add(this.minuteLabel);
            this.Controls.Add(this.hourLabel);
            this.Controls.Add(this.launchManagerButton);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.speedBar);
            this.Controls.Add(this.minutePicker);
            this.Controls.Add(this.hourPicker);
            this.Controls.Add(this.dateTimePicker);
            this.Controls.Add(this.randomstateButton);
            this.Controls.Add(this.trafficNum);
            this.Controls.Add(this.trafficPopulatorButton);
            this.Controls.Add(this.wipeButton);
            this.Controls.Add(this.pauseButton);
            this.Controls.Add(this.playButton);
            this.Controls.Add(this.centersPopulateButton);
            this.Controls.Add(this.controllerNum);
            this.Controls.Add(this.populateControllersButton);
            this.Name = "MainForm";
            this.Text = "Populate controllers";
            ((System.ComponentModel.ISupportInitialize)this.controllerNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.trafficNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.hourPicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.minutePicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)this.speedBar).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private Button populateControllersButton;
        private NumericUpDown controllerNum;
        private Button centersPopulateButton;
        /// <summary>
        /// Button to play the traffic situation.
        /// </summary>
        public Button playButton;
        /// <summary>
        /// Button to Pause the traffic situation.
        /// </summary>
        private Button pauseButton;
        private Button wipeButton;
        private NumericUpDown trafficNum;
        private Button trafficPopulatorButton;
        private Button randomstateButton;
        /// <summary>
        /// Start date setter.
        /// </summary>
        public DateTimePicker dateTimePicker;
        /// <summary>
        /// Start hour setter.
        /// </summary>
        public NumericUpDown hourPicker;
        /// <summary>
        /// Start minute setter.
        /// </summary>
        public NumericUpDown minutePicker;
        private TrackBar speedBar;
        private Label label1;
        private Label label2;
        private Label label3;
        private ProgressBar progressBar1;
        private Button launchManagerButton;
        private Label hourLabel;
        private Label minuteLabel;
    }
}