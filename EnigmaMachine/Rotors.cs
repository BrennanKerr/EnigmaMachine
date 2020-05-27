using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
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

        public Rotors(int rNum = 0, int cNum = 0, int offset = 0)
        {
            rotorNum = rNum;
            cipherNumber = cNum;
            cipher = ROTOR_DEFAULTS[cNum][1];
        }

        #region METHODS

        public string GetCipher()
        {
            return cipher;
        }

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

        public static char GetRotorAdvancements(int index)
        {
            return ROTOR_DEFAULTS[index][2][0];
        }
        #endregion


        public char LetterInCipher(int i) { return cipher[i]; }
        public char LetterInAlphabet(int i) { return alphabet[i]; }
        public int CipherLetterIndex(char l) { return cipher.IndexOf(l); }
        public int AlphabetLetterIndex(char l) { return alphabet.IndexOf(l); }

        public void Rotate()
        {
            cipher = ChanceIndex(cipher, 1);
            alphabet = ChanceIndex(alphabet, 1);
        }

        public string ChanceIndex(string s, int o) { return s.Substring(o, s.Length - o) + s.Substring(0, o); }


        public override string ToString()
        {
            return cipher;
        }
    }
}
