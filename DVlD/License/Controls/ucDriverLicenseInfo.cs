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

namespace DVlD.License
{
    public partial class ucDriverLicenseInfo : UserControl
    {

        private int _LicenseID;
        private clsLicense _License;

        public ucDriverLicenseInfo()
        {
            InitializeComponent();
        }
        public int LicenseID
        {
            get { return _LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return _License; } }

        private void _LoadPersonImage()
        {
            if (_License.DriverInfo.PersoenInfo.Gender== 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _License.DriverInfo.PersoenInfo.ImagePath;

            if (ImagePath != "")
                if (File.Exists(ImagePath))
                    pbPersonImage.Load(ImagePath);
                else
                    MessageBox.Show("Could not find this image: = " + ImagePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

        }

        public void LoadInfo(int LicenseID)
        {
            _LicenseID = LicenseID;
            _License = clsLicense.Find(_LicenseID);
            if (_License == null)
            {
                MessageBox.Show("Could not find License ID = " + _LicenseID.ToString(),
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _LicenseID = -1;
                return;
            }

            lblLicenseID.Text = _License.LicenseID.ToString();
            lblIsActive.Text = _License.IsActive ? "Yes" : "No";
            lblIsDetained.Text = _License.IsDetained ? "Yes" : "No";
            lblClass.Text = _License.LicenseClassIfo.ClassName;
            lblFullName.Text = _License.DriverInfo.PersoenInfo.FullName;
            lblNationalNo.Text = _License.DriverInfo.PersoenInfo.NationalNo;
            lblGendor.Text = _License.DriverInfo.PersoenInfo.Gender == 0 ? "Male" : "Female";
            lblDateOfBirth.Text = (_License.DriverInfo.PersoenInfo.DateOfBirth).ToShortDateString();

            lblDriverID.Text = _License.DriverID.ToString();
            lblIssueDate.Text=(_License.IssueDate).ToShortDateString();
            lblExpirationDate.Text = (_License.ExpirationDate).ToShortDateString();
            lblIssueReason.Text = _License.IssueReasonText;
            lblNotes.Text = _License.Notes == "" ? "No Notes" : _License.Notes;
            _LoadPersonImage();



        }


        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
