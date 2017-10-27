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

        private void button1_Click(object sender, EventArgs e)
        {
            FieldForm form = new FieldForm();
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
                    label3.Text = int.Parse(label3.Text) + points + "";
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
                    label3.Text = int.Parse(label3.Text) - points + "";
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
