using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            
            myCourses.ShowDialog();

        }

        private void btnBookStore_Click(object sender, EventArgs e)
        {
            BookStoreForm myForm = new BookStoreForm();

            myForm.ShowDialog();
        }

        private void btnEReader_Click(object sender, EventArgs e)
        {
             /*
              *  read .exe for ereader;
              * */
        }

        private void btnClassRoom_Click(object sender, EventArgs e)
        {

        }

        private void btnSmartLink_Click(object sender, EventArgs e)
        {
            SmartLinkPage myForm = new SmartLinkPage();

            myForm.ShowDialog();
        }

        private void btnCloudbanc_Click(object sender, EventArgs e)
        {
            CloudbancForm Cloudbanc = new CloudbancForm();

            Cloudbanc.ShowDialog();
        }

        private void btnShop_Click(object sender, EventArgs e)
        {
            ShopForm myForm = new ShopForm();

            myForm.ShowDialog();
        }

        private void btnMedia_Click(object sender, EventArgs e)
        {
            MediaForm myForm = new MediaForm();

            myForm.ShowDialog();
        }

        private void DashboardForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        private void changeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new ActivateForm().Show();
            this.Hide();
        }

       
    }
}
