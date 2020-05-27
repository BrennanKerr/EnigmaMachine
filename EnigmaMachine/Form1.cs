using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace EnigmaMachine
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            StreamReader reader = new StreamReader(Application.StartupPath + FileManagement.GetFilePath(FileNames.Rotors));
            string input = "";

            while (reader.Peek() > 0)
            {
                input += reader.ReadLine() + Environment.NewLine;
            }

            reader.Close();
            MessageBox.Show(input);
        }
    }
}
