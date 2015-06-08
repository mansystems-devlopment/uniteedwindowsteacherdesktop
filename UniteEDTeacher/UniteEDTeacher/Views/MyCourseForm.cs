using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher.Views
{

    public partial class MyCourses : Form
    {
      
        public MyCourses(String url)
        {
            InitializeComponent();

            ActivationModule MoodleAccountActivationModule = new ActivationModule();
            MoodleAccountActivationModule.ModuleName = "moodle";
            MoodleAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(MoodleAccountActivationModule.ModuleName);

            String moodleCoursesUrl = "";
            String moodleLoginUrl = "";
            String moodleUsername = "";
            String moodlePassword = "";


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

                UniteEDNetwork net = new UniteEDNetwork();
                string postData = "username=";
                postData += moodleUsername + "&password=";
                postData += moodlePassword + "&wantsurl=";
                postData += moodleCoursesUrl;

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

                       MessageBox.Show("Opening My Courses",ex.Message + "\n" + ex.StackTrace,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    }
                }, moodleLoginUrl + "?about", postData);
            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
            
        }
    }
}
