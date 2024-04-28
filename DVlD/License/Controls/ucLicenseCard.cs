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

namespace DVlD.License
{
    public partial class ucLicenseCard : UserControl
    {
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private int _localDrivingLicenseApplicationID;
        public ucLicenseCard()
        {
            InitializeComponent();
        }
        public void LodeLicenseInfo(int localDrivingLicenseApplicationID)
        {
            _localDrivingLicenseApplicationID=localDrivingLicenseApplicationID;
            _localDrivingLicenseApplication=clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                (localDrivingLicenseApplicationID);

            lblLocalDrivingLicenseApplicationID.Text = localDrivingLicenseApplicationID.ToString();
         //   lblAppliedFor.Text = _localDrivingLicenseApplication.LicenseClassInfo.ClassName;
           // lblPassedTests.Text = clsTest.GetPassedTestCount(_localDrivingLicenseApplicationID).ToString();

            ucApplicationBasicInfo1.LodeApplictionInfo(_localDrivingLicenseApplication.ApplicationID);
        }
    }
}
