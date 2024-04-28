using DVlD.License;
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
using static DVLD_business.clsPerson;

namespace DVlD.Driver
{
    public partial class frmDriver : Form
    {
        private static DataTable _dt = clsDrivers.GetAllDriverss();

        public frmDriver()
        {
            InitializeComponent();
        }

        private void frmDriver_Load(object sender, EventArgs e)
        {
            _dt = clsDrivers.GetAllDriverss();
            RefreshDataGridView();
            cbFilter.SelectedIndex = 0;
            HenderlRowsInDGV();
        }

        private void HenderlRowsInDGV()
        {

            //stop here 
            if (dgvDriver.RowCount > 0)
            {
                dgvDriver.Columns[0].HeaderText = "رقم السائق";
                dgvDriver.Columns[0].Width = 80;

                dgvDriver.Columns[1].HeaderText = "رقم الشخص";
                dgvDriver.Columns[1].Width = 80;

                dgvDriver.Columns[2].HeaderText = "الرقم الوطني";
                dgvDriver.Columns[2].Width = 170;

                dgvDriver.Columns[3].HeaderText = "الاسم كامل";
                dgvDriver.Columns[3].Width = 110;

                dgvDriver.Columns[4].HeaderText = "تاريخ الاصدار";
                dgvDriver.Columns[4].Width = 110;


                dgvDriver.Columns[5].HeaderText = "الرخص الفعالة";
                dgvDriver.Columns[5].Width = 110;
                




            }
        }

        public void RefreshDataGridView()
        {
            _dt = clsDrivers.GetAllDriverss();
            dgvDriver.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvDriver.CurrentRow?.Cells["DriverID"].Value;
        }

        //search

        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "رقم السائق":
                    FilterColumn = "DriverID";
                    break;

                case "رقم الشخص":
                    FilterColumn = "PersonID";
                    break;


                case "الرقم الوطني":
                    FilterColumn = "NationalNo";
                    break;


                case "الاسم كامل":
                    FilterColumn = "FullName";
                    break;              

                case "تاريخ الاصدار":
                    FilterColumn = "CreatedDate";
                    break;


                case "الرخص الفعالة":
                    FilterColumn = "NumberOfActiveLicenses";
                    break;


                default:
                    FilterColumn = "None";
                    break;

            }

            if (string.IsNullOrWhiteSpace(txtSearch.Text) || FilterColumn == "None")
            {
                _dt.DefaultView.RowFilter = "";
                return;
            }

            if (FilterColumn == "PersonID" || FilterColumn=="DriverID" ||FilterColumn== "الفعالة")
            {
                // Allow only numbers if PersonID is selected
                if (!int.TryParse(txtSearch.Text.Trim(), out _))
                {
                    // Handle invalid input (optional)
                    txtSearch.Text = "";
                    return;
                }

                _dt.DefaultView.RowFilter = $"[{FilterColumn}] = {txtSearch.Text.Trim()}";
            }
            else
            {
                _dt.DefaultView.RowFilter = $"[{FilterColumn}] LIKE '{txtSearch.Text.Trim()}%'";
            }
        }

        private void cbFilter_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            txtSearch.Visible = (cbFilter.Text != "None");

            if (txtSearch.Visible)
            {
                txtSearch.Text = "";
                txtSearch.Focus();
            }
        }

        private void txtSearch_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);

        }

        private void ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void DetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)dgvDriver.CurrentRow.Cells[1].Value;

            frmPepoleDetails frm = new frmPepoleDetails(PersonID);
            frm.ShowDialog();
        }

        private void LicanseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int DriverID = (int)dgvDriver.CurrentRow.Cells[0].Value;

            frmShowPersonLicenseHistory frm = new frmShowPersonLicenseHistory(DriverID,true);
            frm.ShowDialog();
        }
    }
}
