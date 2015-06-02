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
    }
}
