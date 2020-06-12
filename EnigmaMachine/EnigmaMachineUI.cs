/**
 * File:        EnigmaMachineUI.cs
 * Description: Manages the Enigma Machine UI and Form
 * Author:      Brennan Kerr
 * Since:       27 May 2020
 */
using System;
using System.Collections.Generic;
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

        Button[] btnUp = new Button[3];     // stores the buttons that rotate the rotor up
        Button[] btnDown = new Button[3];   // stores the buttons that rotate the rotor down
        Label[] lbCurrent = new Label[3];   // stores the labels that state the current start

        // the order of the letters
        string order = "QWERTZUIOASDFGHJKPYXCVBNML";
        //string order = "QWERTYUIOPASDFGHJKLZXCVBNM";


        bool isClicked = false;
        Label last;

        private Color[] colours =
        {
            Color.Firebrick,
            Color.Red,
            Color.Blue,
            Color.Yellow,
            Color.Green,
            Color.Orange,
            Color.Purple,
            Color.Pink,
            Color.Aqua,
            Color.Brown
        };

        private bool[] colourUsed;
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

            int num = pnRotorNumbers.Controls.Count;
            for (int i = 0; i < num; i++)
                OffsetLoad(i);

            colourUsed = new bool[colours.Length];

            btnClearPlugboard.Location = new Point(this.Width / 2 - btnClearPlugboard.Width / 2, 595);
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

        /// <summary>
        /// Loads the settings for the offset
        /// </summary>
        /// <param name="n"></param>
        public void OffsetLoad(int n)
        {
            int h = pnRotorNumbers.Controls[n].Height;
            int w = pnRotorNumbers.Controls[n].Width;
            int x = pnRotorNumbers.Controls[n].Location.X + w / 2;
            int y = h * 2;

            CreateOffsetButton(x, y, "Z", n);
            y += btnUp[n].Height;
            CreateOffsetLabel(x, y, "A", n);
            y += lbCurrent[n].Height;
            CreateOffsetButton(x, y, "B", n, false);
            
        }

        /// <summary>
        /// Creates the offset labels 
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <param name="t">the text</param>
        /// <param name="n">the rotor number</param>
        private void CreateOffsetLabel(int x, int y, string t, int n)
        {
            // creates a new label and point based on the information provided
            Label lb = new Label();
            lb.Width = btnUp[n].Width;
            lb.TextAlign = ContentAlignment.MiddleCenter;
            x -= lb.Width / 2;
            Point p = new Point(x, y);
            lb.Location = p;
            lb.Text = t;
            lb.Name = "lbRotor" + n + "Offset";

            lb.BorderStyle = BorderStyle.None;
            lb.BackColor = Color.White;

            // adds the label to the rotor panel
            pnRotorNumbers.Controls.Add(lb);
            // adds the label to the array
            lbCurrent[n] = lb;
        }

        /// <summary>
        /// Creates the offset buttons
        /// </summary>
        /// <param name="x">the x coordinate</param>
        /// <param name="y">the y coordinate</param>
        /// <param name="t">the button text</param>
        /// <param name="n">the rotor number</param>
        /// <param name="isUp">if true, the button is for rotating the rotor up a letter</param>
        private void CreateOffsetButton(int x, int y, string t, int n, bool isUp = true)
        {
            // creates a new button
            Button btn = new Button();
            // centres the x coordinate
            x -= btn.Width / 2;

            // sets a new point
            Point p = new Point(x, y);
            
            // sets the button parameters
            btn.Location = p;
            btn.Text = t;
            btn.Name = "btnRotor" + n.ToString() + (isUp ? "Up" : "Down");

            btn.BackColor = Color.LightGray;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;

            // if the button is to rotate the rotor up
            if (isUp)
            {
                btn.Click += RotateUp;
                btnUp[n] = btn;

                pnRotorNumbers.Controls.Add(btnUp[n]);
            }
            // otherwise, its for rotation down
            else
            {
                btn.Click += RotateDown;
                btnDown[n] = btn;

                pnRotorNumbers.Controls.Add(btnDown[n]);
            }
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
                int previous = machine.GetRotor(rNum).CipherNumber;

                machine.GetRotor(rNum).CipherNumber = cb.SelectedIndex;

                // changes the controls back to the default
                btnUp[rNum].Text = "Z";
                lbCurrent[rNum].Text = "A";
                btnDown[rNum].Text = "B";

                ChangeSelectedIndexes(cb, previous);
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

        /// <summary>
        /// Manages the rotate up buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateUp(object sender, EventArgs e)
        {
            int n = 0;

            Button btn = sender as Button;

            if (btn != null)
            {
                foreach (Button b in btnUp)
                {
                    if (btn == b) break;
                    n++;
                }

                RotateCypher(n, 1);
            }
        }

        /// <summary>
        /// Manages the rotate down buttons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RotateDown(object sender, EventArgs e)
        {
            int n = 0;

            Button btn = sender as Button;

            if (btn != null)
            {
                foreach (Button b in btnDown)
                {
                    if (btn == b) break;
                    n++;
                }

                RotateCypher(n, -1);
            }
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
                //int startW = (pn.Width / 2) - ((numberPerRow[i] * WIDTH_DIV));

                int startW = pn.Width / 2 - w / 2;
                startW -= (w * (numberPerRow[i] / 2) -1);
                if (numberPerRow[i] % 2 == 0) startW += w / 2;

                int startH = h * ((HEIGHT_DIV - numberPerRow.GetLength(0)) / 2);

                // runs through each column in the row
                for (int j = 0; j < numberPerRow[i]; j++)
                {
                    // creates a new label
                    Label lb = new Label();

                    // sets the label parameters
                    lb.Location = new Point(startW + w * j, startH + h * i);
                    lb.TextAlign = ContentAlignment.MiddleCenter;
                    lb.Width = w;
                    lb.Height = h;
                    lb.Text = order[c].ToString();
                    lb.Name = prefix + lb.Text;
                    lb.Width = w;

                    // if the label is the keys, bold the font
                    if (pn == pnKeys) lb.Font = new Font(this.Font, FontStyle.Bold);
                    else if (pn == pnPlugboard) SetPlugboardSettings(ref lb);

                    // add the label to the desired panel
                    pn.Controls.Add(lb);

                    // increase the counter
                    c++;
                }
            }
        }

        public void SetPlugboardSettings(ref Label lb)
        {
            lb.Click += PlugboardClicked;
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

            RotateControls(2);
            // rotates the center rotor if required
            if (machine.CheckIfRotate(2))
            {
                RotateControls(1);
                // rotates the left rotor if required
                if (machine.CheckIfRotate(1))
                {
                    RotateControls(0);
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

        /// <summary>
        /// Rotates the cipher
        /// </summary>
        /// <param name="n">the rotor number</param>
        /// <param name="dir">the direction of rotation</param>
        private void RotateCypher(int n, int dir = 1)
        {
            // if the direction is positive, set it to 1
            if (dir > 0) dir = 1;
            // if the direction is negative, set it to -1
            else if (dir < 0) dir = -1;

            // gets the rotor with the desired number
            Rotors r = machine.GetRotor(n);
            // rotates the rotor
            machine.RotateRotor(r, dir);

            // rotate the rotor controls
            RotateControls(n, dir);
        }

        /// <summary>
        /// Rotates the rotor controls
        /// </summary>
        /// <param name="n">the rotor number</param>
        /// <param name="dir">the direction to spin it</param>
        private void RotateControls(int n, int dir = 1)
        {
            // gets the letter of the current rotation
            char l = lbCurrent[n].Text[0];
            // sets the label to the next 
            l = CheckLetter(l, dir);
            lbCurrent[n].Text = l.ToString();

            // sets the buttons to the appropriate letter          
            btnUp[n].Text = CheckLetter(btnUp[n].Text[0], dir).ToString();
            btnDown[n].Text = CheckLetter(btnDown[n].Text[0], dir).ToString();
        }

        /// <summary>
        /// Checks if another rotor has the index of the newly selected one
        /// </summary>
        /// <param name="cb">the current combobox</param>
        /// <param name="previous">the previous index</param>
        private void ChangeSelectedIndexes(ComboBox cb, int previous)
        {
            // runs through each control in the rotor numbers panel
            for (int i = 0; i < pnRotorNumbers.Controls.Count/*cbs.Count*/; i++)
            {
                // attempts to convert the control to a combobox
                ComboBox c = pnRotorNumbers.Controls[i] as ComboBox;

                // if the conversion was successfull
                if (c != null)
                {
                    // if the control is not the same combobox
                    if (c != cb)
                    {
                        // if the indexes are the same, change the new control to the previous index
                        if (c.SelectedIndex == cb.SelectedIndex)
                        {
                            c.SelectedIndex = previous;
                        }
                    }
                }
            }
        }
        #endregion

        /// <summary>
        /// Draws the rotor outlines
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pnRotorNumbers_Paint(object sender, PaintEventArgs e)
        {
            // creates a graphics object for the rotor panel
            Graphics g = pnRotorNumbers.CreateGraphics();

            // run once for each rotor
            for (int i = 0; i < 3; i++)
            {
                // creates a new rectangle
                Rectangle rec = new Rectangle();
                // sets the attributes based on the other controls
                rec.Location = new Point(btnUp[i].Location.X - 1, btnUp[i].Location.Y - 1);
                rec.Width = btnUp[i].Width + 1;
                rec.Height = btnDown[i].Bottom - btnUp[i].Top + 1;
                
                // draws the rectangle
                g.DrawRectangle(new Pen(Color.Black, 1), rec);
            }
        }

        /// <summary>
        /// Manages if a plugboard letter is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PlugboardClicked(object sender, EventArgs e)
        {
            Label lb = sender as Label; // converts the object to a label

            // if it was a label
            if (lb != null)
            {
                char letter = lb.Text[0];   // gets the letter associated with the label

                // checks the plugboard for the letter
                CheckPlugboard(letter);

                // if this is the first click
                if (!isClicked)
                {
                    last = lb;  // stores the current label as the previous one
                }
                // if this is the second click
                else
                {
                    // if the letter is the same, store the last as null
                    if (letter == last.Text[0])
                        last = null;
                    else
                    {
                        // saves the letters to the plugboard
                        machine.AddToPlugboard(last.Text + letter.ToString());

                        // sets the background colours of the corresponding labels
                        Color bg = NextColour();
                        lb.BackColor = bg;
                        last.BackColor = bg;

                        btnClearPlugboard.Visible = true;
                    }

                }

                isClicked = !isClicked; // clicked is opposite
            }
        }


        /// <summary>
        /// Checks the plugboard settings
        /// </summary>
        /// <param name="letter">the current letter</param>
        private void CheckPlugboard(char letter)
        {
            string pb = machine.GetPlugboard();
            // if the letter already exists
            if (pb.Contains(letter.ToString()))
            {
                int index = pb.IndexOf(letter);  // gets the index of the letter

                // removes the background colour off the corresponding label
                RemoveColour(pnPlugboard.Controls[order.IndexOf(letter)]);

                // if the letter comes first in the sequence
                    // remove the colour off the next letter
                if (index % 2 == 0)
                {
                    RemoveColour(pnPlugboard.Controls[order.IndexOf(pb[index + 1])]);
                }
                // otherwise
                    // remove the colour off the previous letter and subtrack 1 from the index
                else
                {
                    RemoveColour(pnPlugboard.Controls[order.IndexOf(pb[index - 1])]);
                    index -= 1;
                }

                // that colour is no longer in use
                colourUsed[index / 2] = false;

                // removes the letters from the plugboard
                machine.RemoveFromPlugboard(index, 2);

                if (machine.GetPlugboard().Length < 2)
                    btnClearPlugboard.PerformClick();
            }
        }

        /// <summary>
        /// Determine the next available colour
        /// </summary>
        /// <returns>the first colour in the array that is not being used</returns>
        private Color NextColour()
        {
            Color colour = Color.White; // sets the default colour to white
            bool found = false;         // determines if the colour was found

            // goes through each colour in the list
            for (int i = 0; i < colours.Length && !found; i++)
            {
                // if the colour isnt being used
                if (!colourUsed[i])
                {
                    // use the colour
                    colour = colours[i];
                    colourUsed[i] = true;
                    found = true;
                }
            }

            return colour;  // return the colour
        }

        /// <summary>
        /// Sets the back colour to the colour of the form
        /// </summary>
        /// <param name="c">the control to change the colour off</param>
        private void RemoveColour(Control c) { c.BackColor = this.BackColor; }

        private void ClearPlugboardSettings(object sender, EventArgs e)
        {
            machine.ValidatePlugBoard("");

            foreach (Label lb in pnPlugboard.Controls)
            {
                isClicked = false;
                RemoveColour(lb);
            }

            btnClearPlugboard.Visible = false;
        }
    }
}
