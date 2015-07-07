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
using System.Management;
using System.Globalization;

namespace UniteEDTeacher
{
    public partial class ActivateForm : Form
    {
        SolidColorBrush brushBlack = new SolidColorBrush(Colors.Black);
        SolidColorBrush brushGrey = new SolidColorBrush(Colors.LightGray);
        SolidColorBrush brushRed = new SolidColorBrush(Colors.Red);
        public ActivateForm()
        {
            InitializeComponent();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            string deviceManufacturer = "";
            string deviceModel = "";
            string deviceSerialNumber = "";
            var c = new CultureInfo("en-GB");
            var r = new RegionInfo(c.LCID);

            
            // create management class object
            ManagementClass mc = new ManagementClass("Win32_ComputerSystem");
            //collection to store all management objects
            ManagementObjectCollection moc = mc.GetInstances();
            if (moc.Count != 0)
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    // display general system information
                    deviceManufacturer = mo["Manufacturer"].ToString();
                    deviceModel = mo["Model"].ToString();
                }
            }

            // create management class object
            mc = new ManagementClass("Win32_BaseBoard");
            //collection to store all management objects
            moc = mc.GetInstances();
            if (moc.Count != 0)
            {
                foreach (ManagementObject mo in mc.GetInstances())
                {
                    deviceSerialNumber = mo["SerialNumber"].ToString();
                }
            }

            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                if (txtUserid.Text != "")
                {
                    pictureBox2.Visible = true;
                    btnActivate.Visible = false;
                    txtUserid.Enabled = false;

                    UniteEDNetwork net = new UniteEDNetwork();

                    string postData = "AppID=";
                    postData += Constant.appId + "&UserID=";
                    postData += txtUserid.Text + "&CellNumber=";
                    postData += "" + "&AppVersion=";
                    postData += Constant.appVersion + "&AppPackName=";
                    postData += Constant.appPackName + "&DeviceModel=";
                    postData += deviceModel + "&IMEI=";
                    postData += Helpers.getUniqueDeviceID() + "&DeviceManufacturer=";
                    postData += deviceManufacturer + "&DeviceOS=";
                    postData += Environment.OSVersion.ToString() + "&Carrier=";
                    postData += "(No ISP)" + "&CountryCode=";
                    postData += "(No Country Code)";
                    postData += "&DeviceSerialNumber=";
                    postData += deviceSerialNumber;

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
                                if (response.ResultCode.Equals("0") || response.ResultCode.Equals("200"))
                                {
                                    

                                    Helpers.SaveSettings("UserID", txtUserid.Text);

                                    Helpers.SaveSettings("AllModuleSetting", JsonConvert.SerializeObject(response.OutActivateUser_ModuleList));

                                    String smartLinkPassword = "";
                                    
                                    foreach (ActivationModule module in response.OutActivateUser_ModuleList)
                                    {
                                        foreach (ModuleSetting moduleSetting in module.ModuleList_Setting)
                                        {
                                            if (moduleSetting.SettingName.Equals("SmartLinkpassword"))
                                            {
                                                smartLinkPassword = moduleSetting.SettingData.ToString();
                                            }
                                        }
                                        
                                        Helpers.SaveSettings(module.ModuleName, JsonConvert.SerializeObject(module.ModuleList_Setting));

                                    }

                                    this.Invoke(
                                        (Action)(() =>
                                            {
                                                UniteEDTeacher.Properties.Settings.Default.activated = true;
                                                UniteEDTeacher.Properties.Settings.Default.Save();

                                                frm.Hide();
                                                DashboardForm dashboardPage = new DashboardForm();
                                                dashboardPage.Show();
                                            }

                                        ));

                                    MessageBox.Show("Your password is '" + smartLinkPassword + "' and can be found in your settings.", "Successfully activated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                }
                                else
                                {
                                    MessageBox.Show(response.ResultMessage, "Activation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                    pictureBox2.Visible = false;
                                    btnActivate.Visible = true;
                                    txtUserid.Enabled = true;
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
