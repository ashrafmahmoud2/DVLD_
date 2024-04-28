using DVlD.GenrelClass;
using DVLD_business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.License.Replace
{
    public partial class frmDetainLicenseApplication : Form
    {
        //stop in make i mange screne of Detain and relse ;
        private int _SelectedLicenseID;
        private int _DetainID = -1;

        public frmDetainLicenseApplication()
        {
            InitializeComponent();
        }

        private void frmDetainLicenseApplication_Load(object sender, EventArgs e)
        {
            ucLicenseLicenseInfoWithFilter1.txtLicenseIDFocus();


            lblDetainDate.Text = (DateTime.Now).ToShortDateString();
             lblCreatedByUser.Text=clsGlobal.CurrentUser.UserName;

        }

        private void ucLicenseLicenseInfoWithFilter1_OnLicenseSelected(int obj)
        {

            _SelectedLicenseID = obj;

            lblLicenseID.Text = _SelectedLicenseID.ToString();

         //   llShowLicenseHistory.Enabled = (_SelectedLicenseID != -1);

            if (_SelectedLicenseID == -1)

            {
                return;
            }

            //ToDo: make sure the license is not detained already.
            if (ucLicenseLicenseInfoWithFilter1.SelectedLicenseInfo.IsDetained)
            {
                MessageBox.Show("Selected License i already detained, choose another one.", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            txtFineFees.Focus();
            btnDetain.Enabled = true;
        }


        private void btnDetain_Click_1(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to detain this license?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }


            _DetainID =
                ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.
                Detain(Convert.ToSingle(txtFineFees.Text), clsGlobal.CurrentUser.UserID);
            if (_DetainID == -1)
            {
                MessageBox.Show("Faild to Detain License", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }
            clsPayment payment = new clsPayment();
            payment.PersoneID = ucLicenseLicenseInfoWithFilter1.
                SelectedLicenseInfo.DriverInfo.PersonID;
            payment.Amount = decimal.Parse(txtFineFees.Text);
            payment.PaymentDate = DateTime.Now;
            payment.Payfor = "حجز رخصة ";

            if (payment._AddNewPayment())
            {
                MessageBox.Show("payment Amount=" + payment.Amount);
            }
            else
            {
                MessageBox.Show("payment Add Failed");
                return;
            }

            lblDetainID.Text = _DetainID.ToString();
            MessageBox.Show("License Detained Successfully with ID=" + _DetainID.ToString(), "License Issued", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btnDetain.Enabled = false;
            ucLicenseLicenseInfoWithFilter1.FilterEnabled = false;
            txtFineFees.Enabled = false;
            //llShowLicenseInfo.Enabled = true;
        }

        private void txtFineFees_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFineFees.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Fees cannot be empty!");
                return;
            }
            else
            {
                errorProvider1.SetError(txtFineFees, null);

            };


            if (!clsValidationHelper.IsNumberOnly(txtFineFees.Text))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFineFees, "Invalid Number.");
            }

            else
            {
                errorProvider1.SetError(txtFineFees, null);
            };
        }

        private void gpDetain_Enter(object sender, EventArgs e)
        {

        }
    }
}
