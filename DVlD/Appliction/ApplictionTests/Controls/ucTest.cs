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
using static DVLD_business.clsTestType;

namespace DVlD.Tests
{
    public partial class ucTest : UserControl
    {
        //stop in get way to now the num of test;
        private int _localDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private clsTestAppointment _testAppointments=new clsTestAppointment();
        private int _testAppointmentID = -1;
        private clsTest _tests;
        private clsTestType.enTestType _TestTypeID;


        public clsTestType.enTestType TestTypeID
        {
            get
            {
                return _TestTypeID;
            }
            set
            {
                _TestTypeID = value;

                switch (_TestTypeID)
                {

                    case clsTestType.enTestType.VisionTest:
                        {
                            lblTestType.Text = "Vision Test";
                            pbTestTypeImage.Image = Resources.Vision_512;
                            break;
                        }

                    case clsTestType.enTestType.WrittenTest:
                        {
                            lblTestType.Text = "Written Test";
                            pbTestTypeImage.Image = Resources.Written_Test_512;
                            break;
                        }
                    case clsTestType.enTestType.StreetTest:
                        {
                            lblTestType.Text = "Street Test";
                            pbTestTypeImage.Image = Resources.driving_test_512;
                            break;


                        }
                }
            }
        }




        public ucTest()
        {
            InitializeComponent();
        }

        public void LoadData(int localDrivingLicenseApplicationID, int testAppointmentID,
         clsTestType.enTestType TestType)
        {
            _TestTypeID = TestType;
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _testAppointmentID = testAppointmentID;
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(localDrivingLicenseApplicationID);

            if (_localDrivingLicenseApplication == null)
            {
              //  MessageBox.Show("Uc is Null");
                return;
            }

            lblLocalDrivingLicenseAppID.Text = _localDrivingLicenseApplicationID.ToString();
            lblDrivingClass.Text = clsLicenseClasses.Find(_localDrivingLicenseApplication.LicenseClassID)?.ClassName ?? "Unknown";
            lblFullName.Text = _localDrivingLicenseApplication.ApplicantFullName;
            lblTrial.Text = clsTest.GetPassedTestCount(localDrivingLicenseApplicationID).ToString();
           lblFees.Text = clsTestType.Find
                (_TestTypeID).Fees.ToString();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            _tests = new clsTest();
            _tests.TestAppointmentID = _testAppointmentID;
            _tests.TestResult = rbPass.Checked;
            _tests.Notes = txtNotes.Text;
            _tests.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            if (_tests == null)
            {
                MessageBox.Show("Tests object is null");

               

                return;
            }
            _tests.Mode = clsTest.enMode.AddNew;
            if (_tests._AddNewTest())
            {
                clsTestType testTyp = clsTestType.Find(_TestTypeID);
                MessageBox.Show("Test Save Successfully");
                //stopo in but payment in the another places;
                clsPayment payment = new clsPayment();
                payment.PersoneID = _localDrivingLicenseApplication.ApplicantPersonID;
                payment.Amount = decimal.Parse(testTyp.Fees.ToString());
                payment.PaymentDate = DateTime.Now;
                payment.Payfor = "حجز موعد اختبار";

                if (payment._AddNewPayment())
                {
                    MessageBox.Show("payment Amount=" + payment.Amount);
                }
                else
                {
                    MessageBox.Show("payment Add Failed");
                    return;
                }
                return;
            }
            else
            {
                MessageBox.Show("Test Save Failed");
            }
        }

        private void ucTest_Load(object sender, EventArgs e)
        {

        }
    }
}
