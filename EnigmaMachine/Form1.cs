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

        public Form1()
        {
            InitializeComponent();

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

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string input = tbInput.Text;
            string output = "";

            for (int i = 0; i < input.Length; i++)
                output += machine.DetermineEncryptedLetter(input[i]);

            tbOutput.Text = output;
        }
    }
}
