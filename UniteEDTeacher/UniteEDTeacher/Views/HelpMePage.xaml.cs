using Newtonsoft.Json;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;
using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
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
    public sealed partial class HelpMePage : UniteEDTeacher.Common.LayoutAwarePage
    {

        #region Properties

        string userId = "";
        string selfCareType = "";
        string product = "";
        string description = "";
        string token = "";
        string clientId = "";

        #endregion
        public HelpMePage()
        {
            this.InitializeComponent();
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;
            rootFrame.Navigate(typeof(SelfCareMenuPage));
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (lpIHave.SelectedItem.ToString() != "" && lpWithMy.SelectedItem.ToString() != "" && txtDescription.Text != "" && txtDescription.Text != "")
            {

                DateTime dateTime = DateTime.UtcNow;
                selfCareType = lpIHave.SelectedItem.ToString();
                product = lpWithMy.SelectedItem.ToString();
                description = txtDescription.Text;
                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    string postData = "aid=" + "&uid=" + userId + "&dt=" + dateTime + "&sctp=" + selfCareType + "&prd=" + product;
                    postData += "&dsc=" + description + "&tk=" + token + "&cid=" + clientId;
                    postData += Constant.appVersion + "&apn=";
                    postData += Constant.appPackName + "&dm=";
                    postData += Constant.deviceModel + "&im=";
                    postData += Constant.IMEI + "&dmf=";
                    postData += "&dsn=";

                    net.PostTicket((httpResponse) =>
                    {
                        try
                        {
                            using (System.IO.StreamReader httpwebStreamReader = new StreamReader(httpResponse.GetResponseStream()))
                            {
                                var re = httpwebStreamReader.ReadToEnd();
                                ActivationResponse response = JsonConvert.DeserializeObject<ActivationResponse>(re);
                                if (response.ResultCode.Equals("0"))
                                {
                                    Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
                                    {
                                        Ticket objTicket = new Ticket();
                                        objTicket.UserId = userId;
                                        objTicket.DateIssued = dateTime.ToString();
                                        objTicket.IHave = selfCareType;
                                        objTicket.WithMy = product;
                                        objTicket.Description = description;
                                        objTicket.TicketNumber = response.TicketNumber;

                                        AddTicket(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, "SapientWindows.sqlite"), objTicket);
                                        MessageDialog dlg = new MessageDialog("Your ticket is submitted.Ticket Number is " + response.TicketNumber);

                                        Frame rootFrame = Window.Current.Content as Frame;
                                        rootFrame.Navigate(typeof(MyTicketsPage));
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
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageDialog dlg = new MessageDialog(ex.Message + "\n" + ex.StackTrace);
                        }

                    }, "Rest_Logging?about", postData);
                }
                else
                {
                    MessageDialog dlg = new MessageDialog("no internet connection");
                }
            }
            else
            {
                if (lpIHave.SelectedItem.ToString() == "")
                {
                    MessageDialog dlg = new MessageDialog("please enter  - description");
                }
                else if (lpWithMy.SelectedItem.ToString() == "")
                {
                    MessageDialog dlg = new MessageDialog("please select - with my");
                }
                else if (txtDescription.Text == "")
                {
                    MessageDialog dlg = new MessageDialog("please enter  - description");
                }

            }
        }
        DateTime dt = DateTime.UtcNow;
        private async void AddTicket(string p, Ticket objTicket)
        {
            var path = ApplicationData.Current.LocalFolder.Path + @"\SapientWindows.sqlite";
            var db = new SQLiteAsyncConnection(path);
            var data = new Ticket
            {

                DateIssued = dt.ToString(),
                IHave = lpIHave.SelectedItem.ToString(),
                WithMy = lpWithMy.SelectedItem.ToString(),
                Description = txtDescription.Text

            };
            int x = await db.InsertAsync(data);
        }

        private void txtDesscription_GotFocus(object sender, RoutedEventArgs e)
        {
            txtDescription.Text = "";
        }
    }
}
