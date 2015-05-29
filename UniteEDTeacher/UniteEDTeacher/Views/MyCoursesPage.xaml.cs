using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace UniteEDTeacher.Views
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class MyCoursesPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        ApplicationDataContainer settings;

        public MyCoursesPage()
        {
            this.InitializeComponent();
            settings = Windows.Storage.ApplicationData.Current.LocalSettings;
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="navigationParameter">The parameter value passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested.
        /// </param>
        /// <param name="pageState">A dictionary of state preserved by this page during an earlier
        /// session.  This will be null the first time a page is visited.</param>
        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {





        }
        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ActivationModule MoodleAccountActivationModule = new ActivationModule();
            MoodleAccountActivationModule.ModuleName = "MoodleAccount";
            MoodleAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(settings, MoodleAccountActivationModule.ModuleName);

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

                            Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {


                                WebviewMyCourses.NavigateToString(cleanstr);
                            });




                            //Check for result code..
                        }
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }
                }, moodleLoginUrl + "?about", postData);
            }
            else
            {
                MessageDialog dlg = new MessageDialog("no internet connection");
                await dlg.ShowAsync();
            }

        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="pageState">An empty dictionary to be populated with serializable state.</param>
        protected override void SaveState(Dictionary<String, Object> pageState)
        {
        }
    }
}
