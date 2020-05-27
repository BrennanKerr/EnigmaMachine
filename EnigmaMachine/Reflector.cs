using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnigmaMachine
{
    public class Reflector
    {
        private static string[,] REFLECTOR_LIST;

        public static void SetReflectorList()
        {
            string[] list = FileManagement.GetInformationFromFile(FileNames.Reflectors).Split('\n');

            REFLECTOR_LIST = new string[list.Length, 2];

            for (int i = 0; i < list.Length; i++)
            {
                string[] l = list[i].Split(' ');
                REFLECTOR_LIST[i, 0] = l[0];
                REFLECTOR_LIST[i, 1] = l[1];
            }
        }

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

        public static string GetReflectorString(int index) { return REFLECTOR_LIST[index, 1]; }
    }
}
