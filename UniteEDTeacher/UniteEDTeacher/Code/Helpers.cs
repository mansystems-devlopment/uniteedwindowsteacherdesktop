//using Newtonsoft.Json;
using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Media.Imaging;
using ZXing;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Management;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using Microsoft.Win32;
//using Windows.Networking;
//using Windows.Networking.Connectivity;
//using Windows.Storage;
//using Windows.UI.Xaml.Media.Imaging;

namespace UniteEDTeacher.Code
{
    static class Helpers
    {
        public static void SaveSettings(string settingName, string settingValue)
        {

            ModuleSetting lms = new ModuleSetting();
            lms.DEFAULT_FILENAME = settingName;
            lms.SettingName = settingName;
            lms.SettingData = settingValue;
            lms.Save();
        }

        public static void SaveModuleSettings(List<ModuleSetting> settings)
        {
            foreach (ModuleSetting setting in settings) {

                ModuleSetting lms = new ModuleSetting();
                lms.Save();
            }            
            
        }

        public static List<ModuleSetting> LoadModuleSettings(string settingName)
        {
            try
            {
                List<ModuleSetting> lms = new List<ModuleSetting>();


                string json = (ModuleSetting.Load(settingName)).SettingData;
                Newtonsoft.Json.Linq.JArray objs = (Newtonsoft.Json.Linq.JArray)JsonConvert.DeserializeObject(json);

                foreach (Newtonsoft.Json.Linq.JObject obj in objs)
                {
                    ModuleSetting ms = new ModuleSetting();
                    ms.SettingName = obj["SettingName"].ToString();
                    ms.SettingData = obj["SettingData"].ToString();
                    lms.Add(ms);
                }

                return (List<ModuleSetting>)(lms);
            }
            catch (Exception ex) {
                
                return null;
            }
                
           
            
        }
        public static String LoadJSONSettings(string settingName)
        {
            
                string json = (ModuleSetting.Load(settingName)).SettingData;
                return json;
           
        }

        private static Bitmap qrcode { set; get; }
        private static async void genQRCode(String text)
        {

            try
            {
                var writer = new BarcodeWriter();
                writer.Format = BarcodeFormat.QR_CODE;
                var result = writer.Write(text);
                var barcodeBitmap = new Bitmap(result);
                qrcode = barcodeBitmap;

            }
            catch (Exception ex)
            { 
            
            }

        
        }
        public static Bitmap getQRCode(String text)
        {

            genQRCode(text);
            return qrcode;

        }

        //Dns.getHostAddresses
        public static String getIPAddress()
        {

            string _IP = null;

            System.Net.IPHostEntry _IPHostEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());

            foreach (System.Net.IPAddress _IPAddress in _IPHostEntry.AddressList)
            {
                if (_IPAddress.AddressFamily.ToString() == "InterNetwork")
                {
                    _IP = _IPAddress.ToString();
                }
            }
            return _IP;

        }

        private static String getMACAddress()
        {

            var macAddr =
             (
                 from nic in NetworkInterface.GetAllNetworkInterfaces()
                 where nic.OperationalStatus == OperationalStatus.Up
                 select nic.GetPhysicalAddress().ToString()
             ).FirstOrDefault();

            return macAddr.ToString();

        }
        private static String getCPUID()
        {
            string cpuInfo = string.Empty;
            ManagementClass mc = new ManagementClass("win32_processor");
            ManagementObjectCollection moc = mc.GetInstances();

            foreach (ManagementObject mo in moc)
            {
                if (cpuInfo == "")
                {
                    //Get only the first CPU's ID
                    cpuInfo = mo.Properties["processorID"].Value.ToString();
                    break;
                }
            }
            return cpuInfo;

        }

        private static String getHardWareDriveID()
        {

            String drive = Path.GetPathRoot(Environment.SystemDirectory);

            drive = drive.Trim(new char[] { '\\' });
            drive = drive.Trim(new char[] { ':' });


            ManagementObject dsk = new ManagementObject(@"win32_logicaldisk.deviceid=""" + drive + @":""");
            dsk.Get();
            string volumeSerial = dsk["VolumeSerialNumber"].ToString();

            return volumeSerial;

        }

        public static String getUniqueDeviceID()
        {

            
                return getMACAddress() + getCPUID() + getHardWareDriveID();
            

        }

        public static bool checkInstalled(string c_name)
        {
            string displayName;

            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            RegistryKey key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(c_name))
                    {
                        return true;
                    }
                }
                key.Close();
            }

            registryKey = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key != null)
            {
                foreach (RegistryKey subkey in key.GetSubKeyNames().Select(keyName => key.OpenSubKey(keyName)))
                {
                    displayName = subkey.GetValue("DisplayName") as string;
                    if (displayName != null && displayName.Contains(c_name))
                    {
                        return true;
                    }
                }
                key.Close();
            }
            return false;
        }
    }
}
