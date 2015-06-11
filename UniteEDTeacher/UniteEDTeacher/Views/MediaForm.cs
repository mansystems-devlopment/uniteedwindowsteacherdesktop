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
    public partial class MediaForm : Form
    {
        private ChromiumWebBrowser browser;
        public MediaForm()
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

        private void MediaForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                browser.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        private void MediaForm_Load(object sender, EventArgs e)
        {

            ActivationModule cloudBancMediaModule = new ActivationModule();
            cloudBancMediaModule.ModuleName = "media";
            cloudBancMediaModule.ModuleList_Setting = Helpers.LoadModuleSettings(cloudBancMediaModule.ModuleName);

            String cloudBancMediaUrl = "";

            foreach (ModuleSetting moduleSetting in cloudBancMediaModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("StoreURL"))
                {
                    cloudBancMediaUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                /* UniteEDNetwork net = new UniteEDNetwork();
                 MediawebBrowser.Navigate(new Uri(cloudBancMediaUrl));*/

                try
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    //WindowState = FormWindowState.Maximized;

                    browser = new ChromiumWebBrowser(cloudBancMediaUrl)
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
