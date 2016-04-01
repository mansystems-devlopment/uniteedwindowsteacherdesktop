using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        private Cookie cookie;
        String smartLinkUrl = "";
        String username = "";
        String password = "";
        String autoLoginUrl = "";

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

                    //DisplayOutput(string.Format("URL: {0}, Status Code: {1}", args.Url, args.HttpStatusCode));
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
        private void AutoLogin()
        {

            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            var client = new HttpClient(handler) { BaseAddress = new Uri(smartLinkUrl) };
            var content = new FormUrlEncodedContent(new[] {
                new KeyValuePair<string, string>("email",username ),
                new KeyValuePair<string, string>("password", password),
            });
            var result = client.PostAsync("/login/", content).Result;
            //Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            CookieCollection collection = handler.CookieContainer.GetCookies(client.BaseAddress);
            cookie = collection[0];
            //Console.WriteLine(cookie.Name + "======" + cookie.Value);
            autoLoginUrl = smartLinkUrl + "/login?" + cookie.ToString();

        }

        private void SmartLinkForm_Load(object sender, EventArgs e)
        {


            ActivationModule smartLinkModule = new ActivationModule();
            smartLinkModule.ModuleName = "Smartlink";
            smartLinkModule.ModuleList_Setting = Helpers.LoadModuleSettings(smartLinkModule.ModuleName);


            foreach (ModuleSetting moduleSetting in smartLinkModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("SmartLinkUrl"))
                {
                    smartLinkUrl = moduleSetting.SettingData;

                }
                if (moduleSetting.SettingName.Equals("SmartLinkusername"))
                {
                    username = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("SmartLinkpassword"))
                {
                    password = moduleSetting.SettingData;
                }
            }

            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                try
                {
                    UniteEDNetwork net = new UniteEDNetwork();
                    AutoLogin();
                    Cef.SetCookie(autoLoginUrl, cookie.Name, cookie.Value, "", cookie.Path, cookie.Secure, cookie.HttpOnly, cookie.Expired, cookie.Expires);                     
                    browser = new ChromiumWebBrowser(autoLoginUrl)
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
