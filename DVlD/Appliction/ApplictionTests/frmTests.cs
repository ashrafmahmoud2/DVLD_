using DVlD.GenrelClass;
using DVlD.License;
using DVlD.Tests.Applintment;

using DVLD_business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.Tests
{
    public partial class frmTests : Form
    {
        //the proplem is how to do the next test

        private int _localDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;
        private clsTestAppointment _testAppointments;
        private int _testAppointmentID = -1;
        private string _nationalNo;

        private clsTestType.enTestType _TestType = clsTestType.enTestType.VisionTest;

        public frmTests(int localDrivingLicenseApplicationID, clsTestType.enTestType TestType)
        {
            InitializeComponent();
            _localDrivingLicenseApplicationID = localDrivingLicenseApplicationID;
            _TestType = TestType;
        }

        private void InitializeData()
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_localDrivingLicenseApplicationID);

            if (_localDrivingLicenseApplication == null)
            {
                MessageBox.Show("_localDrivingLicenseApplication is Null");
                return;
            }

            _nationalNo = clsPerson.Find(_localDrivingLicenseApplication.ApplicantPersonID).NationalNo;
        }

        private void frmTests_Load(object sender, EventArgs e)
        {
             UpdateProgressAndEnableButton();

        }

        private void btnApplintmetns_Click(object sender, EventArgs e)
        {
            _localDrivingLicenseApplication = 
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(_localDrivingLicenseApplicationID);
            _testAppointments =
                clsTestAppointment
                .FindByLocalDrivingLicenseApplicationID(_localDrivingLicenseApplicationID);

            int m = clsTest.GetPassedTestCountbyint(_localDrivingLicenseApplicationID);

            if (m == 0)
            {
                _TestType = clsTestType.enTestType.VisionTest;
            }
            else if (m == 1)
            {
                _TestType = clsTestType.enTestType.WrittenTest;
            }
            else if (m == 2)
            {
                _TestType = clsTestType.enTestType.StreetTest;
            }

            if (_localDrivingLicenseApplication.IsThereAnActiveScheduledTest(_TestType))
            {
                MessageBox.Show("Person Already has an active appointment" +
                    " for this test. You cannot add a new appointment.",
                    "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                DialogResult result = MessageBox.Show("Do you want to load data?", "Confirmation", MessageBoxButtons.OK);

                if (result == DialogResult.OK)
                {
                    MessageBox.Show("FRM tests TEST id" + _testAppointments.TestAppointmentID);
                    uc1.LoadData(_localDrivingLicenseApplicationID,
                        _testAppointments.TestAppointmentID,_TestType);
                }

                return;
            }
            else
            {
                
                MessageBox.Show("Passed test =" + _TestType);
                frmTakeAppointment frmTake = new frmTakeAppointment(_localDrivingLicenseApplicationID,
                  _TestType);
                frmTake.ShowDialog();
               // uc1.LoadData(_localDrivingLicenseApplicationID, _testAppointments.TestAppointmentID,_TestType);
            }




            //---
            clsTest LastTest = _localDrivingLicenseApplication.GetLastTestPerTestType(_TestType);

            if (LastTest == null)
            {
                frmTests frm1 = new frmTests(_localDrivingLicenseApplicationID, _TestType);
                frm1.ShowDialog();
                frmTests_Load(null, null);
                return;
            }

            //if person already passed the test s/he cannot retak it.
            if (LastTest.TestResult == true)
            {
                MessageBox.Show("This person already passed this test before, " +
                    "you can only retake faild test", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            frmTests frm2 = new frmTests
                (LastTest.TestAppointmentInfo.LocalDrivingLicenseApplicationID, _TestType);
            frm2.ShowDialog();
            frmTests_Load(null, null);
            //---
        }
    

        private void HandleExistingTestAppointment()
        {
            _testAppointments = clsTestAppointment.Find(_testAppointmentID);
            DialogResult result = MessageBox.Show($"There is a test on " +
                $"{_testAppointments.AppointmentDate}" +
                                                  $". Do you want to take it?", "Confirmation",
                                                  MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                MessageBox.Show(_testAppointments.TestAppointmentID.ToString());
                uc1.LoadData(_localDrivingLicenseApplicationID, _testAppointmentID, _TestType);
                //UpdateProgressAndEnableButton();
            }
        }

       private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lblAppoiitmentTest_Click(object sender, EventArgs e)
        {

        }

        private void UpdateProgressAndEnableButton()
        {
            clsTestType.enTestType testType = clsTestType.GetTestType(_localDrivingLicenseApplicationID);
            if (clsTest.PassedAllTests(_localDrivingLicenseApplicationID))
            {
               clsApplication applicationss = clsApplication.FindBaseApplication(_localDrivingLicenseApplication.ApplicationID);
                if (applicationss != null)
                {
                    applicationss.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                    applicationss.Mode = clsApplication.enMode.Update;
                    if (!applicationss.Save())
                    {
                        MessageBox.Show("ApplicationStatus error");
                        return;
                    }
                }


               btnVisionTest.FillColor = Color.Green;
                btnTheoryTest.FillColor = Color.Green;
                btnPracticalTest.FillColor = Color.Green;
                Timer progressBarTimer = new Timer();
                progressBarTimer.Interval = 50;
                int steps = 1000 / 50;
                int currentStep = 0;

                progressBarTimer.Tick += (sender, e) =>
                {
                    int progressValue = (currentStep * 100) / steps;
                    ProgressBar1.Value = progressValue;
                    ProgressBar2.Value = progressValue;

                    currentStep++;

                    if (currentStep > steps)
                    {
                        progressBarTimer.Stop();
                    }
                };

                progressBarTimer.Start();


                MessageBox.Show("تم الانتهاء من الاختبارات. هل تريد إصدار الرخصة؟");
            }
            else if (testType==clsTestType.enTestType.StreetTest)
            {
              

               btnVisionTest.FillColor = Color.Green;
                btnTheoryTest.FillColor = Color.Green;

                Timer progressBarTimer = new Timer();
                progressBarTimer.Interval = 50;
                int steps = 1000 / 50;
                int currentStep = 0;

                progressBarTimer.Tick += (sender, e) =>
                {
                    int progressValue = (currentStep * 100) / steps;
                    ProgressBar1.Value = progressValue;
                    ProgressBar2.Value = progressValue;


                    currentStep++;

                    if (currentStep > steps)
                    {
                        progressBarTimer.Stop();
                    }
                };

                progressBarTimer.Start();
            }
            else if (testType == clsTestType.enTestType.WrittenTest)
            {
            
               btnVisionTest.FillColor = Color.Green;

                Timer progressBarTimer = new Timer();
                progressBarTimer.Interval = 50;
                int steps = 1000 / 50;
                int currentStep = 0;

                progressBarTimer.Tick += (sender, e) =>
                {
                    int progressValue = (currentStep * 100) / steps;
                    ProgressBar1.Value = progressValue;

                    currentStep++;

                    if (currentStep > steps)
                    {
                        progressBarTimer.Stop();
                    }
                };

                progressBarTimer.Start();
            }
        }




    }
}
