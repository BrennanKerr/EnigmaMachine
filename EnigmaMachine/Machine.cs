/**
 * File:        Machine.cs
 * Description: Creates a class that defines a given Enigma Machine's settings
 * Author:      Brennan Kerr
 * Since:       27 May 2020
 */

namespace EnigmaMachine
{
    /// <summary>
    /// Defines information about an Enigma Machine
    /// </summary>
    class Machine
    {
        #region FIELDS
        /// <summary>
        /// Defines the max number of combinations that can be made for the plugboard
        /// </summary>
        private static readonly int MAX_NUMBER_OF_PB_COMBINATIONS = 13;
        /// <summary>
        /// Defines the maximum number of rotors
        /// </summary>
        private static readonly int NUMBER_OF_ROTORS = 3;

        /// <summary>
        /// Stores the rotor objects
        /// </summary>
        private Rotors[] rotors = new Rotors[NUMBER_OF_ROTORS];
        /// <summary>
        /// Stores the reflector string
        /// </summary>
        private string reflector;
        /// <summary>
        /// Stores the plugboard string
        /// </summary>
        private string plugboard;
        #endregion

        /// <summary>
        /// Initializes the machine
        /// </summary>
        /// <param name="rotors">the rotor array</param>
        /// <param name="reflector">the reflector string</param>
        /// <param name="plugboard">the plugboard string</param>
        public Machine(Rotors[] rotors, string reflector, string plugboard)
        {
            this.rotors = rotors;
            this.reflector = reflector;
            ValidatePlugBoard(plugboard);
        }

        #region ROTOR_METHODS
        /// <summary>
        /// Returns the Rotor corresponding to the index
        /// </summary>
        /// <param name="index">the rotor number</param>
        /// <returns></returns>
        public Rotors GetRotor(int index) { return rotors[index]; }
        /// <summary>
        /// Sets the rotor with the passed parameter
        /// </summary>
        /// <param name="index">the rotor number</param>
        /// <param name="rotor">the rotor settings</param>
        public void SetRotor(int index, Rotors rotor) { rotors[index] = rotor; }


        /// <summary>
        /// Determines if the next rotor needs to be rotated
        /// </summary>
        /// <param name="num">the rotor index</param>
        /// <returns></returns>
        public bool CheckIfRotate(int num)
        {
            return rotors[num].GetLastAlphabetLetter() == Rotors.GetRotorAdvancements(rotors[num].CipherNumber);
        }

        /// <summary>
        /// Rotates the rotor to the appropriate slot
        /// </summary>
        /// <param name="r"></param>
        /// <param name="offset"></param>
        public void RotateRotor(Rotors r, int offset)
        {
            int lastIndex = r.GetCipher().Length - 1;
            r.Rotate(offset < 0 || offset > lastIndex ? lastIndex : offset);
        }
        #endregion

        #region REFLECTOR_METHODS
        /// <summary>
        /// Gets or sets the reflector
        /// </summary>
        public string Reflector
        {
            get { return reflector; }
            set
            {
                this.reflector = value;
            }
        }
        #endregion

        #region PLUGBOARD_METHODS
        /// <summary>
        /// Validates the plugboard settings
        /// </summary>
        /// <param name="s">the possible plugboard string</param>
        /// <returns>true if valid, false if not</returns>
        public bool ValidatePlugBoard(string s)
        {
            bool valid = true;  // the status of the plug board

            // if the length is not even
            if (s.Length % 2 != 0) valid = false;
            // if there are too many combinations
            else if (s.Length / 2 > MAX_NUMBER_OF_PB_COMBINATIONS) valid = false;

            // run through the list of combinations
            for (int i = 0; i < s.Length && valid; i += 2)
            {
                // if two letters side by side are the same
                if (s[i] == s[i + 1])
                    valid = false;
                // if there are other letters available
                else if (s.Length > 2 && i != s.Length - 3)
                {
                    // stores the rest of the string
                    string afterI = s.Substring(i + 2, s.Length - 2 - i);

                    // if either letter reappears in the string, it is not valid
                    if (afterI.Contains(s[i].ToString()) || afterI.Contains(s[i + 1].ToString()))
                        valid = false;
                }
            }

            // if the plugboard settings are valid, set the board
            if (valid)
                SetPlugBoard(s);
            // otherwise, set it to an empty string
            else SetPlugBoard("");

            return valid;
        }

        /// <summary>
        /// Sets the plugboard to the provided string
        /// </summary>
        /// <param name="s">the string to set for the plugboard</param>
        private void SetPlugBoard(string s) { plugboard = s; }

        /// <summary>
        /// Changes the letter if the letter is utalized in the plugboard
        /// </summary>
        /// <param name="l">the letter to check and replace if needed</param>
        private void CheckPlugboard(ref char l)
        {
            // if the plugboard has values
            if (plugboard != "")
            {
                bool found = false; // determines if a letter was found

                // run though each letter in the plugboard
                for (int i = 0; i < plugboard.Length && !found; i += 2)
                {
                    // if the letter exists in the board, switch it with the corresponding
                    // letter
                    if (plugboard[i] == l)
                    {
                        l = plugboard[i + 1];
                        found = true;
                    }
                    else if (plugboard[i + 1] == l)
                    {
                        l = plugboard[i];
                        found = true;
                    }
                }
            }
        }
        #endregion

        #region ENCRYPTION_METHODS
        /// <summary>
        /// Encrypts the letter through the Enigma algorithm
        /// </summary>
        /// <param name="l">the letter entered</param>
        /// <returns>the encrypted value</returns>
        public char DetermineEncryptedLetter(char l)
        {
            // rotates the right rotor
            rotors[2].Rotate();

            // rotates the center rotor if required
            if (CheckIfRotate(2))
            {
                rotors[1].Rotate();
                // rotates the left rotor if required
                if (CheckIfRotate(1))
                {
                    rotors[0].Rotate();
                }
            }

            char letter = l;    // stores the letter
            int index;          // stores the index of the next letter

            CheckPlugboard(ref letter);

            index = CharToInt(letter);  // stores the index of the letter

            // runs through the rotors from Right to Left
            for (int r = rotors.Length - 1; r >= 0; r--)
                RightToLeft(ref letter, ref index, rotors[r]);

            // goes through the reflector
            letter = reflector[index];
            index = CharToInt(letter);
            letter = (char)(index + (int)'A');

            // runs through the rotors from Left to Right
            for (int r = 0; r < rotors.Length; r++)
                LeftToRight(ref letter, ref index, rotors[r]);

            // gets the final letter
            letter = (char)(index + (int)'A');

            CheckPlugboard(ref letter);

            // returns the letter
            return letter;
        }

        /// <summary>
        /// Gets the letter at the given index in the cipher
        /// as well as the index of the letter in the alphabet string
        /// </summary>
        /// <param name="l">the letter to retrieve</param>
        /// <param name="i">the index of the letter</param>
        /// <param name="r">the rotor to search through</param>
        public static void RightToLeft(ref char l, ref int i, Rotors r)
        {
            l = r.LetterInCipher(i);
            i = r.AlphabetLetterIndex(l);
        }

        /// <summary>
        /// Gets the letter at the given index in the alphabet string
        /// as well as the index of the letter in the cipher
        /// </summary>
        /// <param name="l">the letter to retrieve</param>
        /// <param name="i">the index of the letter</param>
        /// <param name="r">the rotor to search through</param>
        public static void LeftToRight(ref char l, ref int i, Rotors r)
        {
            l = r.LetterInAlphabet(i);
            i = r.CipherLetterIndex(l);
        }

        /// <summary>
        /// Converts the character to an int
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public static int CharToInt(char c) { return c - 'A'; }
        #endregion
    }
}
