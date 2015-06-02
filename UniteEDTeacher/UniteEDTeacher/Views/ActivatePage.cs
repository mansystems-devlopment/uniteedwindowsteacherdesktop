using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Views;
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Threading;
using System.Runtime.InteropServices;
using System.Windows.Media;
using System.Net.NetworkInformation;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher
{
    public partial class ActivatePage : Form
    {
        SolidColorBrush brushBlack = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushGrey = new SolidColorBrush(Colors.LightGray);
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);

        public ActivatePage()
        {
            InitializeComponent();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                if (txtUserid.Text != "")
                {
                    UniteEDNetwork net = new UniteEDNetwork();

                    string postData = "aid=";
                    postData += Constant.appId + "&uid=";
                    postData += txtUserid.Text + "&cno=";
                    postData += "Windows no cell" + "&av=";
                    postData += Constant.appVersion + "&apn=";
                    postData += Constant.appPackName + "&dm=";
                    postData += Constant.deviceModel + "&im=";
                    postData += Constant.IMEI + "&dmf=";
                    postData += Constant.deviceManufacturer + "&dos=";
                    postData += Constant.deviceOS + "&cr=";
                    postData += Constant.carrier + "&cc=";
                    postData += Constant.countryCode;
                    postData += "&dsn=";
                    postData += Constant.deviceSerialNumber;

                    Form frm = this;

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
                                    MessageBox.Show(response.ResultMessage, "Activation", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                    Helpers.SaveSettings("UserID", txtUserid.Text);

                                    foreach (ActivationModule module in response.OutActivateUser_ModuleList)
                                    {
                                        Helpers.SaveSettings(module.ModuleName, JsonConvert.SerializeObject(module.ModuleList_Setting));

                                    }

                                    this.Invoke(
                                        (Action)(() =>
                                            {

                                                frm.Hide();
                                                DashboardForm dashboardPage = new DashboardForm();
                                                dashboardPage.Show();
                                            }

                                        ));



                                }
                                else
                                {
                                    MessageBox.Show(response.ResultMessage, "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

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

                    MessageBox.Show("Please Enter the user ID", "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }

        }

        private void ActivatePage_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }


    }
}
