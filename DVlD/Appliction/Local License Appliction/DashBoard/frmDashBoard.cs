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

namespace DVlD.DashBoard
{
    public partial class frmDashBoard : Form
    {
        public frmDashBoard()
        {
            InitializeComponent();
        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            labPeople.Text=clsPerson.Count().ToString();
            labDriver.Text=clsDrivers.Count().ToString();   
            labUser.Text=clsUser.Count().ToString();
            labPaymetn.Text=clsPayment.Count().ToString();
            labLiscense.Text=clsLicenses.Count().ToString(); 
            labTestApplintmetn.Text=clsTestAppointments.Count().ToString();
           // labAppliction.Text=clsApplicationss.cou().ToString();  
            labDetatend.Text=clsDetainedLicenses.Count().ToString();    

        }

    }
}
