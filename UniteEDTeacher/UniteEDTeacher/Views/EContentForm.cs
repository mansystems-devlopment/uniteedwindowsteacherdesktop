using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher.Views
{
    public partial class EContentForm : Form
    {
        private ChromiumWebBrowser browser;
        public EContentForm()
        {
            InitializeComponent();

        }
        
        public void DisplayOutput(string output)
        {
            Action action = new Action(() => outputLabel.Text = output);

            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }
        }

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
        {

            if (args.IsMainFrame)
            {
                Action action = new Action(() =>
                {

                    DisplayOutput(string.Format("Status Code: {0}", args.HttpStatusCode));
                    pictureBox1.Visible = false;

                });

                if (this.InvokeRequired)
                {
                    this.BeginInvoke(action);
                }
                else
                {
                    action.Invoke();
                }


            }
        }

        private void EContentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                browser.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void EContentForm_Load(object sender, EventArgs e)
        {
            ActivationModule EduDoc = new ActivationModule();
            EduDoc.ModuleName = "EduDoc";
            EduDoc.ModuleList_Setting = Helpers.LoadModuleSettings(EduDoc.ModuleName);

            String eduDocURL = "";

            foreach (ModuleSetting moduleSetting in EduDoc.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("eduDocURL"))
                {
                    eduDocURL = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                try
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    //WindowState = FormWindowState.Maximized;

                    browser = new ChromiumWebBrowser(eduDocURL)
                    {
                        Dock = DockStyle.Fill,
                    };
                    panel1.Controls.Add(browser);
                    browser.FrameLoadEnd += OnBrowserFrameLoadEnd;


                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
