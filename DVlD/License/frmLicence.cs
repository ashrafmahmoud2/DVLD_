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
    public partial class frmLicence : Form
    {
        public frmLicence(int LicenseID)
        {
            InitializeComponent();
            ucDriverLicenseInfo1.LoadInfo(LicenseID);
        }

        private void frmLicence_Load(object sender, EventArgs e)
        {

        }
    }
}
