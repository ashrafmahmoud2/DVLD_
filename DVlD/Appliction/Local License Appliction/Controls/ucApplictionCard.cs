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

namespace DVlD.Appliction
{
    public partial class ucApplictionCard : UserControl
    {
        public ucApplictionCard()
        {
            InitializeComponent();
        }

        private int _LocalDrivingLicebseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicebseApplication;

        public void LodeDate(int LocalDrivingLicebseApplicationID)
        {
            clsLocalDrivingLicenseApplication localDrivingLicenseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(LocalDrivingLicebseApplicationID);

            if(localDrivingLicenseApplication != null )
            {
                ucApplicationBasicInfo1.LodeApplictionInfo(localDrivingLicenseApplication.ApplicationID);
                lblLocalDrivingLicenseApplicationID.Text = localDrivingLicenseApplication.ToString();
                lblAppliedFor.Text = clsLicenseClasses.Find(localDrivingLicenseApplication.LicenseClassID).ClassName;
             //   lblPassedTests.Text = localDrivingLicenseApplication.
                   /// GetPassedTestCount().ToString() + "/3";
            }
            MessageBox.Show("No Application with ApplicationID = " + LocalDrivingLicebseApplicationID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);




        }

        private void ucApplicationBasicInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
