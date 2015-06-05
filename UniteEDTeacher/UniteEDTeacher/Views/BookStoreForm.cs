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
                    panel1.Controls.Add(browser);
                    browser.StatusMessage += OnBrowserStatusMessage;
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
        

        private void OnBrowserStatusMessage(object sender, StatusMessageEventArgs args)
        {
            Action action = new Action(()=> statusLabel.Text = args.Value);

            if (this.InvokeRequired)
            {
                this.BeginInvoke(action);
            }
            else
            {
                action.Invoke();
            }

            
        }
        private void OnBrowserConsoleMessage(object sender, ConsoleMessageEventArgs args)
        {
            DisplayOutput(string.Format("Line: {0}, Source: {1}, Message: {2}", args.Line, args.Source, args.Message));
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
                Action action = new Action(() => {

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
        private void BookStoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            browser.Dispose();            
        }
    }
}
