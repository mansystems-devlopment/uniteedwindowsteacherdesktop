using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Views;

namespace UniteEDTeacher
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (UniteEDTeacher.Properties.Settings.Default.activated)
            {
                Application.Run(new DashboardForm());
            }
            else
            {
                Application.Run(new ActivateForm());
            }

        }
    }
}
