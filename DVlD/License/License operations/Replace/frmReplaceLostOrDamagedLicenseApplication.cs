using DVlD.GenrelClass;
using DVlD.License;
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
using static DVLD_business.clsLicense;

namespace DVlD.Appliction.Replace
{
    public partial class frmReplaceLostOrDamagedLicenseApplication : Form
    {



        private int _GetApplicationTypeID()
        {
            //this will decide which application type to use accirding 
            // to user selection.

            if (rbDamagedLicense.Checked)

                return (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense;
            else
                return (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;
        }

        private enIssueReason _GetIssueReason()
        {
            //this will decide which reason to issue a replacement for

            if (rbDamagedLicense.Checked)

                return enIssueReason.DamagedReplacement;
            else
                return enIssueReason.LostReplacement;
        }








        private void llShowLicenseHistory_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            frmShowPersonLicenseHistory frm =
           new frmShowPersonLicenseHistory(ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.DriverInfo.PersonID);
            frm.ShowDialog();
        }

        private void llShowLicenseInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLicence frm =
                 new frmLicence(_NewLicenseID);
            frm.ShowDialog();
        }







        //
        private int _NewLicenseID = -1;

        public frmReplaceLostOrDamagedLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmReplaceLostOrDamagedLicenseApplication_Load(object sender, EventArgs e)
        {

            ucLicenseLicenseInfoWithFilter1.txtLicenseIDFocus();


            lblApplicationDate.Text = (DateTime.Now).ToShortDateString();
           


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
          //  llShowLicenseHistory.Enabled = (SelectedLicenseID != -1);

            if (SelectedLicenseID == -1)
            {
                return;
            }

            //dont allow a replacement if is Active .
            if (!ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.IsActive)
            {
                MessageBox.Show("Selected License is not Not Active, choose an active license."
                    , "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnIssueReplacement.Enabled = false;
                return;
            }

            btnIssueReplacement.Enabled = true;
        }

        private void btnIssueReplacement_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to Issue a Replacement for the license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            clsLicense NewLicense =
               ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.Replace(_GetIssueReason(),
               clsGlobal.CurrentUser.UserID);

            if (NewLicense == null)
            {
                MessageBox.Show("Faild to Issue a replacemnet for this  License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            clsPayment payment = new clsPayment();
            payment.PersoneID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            payment.Amount = decimal.Parse(lblApplicationFees.Text.ToString());
            payment.PaymentDate = DateTime.Now;
            payment.Payfor = "اصدار بدل تالف او فاقد";

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

            lblRreplacedLicenseID.Text = _NewLicenseID.ToString();
            MessageBox.Show("Licensed Replaced Successfully with ID=" + _NewLicenseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnIssueReplacement.Enabled = false;
            gbReplacementFor.Enabled = false;
            ucLicenseLicenseInfoWithFilter1.FilterEnabled = false;
            //llShowLicenseInfo.Enabled = true;

        }

        private void rbDamagedLicense_CheckedChanged(object sender, EventArgs e)
        {

            //lblTitle.Text = "Replacement for Damaged License";
            //this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();

        }

        private void rbLostLicense_CheckedChanged(object sender, EventArgs e)
        {
            //lblTitle.Text = "Replacement for Lost License";
            //this.Text = lblTitle.Text;
            lblApplicationFees.Text = clsApplicationType.Find(_GetApplicationTypeID()).ApplicationFees.ToString();

        }
    }
}
