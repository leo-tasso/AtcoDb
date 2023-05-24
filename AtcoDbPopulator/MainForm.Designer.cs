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
            ((System.ComponentModel.ISupportInitialize)controllerNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)trafficNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)hourPicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)minutePicker).BeginInit();
            ((System.ComponentModel.ISupportInitialize)speedBar).BeginInit();
            SuspendLayout();
            // 
            // populateControllersButton
            // 
            populateControllersButton.Enabled = false;
            populateControllersButton.Location = new Point(12, 84);
            populateControllersButton.Name = "populateControllersButton";
            populateControllersButton.Size = new Size(151, 23);
            populateControllersButton.TabIndex = 0;
            populateControllersButton.Text = "Populate Controllers";
            populateControllersButton.UseVisualStyleBackColor = true;
            populateControllersButton.Click += PopulateControllersButtonClick;
            // 
            // controllerNum
            // 
            controllerNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            controllerNum.Location = new Point(186, 84);
            controllerNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            controllerNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            controllerNum.Name = "controllerNum";
            controllerNum.Size = new Size(151, 23);
            controllerNum.TabIndex = 1;
            controllerNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // centersPopulateButton
            // 
            centersPopulateButton.Enabled = false;
            centersPopulateButton.Location = new Point(12, 48);
            centersPopulateButton.Name = "centersPopulateButton";
            centersPopulateButton.Size = new Size(151, 23);
            centersPopulateButton.TabIndex = 2;
            centersPopulateButton.Text = "Populate Centers";
            centersPopulateButton.UseVisualStyleBackColor = true;
            centersPopulateButton.Click += CentersPopulateButtonClick;
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
            playButton.Click += PlayButton_Click;
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
            // trafficNum
            // 
            trafficNum.Increment = new decimal(new int[] { 25, 0, 0, 0 });
            trafficNum.Location = new Point(186, 113);
            trafficNum.Maximum = new decimal(new int[] { 1000, 0, 0, 0 });
            trafficNum.Minimum = new decimal(new int[] { 10, 0, 0, 0 });
            trafficNum.Name = "trafficNum";
            trafficNum.Size = new Size(151, 23);
            trafficNum.TabIndex = 8;
            trafficNum.Value = new decimal(new int[] { 10, 0, 0, 0 });
            // 
            // trafficPopulatorButton
            // 
            trafficPopulatorButton.Enabled = false;
            trafficPopulatorButton.Location = new Point(12, 113);
            trafficPopulatorButton.Name = "trafficPopulatorButton";
            trafficPopulatorButton.Size = new Size(151, 23);
            trafficPopulatorButton.TabIndex = 7;
            trafficPopulatorButton.Text = "Populate Traffic";
            trafficPopulatorButton.UseVisualStyleBackColor = true;
            trafficPopulatorButton.Click += TrafficPopulatorButtonClick;
            // 
            // randomstateButton
            // 
            randomstateButton.Enabled = false;
            randomstateButton.Location = new Point(12, 142);
            randomstateButton.Name = "randomstateButton";
            randomstateButton.Size = new Size(151, 23);
            randomstateButton.TabIndex = 9;
            randomstateButton.Text = "Random State Traffic";
            randomstateButton.UseVisualStyleBackColor = true;
            randomstateButton.Click += RandomStateButton_Click;
            // 
            // dateTimePicker
            // 
            dateTimePicker.Location = new Point(186, 142);
            dateTimePicker.MaxDate = new DateTime(2023, 12, 31, 0, 0, 0, 0);
            dateTimePicker.MinDate = new DateTime(2023, 1, 1, 0, 0, 0, 0);
            dateTimePicker.Name = "dateTimePicker";
            dateTimePicker.Size = new Size(200, 23);
            dateTimePicker.TabIndex = 10;
            // 
            // hourPicker
            // 
            hourPicker.Location = new Point(398, 142);
            hourPicker.Maximum = new decimal(new int[] { 23, 0, 0, 0 });
            hourPicker.Name = "hourPicker";
            hourPicker.Size = new Size(35, 23);
            hourPicker.TabIndex = 11;
            hourPicker.Value = new decimal(new int[] { 12, 0, 0, 0 });
            // 
            // minutePicker
            // 
            minutePicker.Location = new Point(439, 142);
            minutePicker.Maximum = new decimal(new int[] { 59, 0, 0, 0 });
            minutePicker.Name = "minutePicker";
            minutePicker.Size = new Size(35, 23);
            minutePicker.TabIndex = 12;
            // 
            // speedBar
            // 
            speedBar.Enabled = false;
            speedBar.Location = new Point(185, 171);
            speedBar.Maximum = 86400;
            speedBar.Minimum = 1;
            speedBar.Name = "speedBar";
            speedBar.Size = new Size(289, 45);
            speedBar.TabIndex = 13;
            speedBar.Value = 1;
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
            Controls.Add(speedBar);
            Controls.Add(minutePicker);
            Controls.Add(hourPicker);
            Controls.Add(dateTimePicker);
            Controls.Add(randomstateButton);
            Controls.Add(trafficNum);
            Controls.Add(trafficPopulatorButton);
            Controls.Add(wipeButton);
            Controls.Add(pauseButton);
            Controls.Add(playButton);
            Controls.Add(centersPopulateButton);
            Controls.Add(controllerNum);
            Controls.Add(populateControllersButton);
            Name = "MainForm";
            Text = "Populate controllers";
            ((System.ComponentModel.ISupportInitialize)controllerNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)trafficNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)hourPicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)minutePicker).EndInit();
            ((System.ComponentModel.ISupportInitialize)speedBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
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
    }
}