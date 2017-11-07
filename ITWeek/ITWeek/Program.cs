using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ITWeek
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {

            string path = Application.StartupPath + "\\users";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            if(Directory.GetFiles(path).Length==0)
            {
                Application.EnableVisualStyles();
                RootForm rf = new RootForm();
                if(rf.ShowDialog() == DialogResult.OK)
                {
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ITWeek());
                }
            }else
            {
                Application.EnableVisualStyles();
                LoginForm lf = new LoginForm();
                if(lf.ShowDialog() == DialogResult.OK)
                {
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ITWeek());
                }
            }
            

        }
    }
}
