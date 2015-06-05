using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher.Views
{
    public partial class SmartLinkForm : BaseForm
    {
        public SmartLinkForm()
        {
            InitializeComponent();


            ActivationModule smartLinkModule = new ActivationModule();
            smartLinkModule.ModuleName = "Smartlink";
            smartLinkModule.ModuleList_Setting = Helpers.LoadModuleSettings(smartLinkModule.ModuleName);

            String smartLinkUrl = "";

            foreach (ModuleSetting moduleSetting in smartLinkModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("SmartLinkUrl"))
                {
                    smartLinkUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                UniteEDNetwork net = new UniteEDNetwork();
                
                SmartLinkwebBrowser.Navigate(new Uri(smartLinkUrl));

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
