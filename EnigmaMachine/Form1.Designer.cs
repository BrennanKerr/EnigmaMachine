namespace EnigmaMachine
{
    partial class Form1
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
            this.cbRotorOne = new System.Windows.Forms.ComboBox();
            this.cbRotorTwo = new System.Windows.Forms.ComboBox();
            this.cbRotorThree = new System.Windows.Forms.ComboBox();
            this.pnRotorNumbers = new System.Windows.Forms.Panel();
            this.cbReflector = new System.Windows.Forms.ComboBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.pnLamps = new System.Windows.Forms.Panel();
            this.pnKeys = new System.Windows.Forms.Panel();
            this.pnPlugboard = new System.Windows.Forms.Panel();
            this.pnRotorNumbers.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRotorOne
            // 
            this.cbRotorOne.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRotorOne.FormattingEnabled = true;
            this.cbRotorOne.Location = new System.Drawing.Point(33, 19);
            this.cbRotorOne.Name = "cbRotorOne";
            this.cbRotorOne.Size = new System.Drawing.Size(121, 21);
            this.cbRotorOne.TabIndex = 0;
            this.cbRotorOne.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // cbRotorTwo
            // 
            this.cbRotorTwo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRotorTwo.FormattingEnabled = true;
            this.cbRotorTwo.Location = new System.Drawing.Point(193, 19);
            this.cbRotorTwo.Name = "cbRotorTwo";
            this.cbRotorTwo.Size = new System.Drawing.Size(121, 21);
            this.cbRotorTwo.TabIndex = 1;
            this.cbRotorTwo.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // cbRotorThree
            // 
            this.cbRotorThree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRotorThree.FormattingEnabled = true;
            this.cbRotorThree.Location = new System.Drawing.Point(345, 19);
            this.cbRotorThree.Name = "cbRotorThree";
            this.cbRotorThree.Size = new System.Drawing.Size(121, 21);
            this.cbRotorThree.TabIndex = 2;
            this.cbRotorThree.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // pnRotorNumbers
            // 
            this.pnRotorNumbers.Controls.Add(this.cbRotorOne);
            this.pnRotorNumbers.Controls.Add(this.cbRotorTwo);
            this.pnRotorNumbers.Controls.Add(this.cbRotorThree);
            this.pnRotorNumbers.Location = new System.Drawing.Point(21, 12);
            this.pnRotorNumbers.Name = "pnRotorNumbers";
            this.pnRotorNumbers.Size = new System.Drawing.Size(486, 60);
            this.pnRotorNumbers.TabIndex = 3;
            // 
            // cbReflector
            // 
            this.cbReflector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReflector.FormattingEnabled = true;
            this.cbReflector.Location = new System.Drawing.Point(548, 33);
            this.cbReflector.Name = "cbReflector";
            this.cbReflector.Size = new System.Drawing.Size(121, 21);
            this.cbReflector.TabIndex = 4;
            this.cbReflector.SelectedIndexChanged += new System.EventHandler(this.ReflectorIndexChanged);
            // 
            // tbOutput
            // 
            this.tbOutput.Enabled = false;
            this.tbOutput.Location = new System.Drawing.Point(12, 104);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(776, 20);
            this.tbOutput.TabIndex = 6;
            // 
            // pnLamps
            // 
            this.pnLamps.Location = new System.Drawing.Point(12, 153);
            this.pnLamps.Name = "pnLamps";
            this.pnLamps.Size = new System.Drawing.Size(776, 100);
            this.pnLamps.TabIndex = 8;
            // 
            // pnKeys
            // 
            this.pnKeys.Location = new System.Drawing.Point(12, 259);
            this.pnKeys.Name = "pnKeys";
            this.pnKeys.Size = new System.Drawing.Size(776, 100);
            this.pnKeys.TabIndex = 9;
            // 
            // pnPlugboard
            // 
            this.pnPlugboard.Location = new System.Drawing.Point(12, 365);
            this.pnPlugboard.Name = "pnPlugboard";
            this.pnPlugboard.Size = new System.Drawing.Size(776, 144);
            this.pnPlugboard.TabIndex = 10;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 601);
            this.Controls.Add(this.pnPlugboard);
            this.Controls.Add(this.pnKeys);
            this.Controls.Add(this.pnLamps);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.cbReflector);
            this.Controls.Add(this.pnRotorNumbers);
            this.Name = "Form1";
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            this.pnRotorNumbers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRotorOne;
        private System.Windows.Forms.ComboBox cbRotorTwo;
        private System.Windows.Forms.ComboBox cbRotorThree;
        private System.Windows.Forms.Panel pnRotorNumbers;
        private System.Windows.Forms.TextBox tbOutput;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Panel pnLamps;
        private System.Windows.Forms.Panel pnKeys;
        private System.Windows.Forms.Panel pnPlugboard;
        private System.Windows.Forms.ComboBox cbReflector;
    }
}

