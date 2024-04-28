using DVlD.GenrelClass;
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
    public partial class frmIssueDriverLicenseFirstTime : Form
    {

        private int _LocalDrivingLicenseApplicationID;
        private clsLocalDrivingLicenseApplication _LocalDrivingLicenseApplication;



        public frmIssueDriverLicenseFirstTime(int LocalDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _LocalDrivingLicenseApplicationID= LocalDrivingLicenseApplicationID;
        }

        private void frmIssueDriverLicenseFirstTime_Load(object sender, EventArgs e)
        {
            txtNotes.Focus();
            _LocalDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingAppLicenseID(_LocalDrivingLicenseApplicationID);

            if (_LocalDrivingLicenseApplication == null)
            {

                MessageBox.Show("No Applicaiton with ID=" + _LocalDrivingLicenseApplicationID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }


            if (!_LocalDrivingLicenseApplication.PassedAllTests())
            {

                MessageBox.Show("Person Should Pass All Tests First.", "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;
            }

            int LicenseID = _LocalDrivingLicenseApplication.GetActiveLicenseID();
            if (LicenseID != -1)
            {

                MessageBox.Show("Person already has License before with License ID=" + LicenseID.ToString(), "Not Allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
                return;

            }

            ucLicenseCard1.LodeLicenseInfo(_LocalDrivingLicenseApplicationID);


        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {
            int LicenseID = 
                IssueLicenseForTheFirtTime(txtNotes.Text.Trim(),
                clsGlobal.CurrentUser.UserID);

            if (LicenseID != -1)
            {
                MessageBox.Show("License Issued Successfully with License ID = " + LicenseID.ToString(),
                    "Succeeded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clsPayment payment = new clsPayment();
                payment.PersoneID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                payment.Amount = decimal.TryParse(clsApplicationType
                    .Find((int)clsApplication.enApplicationType.
                    NewDrivingLicense).ApplicationFees.ToString()
                    , out var amount) ? amount : 0;

                payment.PaymentDate = DateTime.Now;
                payment.Payfor = "اصدار الرخصة لاول مره";

                if (payment._AddNewPayment())
                {
                    MessageBox.Show("payment Amount=" + payment.Amount);
                }
                else
                {
                    MessageBox.Show("payment Add Failed");
                    return;
                }


                this.Close();
            }
            else
            {
                MessageBox.Show("License Was not Issued ! ",
                 "Faild", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int IssueLicenseForTheFirtTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;


            clsDrivers Driver = 
                clsDrivers.
                FindByPersoneID(_LocalDrivingLicenseApplication.ApplicantPersonID);

          


            if (Driver.Address == null)
            {

                //we check if the driver already there for this person.
                Driver = new clsDrivers();

                Driver.PersonID = _LocalDrivingLicenseApplication.ApplicantPersonID;
                Driver.CreatedDate= DateTime.Now;
                Driver.CreatedByUserID = CreatedByUserID;

                if (Driver._AddNewDrivers())
                {
                    MessageBox.Show("Driver Add  Scssfuly");

                    DriverID = Driver.DriverID;

                }
                else
                {
                    MessageBox.Show("Driver Add  Faild");

                    return -1;
                }
            }
            else
            {

                DriverID = Driver.DriverID;

            }

            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = _LocalDrivingLicenseApplication.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = _LocalDrivingLicenseApplication.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(_LocalDrivingLicenseApplication.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = (float)_LocalDrivingLicenseApplication.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License._AddNewLicense())
            {
                //now we should set the application status to complete.
                _LocalDrivingLicenseApplication.SetComplete();
                MessageBox.Show("License add ");
                return License.LicenseID;
            }

            else
                MessageBox.Show("License faild ");

            return -1;
        }
    }
}
