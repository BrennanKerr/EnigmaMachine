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
            this.plugboard = plugboard;
        }

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

            char letter = l;                // stores the letter
            int index = CharToInt(letter);  // stores the index of the letter

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
    }
}
