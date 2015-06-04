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
    public partial class BookStoreForm : BaseForm
    {
        public BookStoreForm()
        {
            InitializeComponent();


            ActivationModule bookStoreModule = new ActivationModule();
            bookStoreModule.ModuleName = "books";
            bookStoreModule.ModuleList_Setting = Helpers.LoadModuleSettings(bookStoreModule.ModuleName);

            String bookStoreUrl = "";

            StorewebBrowser.ScriptErrorsSuppressed = true;

            foreach (ModuleSetting moduleSetting in bookStoreModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("bookStoreUrl"))
                {
                    bookStoreUrl = moduleSetting.SettingData;
                }
            }
            if (NetworkInterface.GetIsNetworkAvailable() == true)
            {

                UniteEDNetwork net = new UniteEDNetwork();
                StorewebBrowser.Navigate(new Uri(bookStoreUrl));

            }
            else
            {
                MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        }
    }
}
