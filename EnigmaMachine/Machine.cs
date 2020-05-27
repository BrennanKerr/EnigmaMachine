using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
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


        public char DetermineEncryptedLetter(char l)
        {
            rotors[2].Rotate();

            if (CheckIfRotate(2))
            {
                rotors[1].Rotate();
                if (CheckIfRotate(1))
                {
                    rotors[0].Rotate();
                }
            }

            char letter = l;
            int index = CharToInt(letter);

            for (int r = rotors.Length - 1; r >= 0; r--)
                RightToLeft(ref letter, ref index, rotors[r]);

            letter = reflector[index];
            index = CharToInt(letter);
            letter = (char)(index + (int)'A');

            for (int r = 0; r < rotors.Length; r++)
                LeftToRight(ref letter, ref index, rotors[r]);

            letter = (char)(index + (int)'A');

            return letter;
        }

        public static void RightToLeft(ref char l, ref int i, Rotors r)
        {
            l = r.LetterInCipher(i);
            i = r.AlphabetLetterIndex(l);
        }

        public static void LeftToRight(ref char l, ref int i, Rotors r)
        {
            l = r.LetterInAlphabet(i);
            i = r.CipherLetterIndex(l);
        }

        public static int CharToInt(char c) { return c - 'A'; }

        private bool CheckIfRotate(int num)
        {
            return rotors[num].GetCipher()[25] == Rotors.GetRotorAdvancements(rotors[num].CipherNumber);
        }
    }
}
