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
    public partial class FieldForm : Form
    {


        public FieldForm()
        {
            InitializeComponent();
        }

        public FieldForm(string text)
        {
            InitializeComponent();
            textBox1.Text = text;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Введите в поле значение", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            this.DialogResult = DialogResult.OK;
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button1_Click(null, null);
            }
        }
    }
}
