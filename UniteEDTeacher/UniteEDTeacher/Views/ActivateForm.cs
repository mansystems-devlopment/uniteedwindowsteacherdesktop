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
        string SmartLink = "Smartlink";
        string Reader = "e-reader";
        string Mybooks = "books";
        string ClassRoom = "ClassRoom";
        string MyCourses = "moodle";
        string Cloudbanc = "Cloudbanc";

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
                                    goto ModuleStatus;
                                ModuleStatus:
                                    ModuleStatus(net); 

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
                            MessageBox.Show(ex.Message);


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

        private void ModuleStatus(UniteEDNetwork net)
        {
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {
                SettingDataSource sd = new SettingDataSource();
                foreach (ActivationModule am in sd.ActivationModules)
                {
                    
                    string Module = am.ModuleName;

                    string Uid = Helpers.LoadJSONSettings("UserID");
                    string postData = "UserID=";
                    postData += Uid.ToString() + "&ModuleName=";
                    postData += Module + "&AppPackName=";
                    postData += Constant.appPackName;

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

                                    dynamic x = Newtonsoft.Json.JsonConvert.DeserializeObject(re);
                                    var OutAppStatus_OutApps = x.OutAppStatus_OutApps;
                                    foreach (var item in OutAppStatus_OutApps)
                                    {
                                        var Active = item.Active;
                                        var ModuleName = item.ModuleName;

                                        string ModuleData = ModuleName + ' ' + Active;

                                        if (ModuleData.Contains(SmartLink.ToString()))
                                        {
                                            UniteEDTeacher.Properties.Settings.Default.SmartLink = Active;
                                            UniteEDTeacher.Properties.Settings.Default.Save();
                                        }
                                        if (ModuleData.Contains(Mybooks.ToString()))
                                        {
                                            UniteEDTeacher.Properties.Settings.Default.MyBooks = Active;
                                            UniteEDTeacher.Properties.Settings.Default.Save();
                                        }
                                        if (ModuleData.Contains(Reader.ToString()))
                                        {
                                            UniteEDTeacher.Properties.Settings.Default.Reader = Active;
                                            UniteEDTeacher.Properties.Settings.Default.Save();
                                        }
                                        //if (ModuleData.Contains(Media.ToString()))
                                        //{
                                        //    UniteEDTeacher.Properties.Settings.Default.Media = Active;
                                        //    UniteEDTeacher.Properties.Settings.Default.Save();
                                        //}
                                        if (ModuleData.Contains(Cloudbanc.ToString()))
                                        {
                                            UniteEDTeacher.Properties.Settings.Default.Cloudbanc = Active;
                                            UniteEDTeacher.Properties.Settings.Default.Save();
                                        }
                                        if (ModuleData.Contains(ClassRoom.ToString()))
                                        {
                                            UniteEDTeacher.Properties.Settings.Default.Classroom = Active;
                                            UniteEDTeacher.Properties.Settings.Default.Save();
                                        }
                                        if (ModuleData.Contains(MyCourses.ToString()))
                                        {
                                            UniteEDTeacher.Properties.Settings.Default.Courses = Active;
                                            UniteEDTeacher.Properties.Settings.Default.Save();
                                        }

                                    }

                                }

                            }
                        }
                        catch (Exception ex)
                        {

                            Debug.WriteLine(ex.Message + "\n" + ex.StackTrace);
                            MessageBox.Show(ex.Message);
                        }
                    }, "GetAppModuleSettings?about", postData);

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
