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
    public partial class frmShowPersonLicenseHistory : Form
    {
       private int _PersonID;
        private int _DriverID;
        public frmShowPersonLicenseHistory()
        {
            InitializeComponent();
           
        }
        public frmShowPersonLicenseHistory(int PerosneID)
        {
            InitializeComponent();
            _PersonID = PerosneID;

           
        }
        public frmShowPersonLicenseHistory(int DriverID,bool IsDriver=false)
        {
            InitializeComponent();
            _DriverID = DriverID;
            _PersonID = clsDrivers.Find(DriverID).PersonID;


        }

        private void frmShowPersonLicenseHistory_Load(object sender, EventArgs e)
        {

            if(_PersonID != -1)
            {
                clsPerson pp = clsPerson.Find(_PersonID);
                ucPersoneCardWithFilter1.LoadPersonInfo(_PersonID);


                 MessageBox.Show("persoenID:"+_PersonID.ToString());
                MessageBox.Show("DriverID:"+_DriverID.ToString());
                ucDriverLicensesHistory1.LoadInfo(_DriverID);
            }
            

        }

        private void ucPersoneCardWithFilter1_OnPersonSelecteD(int obj)
        {
            _PersonID = obj;
            if (_PersonID == -1)
            {
                ucDriverLicensesHistory1.Clear();
            }
            else
                ucDriverLicensesHistory1.LoadInfoByPersonID(_PersonID);

        }
    }
}
