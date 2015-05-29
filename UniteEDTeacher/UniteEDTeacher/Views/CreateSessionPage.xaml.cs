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
    public sealed partial class CreateSessionPage : UniteEDTeacher.Common.LayoutAwarePage
    {

        ApplicationDataContainer settings;

        public CreateSessionPage()
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
                string postData = "User_ID=";
                postData += Helpers.LoadJSONSettings(settings,"Login_Username") + "&FullName=";
                postData += Helpers.LoadJSONSettings(settings, "Login_FirstName") + " " + Helpers.LoadJSONSettings(settings, "Login_SurnName");

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
                            TeacherInfo teacherInfo = JsonConvert.DeserializeObject<TeacherInfo>(re);

                            if (teacherInfo.ResultCode.Equals("0"))
                            {
                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {


                                    Helpers.SaveSettings(settings, "TeacherInfo_LevelInfo", JsonConvert.SerializeObject(teacherInfo.OutTeacherInfo_Out_Level));
                                    Helpers.SaveSettings(settings, "TeacherInfo_SubjectInfo", JsonConvert.SerializeObject(teacherInfo.OutTeacherInfo_OutSubjects));
                                    Helpers.SaveSettings(settings, "TeacherInfo_ClassInfo", JsonConvert.SerializeObject(teacherInfo.OutTeacherInfo_OutClasses));
                                    Helpers.SaveSettings(settings, "TeacherInfo_Class", JsonConvert.SerializeObject(teacherInfo.Class_));

                                    SubjectList.ItemsSource = teacherInfo.OutTeacherInfo_OutSubjects;
                                    GradeList.ItemsSource = teacherInfo.OutTeacherInfo_Out_Level;
                                    ClassList.ItemsSource = teacherInfo.OutTeacherInfo_OutClasses;

                                    ProgressBar1.IsIndeterminate = false; //for progress bar
                                    ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                                  
                                });


                            }
                            else
                            {

                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    MessageDialog dlg = new MessageDialog(teacherInfo.ResultMessage);
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
                }, "GetTeacherInfo?about", postData);
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

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                UniteEDNetwork net = new UniteEDNetwork();
                //TODO get saved values
                string postData = "TeacherID=";
                postData += Helpers.LoadJSONSettings(settings, "Login_Username") + "&Subject=";
                postData += ((SubjectInfo)SubjectList.SelectedItem).SubjectName + "&Level=";
                postData += ((LevelInfo)GradeList.SelectedItem).Name + "&Class_=";
                postData += ((ClassInfo)ClassList.SelectedItem).ClassName;

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
                            Session session = JsonConvert.DeserializeObject<Session>(re);

                            if (session.ResultCode.Equals("0"))
                            {
                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {


                                    Helpers.SaveSettings(settings, "TeacherInfo_Session", JsonConvert.SerializeObject(Regex.Match(session.SessionID, @"\d+").Value));

                                    Frame rootFrame = Window.Current.Content as Frame;
                                    rootFrame.Navigate(typeof(RecordAttendancePage));

                                    ProgressBar1.IsIndeterminate = false; //for progress bar
                                    ProgressBar1.Visibility = Windows.UI.Xaml.Visibility.Collapsed;

                                });


                            }
                            else
                            {

                                Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                {
                                    MessageDialog dlg = new MessageDialog(session.ResultMessage);
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
                }, "CreateClassSession?about", postData);
            }
            else
            {
                MessageDialog dlg = new MessageDialog("no internet connection");
                await dlg.ShowAsync();
            }
        }
    }
}
