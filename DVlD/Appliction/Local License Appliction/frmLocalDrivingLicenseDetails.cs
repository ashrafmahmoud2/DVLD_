using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.Appliction.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseDetails : Form
    {
        public frmLocalDrivingLicenseDetails(int LocalDrivingLicenseID)
        {
            InitializeComponent();
            ucApplictionCard1.LodeDate(LocalDrivingLicenseID);

        }

        private void frmLocalDrivingLicenseDetails_Load(object sender, EventArgs e)
        {

        }
    }
}
