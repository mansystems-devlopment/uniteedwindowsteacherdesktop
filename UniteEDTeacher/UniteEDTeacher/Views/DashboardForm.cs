using CefSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Code;
using UniteEDTeacher.Serialization;

namespace UniteEDTeacher.Views
{
    public partial class DashboardForm : Form
    {
        public DashboardForm()
        {


            InitializeComponent();
        }

        private void btnMyCourses_Click(object sender, EventArgs e)
        {
            MyCourses myCourses = new MyCourses("");
            
            myCourses.Show();

        }

        private void btnBookStore_Click(object sender, EventArgs e)
        {
            BookStoreForm myForm = new BookStoreForm();

            myForm.Show();
        }

        private void btnEReader_Click(object sender, EventArgs e)
        {
            ActivationModule EreaderModule = new ActivationModule();
            EreaderModule.ModuleName = "e-reader";
            EreaderModule.ModuleList_Setting = Helpers.LoadModuleSettings(EreaderModule.ModuleName);

            String EreaderUrl = "";
            String EreaderName = "";

            foreach (ModuleSetting moduleSetting in EreaderModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("DownloadURL"))
                {
                    EreaderUrl = moduleSetting.SettingData;
                }
                if (moduleSetting.SettingName.Equals("Name"))
                {
                    EreaderName = moduleSetting.SettingData;
                }
            }

            if (Helpers.checkInstalled(EreaderName))
            {

                try
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files\Snapplify\"+EreaderName);
                }
                catch
                {

                    try
                    {
                        System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Snapplify\" + EreaderName);
                    }
                    catch {
                        MessageBox.Show("There was an Error Opening the application", "Open Snapplify", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {

                MessageBox.Show(EreaderName +" was not found on your PC. Click Ok to Download. Once you have installed "+ EreaderName+", Click on Ereader again" , "Open "+EreaderName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (NetworkInterface.GetIsNetworkAvailable() == true)
                {

                    UniteEDNetwork net = new UniteEDNetwork();

                    WebBrowser web = new WebBrowser();
                    web.ScriptErrorsSuppressed = true;

                    web.Navigate(new Uri(EreaderUrl));

                }
                else
                {
                    MessageBox.Show("Could not connect to internet", "Network connection", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
        }

        private void btnClassRoom_Click(object sender, EventArgs e)
        {
            ActivationModule classRoomModule = new ActivationModule();
            classRoomModule.ModuleName = "ClassRoom";
            classRoomModule.ModuleList_Setting = Helpers.LoadModuleSettings(classRoomModule.ModuleName);

            String ClassRoomName = "";

            foreach (ModuleSetting moduleSetting in classRoomModule.ModuleList_Setting)
            {

                if (moduleSetting.SettingName.Equals("Name"))
                {
                    ClassRoomName = moduleSetting.SettingData;
                }
            }            

            if (Helpers.checkInstalled(ClassRoomName))
            {

                try
                {
                    System.Diagnostics.Process.Start(@"C:\Program Files\Mythware\Classroom Management by Mythware\" + ClassRoomName);
                }
                catch
                {

                    try
                    {
                        System.Diagnostics.Process.Start(@"C:\Program Files (x86)\Mythware\Classroom Management by Mythware\" + ClassRoomName);
                    }
                    catch
                    {
                        MessageBox.Show("There was an Error Opening the application", "Open ClassRoom", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {

                MessageBox.Show(ClassRoomName + " was not found on your PC. Click Ok to Download. Once you have installed " + ClassRoomName + ", Click on ClassRoom again", "Open " + ClassRoomName, MessageBoxButtons.OK, MessageBoxIcon.Information);

                new ClassRoomForm().Show();
            }
            
        }

        private void btnSmartLink_Click(object sender, EventArgs e)
        {
            SmartLinkForm myForm = new SmartLinkForm();

            myForm.Show();
        }

        private void btnCloudbanc_Click(object sender, EventArgs e)
        {
            CloudbancForm Cloudbanc = new CloudbancForm();

            Cloudbanc.Show();
        }

        

        private void btnMedia_Click(object sender, EventArgs e)
        {
            MediaForm myForm = new MediaForm();

            myForm.Show();
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            Cef.Shutdown();
            Application.Exit();
        }

        private void changeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ActivateForm().Show();
            this.Hide();
        }

        private void teacherOnlineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new TeacherOnlineForm().Show();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new SettingsForm().Show();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().Show();
        }

        private void DashboardForm_Load(object sender, EventArgs e)
        {

        }

       
    }
}
