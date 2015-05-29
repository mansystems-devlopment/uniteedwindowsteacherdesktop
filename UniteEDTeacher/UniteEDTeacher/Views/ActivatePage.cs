using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using UniteEDTeacher.Views;

namespace UniteEDTeacher
{
    public partial class ActivatePage : Form
    {
        public ActivatePage()
        {
            InitializeComponent();
        }

        private void btnActivate_Click(object sender, EventArgs e)
        {
            DashboardPage Dashboard = new DashboardPage();
            Dashboard.Show();
            this.Close();

        }

        private void txtUserId_TextChanged(object sender, EventArgs e)
        {
            txtUserId.Text = "";
        }
    }
}
