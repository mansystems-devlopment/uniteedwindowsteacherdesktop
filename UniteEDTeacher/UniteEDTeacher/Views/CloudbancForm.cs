using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher.Views
{
    public partial class CloudbancForm : BaseForm
    {
        public CloudbancForm()
        {
            InitializeComponent();

            ActivationModule CloudbancModule = new ActivationModule();
            CloudbancModule.ModuleName = "Cloudbanc";
            CloudbancModule.ModuleList_Setting = Helpers.LoadModuleSettings(CloudbancModule.ModuleName);

            String cloudbancURL = "";


            foreach (ModuleSetting moduleSetting in CloudbancModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("cloudbancURL"))
                {
                    cloudbancURL = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                UniteEDNetwork net = new UniteEDNetwork();
                CloudbancwebBrowser.Navigate(new Uri(cloudbancURL));
               
            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }
    }
}
