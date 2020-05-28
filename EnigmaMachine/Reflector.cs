/**
 * File:        Reflector.cs
 * Description: Creates a static class that stores information about the reflectors
 * Author:      Brennan Kerr
 * Since:       27 May 2020
 */

namespace EnigmaMachine
{
    /// <summary>
    /// Defines the reflector information
    /// </summary>
    public static class Reflector
    {
        /// <summary>
        /// Stores the reflector strings
        /// </summary>
        private static string[,] REFLECTOR_LIST;

        /// <summary>
        /// Returns the reflector list
        /// </summary>
        public static void SetReflectorList()
        {
            // a temporary list to store each line returned
            string[] list = FileManagement.GetInformationFromFile(FileNames.Reflectors).Split('\n');

            // sets the first dimension to the lengh of the temporary array
            REFLECTOR_LIST = new string[list.Length, 2];

            // sets the array and the second dimension size
            for (int i = 0; i < list.Length; i++)
            {
                string[] l = list[i].Split(' ');
                REFLECTOR_LIST[i, 0] = l[0];
                REFLECTOR_LIST[i, 1] = l[1];
            }
        }

        /// <summary>
        /// Retrieves the Reflector names
        /// </summary>
        public static string[] GetReflectorList
        {
            get
            {
                if (REFLECTOR_LIST == null) SetReflectorList();

                string[] options = new string[REFLECTOR_LIST.GetLength(0)];

                for (int i = 0; i < options.Length; i++)
                    options[i] = REFLECTOR_LIST[i, 0];
                return options;
            }
        }

        /// <summary>
        /// Retrieves the reflector strings
        /// </summary>
        /// <param name="index">the index of the reflector from REFLECTOR_LIST</param>
        /// <returns>the corresponding string</returns>
        public static string GetReflectorString(int index) { return REFLECTOR_LIST[index, 1]; }
    }
}
