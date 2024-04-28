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

namespace DVlD.License.InternationalLicense
{
    public partial class frmAddInternationalLicense : Form
    {
        private int _InternationalLicenseID = -1;

        public frmAddInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmAddInternationalLicense_Load(object sender, EventArgs e)
        {

            lblApplicationDate.Text = (DateTime.Now).ToString();
            lblIssueDate.Text = lblApplicationDate.Text;
            lblExpirationDate.Text = (DateTime.Now.AddYears(1)).ToString();//add one year.
            lblFees.Text = clsApplicationType.Find
                ((int)clsApplication.enApplicationType.NewInternationalLicense).ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;


        }

        private void ucLicenseLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblLocalLicenseID.Text = SelectedLicenseID.ToString();

            //llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)

            {
                return;
            }




            //check the license class, person could not issue international license without having
            //normal license of class 3.

            if (ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClass != 3)
            {
                MessageBox.Show("Selected License should be Class 3, select another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //check if person already have an active international license
            int ActiveInternaionalLicenseID = clsInternationalLicenses.GetActiveInternationalLicenseIDByDriverID(ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.DriverID);

            if (ActiveInternaionalLicenseID != -1)
            {
                MessageBox.Show("Person already have an active international license with ID = " + ActiveInternaionalLicenseID.ToString(), "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //llShowLicenseInfo.Enabled = true;
                _InternationalLicenseID = ActiveInternaionalLicenseID;
                btnIssueLicense.Enabled = false;
                return;
            }

            btnIssueLicense.Enabled = true;
        }

        private void btnIssueLicense_Click(object sender, EventArgs e)
        {

            if (MessageBox.Show("Are you sure you want to issue the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }






            clsInternationalLicenses InternationalLicense = new clsInternationalLicenses();
            //those are the information for the base application, because it inhirts from application, they are part of the sub class.

            InternationalLicense.ApplicantPersonID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            InternationalLicense.ApplicationDate = DateTime.Now;
            InternationalLicense.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            InternationalLicense.LastStatusDate = DateTime.Now;
            InternationalLicense.PaidFees = clsApplicationType.
                Find((int)clsApplication.enApplicationType
                .NewInternationalLicense).ApplicationFees;
            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            
            InternationalLicense.DriverID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverID;
            InternationalLicense.IssuedUsingLocalLicenseID = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseID;
            InternationalLicense.IssueDate = DateTime.Now;
            InternationalLicense.ExpirationDate = DateTime.Now.AddYears(1);

            InternationalLicense.CreatedByUserID = clsGlobal.CurrentUser.UserID;

            if (!InternationalLicense.Save())
            {
                MessageBox.Show("Faild to Issue International License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            clsPayment payment = new clsPayment();
            payment.PersoneID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            payment.Amount = decimal.Parse(clsApplicationType.Find
                ((int)clsApplication.enApplicationType.NewInternationalLicense)
                .ApplicationFees.ToString());
            payment.PaymentDate = DateTime.Now;
            payment.Payfor = "اصدار رخصة دولية";

            if (payment._AddNewPayment())
            {
                MessageBox.Show("payment Amount=" + payment.Amount);
            }
            else
            {
                MessageBox.Show("payment Add Failed");
                return;
            }

            lblApplicationID.Text = InternationalLicense.ApplicationID.ToString();
            _InternationalLicenseID = InternationalLicense.InternationalLicenseID;
            lblInternationalLicenseID.Text = InternationalLicense.InternationalLicenseID.ToString();
            MessageBox.Show("International License Issued Successfully with ID=" + InternationalLicense.InternationalLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueLicense.Enabled = false;
            ucLicenseLicenseInfoWithFilter1.FilterEnabled = false;
            //llShowLicenseInfo.Enabled = true;



        }
    }
}
