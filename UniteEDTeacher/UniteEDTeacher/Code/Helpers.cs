//using Newtonsoft.Json;
using UniteEDTeacher.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
//using Windows.Networking;
//using Windows.Networking.Connectivity;
//using Windows.Storage;
//using Windows.UI.Xaml.Media.Imaging;

namespace UniteEDTeacher.Code
{
    static class Helpers
    {
        public static void SaveSettings(ApplicationDataContainer localSettings, string settingName, string settingValue)
        {
            
            if (!localSettings.Containers.ContainsKey("AppSettings"))
            {
                localSettings = localSettings.CreateContainer("AppSettings", Windows.Storage.ApplicationDataCreateDisposition.Always);
                Debug.WriteLine("New Container created");
            }
            else
            {
                Debug.WriteLine("Container already existed");
            }
            if (localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (localSettings.Containers["AppSettings"].Values.ContainsKey(settingName))
                    localSettings.Containers["AppSettings"].Values[settingName] = settingValue;
                else
                    localSettings.Containers["AppSettings"].Values.Add(settingName, settingValue);
            }
        }

        public static void SaveModuleSettings(ApplicationDataContainer localSettings, string settingName, List<ModuleSetting> setting)
        {
            if (!localSettings.Containers.ContainsKey("AppSettings"))
            {
                localSettings = localSettings.CreateContainer("AppSettings", Windows.Storage.ApplicationDataCreateDisposition.Always);
                Debug.WriteLine("New Container created");
            }
            else
            {
                Debug.WriteLine("Container already existed");
            }
            if (localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (localSettings.Containers["AppSettings"].Values.ContainsKey(settingName))
                    localSettings.Containers["AppSettings"].Values[settingName] = setting;
                else
                    localSettings.Containers["AppSettings"].Values.Add(settingName, setting);
            }
        }

        public static List<ModuleSetting> LoadModuleSettings(ApplicationDataContainer localSettings, string settingName)
        {
            List<ModuleSetting> lms = new List<ModuleSetting>();
            if (localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (localSettings.Containers["AppSettings"].Values.ContainsKey(settingName))
                {
                    string json = (String)(localSettings.Containers["AppSettings"].Values[settingName]);
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
                else
                    return null;
            }
            else {
                return null;
            }
        }
        public static String LoadJSONSettings(ApplicationDataContainer localSettings, string settingName)
        {
            if (localSettings.Containers.ContainsKey("AppSettings"))
            {
                if (localSettings.Containers["AppSettings"].Values.ContainsKey(settingName))
                    return (String)(localSettings.Containers["AppSettings"].Values[settingName]);
                else
                    return null;
            }
            else
            {
                return null;
            }
        }
        private static BitmapSource qrcode { set;get; }
        private static async void genQRCode(String text)
        {

            try
            {
                BitmapSource x = await TCD.Device.Camera.Barcodes.Encoder.GenerateQRCodeAsync(text, 300);
                qrcode = x;
            }
            catch (Exception ex)
            { 
            
            }

        
        }
        public static BitmapSource getQRCode(String text) {

            genQRCode(text);
            return qrcode;

        }

        //Dns.getHostAddresses
        public static HostName getIPAddress()
        {

            IReadOnlyList<HostName> hosts = NetworkInformation.GetHostNames();
            foreach (HostName aName in hosts)
            {
                if (aName.Type == HostNameType.Ipv4)
                {
                    return aName;
                }
            }
            return null;

        }
    }
}
