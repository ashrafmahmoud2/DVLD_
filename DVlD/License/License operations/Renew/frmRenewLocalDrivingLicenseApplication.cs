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

namespace DVlD.Appliction.Renew
{
    public partial class frmRenewLocalDrivingLicenseApplication : Form
    {
        private int _NewLicenseID = -1;
        public frmRenewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmRenewLocalDrivingLicenseApplication_Load(object sender, EventArgs e)
        {
            ucLicenseLicenseInfoWithFilter1.txtLicenseIDFocus();


            lblApplicationDate.Text = (DateTime.Now).ToShortDateString();
            lblIssueDate.Text = lblApplicationDate.Text;

            lblExpirationDate.Text = "???";


            lblApplicationFees.Text =
               clsApplicationType.
              Find((int)clsApplication.enApplicationType.RenewDrivingLicense)
              .ApplicationFees.ToString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }

        private void ucLicenseLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {
            int SelectedLicenseID = obj;

            lblOldLicenseID.Text = SelectedLicenseID.ToString();

            //llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)

            {
                return;
            }

            int DefaultValidityLength = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.LicenseClassIfo.DefaultValidityLength;
            lblExpirationDate.Text = (DateTime.Now.AddYears(DefaultValidityLength)).ToShortTimeString();
            lblLicenseFees.Text =
                ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.
                LicenseClassIfo.
                ClassFees.ToString();
            lblTotalFees.Text =
                (lblApplicationFees.Text+ lblLicenseFees.Text).ToString();
            txtNotes.Text = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.Notes;


            //check the license is not Expired.
            if (!ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.IsLicenseExpired())
            {
                MessageBox.Show("Selected License is not yet expiared, it will expire on: " +
                    (ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.ExpirationDate)
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }

            //check the license is not Expired.
            if (!ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnRenewLicense.Enabled = false;
                return;
            }



            btnRenewLicense.Enabled = true;

        }



        private void btnRenewLicense_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Renew the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
                ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.RenewLicense(txtNotes.Text.Trim(),
                clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("فشلت عملية تجديد الرخصة", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            clsPayment payment = new clsPayment();
            payment.PersoneID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            payment.Amount = decimal.Parse(lblTotalFees.Text.ToString());
            payment.PaymentDate = DateTime.Now;
            payment.Payfor = "تجديد رخصة ";

            if (payment._AddNewPayment())
            {
                MessageBox.Show("payment Amount=" + payment.Amount);
            }
            else
            {
                MessageBox.Show("payment Add Failed");
                return;
            }


            lblApplicationID.Text = NewLicense.ApplicationID.ToString();
            _NewLicenseID = NewLicense.LicenseID;
            lblRenewedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("تم تجديد الرخصة بناجح ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRenewLicense.Enabled = false;
            ucLicenseLicenseInfoWithFilter1.FilterEnabled = false;
        //    llShowLicenseInfo.Enabled = true;

         

            
        }

      
    }
}
