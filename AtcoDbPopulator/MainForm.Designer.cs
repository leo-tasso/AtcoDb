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
            populateControllersButton = new Button();
            controllerNum = new NumericUpDown();
            centersPopulateButton = new Button();
            playButton = new Button();
            pauseButton = new Button();
            wipeButton = new Button();
            trafficNum = new NumericUpDown();
            trafficPopulatorButton = new Button();
            randomstateButton = new Button();
            dateTimePicker = new DateTimePicker();
            hourPicker = new NumericUpDown();
            minutePicker = new NumericUpDown();
            speedBar = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            progressBar1 = new ProgressBar();
            launchManagerButton = new Button();
            hourLabel = new Label();
            minuteLabel = new Label();
            LaunchTwr = new Button();
            ((System.ComponentModel.ISupportInitialize)controllerNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trafficNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hourPicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minutePicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speedBar).BeginInit();
            this.SuspendLayout();
            // 
            // populateControllersButton
            // 
            populateControllersButton.Enabled = false;
            populateControllersButton.Location = new Point(11, 49);
            populateControllersButton.Name = "populateControllersButton";
            populateControllersButton.Size = new Size(151, 23);
            populateControllersButton.TabIndex = 0;
            populateControllersButton.Text = "Populate Controllers";
            populateControllersButton.UseVisualStyleBackColor = true;
            populateControllersButton.Click += this.PopulateControllersButtonClick;
            // 
            // controllerNum
            // 
            controllerNum.Enabled = false;
            controllerNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            controllerNum.Location = new Point(185, 49);
            controllerNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            controllerNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            controllerNum.Name = "controllerNum";
            controllerNum.Size = new Size(49, 23);
            controllerNum.TabIndex = 1;
            controllerNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // centersPopulateButton
            // 
            centersPopulateButton.Enabled = false;
            centersPopulateButton.Location = new Point(268, 12);
            centersPopulateButton.Name = "centersPopulateButton";
            centersPopulateButton.Size = new Size(241, 23);
            centersPopulateButton.TabIndex = 2;
            centersPopulateButton.Text = "Populate Centers";
            centersPopulateButton.UseVisualStyleBackColor = true;
            centersPopulateButton.Click += this.CentersPopulateButtonClick;
            // 
            // playButton
            // 
            playButton.Enabled = false;
            playButton.Location = new Point(96, 109);
            playButton.Name = "playButton";
            playButton.Size = new Size(66, 23);
            playButton.TabIndex = 4;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += this.PlayButton_Click;
            // 
            // pauseButton
            // 
            pauseButton.Enabled = false;
            pauseButton.Location = new Point(11, 109);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(59, 23);
            pauseButton.TabIndex = 5;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            pauseButton.Click += this.PauseButtonClick;
            // 
            // wipeButton
            // 
            wipeButton.Location = new Point(12, 12);
            wipeButton.Name = "wipeButton";
            wipeButton.Size = new Size(244, 23);
            wipeButton.TabIndex = 6;
            wipeButton.Text = "Wipe DB";
            wipeButton.UseVisualStyleBackColor = true;
            wipeButton.Click += this.WipeButtonClick;
            // 
            // trafficNum
            // 
            trafficNum.Enabled = false;
            trafficNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            trafficNum.Location = new Point(425, 49);
            trafficNum.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            trafficNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            trafficNum.Name = "trafficNum";
            trafficNum.Size = new Size(83, 23);
            trafficNum.TabIndex = 8;
            trafficNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // trafficPopulatorButton
            // 
            trafficPopulatorButton.Enabled = false;
            trafficPopulatorButton.Location = new Point(268, 49);
            trafficPopulatorButton.Name = "trafficPopulatorButton";
            trafficPopulatorButton.Size = new Size(151, 23);
            trafficPopulatorButton.TabIndex = 7;
            trafficPopulatorButton.Text = "Populate Traffic";
            trafficPopulatorButton.UseVisualStyleBackColor = true;
            trafficPopulatorButton.Click += this.TrafficPopulatorButtonClick;
            // 
            // randomstateButton
            // 
            randomstateButton.Enabled = false;
            randomstateButton.Location = new Point(11, 80);
            randomstateButton.Name = "randomstateButton";
            randomstateButton.Size = new Size(151, 23);
            randomstateButton.TabIndex = 9;
            randomstateButton.Text = "Random State Traffic";
            randomstateButton.UseVisualStyleBackColor = true;
            randomstateButton.Click += this.RandomStateButton_Click;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Enabled = false;
            dateTimePicker.Location = new Point(185, 80);
            dateTimePicker.MaxDate = new DateTime(2023, 12, 31, 0, 0, 0, 0);
            dateTimePicker.MinDate = new DateTime(2023, 1, 1, 0, 0, 0, 0);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 10;
            // 
            // hourPicker
            // 
            hourPicker.Location = new Point(411, 80);
            hourPicker.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            hourPicker.Name = "hourPicker";
            hourPicker.Size = new Size(35, 23);
            hourPicker.TabIndex = 11;
            hourPicker.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // minutePicker
            // 
            minutePicker.Location = new Point(473, 82);
            minutePicker.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            minutePicker.Name = "minutePicker";
            minutePicker.Size = new Size(35, 23);
            minutePicker.TabIndex = 12;
            // 
            // speedBar
            // 
            speedBar.Enabled = false;
            speedBar.Location = new Point(184, 109);
            speedBar.Maximum = 86400;
            speedBar.Minimum = 1;
            speedBar.Name = "speedBar";
            speedBar.Size = new Size(324, 45);
            speedBar.TabIndex = 13;
            speedBar.Value = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(185, 139);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 14;
            label1.Text = "x1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(453, 139);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 15;
            label2.Text = "x86'400";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(330, 139);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 16;
            label3.Text = "x43'200";
            // 
            // progressBar1
            // 
            progressBar1.Location = new Point(11, 160);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(497, 23);
            progressBar1.TabIndex = 17;
            // 
            // launchManagerButton
            // 
            launchManagerButton.Location = new Point(11, 200);
            launchManagerButton.Name = "launchManagerButton";
            launchManagerButton.Size = new Size(106, 23);
            launchManagerButton.TabIndex = 18;
            launchManagerButton.Text = "Launch Manager";
            launchManagerButton.UseVisualStyleBackColor = true;
            launchManagerButton.Click += this.LaunchManagerButton_Click;
            // 
            // hourLabel
            // 
            hourLabel.AutoSize = true;
            hourLabel.Location = new Point(391, 82);
            hourLabel.Name = "hourLabel";
            hourLabel.Size = new Size(14, 15);
            hourLabel.TabIndex = 19;
            hourLabel.Text = "h";
            // 
            // minuteLabel
            // 
            minuteLabel.AutoSize = true;
            minuteLabel.Location = new Point(453, 84);
            minuteLabel.Name = "minuteLabel";
            minuteLabel.Size = new Size(18, 15);
            minuteLabel.TabIndex = 20;
            minuteLabel.Text = "m";
            // 
            // LaunchTwr
            // 
            LaunchTwr.Location = new Point(129, 200);
            LaunchTwr.Name = "LaunchTwr";
            LaunchTwr.Size = new Size(75, 23);
            LaunchTwr.TabIndex = 21;
            LaunchTwr.Text = "Launch Twr";
            LaunchTwr.UseVisualStyleBackColor = true;
            LaunchTwr.Click += this.LaunchTwr_Click;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new SizeF(7F, 15F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.ClientSize = new Size(521, 237);
            this.Controls.Add(LaunchTwr);
            this.Controls.Add(minuteLabel);
            this.Controls.Add(hourLabel);
            this.Controls.Add(launchManagerButton);
            this.Controls.Add(progressBar1);
            this.Controls.Add(label3);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(speedBar);
            this.Controls.Add(minutePicker);
            this.Controls.Add(hourPicker);
            this.Controls.Add(dateTimePicker);
            this.Controls.Add(randomstateButton);
            this.Controls.Add(trafficNum);
            this.Controls.Add(trafficPopulatorButton);
            this.Controls.Add(wipeButton);
            this.Controls.Add(pauseButton);
            this.Controls.Add(playButton);
            this.Controls.Add(centersPopulateButton);
            this.Controls.Add(controllerNum);
            this.Controls.Add(populateControllersButton);
            this.Name = "MainForm";
            this.Text = "Populate controllers";
            ((System.ComponentModel.ISupportInitialize)controllerNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)trafficNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)hourPicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)minutePicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)speedBar).EndInit();
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
        private Button LaunchTwr;
    }
}