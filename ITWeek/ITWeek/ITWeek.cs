using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITWeek
{
    public partial class ITWeek : Form
    {

        private string CSVpath = Application.StartupPath + @"\data\users.csv";

        public ITWeek()
        {
            InitializeComponent();
            Dictionary<string, int> users = ReadInCSV(CSVpath);
            
            foreach (string s in users.Keys) { listBox1.Items.Add(s + " " + users[s]); }
        }
        public static Dictionary<string, int> ReadInCSV(string absolutePath)
        {
            Dictionary<string, int> result = new Dictionary<string, int>();
            using (StreamReader fileReader = File.OpenText(absolutePath))
            {
                while(!fileReader.EndOfStream)
                {
                    string[] line = fileReader.ReadLine().Split(',');
                    result.Add(line[0], int.Parse(line[1]));
                }
            }
            return result;
        }
        public static void WriteInCSV(string absolutePath, Dictionary<string,int> dict)
        {
            using (FileStream fileReader = File.OpenWrite(absolutePath))
            {
                string a = "";
                foreach(string sa in dict.Keys)
                {
                    if(a=="")
                    {
                        a += sa + "," + dict[sa];
                    }
                    else
                    {
                        a += "\n" + sa + "," + dict[sa];
                    }
                }
                byte[] s = Encoding.UTF8.GetBytes(a);
                fileReader.Write(s, 0, s.Length);
                fileReader.Close();
            }
           
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UserSettings settings = new UserSettings();

        }
    }
}
