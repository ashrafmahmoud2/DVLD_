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
    public partial class frmInternationalLicenseDetails : Form
    {
        public frmInternationalLicenseDetails()
        {
            InitializeComponent();
        }
        public frmInternationalLicenseDetails(int InternationalLicenseID)
        {
            InitializeComponent();
            ucDriverInternationalLicenseInfo1.
                LoadInternationalLicensesInfo(InternationalLicenseID);
        }

        private void frmInternationalLicenseDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
