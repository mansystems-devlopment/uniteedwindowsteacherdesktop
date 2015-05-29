using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniteEDTeacher.Views
{
    public partial class DashboardPage : Form
    {
        public DashboardPage()
        {
            InitializeComponent();
        }

        private void btnMyCourses_Click(object sender, EventArgs e)
        {
            MyCourseWebview frm = new MyCourseWebview();
            frm.Show();
            this.Close();
        }
    }
}
