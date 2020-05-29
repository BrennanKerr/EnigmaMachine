﻿namespace EnigmaMachine
{
    partial class EnigmaMachineUI
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
            this.cbRotorOne.Location = new System.Drawing.Point(0, 0);
            this.cbRotorOne.Name = "cbRotorOne";
            this.cbRotorOne.Size = new System.Drawing.Size(191, 21);
            this.cbRotorOne.TabIndex = 0;
            this.cbRotorOne.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // cbRotorTwo
            // 
            this.cbRotorTwo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRotorTwo.FormattingEnabled = true;
            this.cbRotorTwo.Location = new System.Drawing.Point(197, 0);
            this.cbRotorTwo.Name = "cbRotorTwo";
            this.cbRotorTwo.Size = new System.Drawing.Size(191, 21);
            this.cbRotorTwo.TabIndex = 1;
            this.cbRotorTwo.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // cbRotorThree
            // 
            this.cbRotorThree.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRotorThree.FormattingEnabled = true;
            this.cbRotorThree.Location = new System.Drawing.Point(399, 0);
            this.cbRotorThree.Name = "cbRotorThree";
            this.cbRotorThree.Size = new System.Drawing.Size(193, 21);
            this.cbRotorThree.TabIndex = 2;
            this.cbRotorThree.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // pnRotorNumbers
            // 
            this.pnRotorNumbers.Controls.Add(this.cbRotorOne);
            this.pnRotorNumbers.Controls.Add(this.cbRotorTwo);
            this.pnRotorNumbers.Controls.Add(this.cbRotorThree);
            this.pnRotorNumbers.Location = new System.Drawing.Point(195, 31);
            this.pnRotorNumbers.Name = "pnRotorNumbers";
            this.pnRotorNumbers.Size = new System.Drawing.Size(593, 115);
            this.pnRotorNumbers.TabIndex = 3;
            // 
            // cbReflector
            // 
            this.cbReflector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbReflector.FormattingEnabled = true;
            this.cbReflector.Location = new System.Drawing.Point(12, 31);
            this.cbReflector.Name = "cbReflector";
            this.cbReflector.Size = new System.Drawing.Size(121, 21);
            this.cbReflector.TabIndex = 4;
            this.cbReflector.SelectedIndexChanged += new System.EventHandler(this.ReflectorIndexChanged);
            // 
            // tbOutput
            // 
            this.tbOutput.Enabled = false;
            this.tbOutput.Location = new System.Drawing.Point(12, 208);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(776, 20);
            this.tbOutput.TabIndex = 6;
            // 
            // pnLamps
            // 
            this.pnLamps.Location = new System.Drawing.Point(12, 234);
            this.pnLamps.Name = "pnLamps";
            this.pnLamps.Size = new System.Drawing.Size(776, 100);
            this.pnLamps.TabIndex = 8;
            // 
            // pnKeys
            // 
            this.pnKeys.Location = new System.Drawing.Point(12, 340);
            this.pnKeys.Name = "pnKeys";
            this.pnKeys.Size = new System.Drawing.Size(776, 100);
            this.pnKeys.TabIndex = 9;
            // 
            // pnPlugboard
            // 
            this.pnPlugboard.Location = new System.Drawing.Point(12, 446);
            this.pnPlugboard.Name = "pnPlugboard";
            this.pnPlugboard.Size = new System.Drawing.Size(776, 144);
            this.pnPlugboard.TabIndex = 10;
            // 
            // EnigmaMachineUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 678);
            this.Controls.Add(this.pnPlugboard);
            this.Controls.Add(this.pnKeys);
            this.Controls.Add(this.pnLamps);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.cbReflector);
            this.Controls.Add(this.pnRotorNumbers);
            this.Name = "EnigmaMachineUI";
            this.Text = "Enigma Machine";
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

