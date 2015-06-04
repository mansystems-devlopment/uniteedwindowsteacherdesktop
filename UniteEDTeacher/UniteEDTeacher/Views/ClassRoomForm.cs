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
    public partial class ClassRoomForm : BaseForm
    {
        public ClassRoomForm()
        {
            InitializeComponent();


            ActivationModule classRoomModule = new ActivationModule();
            classRoomModule.ModuleName = "ClassRoom";
            classRoomModule.ModuleList_Setting = Helpers.LoadModuleSettings(classRoomModule.ModuleName);

            String classRoomUrl = "";

            foreach (ModuleSetting moduleSetting in classRoomModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("ClassRoomUrl"))
                {
                    classRoomUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                UniteEDNetwork net = new UniteEDNetwork();
                classRoomwebBrowser.Navigate(new Uri(classRoomUrl));

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
