using DVlD.People;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVlD.Appliction
{
    public partial class ucApplicationBasicInfo : UserControl
    {
        private clsApplication applicationss; // Declare applicationss at the class level

        private int _ApplicationID = -1;
        public int ApplicationID
        {
            get { return _ApplicationID; }
        }

        public ucApplicationBasicInfo()
        {
            InitializeComponent();
        }

        public void LodeApplictionInfo(int ApplicationID)
        {
            applicationss = clsApplication.FindBaseApplication(ApplicationID);

            if (applicationss != null)
            {
                _ApplicationID= applicationss.ApplicationID;    
                lblApplicationID.Text = applicationss.ApplicationID.ToString();
                lblStatus.Text = applicationss.StatusText.ToString();
                lblFees.Text = applicationss.PaidFees.ToString();
              //  lblType.Text = applicationss.ApplicationTypeInfo.ApplicationTypeTitle.ToString();
                lblApplicant.Text = applicationss.ApplicantFullName.ToString();
                lblDate.Text = applicationss.ApplicationDate.ToString();
                lblStatusDate.Text = applicationss.LastStatusDate.ToString();
                lblCreatedByUser.Text = applicationss.CreatedByUserID.ToString();
            }
            else
                MessageBox.Show("No Application with ApplicationID = " + ApplicationID.ToString(), 
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        private void llViewPersonInfo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (applicationss != null) // Check if applicationss is not null
            {
                frmPepoleDetails frm = new frmPepoleDetails(applicationss.ApplicantPersonID);
                frm.ShowDialog();
            }
        }
    }
}

