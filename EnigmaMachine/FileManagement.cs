/**
 * File:        FileManagement.cs
 * Description: Creates a static class and an enumeration that manages the applications files
 * Author:      Brennan Kerr
 * Since:       27 May 2020
 */
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

        #region FILE_INFORMATION
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

        /// <summary>
        /// Retrieves information from the given file
        /// </summary>
        /// <param name="fn"></param>
        /// <returns></returns>
        public static string GetInformationFromFile(FileNames fn)
        {
            // the information from the given file
            string information = "";
            // the path of the file
            string path = Application.StartupPath + GetFilePath(fn);

            // creates a StreamReader to retrieve the information
            StreamReader reader = new StreamReader(path);

            // reads each line and appends a new line
            while (reader.Peek() > 0)
            {
                information += reader.ReadLine() + "\n";
            }

            // closes the stream reader
            reader.Close();

            // removes the last new line character
            information = information.Substring(0, information.Length - 5);

            // returns the information
            return information;
        }
        #endregion

        #region OUTPUT_METHODS
        /// <summary>
        /// Appends a string to the output file
        /// </summary>
        /// <param name="append">the string to append</param>
        public static void AppendToOutput(string append)
        {
            // opens the file in a streamwriter
            StreamWriter write = new StreamWriter(Application.StartupPath + GetFilePath(FileNames.Output), true);
            write.Write(append);    // adds the string
            write.Close();          // closes the writer
        }

        /// <summary>
        /// Clears the output file
        /// </summary>
        public static void ClearOutput()
        {
            // creates a stream writer
            StreamWriter write = new StreamWriter(Application.StartupPath + GetFilePath(FileNames.Output));
            write.Write("");    // writes an empty string
            write.Close();      // closes the writer
        }
        #endregion
    }
}
