using DVlD.GenrelClass;
using DVlD.Properties;
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
using TheArtOfDevHtmlRenderer.Adapters;
using static DVLD_business.clsTestType;

namespace DVlD.Tests.Applintment
{
    public partial class frmTakeAppointment : Form
    {

        //stop in get way to now next test


        private int _localDrivingLicenseApplicationID;
        private int _testAppointmentID;

        private clsTestType.enTestType _TestTypeID = clsTestType.enTestType.VisionTest;


        public frmTakeAppointment(int localDrivingLicenseApplicationID,
            clsTestType.enTestType TestTypeID,int AppointmentID=-1)

        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _TestTypeID = TestTypeID;
            _testAppointmentID = AppointmentID;

            
        }

        private void frmTakeAppointment_Load(object sender, EventArgs e)
        {


           

            ucTakeAppointment1.TestTypeID = _TestTypeID;
            ucTakeAppointment1.LoadInfo(_localDrivingLicenseApplicationID, _testAppointmentID);
        
    }

        private void ucTakeAppointment1_Load(object sender, EventArgs e)
        {

        }
    }
}
