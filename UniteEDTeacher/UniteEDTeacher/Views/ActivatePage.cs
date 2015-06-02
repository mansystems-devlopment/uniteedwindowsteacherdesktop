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
using System.Net;
using System.Net.Sockets;
using System.IO;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Windows.Threading;
using System.Runtime.InteropServices;

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
            //this.Close();

            DashboardForm dashboardPage = new DashboardForm();
            dashboardPage.Show();
            this.Hide();  
        }


    }
}
