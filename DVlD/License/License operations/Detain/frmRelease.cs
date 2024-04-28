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

namespace DVlD.License.Detain
{
    public partial class frmRelease : Form
    {
        private int _SelectedLicenseID;
        private int _releaseID = -1;

        public frmRelease()
        {
            InitializeComponent();
        }

        public frmRelease(int LicenseID)
        {
            InitializeComponent();
            _SelectedLicenseID = LicenseID;
            ucLicenseLicenseInfoWithFilter1.LoadLicenseInfo(LicenseID);
            ucLicenseLicenseInfoWithFilter1.FilterEnabled = false;
        }


        private void frmRelease_Load(object sender, EventArgs e)
        {
            ucLicenseLicenseInfoWithFilter1.txtLicenseIDFocus();


            lblDetainDate.Text = (DateTime.Now).ToShortDateString();
            lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
        }


        private void ucLicenseLicenseInfoWithFilter1_OnLicenseSelected_1(int obj)
        {

            _SelectedLicenseID = obj;


        //    llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)

            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (!ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License not detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            lblDetainID.Text = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainID.ToString();
            lblDetainDate.Text = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.DetainedInfo.DetainDate.ToString();
            lblApplicationFees.Text = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicsense).ApplicationFees.ToString();
            lblLicenseID.Text = _SelectedLicenseID.ToString();
            lblCreatedByUser.Text = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.CreatedByUserID.ToString();
            lblApplicationID.Text = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.ApplicationID.ToString();
            lblFineFees.Text = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.PaidFees.ToString();
            lblTotalFees.Text =
                (int.Parse(lblApplicationFees.Text) +int.Parse( lblFineFees.Text)).ToString();
            btnRelease.Enabled = true;
        }

        private void btnRelease_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to release this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            int ApplictionID = ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.ApplicationID;
           bool IsReelease =
                ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.
                ReleaseDetainedLicense(clsGlobal.CurrentUser.UserID, 
              ref ApplictionID);
            
            if (!IsReelease)
            {
                MessageBox.Show("Faild to release License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            clsPayment payment = new clsPayment();
            payment.PersoneID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            payment.Amount = decimal.Parse(lblTotalFees.Text);
            payment.PaymentDate = DateTime.Now;
            payment.Payfor = "فك رخصة ";

            if (payment._AddNewPayment())
            {
                MessageBox.Show("payment Amount=" + payment.Amount);
            }
            else
            {
                MessageBox.Show("payment Add Failed");
                return;
            }

            lblDetainID.Text = _releaseID.ToString();
            MessageBox.Show("License release Successfully with ID=" + _releaseID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnRelease.Enabled = false;
            ucLicenseLicenseInfoWithFilter1.FilterEnabled = false;
          //  llShowLicenseInfo.Enabled = true;
        }

        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
    }
}
