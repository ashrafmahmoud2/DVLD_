using DVLD_business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.Dashboard
{
    public partial class frmDashboard : Form
    {
        public frmDashboard()
        {
            InitializeComponent();
        }

        private void guna2CustomGradientPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void guna2CustomGradientPanel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            lblPeopele.Text = clsPerson.Count().ToString();
            lblDriver.Text=clsDrivers.Count().ToString();
            lblLocalLicense.Text = clsLicense.GetAllLicenses().Columns.Count.ToString();
            lblinternationLicense.Text=clsInternationalLicenses.Count().ToString(); 
            lblPayment.Text=clsPayment.Count().ToString();
            lblUser.Text=clsUser.Count().ToString();
        }

        private void guna2CustomGradientPanel7_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
