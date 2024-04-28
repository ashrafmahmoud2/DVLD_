using DVlD.Appliction.Local_Driving_License;
using DVlD.Appliction.Renew;
using DVlD.Appliction.Replace;
using DVlD.License.Detain;
using DVlD.License.InternationalLicense;
using DVlD.License.Local_Driving_License;
using DVlD.License.Replace;
using DVlD.People;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.Appliction
{
    public partial class frmApplcitions : Form
    {
        // but arbic word and update the ui



        private Form currentChildForm;

        public void OpenChildForm(Form childForm)
        {
            try
            {
                // Close current child form
                if (currentChildForm != null)
                {
                    currentChildForm.Close();
                }

                // Set the new child form
                currentChildForm = childForm;

                // Set child form properties
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;

                // Set the location to center the child form on the screen
                childForm.StartPosition = FormStartPosition.CenterScreen;

                // Add child form to the main form's Controls collection
                this.Controls.Add(childForm);
                this.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();

                // Update title or perform other actions based on the child form
                if (childForm.Tag != null)
                {
                    //lblTitleChildForm.Text = childForm.Tag.ToString();
                }
                else
                {
                    // lblTitleChildForm.Text = childForm.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public frmApplcitions()
        {
            InitializeComponent();
        }

        private void btnLocalLicense_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLocalDrivingLicenseApplictions());
         
        }

        private void btnReleaseDetainedLicense_Click(object sender, EventArgs e)
        {
            frmRelease frm=new frmRelease();
            frm.Show();
        }

        private void btnIssuinglocallicenses_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm =
                new frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
        }

        private void btnLicenserenewal_Click(object sender, EventArgs e)
        {
            frmRenewLocalDrivingLicenseApplication FRM=new frmRenewLocalDrivingLicenseApplication();
            FRM.ShowDialog();
        }

        private void btnIssuingreplacement_Click(object sender, EventArgs e)
        {
            frmReplaceLostOrDamagedLicenseApplication frm=new frmReplaceLostOrDamagedLicenseApplication();
            frm.ShowDialog();
        }

        private void btnReleaselicense_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDetain_Release());

        }

        private void btnIssuinginternationallicense_Click(object sender, EventArgs e)
        {
            frmAddInternationalLicense frmAddInternationalLicense = new frmAddInternationalLicense();
            frmAddInternationalLicense.ShowDialog();
        }

        private void btnIntarnationalLisese_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmInternationalLicense());
           
        }

        private void btnManageDetainedLicenses_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmDetain_Release());
          
        }

        private void btnDetainLicense_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm= new frmDetainLicenseApplication();
            frm.ShowDialog();
        }

        private void btnRetakeTest_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLocalDrivingLicenseApplictions());
        }

        private void btnLocatLicese_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmLocalDrivingLicense());
        }
    }
}

