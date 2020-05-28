/**
 * File:        EnigmaMachineUI.cs
 * Description: Manages the Enigma Machine UI and Form
 * Author:      Brennan Kerr
 * Since:       27 May 2020
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace EnigmaMachine
{
    /// <summary>
    /// Manages the User Interface and events
    /// </summary>
    public partial class EnigmaMachineUI : Form
    {
        #region FIELDS
        Machine machine;            // the enigma machine
        bool keyPressed = false;    // determines if a key is currently pressed
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Initializes the form
        /// </summary>
        public EnigmaMachineUI()
        {
            InitializeComponent();
            this.KeyPreview = true; // makes the form the starting point for key presses

            // sets the Rotor defaults
            Rotors.SetRotorDefaults = FileManagement.GetInformationFromFile(FileNames.Rotors);
            // initializes three rotors
            Rotors[] rotors = new Rotors[3];

            // runs through each control in the rotor panel
            for (int i = 0; i < pnRotorNumbers.Controls.Count; i++)
            {
                // attempts to convert the control to a combobox
                ComboBox cb = pnRotorNumbers.Controls[i] as ComboBox;

                // if successful
                if (cb != null)
                {
                    // sets the rotor information
                    rotors[i] = new Rotors(i);
                    RotorLoad(cb, rotors[i]);
                    cb.SelectedIndex = i;
                }
            }

            // loads the reflector combo box
            ReflectorLoad();
            // initializes a new machine
            machine = new Machine(rotors, Reflector.GetReflectorString(cbReflector.SelectedIndex), "");

            // adds the labels to the three letter panels
            AddLabels(pnLamps);
            AddLabels(pnKeys);
            AddLabels(pnPlugboard);
        }
        #endregion

        #region LOAD_METHODS
        /// <summary>
        /// Sets the initial rotor settings
        /// </summary>
        /// <param name="cb">the combobox to set</param>
        /// <param name="rotor">The rotor to store the information in</param>
        public void RotorLoad(ComboBox cb, Rotors rotor)
        {
            // if the combobox is initialized
            if (cb != null)
            {
                // store the rotor number and sets the cipher number to the index
                int rNum = pnRotorNumbers.Controls.IndexOf(cb as Control);
                rotor.CipherNumber = rNum;

                // if the combobox has values in it, clear it
                if (cb.Items.Count > 0) cb.Items.Clear();

                // sets the combobox information based on the numbers from the rotors
                string[] rotorNums = Rotors.GetRotorNumbers().Split(' ');
                cb.Items.AddRange(rotorNums);
                // appends the keydown event
                cb.KeyDown += ComboBoxKeyDown;
            }
        }

        /// <summary>
        /// Loads the reflector information
        /// </summary>
        public void ReflectorLoad()
        {
            // sets the items to the reflectors in the file
            cbReflector.Items.AddRange(Reflector.GetReflectorList);
            // sets the starting index to 1
            cbReflector.SelectedIndex = 1;
        }
        #endregion

        #region CONTROL_EVENT_HANDLERS
        /// <summary>
        /// Sets the corresponding rotor cipher based on the index
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void RotorIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;   // converts the object to a combobox

            // if the conversion was successful and the machine is set
            if (cb != null && machine != null)
            {
                // set the cipher number to the rotor with the same index as the combobox
                int rNum = pnRotorNumbers.Controls.IndexOf(sender as Control);
                machine.GetRotor(rNum).CipherNumber = cb.SelectedIndex;
            }
        }

        /// <summary>
        /// Checks the index information for the reflector
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ReflectorIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;   // converts the object to a combobox

            // if the conversion was successful and the machine is not null
            if(cb != null && machine != null)
            {
                machine.Reflector = Reflector.GetReflectorString(cb.SelectedIndex);
            }
        }

        /// <summary>
        /// Manages the combobox keydown event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ComboBoxKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// Manages the letter click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LetterClick(object sender, EventArgs e)
        {
            EncryptNewValue((sender as Button).Text[0]);
        }
        #endregion

        #region FORM_EVENT_HANDLERS
        /// <summary>
        /// Manages the form key down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            // if the key was a letter, encrypt the letter
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z && !keyPressed)
            {
                EncryptNewValue(e.KeyCode.ToString()[0]);
                keyPressed = true;
            }
        }

        /// <summary>
        /// Manages the key up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            keyPressed = false;
            // reset all the back colours
            foreach (Label l in pnLamps.Controls)
            {
                l.BackColor = this.BackColor;
            }
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Adds label to a given panel
        /// </summary>
        /// <param name="pn"></param>
        private void AddLabels(Panel pn)
        {
            const int HEIGHT_DIV = 5;   // the hight divider
            const int WIDTH_DIV = 20;   // the width divider

            // the order of the letters
            string order = "QWERTZUIOASDFGHJKPYXCVBNML";
            // the number of letters per row
            int[] numberPerRow = { 9, 8, 9 };

            // determines the seperation width and height
            int h = pn.Height / HEIGHT_DIV;
            int w = pn.Width / WIDTH_DIV;

            int c = 0;  // counts the index for the letter

            // sets the label prefix
            string prefix = "lb" + pn.Name.Substring(2, pn.Name.Length - 2);

            // runs through each row
            for (int i = 0; i < numberPerRow.Length; i++)
            {
                // determines the starting width
                int startW = (pn.Width / 2) - ((numberPerRow[i] * WIDTH_DIV));

                // runs through each column in the row
                for (int j = 0; j < numberPerRow[i]; j++)
                {
                    // creates a new label
                    Label lb = new Label();

                    // sets the label parameters
                    lb.Location = new Point(startW + w * j, h * i);
                    lb.Width = w;
                    lb.Height = h;
                    lb.Text = order[c].ToString();
                    lb.Name = prefix + lb.Text;
                    lb.Click += LetterClick;

                    // if the label is the keys, bold the font
                    if (pn == pnKeys) lb.Font = new Font(this.Font, FontStyle.Bold);

                    // add the label to the desired panel
                    pn.Controls.Add(lb);

                    // increase the counter
                    c++;
                }
            }
        }

        /// <summary>
        /// Encrypts the letter
        /// </summary>
        /// <param name="c">the character to encrypt</param>
        public void EncryptNewValue(char c)
        {
            // sends the letter through the machine to encrypt
            char encrypted = machine.DetermineEncryptedLetter(c);
            // the string to append
            string encryptedString = encrypted.ToString();

            // determines if a space is needed
            CheckOutputMultiple(ref encryptedString);

            // if the output textbox is 0, clear the output file
            if (tbOutput.Text.Length <= 0)
                FileManagement.ClearOutput();

            // appends the string to the output textbox and output file
            tbOutput.Text += encryptedString;
            FileManagement.AppendToOutput(encryptedString);

            // lights up the lamp for the encrypted letter
            foreach (Label l in pnLamps.Controls)
            {
                if (l.Text == encrypted.ToString())
                {
                    l.BackColor = Color.Yellow;
                    break;
                }
            }
        }

        /// <summary>
        /// Determines if a space needs to be added
        /// </summary>
        /// <param name="str">the string to check</param>
        private void CheckOutputMultiple(ref string str)
        {
            string[] s = tbOutput.Text.Split(' ');
            if (s[s.Length - 1].Length == 4) str += " ";
        }

        /// <summary>
        /// Checks the letter to determine next letter
        /// </summary>
        /// <param name="letter">the letter to check</param>
        /// <param name="dir">the direction (+ number to increase, - number to decrease)</param>
        /// <returns></returns>
        private char CheckLetter(char letter, int dir = 1)
        {
            char l = letter;    // stores the entered letter

            // if the direction is up
            if (dir > 0)
            {
                if (l == 'Z')
                    l = 'A';
                else l++;
            }
            // if the direction is down
            else if (dir < 0)
            {
                if (l == 'A')
                    l = 'Z';
                else l--;
            }

            // return the letter
            return l;
        }

        #endregion

        

        private void btnRotorOneUp_Click(object sender, EventArgs e)
        {
            Rotors r = machine.GetRotor(0);
            machine.RotateRotor(r, 1);

            char l = lbRotorOneOffset.Text[0];

            l = CheckLetter(l, 1);
            lbRotorOneOffset.Text = l.ToString();

            btnRotorOneUp.Text = CheckLetter(btnRotorOneUp.Text[0], 1).ToString();
            btnRotorOneDown.Text = CheckLetter(btnRotorOneDown.Text[0], 1).ToString();
        }

        private void btnRotorOneDown_Click(object sender, EventArgs e)
        {

        }

    }
}
