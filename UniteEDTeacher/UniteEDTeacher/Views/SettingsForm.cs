using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher.Views
{
    public partial class SettingsForm : Form
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void SettingsForm_Load(object sender, EventArgs e)
        {
            SettingDataSource sd = new SettingDataSource();
            ColumnHeader ch1 = new ColumnHeader();
            ch1.Text = "Setting Name";

            ColumnHeader ch2 = new ColumnHeader();
            ch1.Text = "Setting Data";

            foreach (ActivationModule am in sd.ActivationModules)
            {
                ListViewGroup lvg = new ListViewGroup(am.ModuleName, am.ModuleName);
                
              
                listView1.Groups.Add(lvg);

                foreach (ModuleSetting ms in am.ModuleList_Setting)
                {

                    ListViewItem row = new ListViewItem("");
                    ListViewItem.ListViewSubItem lvsub1 = new ListViewItem.ListViewSubItem(row, ms.SettingName);
                    ListViewItem.ListViewSubItem lvsub2 = new ListViewItem.ListViewSubItem(row, ms.SettingData);
                    row.SubItems.AddRange(new ListViewItem.ListViewSubItem[] { lvsub1, lvsub2 });
                    row.Group = lvg;

                    listView1.Items.Add(row);

                }

            }
        }
    }
}
