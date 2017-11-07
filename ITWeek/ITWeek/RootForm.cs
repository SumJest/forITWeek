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
using System.Runtime.InteropServices;

namespace ITWeek
{
    public partial class RootForm : Form
    {

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        public static extern short GetKeyState(int keyCode);

        bool isMatch = false;


        public RootForm()
        {
            InitializeComponent();
        }


        private void textBox6_TextChanged(object sender, EventArgs e)
        {
                if (textBox6.Text != "" && textBox5.Text != "")
                {
                    if (textBox5.Text.Equals(textBox6.Text))
                    {
                        label11.ForeColor = Color.Green;
                        label11.Text = "Passwords match";
                        isMatch = true;
                    }
                    else
                    {
                        label11.ForeColor = Color.Red;
                        label11.Text = "Passwords do not match";
                        isMatch = false;
                    }
                }
                else if (textBox6.Text == "" || textBox5.Text == "")
                {
                    label11.Text = "";
                    label11.ForeColor = Color.Black;
                    isMatch = false;
                }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
                if (textBox6.Text != "" && textBox5.Text != "")
                {
                    if (textBox5.Text.Equals(textBox6.Text))
                    {
                        label11.ForeColor = Color.Green;
                        label11.Text = "Пароли совпадают";
                        isMatch = true;
                    }
                    else
                    {
                        label11.ForeColor = Color.Red;
                        label11.Text = "Пароли не совпадают";
                        isMatch = false;
                    }
                }
                else if (textBox6.Text == "" || textBox5.Text == "")
                {
                    label11.Text = "";
                    label11.ForeColor = Color.Black;
                    isMatch = false;
                }
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text) || string.IsNullOrEmpty(textBox3.Text) || string.IsNullOrEmpty(textBox4.Text) || string.IsNullOrEmpty(textBox5.Text) || string.IsNullOrEmpty(textBox6.Text))
            {
                MessageBox.Show("Нужно заполнить все поля!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(!isMatch)
            {
                MessageBox.Show("Пароли не совпадают!", "Внимание!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                MySqlConnection connection = new MySqlConnection(string.Format("server={0};user={1};database=itweek;password={2}", textBox1.Text, textBox2.Text, textBox3.Text));
                connection.Open();
                connection.Close();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sdata = textBox1.Text + "\n" + textBox2.Text + "\n" + textBox3.Text;
            byte[] data = Encoding.ASCII.GetBytes(sdata);
            try
            {
                RCC5 rcc5 = new RCC5(Encoding.ASCII.GetBytes(textBox5.Text));
                byte[] edata = rcc5.Encode(data);
                FileStream stream = File.Create(Application.StartupPath + @"\users\" + textBox4.Text);
                stream.Write(edata, 0,edata.Length);
                stream.Close();
                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, ex.GetType().ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
        private void KeyForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) { button1_Click(null, null); }
        }


        private void button2_MouseUp(object sender, MouseEventArgs e)
        {
            textBox5.PasswordChar = '•';
        }

        private void button3_MouseUp(object sender, MouseEventArgs e)
        {
            textBox6.PasswordChar = '•';
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            textBox5.PasswordChar = default(char);
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            textBox6.PasswordChar = default(char);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label9.ForeColor = (((ushort)GetKeyState(0x90)) & 0xffff) == 0 ? Color.Black : Color.LimeGreen;
            label10.ForeColor = (((ushort)GetKeyState(0x14)) & 0xffff) == 0 ? Color.Black : Color.LimeGreen;
        }

    }
}
