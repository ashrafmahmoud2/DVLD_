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

namespace DVlD.License.InternationalLicense
{
    public partial class frmInternationalLicense : Form
    {
        
        //fix the proplem of uc in persoenInfro;
       
        private static DataTable _dt = clsInternationalLicenses.GetAllInternationalLicenses();


        public frmInternationalLicense()
        {
            InitializeComponent();
        }

        private void frmInternationalLicense_Load(object sender, EventArgs e)
        {
            _dt = clsInternationalLicenses.GetAllInternationalLicenses();
            RefreshDataGridView();
            // PopulateFilterComboBox();
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.RowCount.ToString();
            HenderlRowsInDGV();

        }
        private void HenderlRowsInDGV()
        {

            if (dgvPeople.RowCount > 0)
            {
                dgvPeople.Columns[0].HeaderText = "رقم الرخصة الدولية";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "رقم الطلب:";
                dgvPeople.Columns[1].Width = 80;

                dgvPeople.Columns[2].HeaderText = "الاسم كامل";
                dgvPeople.Columns[2].Width = 170;

                dgvPeople.Columns[3].HeaderText = "رقم السائق";
                dgvPeople.Columns[3].Width = 110;

                dgvPeople.Columns[4].HeaderText = "رقم الرخصة المحلية";
                dgvPeople.Columns[4].Width = 110;


                dgvPeople.Columns[5].HeaderText = "تاريخ الاصدار";
                dgvPeople.Columns[5].Width = 110;

                dgvPeople.Columns[6].HeaderText = "صالحة؟";
                dgvPeople.Columns[6].Width = 110;


            }
        }

        public void RefreshDataGridView()
        {
            _dt = clsInternationalLicenses.GetAllInternationalLicenses();
            dgvPeople.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvPeople.CurrentRow?.Cells["InternationalLicenseID"].Value;
        }


        //search
        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtSearch.Visible = (cbFilter.Text != "None");

            if (txtSearch.Visible)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
            
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            // Map selected filter to real column name
            switch (cbFilter.Text)
            {
                case "الاسم":
                    filterColumn = "FullName";
                    break;

                case "رقم الرخصة الدولية":
                    filterColumn = "InternationalLicenseID";
                    break;

                case "رقم السائق":
                    filterColumn = "DriverID";
                    break;

                case "رقم الطلب":
                    filterColumn = "ApplicationID";
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

            //// Allow only numbers if PersonID is selected
            //if (!int.TryParse(txtSearch.Text.Trim(), out _))
            //{
            //    // Handle invalid input (optional)
            //    txtSearch.Text = "";
            //    return;
            //}

            string filterExpression = "";

            if (filterColumn == "FullName")
            {
                filterExpression = $"[{filterColumn}] LIKE '{txtSearch.Text.Trim()}%'";
            }
            else
            {
                filterExpression = $"[{filterColumn}] = {txtSearch.Text.Trim()}";
            }

            _dt.DefaultView.RowFilter = filterExpression;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void اظهارالتفاصيلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInternationalLicenseDetails frm=new frmInternationalLicenseDetails(
                GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void اضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddInternationalLicense frm = new frmAddInternationalLicense();
            frm.ShowDialog();
        }
    }
}
