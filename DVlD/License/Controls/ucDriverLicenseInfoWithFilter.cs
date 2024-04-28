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

namespace DVlD.License
{
    public partial class ucLicenseLicenseInfoWithFilter : UserControl
    {
       
        // Define a custom event handler delegate with parameters
        public event Action<int> OnLicenseSelected;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int LicenseID)
        {
            Action<int> handler = OnLicenseSelected;
            if (handler != null)
            {
                handler(LicenseID); // Raise the event with the parameter
            }
        }

        public ucLicenseLicenseInfoWithFilter()
        {
            InitializeComponent();
        }

        private bool _FilterEnabled = true;

        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        private int _LicenseID = -1;

        public int LicenseID
        {
            get { return ucDriverLicenseInfo1.LicenseID; }
        }

        public clsLicense SelectedLicenseInfo
        { get { return ucDriverLicenseInfo1.SelectedLicenseInfo; } }

        public void LoadLicenseInfo(int LicenseID)
        {


            txtLicenseID.Text = LicenseID.ToString();
            ucDriverLicenseInfo1.LoadInfo(LicenseID);
            _LicenseID = ucDriverLicenseInfo1.LicenseID;
            if (OnLicenseSelected != null && FilterEnabled)
                // Raise the event with a parameter
                OnLicenseSelected(_LicenseID);


        }

        public void txtLicenseIDFocus()
        {
            txtLicenseID.Focus();
        }

        private void gbFilters_Enter(object sender, EventArgs e)
        {

        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtLicenseID.Focus();
                return;

            }
            _LicenseID = int.Parse(txtLicenseID.Text);
            LoadLicenseInfo(_LicenseID);
        }

        private void txtLicenseID_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);


            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }
        }
    }
}
