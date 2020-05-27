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
            this.tbInput = new System.Windows.Forms.TextBox();
            this.tbOutput = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.pnRotorNumbers.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbRotorOne
            // 
            this.cbRotorOne.FormattingEnabled = true;
            this.cbRotorOne.Location = new System.Drawing.Point(33, 19);
            this.cbRotorOne.Name = "cbRotorOne";
            this.cbRotorOne.Size = new System.Drawing.Size(121, 21);
            this.cbRotorOne.TabIndex = 0;
            this.cbRotorOne.SelectedIndexChanged += new System.EventHandler(this.RotorIndexChanged);
            // 
            // cbRotorTwo
            // 
            this.cbRotorTwo.FormattingEnabled = true;
            this.cbRotorTwo.Location = new System.Drawing.Point(193, 19);
            this.cbRotorTwo.Name = "cbRotorTwo";
            this.cbRotorTwo.Size = new System.Drawing.Size(121, 21);
            this.cbRotorTwo.TabIndex = 1;
            // 
            // cbRotorThree
            // 
            this.cbRotorThree.FormattingEnabled = true;
            this.cbRotorThree.Location = new System.Drawing.Point(345, 19);
            this.cbRotorThree.Name = "cbRotorThree";
            this.cbRotorThree.Size = new System.Drawing.Size(121, 21);
            this.cbRotorThree.TabIndex = 2;
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
            this.cbReflector.FormattingEnabled = true;
            this.cbReflector.Location = new System.Drawing.Point(548, 33);
            this.cbReflector.Name = "cbReflector";
            this.cbReflector.Size = new System.Drawing.Size(121, 21);
            this.cbReflector.TabIndex = 4;
            this.cbReflector.SelectedIndexChanged += new System.EventHandler(this.ReflectorIndexChanged);
            // 
            // tbInput
            // 
            this.tbInput.Location = new System.Drawing.Point(152, 150);
            this.tbInput.Name = "tbInput";
            this.tbInput.Size = new System.Drawing.Size(235, 20);
            this.tbInput.TabIndex = 5;
            // 
            // tbOutput
            // 
            this.tbOutput.Enabled = false;
            this.tbOutput.Location = new System.Drawing.Point(152, 200);
            this.tbOutput.Name = "tbOutput";
            this.tbOutput.Size = new System.Drawing.Size(235, 20);
            this.tbOutput.TabIndex = 6;
            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(450, 181);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.TabIndex = 7;
            this.btnSubmit.Text = "button1";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.tbOutput);
            this.Controls.Add(this.tbInput);
            this.Controls.Add(this.cbReflector);
            this.Controls.Add(this.pnRotorNumbers);
            this.Name = "Form1";
            this.Text = "Form1";
            this.pnRotorNumbers.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbRotorOne;
        private System.Windows.Forms.ComboBox cbRotorTwo;
        private System.Windows.Forms.ComboBox cbRotorThree;
        private System.Windows.Forms.Panel pnRotorNumbers;
        private System.Windows.Forms.ComboBox cbReflector;
        private System.Windows.Forms.TextBox tbInput;
        private System.Windows.Forms.TextBox tbOutput;
        private System.Windows.Forms.Button btnSubmit;
    }
}

