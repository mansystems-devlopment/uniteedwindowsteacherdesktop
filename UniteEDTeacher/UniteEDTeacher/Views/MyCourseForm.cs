using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;
using System.Net;
using System.Runtime.InteropServices;
using CefSharp.WinForms;
using CefSharp;

namespace UniteEDTeacher.Views
{

    public partial class MyCourses : Form
    {
        ActivationModule activationModule;
        private ChromiumWebBrowser browser;
        private Cookie cookie;
            
            String moodleCoursesUrl = "";
            String moodleLoginUrl = "";
            String moodleUsername = "";
            String moodlePassword = "";
        public MyCourses()
        {
            InitializeComponent();
                   
            
        }
        public MyCourses(String url) //original constructor
        {
            InitializeComponent();
           

        }
        private void init()
        {

            activationModule.ModuleName = "moodle";
            activationModule.ModuleList_Setting = Helpers.LoadModuleSettings(activationModule.ModuleName);

            foreach (ModuleSetting moduleSetting in activationModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("moodleCoursesUrl"))
                {
                    moodleCoursesUrl = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("moodleLoginUrl"))
                {
                    moodleLoginUrl = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("moodleUsername"))
                {
                    moodleUsername = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("moodlePassword"))
                {
                    moodlePassword = moduleSetting.SettingData;
                }
            }
            LoginCookie();
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
        private void LoginCookie()
        {

            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer };
            var client = new HttpClient(handler) { BaseAddress=new Uri(moodleCoursesUrl)};
            var content = new FormUrlEncodedContent(new [] {
                new KeyValuePair<string, string>("username",moodleUsername ),
                new KeyValuePair<string, string>("password", moodlePassword),
            });
            var result = client.PostAsync("/login/", content).Result;
           // Console.WriteLine(result.Content.ReadAsStringAsync().Result);
            CookieCollection collection = handler.CookieContainer.GetCookies(client.BaseAddress);
            cookie = collection[0];
           // Console.WriteLine(cookie.Name + "======" + cookie.Value);

           
        }


        private void MyCourses_Load(object sender, EventArgs e)
        {
            ActivationModule MoodleAccountActivationModule = new ActivationModule();
            MoodleAccountActivationModule.ModuleName = "moodle";
            MoodleAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(MoodleAccountActivationModule.ModuleName);


            foreach (ModuleSetting moduleSetting in MoodleAccountActivationModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("moodleCoursesUrl"))
                {
                    moodleCoursesUrl = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("moodleLoginUrl"))
                {
                    moodleLoginUrl = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("moodleUsername"))
                {
                    moodleUsername = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("moodlePassword"))
                {
                    moodlePassword = moduleSetting.SettingData;
                }
            }
            

            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
               
                try
                {
                    UniteEDNetwork net = new UniteEDNetwork();
                    LoginCookie();
                  
                    
                    Cef.SetCookie(moodleCoursesUrl, cookie.Name, cookie.Value,"", cookie.Path, cookie.Secure, cookie.HttpOnly, cookie.Expired, cookie.Expires);
                                 
                    browser = new ChromiumWebBrowser(moodleCoursesUrl)
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

                /*
                UniteEDNetwork net = new UniteEDNetwork();
                string postData = "username=";
                postData += moodleUsername + "&password=";
                postData += moodlePassword + "&wantsurl=";
                postData += moodleLoginUrl;

               
                net.PostMoodleData((httpResponse) =>
                {
                    try
                    {
                        using (System.IO.StreamReader httpwebStreamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            // ProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            // LayoutRoot.IsTapEnabled = true;
                            var re = httpwebStreamReader.ReadToEnd();
                            //login response
                            string cleanstr = re.ToString().TrimStart();
                            cleanstr = cleanstr.TrimEnd();

                            this.Invoke(
                                        (Action)(() =>
                                        {
                                            MyCoursewebBrowser.Navigate(new Uri(moodleLoginUrl));

                                        }

                                        ));




                            //Check for result code..
                        }
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Opening My Courses", ex.Message + "\n" + ex.StackTrace, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }, moodleLoginUrl + "?about", postData);
                */ 
            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void MyCourses_FormClosing(object sender, FormClosingEventArgs e)
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
