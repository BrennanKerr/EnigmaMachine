/**
 * File:        Rotors.cs
 * Description: Creates a class that defines information about a Rotor
 * Author:      Brennan Kerr
 * Since:       27 May 2020
 */

namespace EnigmaMachine
{
    /// <summary>
    /// Defines information about a rotor
    /// </summary>
    public class Rotors
    {
        /// <summary>
        /// Stores the defaults of each rotor number option
        /// </summary>
        private static string[][] ROTOR_DEFAULTS;

        #region FIELDS
        /// <summary>
        /// Stores the rotor number of the current rotor
        /// </summary>
        private int rotorNum;
        /// <summary>
        /// Stores the current cipher order
        /// </summary>
        private string cipher;
        /// <summary>
        /// Stores the cipher number used
        /// </summary>
        private int cipherNumber;
        /// <summary>
        /// Stores the current alphabet format
        /// </summary>
        private string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        #endregion

        /// <summary>
        /// Initializes the Rotor
        /// </summary>
        /// <param name="rNum">the number of the rotor in the array</param>
        /// <param name="cNum">the cipher index number from ROTOR_DEFAULTS</param>
        /// <param name="offset">the offset of the rotor</param>
        public Rotors(int rNum = 0, int cNum = 0, int offset = 0)
        {
            rotorNum = rNum;
            cipherNumber = cNum;
            cipher = ROTOR_DEFAULTS[cNum][1];

            if (offset > 0) Rotate(offset);
        }

        #region METHODS
        /// <summary>
        /// Retrieves the cipher string
        /// </summary>
        /// <returns></returns>
        public string GetCipher() { return cipher; }
        /// <summary>
        /// Returns the last letter in the alphabet string
        /// </summary>
        /// <returns></returns>
        public char GetLastAlphabetLetter() { return alphabet[alphabet.Length - 1]; }

        /// <summary>
        /// Gets or sets the cipher number
        /// </summary>
        public int CipherNumber
        {
            get { return cipherNumber; }
            set 
            { 
                cipherNumber = value;
                cipher = ROTOR_DEFAULTS[cipherNumber][1];
            }
        }
        #endregion

        #region STATIC_METHODS
        /// <summary>
        /// Sets the default rotor options
        /// </summary>
        public static string SetRotorDefaults
        {
            set
            {
                if (ROTOR_DEFAULTS == null)
                {
                    string[] lines = value.Split('\n');
                    string[][] temp = new string[lines.Length][];

                    for (int i = 0; i < lines.Length; i++)
                    {
                        string[] cur = lines[i].Split(' ');
                        temp[i] = new string[cur.Length];

                        for (int j = 0; j < cur.Length; j++)
                        {
                            temp[i][j] = cur[j];
                        }
                    }

                    
                    ROTOR_DEFAULTS = new string[temp.GetLength(0)][];
                    for (int i = 0; i < ROTOR_DEFAULTS.GetLength(0); i++)
                    {
                        ROTOR_DEFAULTS[i] = new string[temp[i].GetLength(0)];

                        ROTOR_DEFAULTS[i] = temp[i];
                    }
                }
            }
        }

        /// <summary>
        /// Returns all the rotor numbers
        /// </summary>
        /// <returns></returns>
        public static string GetRotorNumbers()
        {
            string str = ROTOR_DEFAULTS[0][0];

            for (int i = 1; i < ROTOR_DEFAULTS.GetLength(0); i++)
                str += " " + ROTOR_DEFAULTS[i][0];

            return str;
        }

        /// <summary>
        /// Retrieve the letters that force the next rotor to advance
        /// </summary>
        /// <param name="index">the cipher index</param>
        /// <returns>the character that forces the rotation</returns>
        public static char GetRotorAdvancements(int index)
        {
            return ROTOR_DEFAULTS[index][2][0];
        }
        #endregion

        /// <summary>
        /// Retrieves the letter from the cipher at the given index
        /// </summary>
        /// <param name="i">the index of the letter</param>
        /// <returns>the character at the index</returns>
        public char LetterInCipher(int i) { return cipher[i]; }
        /// <summary>
        /// Retrieves the letter from the alphabet string at the given index
        /// </summary>
        /// <param name="i">the index of the letter</param>
        /// <returns>the character at the index</returns>
        public char LetterInAlphabet(int i) { return alphabet[i]; }
        /// <summary>
        /// Retrieves the index of the letter in the cipher
        /// </summary>
        /// <param name="l">the letter</param>
        /// <returns>the index of the letter</returns>
        public int CipherLetterIndex(char l) { return cipher.IndexOf(l); }
        /// <summary>
        /// Retrieves the index of the letter in the alphabet string
        /// </summary>
        /// <param name="l">the letter</param>
        /// <returns>the index of the letter</returns>
        public int AlphabetLetterIndex(char l) { return alphabet.IndexOf(l); }

        /// <summary>
        /// Rotates the rotor
        /// </summary>
        /// <param name="rotations"></param>
        public void Rotate(int rotations = 1)
        {
            // rotates the cipher string
            cipher = ChangeIndex(cipher, rotations);
            // rotates the alphabet string
            alphabet = ChangeIndex(alphabet, rotations);
        }

        /// <summary>
        /// Changes the starting index for the string
        /// </summary>
        /// <param name="s">the string to change the starting index</param>
        /// <param name="o">the offset of the string</param>
        /// <returns>the string offset to the given index</returns>
        public string ChangeIndex(string s, int o) { return s.Substring(o, s.Length - o) + s.Substring(0, o); }

        /// <summary>
        /// Returns the rotor as a string
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return cipher;
        }
    }
}
