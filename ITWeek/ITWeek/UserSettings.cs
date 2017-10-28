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
        public UserSettings(string name, int points)
        {
            InitializeComponent();
            label4.Text = name;
            label3.Text = points.ToString();
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
            if(!string.IsNullOrEmpty(label4.Text) && int.TryParse(label3.Text, out i))
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
