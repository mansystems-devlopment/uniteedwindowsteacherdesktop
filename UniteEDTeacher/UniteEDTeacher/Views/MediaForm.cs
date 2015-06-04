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
    public partial class MediaForm : BaseForm
    {
        public MediaForm()
        {
            InitializeComponent();

            ActivationModule cloudBancMediaModule = new ActivationModule();
            cloudBancMediaModule.ModuleName = "media";
            cloudBancMediaModule.ModuleList_Setting = Helpers.LoadModuleSettings(cloudBancMediaModule.ModuleName);

            String cloudBancMediaUrl = "";

            foreach (ModuleSetting moduleSetting in cloudBancMediaModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("StoreURL"))
                {
                    cloudBancMediaUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                UniteEDNetwork net = new UniteEDNetwork();
                MediawebBrowser.Navigate(new Uri(cloudBancMediaUrl));

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
