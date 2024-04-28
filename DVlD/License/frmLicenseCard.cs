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
    public partial class frmLicenseCard : Form
    {
        private int _localDrivingLicenseApplication;
        public frmLicenseCard(int localDrivingLicenseApplicationID)
        {
            InitializeComponent();
            _localDrivingLicenseApplication=localDrivingLicenseApplicationID;
        }

        private void frmLicenseCard_Load(object sender, EventArgs e)
        {
            ucLicenseCard1.LodeLicenseInfo(_localDrivingLicenseApplication);
        }
    }
}
