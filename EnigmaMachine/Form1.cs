using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnigmaMachine
{
    public partial class Form1 : Form
    {
        Machine machine;
        bool keyPressed = false;

        public Form1()
        {
            InitializeComponent();
            this.KeyPreview = true;

            Rotors.SetRotorDefaults = FileManagement.GetInformationFromFile(FileNames.Rotors);
            Rotors[] rotors = new Rotors[3];

            for (int i = 0; i < pnRotorNumbers.Controls.Count; i++)
            {
                ComboBox cb = pnRotorNumbers.Controls[i] as ComboBox;

                if (cb != null)
                {
                    rotors[i] = new Rotors(i);
                    RotorLoad(cb, rotors[i]);
                    cb.SelectedIndex = i;
                }
            }

            ReflectorLoad();
            machine = new Machine(rotors, Reflector.GetReflectorString(cbReflector.SelectedIndex), "");

            AddLabels(pnLamps);
            AddLabels(pnKeys);
            AddLabels(pnPlugboard);
        }

        /// <summary>
        /// Sets the initial rotor settings
        /// </summary>
        /// <param name="cb"></param>
        public void RotorLoad(ComboBox cb, Rotors rotor)
        {
            if (cb != null)
            {
                int rNum = pnRotorNumbers.Controls.IndexOf(cb as Control);
                rotor.CipherNumber = rNum;

                if (cb.Items.Count > 0) cb.Items.Clear();

                string[] rotorNums = Rotors.GetRotorNumbers().Split(' ');
                cb.Items.AddRange(rotorNums);
                cb.KeyDown += ComboBoxKeyDown;
            }
        }

        public void ReflectorLoad()
        {
            cbReflector.Items.AddRange(Reflector.GetReflectorList);
            cbReflector.SelectedIndex = 1;
        }

        /// <summary>
        /// Sets the corresponding rotor cipher based on the index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RotorIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if (cb != null && machine != null)
            {
                int rNum = pnRotorNumbers.Controls.IndexOf(sender as Control);
                machine.GetRotor(rNum).CipherNumber = cb.SelectedIndex;

                MessageBox.Show(machine.GetRotor(rNum).GetCipher());
            }
        }

        public void ReflectorIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            if(cb != null && machine != null)
            {
                machine.Reflector = Reflector.GetReflectorString(cb.SelectedIndex);
                //MessageBox.Show(machine.Reflector);
            }
        }

        public void ComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        private void LetterClick(object sender, EventArgs e)
        {
            EncryptNewValue((sender as Button).Text[0]);
        }

        private void AddLabels(Panel pn)
        {
            const int HEIGHT_DIV = 5;
            const int WIDTH_DIV = 20;

            string order = "QWERTZUIOASDFGHJKPYXCVBNML";
            int[] numberPerRow = { 9, 8, 9 };

            int h = pn.Height / HEIGHT_DIV;
            int w = pn.Width / WIDTH_DIV;

            int c = 0;

            string prefix = "lb" + pn.Name.Substring(2, pn.Name.Length - 2);
            for (int i = 0; i < numberPerRow.Length; i++)
            {
                int startW = (pn.Width / 2) - ((numberPerRow[i] * WIDTH_DIV));

                for (int j = 0; j < numberPerRow[i]; j++)
                {
                    Label lb = new Label();

                    lb.Location = new Point(startW + w * j, h * i);
                    lb.Width = w;
                    lb.Height = h;
                    lb.Text = order[c].ToString();
                    lb.Name = prefix + lb.Text;

                    if (pn == pnKeys) lb.Font = new Font(this.Font, FontStyle.Bold);
                    c++;
                    lb.Click += LetterClick;
                    pn.Controls.Add(lb);
                }
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z && !keyPressed)
            {
                EncryptNewValue(e.KeyCode.ToString()[0]);
                keyPressed = true;
            }
        }

        public void EncryptNewValue(char c)
        {
            char encrypted = machine.DetermineEncryptedLetter(c);
            string encryptedString = encrypted.ToString();

            CheckOutputMultiple(ref encryptedString);

            if (tbOutput.Text.Length <= 0)
                FileManagement.ClearOutput();

            tbOutput.Text += encryptedString;
            FileManagement.AppendToOutput(encryptedString);

            foreach (Label l in pnLamps.Controls)
            {
                if (l.Text == encrypted.ToString())
                {
                    l.BackColor = Color.Yellow;
                }
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyPressed = false;
            foreach (Label l in pnLamps.Controls)
            {
                l.BackColor = this.BackColor;
            }
        }

        private void CheckOutputMultiple(ref string str)
        {
            string[] s = tbOutput.Text.Split(' ');
            if (s[s.Length - 1].Length == 4) str += " ";
        }
    }
}
