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
            PopulateControllers = new Button();
            ControllerNum = new NumericUpDown();
            CentersPopulate = new Button();
            playButton = new Button();
            pauseButton = new Button();
            wipeButton = new Button();
            TrafficCounter = new NumericUpDown();
            TrafficPopulatorbutton = new Button();
            RandomstateButton = new Button();
            dateTimePicker1 = new DateTimePicker();
            HourPicker = new NumericUpDown();
            MinutePicker = new NumericUpDown();
            SpeedBar = new TrackBar();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            ((System.ComponentModel.ISupportInitialize)ControllerNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrafficCounter).BeginInit();
            ((System.ComponentModel.ISupportInitialize)HourPicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MinutePicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)SpeedBar).BeginInit();
            SuspendLayout();
            // 
            // PopulateControllers
            // 
            PopulateControllers.Enabled = false;
            PopulateControllers.Location = new Point(12, 84);
            PopulateControllers.Name = "PopulateControllers";
            PopulateControllers.Size = new Size(151, 23);
            PopulateControllers.TabIndex = 0;
            PopulateControllers.Text = "Populate Controllers";
            PopulateControllers.UseVisualStyleBackColor = true;
            PopulateControllers.Click += PopulateControllersClick;
            // 
            // ControllerNum
            // 
            ControllerNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            ControllerNum.Location = new Point(186, 84);
            ControllerNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            ControllerNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            ControllerNum.Name = "ControllerNum";
            ControllerNum.Size = new Size(151, 23);
            ControllerNum.TabIndex = 1;
            ControllerNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // CentersPopulate
            // 
            CentersPopulate.Enabled = false;
            CentersPopulate.Location = new Point(12, 48);
            CentersPopulate.Name = "CentersPopulate";
            CentersPopulate.Size = new Size(151, 23);
            CentersPopulate.TabIndex = 2;
            CentersPopulate.Text = "Populate Centers";
            CentersPopulate.UseVisualStyleBackColor = true;
            CentersPopulate.Click += CentersPopulateClick;
            // 
            // playButton
            // 
            playButton.Enabled = false;
            playButton.Location = new Point(97, 171);
            playButton.Name = "playButton";
            playButton.Size = new Size(66, 23);
            playButton.TabIndex = 4;
            playButton.Text = "Play";
            playButton.UseVisualStyleBackColor = true;
            playButton.Click += playButton_Click;
            // 
            // pauseButton
            // 
            pauseButton.Enabled = false;
            pauseButton.Location = new Point(12, 171);
            pauseButton.Name = "pauseButton";
            pauseButton.Size = new Size(59, 23);
            pauseButton.TabIndex = 5;
            pauseButton.Text = "Pause";
            pauseButton.UseVisualStyleBackColor = true;
            pauseButton.Click += PauseButtonClick;
            // 
            // wipeButton
            // 
            wipeButton.Location = new Point(12, 12);
            wipeButton.Name = "wipeButton";
            wipeButton.Size = new Size(151, 23);
            wipeButton.TabIndex = 6;
            wipeButton.Text = "Wipe DB";
            wipeButton.UseVisualStyleBackColor = true;
            wipeButton.Click += WipeButtonClick;
            // 
            // TrafficCounter
            // 
            TrafficCounter.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            TrafficCounter.Location = new Point(186, 113);
            TrafficCounter.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            TrafficCounter.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            TrafficCounter.Name = "TrafficCounter";
            TrafficCounter.Size = new Size(151, 23);
            TrafficCounter.TabIndex = 8;
            TrafficCounter.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // TrafficPopulatorbutton
            // 
            TrafficPopulatorbutton.Enabled = false;
            TrafficPopulatorbutton.Location = new Point(12, 113);
            TrafficPopulatorbutton.Name = "TrafficPopulatorbutton";
            TrafficPopulatorbutton.Size = new Size(151, 23);
            TrafficPopulatorbutton.TabIndex = 7;
            TrafficPopulatorbutton.Text = "Populate Traffic";
            TrafficPopulatorbutton.UseVisualStyleBackColor = true;
            TrafficPopulatorbutton.Click += TrafficPopulatorbuttonClick;
            // 
            // RandomstateButton
            // 
            RandomstateButton.Enabled = false;
            RandomstateButton.Location = new Point(12, 142);
            RandomstateButton.Name = "RandomstateButton";
            RandomstateButton.Size = new Size(151, 23);
            RandomstateButton.TabIndex = 9;
            RandomstateButton.Text = "Random State Traffic";
            RandomstateButton.UseVisualStyleBackColor = true;
            RandomstateButton.Click += RandomstateButton_Click;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.Location = new Point(186, 142);
            dateTimePicker1.MaxDate = new DateTime(2023, 12, 31, 0, 0, 0, 0);
            dateTimePicker1.MinDate = new DateTime(2023, 1, 1, 0, 0, 0, 0);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(200, 23);
            dateTimePicker1.TabIndex = 10;
            // 
            // HourPicker
            // 
            HourPicker.Location = new Point(398, 142);
            HourPicker.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            HourPicker.Name = "HourPicker";
            HourPicker.Size = new Size(35, 23);
            HourPicker.TabIndex = 11;
            HourPicker.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // MinutePicker
            // 
            MinutePicker.Location = new Point(439, 142);
            MinutePicker.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            MinutePicker.Name = "MinutePicker";
            MinutePicker.Size = new Size(35, 23);
            MinutePicker.TabIndex = 12;
            // 
            // SpeedBar
            // 
            SpeedBar.Enabled = false;
            SpeedBar.Location = new Point(185, 171);
            SpeedBar.Maximum = 86400;
            SpeedBar.Name = "SpeedBar";
            SpeedBar.Size = new Size(289, 45);
            SpeedBar.TabIndex = 13;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(186, 201);
            label1.Name = "label1";
            label1.Size = new Size(19, 15);
            label1.TabIndex = 14;
            label1.Text = "x1";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(437, 201);
            label2.Name = "label2";
            label2.Size = new Size(46, 15);
            label2.TabIndex = 15;
            label2.Text = "x86'400";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(311, 201);
            label3.Name = "label3";
            label3.Size = new Size(46, 15);
            label3.TabIndex = 16;
            label3.Text = "x43'200";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(SpeedBar);
            Controls.Add(MinutePicker);
            Controls.Add(HourPicker);
            Controls.Add(dateTimePicker1);
            Controls.Add(RandomstateButton);
            Controls.Add(TrafficCounter);
            Controls.Add(TrafficPopulatorbutton);
            Controls.Add(wipeButton);
            Controls.Add(pauseButton);
            Controls.Add(playButton);
            Controls.Add(CentersPopulate);
            Controls.Add(ControllerNum);
            Controls.Add(PopulateControllers);
            Name = "MainForm";
            Text = "Populate controllers";
            ((System.ComponentModel.ISupportInitialize)ControllerNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrafficCounter).EndInit();
            ((System.ComponentModel.ISupportInitialize)HourPicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)MinutePicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)SpeedBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button PopulateControllers;
        private NumericUpDown ControllerNum;
        private Button CentersPopulate;
        public Button playButton;
        private Button pauseButton;
        private Button wipeButton;
        private NumericUpDown TrafficCounter;
        private Button TrafficPopulatorbutton;
        private Button RandomstateButton;
        public DateTimePicker dateTimePicker1;
        public NumericUpDown HourPicker;
        public NumericUpDown MinutePicker;
        private TrackBar SpeedBar;
        private Label label1;
        private Label label2;
        private Label label3;
    }
}