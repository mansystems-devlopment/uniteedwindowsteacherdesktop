using Newtonsoft.Json;
using SapientTeacher.Code;
using SapientTeacher.Serialization;
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

namespace SapientTeacher.Views
{


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogIn : Page
    {

        SolidColorBrush brushBlack = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushGrey = new SolidColorBrush(Color.FromArgb(0xff, 0x9C, 0x9A, 0x9A));
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);

         ApplicationDataContainer settings;


        public LogIn()
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

        private void txtPin_GotFocus(object sender, RoutedEventArgs e)
        {
            txtPinWatermark.Opacity = 0;
            txtPin.Opacity = 100;
        }

        private void txtPin_LostFocus(object sender, RoutedEventArgs e)
        {
            if (txtPinWatermark.Text.Equals("PIN"))
            {

                //  txtPin.Foreground = brushGrey;
                txtPinWatermark.BorderBrush = brushRed;
                txtPin.BorderBrush = brushRed;
            }
            else
            {
                txtPinWatermark.BorderBrush = brushGrey;
                txtPin.BorderBrush = brushGrey;
            }
            ChecktxtPinWatermark();
        }

        public void ChecktxtPinWatermark()
        {
            var passwordEmpty = string.IsNullOrEmpty(txtPin.Password);
            txtPinWatermark.Opacity = passwordEmpty ? 100 : 0;
            txtPin.Opacity = passwordEmpty ? 0 : 100;
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtPin.Password != "" && txtPin.Password != "PIN")
            {
                if (chboxKeepLogin.IsChecked == true)
                {
                    settings.Containers["AppSettings"].Values["remember_pass"] = txtPin.Password;
                }
                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    ProgressBar.Visibility=Windows.UI.Xaml.Visibility.Visible;
                    SapientNetwork net = new SapientNetwork();
                 
                    string postData = "uid=";
                    postData += txtUserid.Text + "&pw=";
                    postData += txtPin.Password + "&aid=";
                    postData += Constant.appId + "&apn=";
                    postData += Constant.appVersion+ "&av=";
                    postData += Constant.appVersion;

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
                                LoginResponse response = JsonConvert.DeserializeObject<LoginResponse>(re);


                                if (response.ResultCode.Equals("0"))
                                {
                                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                    {

                                        Helpers.SaveSettings(settings, "Login_FirstName", response.FirstName);
                                        Helpers.SaveSettings(settings, "Login_SurnName", response.Surname);
                                        Helpers.SaveSettings(settings, "Login_Username", txtUserid.Text);
                                        Helpers.SaveSettings(settings, "Login_Password", txtPin.Password); 

                                        Frame rootFrame = Window.Current.Content as Frame;
                                        rootFrame.Navigate(typeof(Dashboard));
                                    });
                                   
                                }
                                else {

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
                    }, "Authentication?about", postData);
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
                if (txtPin.Password == "" || txtPin.Password == "PIN")
                {
                    txtPin.BorderBrush = brushRed;
                    MessageDialog dlg = new MessageDialog("please enter - pin");
                    await dlg.ShowAsync();
                }
            }
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(Activate));
        }
    }
}
