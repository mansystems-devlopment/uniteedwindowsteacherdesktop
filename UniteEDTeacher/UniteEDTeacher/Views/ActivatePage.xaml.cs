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
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI;
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
    public sealed partial class Activate : Page
    {

        string deviceManufacturer = "Lenovo";
        string deviceSerialNumber = "2d9090da-27b1-4797-a3aa-7d762f553ddd";
        string deviceOS = "Windows 8";
        //string carrier = "Reliance";
        //To get the carrier network information.
        string carrier = "(No Network)";
        string countryCode = "SA";
        string url = "(http://41.0.104.2:8081/rest/)";


        SolidColorBrush brushBlack = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushGrey = new SolidColorBrush(Color.FromArgb(0xff, 0x9C, 0x9A, 0x9A));
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);

        ApplicationDataContainer settings;

        public Activate()
        {
            this.InitializeComponent();
            settings = Windows.Storage.ApplicationData.Current.LocalSettings;
        }
        private void txtUserid_GotFocus(object sender, RoutedEventArgs e)
        {
            txtUseridWatermark.Opacity = 0;
            txtUserid.Opacity = 100;
        }

        private void txtUserid_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtUseridWatermark.Text.Equals("User ID"))
            {

                //  txtPin.Foreground = brushGrey;
                txtUseridWatermark.BorderBrush = brushRed;
                txtUserid.BorderBrush = brushRed;
            }
            else
            {
                txtUseridWatermark.BorderBrush = brushGrey;
                txtUserid.BorderBrush = brushGrey;
            }
            CheckUseridWatermark();
        }

        public void CheckUseridWatermark()
        {
            var passwordEmpty = string.IsNullOrEmpty(txtUserid.Text);
            txtUseridWatermark.Opacity = passwordEmpty ? 100 : 0;
            txtUserid.Opacity = passwordEmpty ? 0 : 100;
        }

        private async void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            if (txtUserid.Text != "")
            {
               
                
                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    string postData = "aid=";
                    postData += Constant.appId + "&uid=";
                    postData += txtUserid.Text +  "&cno=";
                    postData += "Windows no cell" + "&av=";
                    postData += Constant.appVersion + "&apn=";
                    postData += Constant.appPackName + "&dm=";
                    postData += Constant.deviceModel + "&im=";
                    postData += Constant.IMEI + "&dmf=";
                    postData += deviceManufacturer + "&dos=";
                    postData += deviceOS + "&cr=";
                    postData += carrier + "&cc=";
                    postData += countryCode;
                    postData += "&dsn=";
                    postData += deviceSerialNumber;

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
                                ActivationResponse response = JsonConvert.DeserializeObject<ActivationResponse>(re);
                                if (response.ResultCode.Equals("0"))
                                {
                                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                    {
                                        MessageDialog dlg = new MessageDialog(response.ResultMessage);
                                        dlg.ShowAsync();

                                        Helpers.SaveSettings(settings, "UserID", txtUserid.Text);

                                        foreach (ActivationModule module in response.OutActivateUser_ModuleList)
                                        {
                                            Helpers.SaveSettings(settings, module.ModuleName, JsonConvert.SerializeObject(module.ModuleList_Setting));

                                        }
                                        Frame rootFrame = Window.Current.Content as Frame;
                                        rootFrame.Navigate(typeof(Dashboard));

                                    });


                                }
                                else
                                {

                                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                    {
                                        MessageDialog dlg = new MessageDialog(response.ResultMessage);
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
                    }, "ActivateUser?about", postData);
                }
                else
                {
                    MessageDialog dlg = new MessageDialog("no internet connection");
                    await dlg.ShowAsync();
                }
            }
            else
            {
                /*if (txtUserId.Text == "" || txtUserId.Text == "User ID")
                {
                    txtUserId.BorderBrush = brushRed;
                    MessageBox.Show("please enter - User ID");
                }
                */
                if (txtUserid.Text == "")
                {
                    txtUserid.BorderBrush = brushRed;
                    MessageDialog dlg = new MessageDialog("please enter - User Id");
                    await dlg.ShowAsync();
                }
            }
        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Dashboard));
        }

    }
}
