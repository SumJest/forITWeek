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
            LoadCSV();
        }
        private Dictionary<string, int> ReadInCSV(string absolutePath)
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
        private void WriteInCSV(string absolutePath, Dictionary<string,int> dict)
        {
            string a = "";
            foreach (string sa in dict.Keys)
            {
                if (a == "")
                {
                    a += sa + "," + dict[sa];
                }
                else
                {
                    a += "\n" + sa + "," + dict[sa];
                }
            }
            byte[] s = Encoding.UTF8.GetBytes(a);
            File.Delete(absolutePath);
            using (FileStream fileReader = File.OpenWrite(absolutePath))
            {
                fileReader.Write(s, 0, s.Length);
                fileReader.Close();
            }
           
        }
        public void SaveCSV()
        {
            Dictionary<string, int> dict = new Dictionary<string, int>();
            foreach(string s in listBox1.Items)
            {    
                string a = "";
                string[] line = s.Split(' ');
                for(int i = 0; i < line.Length -1; i ++)
                {
                    a += string.IsNullOrEmpty(a) ? line[i] : " " + line[i];
                }
                dict.Add(a, int.Parse(line[line.Length-1]));
            }
            WriteInCSV(CSVpath, dict);
        }
        public void LoadCSV()
        {
            Dictionary<string, int> dict = ReadInCSV(CSVpath);
            foreach (string s in dict.Keys)
            {
                listBox1.Items.Add(s + " " + dict[s]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            UserSettings settings = new UserSettings();
            if(settings.ShowDialog() == DialogResult.OK)
            {
                listBox1.Items.Add(settings.label4.Text + " " + settings.label3.Text);
            }
            SaveCSV();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                SaveCSV();
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if(index != -1)
            {
                string a = "";
                string[] line = listBox1.Items[index].ToString().Split(' ');
                for (int i = 0; i < line.Length - 1; i++)
                {
                    a += string.IsNullOrEmpty(a) ? line[i] : " " + line[i];
                }
                UserSettings settings = new UserSettings(a, int.Parse(line[line.Length-1]));
                if(settings.ShowDialog() == DialogResult.OK)
                {
                    listBox1.Items.RemoveAt(index);
                    listBox1.Items.Insert(index,settings.label4.Text + " " + settings.label3.Text);
                    SaveCSV();
                }
            }
        }
    }
}
