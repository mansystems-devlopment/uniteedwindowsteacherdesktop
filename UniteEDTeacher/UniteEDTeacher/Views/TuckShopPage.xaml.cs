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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace UniteEDTeacher.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TuckShopPage : UniteEDTeacher.Common.LayoutAwarePage
    {
        ApplicationDataContainer settings;

        public TuckShopPage()
        {
            this.InitializeComponent();
            settings = Windows.Storage.ApplicationData.Current.LocalSettings;

            //string url = "ohttp://theridgetuckshop.co.za/index.php";
            //WebviewMyCourses.Navigate(new Uri(url));

        }

        protected async override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
        {
 

        }

        async protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ActivationModule SchoolAccountActivationModule = new ActivationModule();
            SchoolAccountActivationModule.ModuleName = "School";
            SchoolAccountActivationModule.ModuleList_Setting = Helpers.LoadModuleSettings(settings, SchoolAccountActivationModule.ModuleName);

            String tuckUrl = "";
            String sportUrl = "";
            String schoolUrl = "";


            foreach (ModuleSetting schoolSetting in SchoolAccountActivationModule.ModuleList_Setting)
            {

                if (schoolSetting.SettingName.Equals("tuckUrl"))
                {
                    tuckUrl = schoolSetting.SettingData;
                }
                if (schoolSetting.SettingName.Equals("sportUrl"))
                {
                    sportUrl = schoolSetting.SettingData;
                }
                if (schoolSetting.SettingName.Equals("schoolUrl"))
                {
                    schoolUrl = schoolSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                UniteEDNetwork net = new UniteEDNetwork();
                string postData = "wantsurl=";
                postData += tuckUrl;

                net.PostMoodleData((httpResponse) =>
                {
                    try
                    {
                        using (System.IO.StreamReader httpwebStreamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            // ProgressBar.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
                            // LayoutRoot.IsTapEnabled = true;
                            //var re = httpwebStreamReader.ReadToEnd();
                            //login response
                            //string cleanstr = re.ToString().TrimStart();
                            //cleanstr = cleanstr.TrimEnd();

                           Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                            {
                                //WebviewMyCourses.NavigateToString(tuckUrl);
                                WebviewMyCourses.Navigate(new Uri(tuckUrl));
                            });




                            //Check for result code..
                        }
                    }
                    catch (Exception ex)
                    {

                        Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                    }
                }, tuckUrl + "?about", postData);
            }
            else
            {
                MessageDialog dlg = new MessageDialog("no internet connection");
                await dlg.ShowAsync();
            }

        }
        protected override void SaveState(Dictionary<String, Object> pageState)
        {

        } 

    }
}
