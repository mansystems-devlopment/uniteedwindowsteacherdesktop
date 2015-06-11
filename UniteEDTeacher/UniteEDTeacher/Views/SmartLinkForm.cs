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
    public partial class SmartLinkForm : Form
    {
        private ChromiumWebBrowser browser;
        public SmartLinkForm()
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

                    DisplayOutput(string.Format("URL: {0}, Status Code: {1}", args.Url, args.HttpStatusCode));
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

        private void SmartLinkForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                browser.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void SmartLinkForm_Load(object sender, EventArgs e)
        {


            ActivationModule smartLinkModule = new ActivationModule();
            smartLinkModule.ModuleName = "Smartlink";
            smartLinkModule.ModuleList_Setting = Helpers.LoadModuleSettings(smartLinkModule.ModuleName);

            String smartLinkUrl = "";

            foreach (ModuleSetting moduleSetting in smartLinkModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("SmartLinkUrl"))
                {
                    smartLinkUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    //WindowState = FormWindowState.Maximized;

                    browser = new ChromiumWebBrowser(smartLinkUrl)
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
