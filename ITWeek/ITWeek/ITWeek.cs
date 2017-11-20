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

using MySql.Data.MySqlClient;

namespace ITWeek
{
    public partial class ITWeek : Form
    {

        private string CSVpath = Application.StartupPath + @"\data\users.csv";

        MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();

        //private List<Student> students = new List<Student>();

        public ITWeek(ConnInfo conninfo)
        {
            InitializeComponent();
            // LoadCSV();
            conn_string.Server = conninfo.Server;
            conn_string.UserID = conninfo.Username;
            conn_string.Password = conninfo.Password;
            conn_string.Database = "itweek";
            UpdateBD();
        }
        public void UpdateBD()
        {
            MySqlConnection connection = new MySqlConnection(conn_string.ToString());
            connection.Open();
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM students",connection);
            MySqlDataReader reader = cmd.ExecuteReader();
            int id = listBox1.SelectedIndex;
            listBox1.Items.Clear();
            while (reader.Read())
            {
                string line = "";
                for(int i = 0; i < reader.FieldCount; i ++)
                {
                    if (string.IsNullOrEmpty(line)) { line += reader[i]; }
                    else { line += " " + reader[i]; }
                }
                listBox1.Items.Add(line);
            }
            if (id>-1 && id<listBox1.Items.Count) { listBox1.SelectedIndex = id; }
            connection.Close();
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
        public void add2mysql(int id, string name, string klass, int points)
        {
            MySqlConnection connection = new MySqlConnection(conn_string.ToString()); 
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(string.Format("INSERT INTO students VALUES({0},'{1}','{2}',{3})", id, name, klass, points), connection);
            cmd.ExecuteNonQuery();
            connection.Close();
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
                bool end = false;
                for(int i = 0; i < listBox1.Items.Count; i ++)
                {
                    if(int.Parse(listBox1.Items[i].ToString().Split(' ')[0]) != i)
                    {
                        add2mysql(i, settings.label4.Text, settings.label6.Text, int.Parse(settings.label3.Text));
                        //list.Add(i + " " + settings.label4.Text + " " + settings.label6.Text + " " + settings.label3.Text);
                        //list.Add(listBox1.Items[i].ToString());
                        listBox1.Items.Insert(i, i + " " + settings.label4.Text + " " + settings.label6.Text + " " + settings.label3.Text);
                        end = true;
                    }
                }
                if (!end) { add2mysql(listBox1.Items.Count, settings.label4.Text, settings.label6.Text, int.Parse(settings.label3.Text)); listBox1.Items.Add(listBox1.Items.Count + " " + settings.label4.Text + " " + settings.label6.Text + " " + settings.label3.Text); }

            }
      //      SaveCSV();

        }

        public void uptd2mysql(int id, string name, string klass, int points)
        {

            MySqlConnection connection = new MySqlConnection(conn_string.ToString());
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(string.Format("UPDATE students SET name='{1}',class='{2}',points={3} WHERE id = {0}", id, name, klass, points), connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }
        public void delFromMysql(int id)
        {

            MySqlConnection connection = new MySqlConnection(conn_string.ToString());
            connection.Open();
            MySqlCommand cmd = new MySqlCommand(string.Format("DELETE FROM students WHERE id = {0}", id), connection);
            cmd.ExecuteNonQuery();
            connection.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex!=-1)
            {
                delFromMysql(int.Parse(listBox1.Items[listBox1.SelectedIndex].ToString().Split(' ')[0]));
                listBox1.Items.RemoveAt(listBox1.SelectedIndex);
                //SaveCSV();
            }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            int index = listBox1.IndexFromPoint(e.Location);
            if(index != -1)
            {
                string a = "";
                string[] line = listBox1.Items[index].ToString().Split(' ');
                for (int i = 1; i < line.Length - 2; i++)
                {
                    a += string.IsNullOrEmpty(a) ? line[i] : " " + line[i];
                }
                UserSettings settings = new UserSettings(a, line[line.Length - 2], int.Parse(line[line.Length-1]));
                if (settings.ShowDialog() == DialogResult.OK)
                {
                    uptd2mysql(index, settings.label4.Text, settings.label6.Text, int.Parse(settings.label3.Text));
                    listBox1.Items.RemoveAt(index);
                    listBox1.Items.Insert(index, index + " " + settings.label4.Text + " "+ settings.label6.Text + " "  + settings.label3.Text);
                   // SaveCSV();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            UpdateBD();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            UpdateBD();
        }
    }
}
