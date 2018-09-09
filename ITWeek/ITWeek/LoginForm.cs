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
using MySql.Data.MySqlClient;

namespace ITWeek
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public MySqlConnectionStringBuilder conn_string = new MySqlConnectionStringBuilder();

        private void button1_Click(object sender, EventArgs e)
        {
            string dir = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\itweek\\user";
            if (Directory.GetFiles(dir).Length == 0)
            {
                MessageBox.Show("Файл пользователя не найден! Перезапустите программу.", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Заполните поле!", "Внимание", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                RCC5 rcc5 = new RCC5(Encoding.ASCII.GetBytes(textBox1.Text));
                byte[] edata = File.ReadAllBytes(Directory.GetFiles(dir)[0]);
                byte[] data = rcc5.Decode(edata);
                string sdata = Encoding.ASCII.GetString(data);
                string[] msdata = sdata.Split('\n');
                conn_string.Server = msdata[0];
                conn_string.UserID = msdata[1];
                conn_string.Password = msdata[2];
                conn_string.Database = "usersitweek";
                MySqlConnection connection = new MySqlConnection(conn_string.ToString());
                connection.Open();
                connection.Close();
                DialogResult = DialogResult.OK;
                Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }
        }
    }
}
