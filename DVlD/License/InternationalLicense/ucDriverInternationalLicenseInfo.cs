using DVlD.People;
using DVlD.Properties;
using DVLD_business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.License.InternationalLicense
{
    public partial class ucDriverInternationalLicenseInfo : UserControl
    {
         
        public ucDriverInternationalLicenseInfo()
        {
            InitializeComponent();
        }

        private clsInternationalLicenses _InternationalLicenses;

        private int _InternationalLicensesID = -1;

        public int InternationalLicensesID
        {
            get { return _InternationalLicensesID; }
        }

        public clsInternationalLicenses SelectedInternationalLicensesInfo
        {
            get { return _InternationalLicenses; }
        }

        public void LoadInternationalLicensesInfo(int InternationalLicensesID)
        {
            _InternationalLicenses = clsInternationalLicenses.Find(InternationalLicensesID);
            if (_InternationalLicenses == null)
            {
                MessageBox.Show("No InternationalLicenses with InternationalLicensesID. = " 
                    + InternationalLicensesID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillInternationalLicensesInfo();
        }

        private void _FillInternationalLicensesInfo()
        {
            lblFullName.Text = _InternationalLicenses.ApplicantFullName;
            lblInternationalLicenseID.Text = _InternationalLicenses.InternationalLicenseID.ToString();
           lblLocalLicenseID.Text = _InternationalLicenses.IssuedUsingLocalLicenseID.ToString();
         //  lblNationalNo.Text=_InternationalLicenses.PersoneInfo.NationalNo.ToString();
          //  lblGendor.Text = _InternationalLicenses.PersoneInfo.Gender.ToString();
            lblIssueDate.Text = _InternationalLicenses.IssueDate.ToString();
            lblApplicationID.Text = _InternationalLicenses.ApplicationID.ToString();
            lblIsActive.Text = _InternationalLicenses.IsActive.ToString();
           // lblDateOfBirth.Text= _InternationalLicenses.PersoneInfo.DateOfBirth.ToString();
            lblDriverID.Text=_InternationalLicenses.DriverID.ToString();
            lblExpirationDate.Text= _InternationalLicenses.ExpirationDate.ToString();
            _LoadInternationalLicensesImage();
        }
        private void _LoadInternationalLicensesImage()
        {
            //if (_InternationalLicenses.PersoneInfo.Gender == 0)
            //    pbPersonImage.Image = Resources.Male_512;
            //else
            //    pbPersonImage.Image = Resources.Female_512;

            //string ImagePath = _InternationalLicenses.PersoneInfo.ImagePath;
            //if (ImagePath != "")
            //    if (File.Exists(ImagePath))
            //        pbPersonImage.ImageLocation = ImagePath;
            //else
            //   MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }


    }
}
