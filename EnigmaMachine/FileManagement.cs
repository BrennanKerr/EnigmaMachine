using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace EnigmaMachine
{
    /// <summary>
    /// The files that are stored in FILE_PATH array
    /// </summary>
    public enum FileNames
    {
        Rotors,
        Reflectors,
        Output
    }

    /// <summary>
    /// Manages the file informaiton
    /// </summary>
    public static class FileManagement
    {
        /// <summary>
        /// Stores the path for each file (ordered the same as FileNames)
        /// </summary>
        private static readonly string[] FILE_PATH =
        {
            "/files/rotors.txt",
            "/files/reflectors.txt",
            "/files/output.txt"
        };

        /// <summary>
        /// Gets the corresponding path of the file name passed
        /// </summary>
        /// <param name="fn">the file name defined in FileNames</param>
        /// <returns>the file path</returns>
        private static string GetFilePath(FileNames fn)
        {
            string path = "";   // stores the path

            // determines the file
            if (fn == FileNames.Rotors) path = FILE_PATH[0];
            else if (fn == FileNames.Output) path = FILE_PATH[2];
            else path = FILE_PATH[1];

            return path;    // returns the file
        }

        public static string GetInformationFromFile(FileNames fn)
        {
            string information = "";
            string path = Application.StartupPath + GetFilePath(fn);

            StreamReader reader = new StreamReader(path);

            while (reader.Peek() > 0)
            {
                information += reader.ReadLine() + "\n";
            }

            reader.Close();

            information = information.Substring(0, information.Length - 5);

            return information;
        }

        public static void AppendToOutput(string append)
        {
            StreamWriter write = new StreamWriter(Application.StartupPath + GetFilePath(FileNames.Output), true);
            write.Write(append);
            write.Close();
        }

        public static void ClearOutput()
        {
            StreamWriter write = new StreamWriter(Application.StartupPath + GetFilePath(FileNames.Output));
            write.Write("");
            write.Close();
        }
    }
}
