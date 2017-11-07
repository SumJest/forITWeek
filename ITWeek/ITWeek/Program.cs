﻿using System;
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
                Application.SetCompatibleTextRenderingDefault(false);
                RootForm rf = new RootForm();
                if(rf.ShowDialog() == DialogResult.OK)
                {
                    Application.EnableVisualStyles();
                    Application.Run(new ITWeek(rf.conninfo));
                }
            }else
            {
                Application.SetCompatibleTextRenderingDefault(false);

                LoginForm lf = new LoginForm();
                if(lf.ShowDialog() == DialogResult.OK)
                {
                    Application.EnableVisualStyles();
                    Application.Run(new ITWeek(lf.conninfo));
                }
            }
            

        }
    }
}
