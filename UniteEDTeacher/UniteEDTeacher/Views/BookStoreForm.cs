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
using System.Windows.Threading;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;


namespace UniteEDTeacher.Views
{
    public partial class BookStoreForm : Form
    {
        private  ChromiumWebBrowser browser;

        public BookStoreForm()
        {
            InitializeComponent();


        }

        private void BookStoreForm_Load(object sender, EventArgs e)
        {


            ActivationModule bookStoreModule = new ActivationModule();
            bookStoreModule.ModuleName = "books";
            bookStoreModule.ModuleList_Setting = Helpers.LoadModuleSettings(bookStoreModule.ModuleName);

            String bookStoreUrl = "";


            foreach (ModuleSetting moduleSetting in bookStoreModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("bookStoreUrl"))
                {
                    bookStoreUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                try
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    //WindowState = FormWindowState.Maximized;

                    browser = new ChromiumWebBrowser(bookStoreUrl)
                    {
                        Dock = DockStyle.Fill,
                    };
                    panel2.Controls.Add(browser);
                    browser.FrameLoadEnd += OnBrowserFrameLoadEnd;


                }
                catch (Exception ex) {
                    MessageBox.Show(ex.Message);
                }

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
        
        public void DisplayOutput(string output)
        {
            try
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
            catch { }
        }

        private void OnBrowserFrameLoadEnd(object sender, FrameLoadEndEventArgs args)
        {

            if (args.IsMainFrame)
            {
                Action action = new Action(() => {

                    DisplayOutput(string.Format("URL: {0}, Status Code: {1}", args.Url, args.HttpStatusCode));
                    pictureBox2.Visible = false;
                
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
        private void BookStoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                browser.Dispose();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
