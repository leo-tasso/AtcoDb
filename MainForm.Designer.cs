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
            button1 = new Button();
            button2 = new Button();
            wipeButton = new Button();
            TrafficCounter = new NumericUpDown();
            TrafficPopulatorbutton = new Button();
            RandomstateButton = new Button();
            ((System.ComponentModel.ISupportInitialize)ControllerNum).BeginInit();
            ((System.ComponentModel.ISupportInitialize)TrafficCounter).BeginInit();
            SuspendLayout();
            // 
            // PopulateControllers
            // 
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
            CentersPopulate.Location = new Point(12, 48);
            CentersPopulate.Name = "CentersPopulate";
            CentersPopulate.Size = new Size(151, 23);
            CentersPopulate.TabIndex = 2;
            CentersPopulate.Text = "Populate Centers";
            CentersPopulate.UseVisualStyleBackColor = true;
            CentersPopulate.Click += CentersPopulateClick;
            // 
            // button1
            // 
            button1.Location = new Point(398, 401);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 4;
            button1.Text = "Play";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(317, 401);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 5;
            button2.Text = "Pause";
            button2.UseVisualStyleBackColor = true;
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
            RandomstateButton.Location = new Point(12, 142);
            RandomstateButton.Name = "RandomstateButton";
            RandomstateButton.Size = new Size(151, 23);
            RandomstateButton.TabIndex = 9;
            RandomstateButton.Text = "Random State Traffic";
            RandomstateButton.UseVisualStyleBackColor = true;
            RandomstateButton.Click += RandomstateButton_Click;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(RandomstateButton);
            Controls.Add(TrafficCounter);
            Controls.Add(TrafficPopulatorbutton);
            Controls.Add(wipeButton);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(CentersPopulate);
            Controls.Add(ControllerNum);
            Controls.Add(PopulateControllers);
            Name = "MainForm";
            Text = "Populate controllers";
            ((System.ComponentModel.ISupportInitialize)ControllerNum).EndInit();
            ((System.ComponentModel.ISupportInitialize)TrafficCounter).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button PopulateControllers;
        private NumericUpDown ControllerNum;
        private Button CentersPopulate;
        private Button button1;
        private Button button2;
        private Button wipeButton;
        private NumericUpDown TrafficCounter;
        private Button TrafficPopulatorbutton;
        private Button RandomstateButton;
    }
}