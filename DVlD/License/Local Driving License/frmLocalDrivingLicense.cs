using DVlD.Appliction.Local_Driving_License;
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

namespace DVlD.License.Local_Driving_License
{
    public partial class frmLocalDrivingLicense : Form
    {
        public frmLocalDrivingLicense()
        {
            InitializeComponent();
        }

        private void frmLocalDrivingLicense_Load(object sender, EventArgs e)
        {
            _dt =clsLicense.GetAllLicenses();
            RefreshDataGridView();
            // PopulateFilterComboBox();
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.RowCount.ToString();
            HenderlRowsInDGV();
        }

        private static DataTable _dt =clsLicense.GetAllLicenses();

        private void HenderlRowsInDGV()
        {


        }

        public void RefreshDataGridView()
        {
            _dt =clsLicense.GetAllLicenses();
            dgvPeople.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvPeople.CurrentRow?.Cells["LicenseID"].Value;
        }


        //SEARCH
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            // Map selected filter to real column name
            switch (cbFilter.Text)
            {
                case "الاسم كامل":
                    filterColumn = "FullName";
                    break;

                case "رقم الرخصة":
                    filterColumn = "LicenseID";
                    break;

                case "نوع الفئة":
                    filterColumn = "ClassName";
                    break;

                case "مصدر الرخصة":
                    filterColumn = "UserName";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtSearch.Text) || filterColumn == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            string filterExpression = string.IsNullOrWhiteSpace(txtSearch.Text)
                ? $"[{filterColumn}] LIKE '{txtSearch.Text.Trim()}%'"
                : $"[{filterColumn}] = '{txtSearch.Text.Trim()}'";

            _dt.DefaultView.RowFilter = filterExpression;


            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "رقم الرخصة")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearch.Visible = (cbFilter.Text != "None");

            if (txtSearch.Visible)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void اظهارالتفاصيلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicence frm=new frmLicence(GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            frmLicence frm = new frmLicence(GetPersonIDFromDGV());
            frm.ShowDialog();
        }
    }
}
