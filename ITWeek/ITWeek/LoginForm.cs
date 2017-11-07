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

namespace ITWeek
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        public ConnInfo conninfo;

        private void button1_Click(object sender, EventArgs e)
        {
            string dir = Application.StartupPath + "\\users";
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
                conninfo = new ConnInfo(msdata[0], msdata[1], msdata[2]);
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
