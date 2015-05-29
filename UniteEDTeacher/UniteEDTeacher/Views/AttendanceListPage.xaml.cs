using Newtonsoft.Json;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
    public sealed partial class AttendanceListPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        ApplicationDataContainer settings;

        public AttendanceListPage()
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

            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                UniteEDNetwork net = new UniteEDNetwork();
                string postData = "TeacherID=";
                postData += Helpers.LoadJSONSettings(settings, "Login_Username") + "&SessionID=";
                postData += Regex.Match(Helpers.LoadJSONSettings(settings, "TeacherInfo_Session"), @"\d+").Value;

                net.PostData((httpResponse) =>
                {
                    try
                    {
                        using (System.IO.StreamReader httpwebStreamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            // ProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            // LayoutRoot.IsTapEnabled = true;
                            var re = httpwebStreamReader.ReadToEnd();
                            //login response
                            List<Attendance> attendancelist = JsonConvert.DeserializeObject<List<Attendance>>(re);

                            if (attendancelist.Count>0)
                            {
                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {

                                    ProgressBar1.IsIndeterminate = false; //for progress bar
                                    ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                                    ListBoxAttendanceList.ItemsSource = attendancelist;

                                });


                            }
                            else
                            {

                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    MessageDialog dlg = new MessageDialog("Could not get attendance list");
                                    dlg.ShowAsync();
                                });


                            }

                            //Check for result code..
                        }
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }
                }, "GetAttendanceDetails?about", postData);
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
