using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ITWeek
{
    public partial class UserSettings : Form
    {
        public UserSettings()
        {
            InitializeComponent();
        }
        public UserSettings(string name,string klass, int points)
        {
            InitializeComponent();
            label4.Text = name;
            label3.Text = points.ToString();
            label6.Text = klass;
         }

        private void button1_Click(object sender, EventArgs e)
        {
            FieldForm form = new FieldForm("" + label4.Text);
            if(form.ShowDialog() == DialogResult.OK)
            {
                label4.Text = form.textBox1.Text;
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FieldForm form = new FieldForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                int points = 0;
                if(int.TryParse(form.textBox1.Text, out points))
                {
                    int ipoints = 0;
                    int.TryParse(label3.Text, out ipoints);
                    label3.Text = (ipoints + points).ToString();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            FieldForm form = new FieldForm();
            if (form.ShowDialog() == DialogResult.OK)
            {
                int points = 0;
                if (int.TryParse(form.textBox1.Text, out points))
                {
                    int ipoints = 0;
                    int.TryParse(label3.Text, out ipoints);
                    label3.Text = (ipoints - points).ToString();
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = 0;
            if(!string.IsNullOrEmpty(label4.Text) && int.TryParse(label3.Text, out i) && !string.IsNullOrEmpty(label6.Text))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            FieldForm form = new FieldForm("" + label6.Text);
            if (form.ShowDialog() == DialogResult.OK)
            {
                if (form.textBox1.Text.Length>3) { MessageBox.Show("Класс не может содержать больше 3 символов!","Предупреждение",MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                label6.Text = form.textBox1.Text;
            }
        }
    }
}
