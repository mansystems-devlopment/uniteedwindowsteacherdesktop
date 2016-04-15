using CefSharp;
using Squirrel;
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

            Cef.Initialize(new CefSettings());

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (UniteEDTeacher.Properties.Settings.Default.activated)
            {
                checkForUpdates();
                Application.Run(new DashboardForm());
            }
            else
            {
                Application.Run(new ActivateForm());
            }

        }

        private async static void checkForUpdates()
        {
            using (var mgr = UpdateManager.GitHubUpdateManager("https://github.com/mansystems-devlopment/uniteedwindowsteacherdesktop/tree/master/UniteEDTeacher"))
            {
                await mgr.Result.UpdateApp();
            } 
        }
    }
}
