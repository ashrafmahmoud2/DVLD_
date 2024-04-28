using DVlD.License.Replace;
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

namespace DVlD.License.Detain
{
    public partial class frmDetain_Release : Form
    {   
        private static DataTable _dt = clsDetainedLicense.GetAllDetainedLicenses();

        public frmDetain_Release()
        {
            InitializeComponent();
        }


        private void frmDetain_Release_Load(object sender, EventArgs e)
        {
            _dt = clsDetainedLicense.GetAllDetainedLicenses();
            RefreshDataGridView();
            // PopulateFilterComboBox();
            cbFilter.SelectedIndex = 0;
            HenderlRowsInDGV();
        }
        private void HenderlRowsInDGV()
        {

            //stop here 
            if (dgvPeople.RowCount > 0)
            {
                dgvPeople.Columns[0].HeaderText = "رقم الحجز";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "رقم الرخصة";
                dgvPeople.Columns[1].Width = 80;

                dgvPeople.Columns[2].HeaderText = "تاريخ الحجز";
                dgvPeople.Columns[2].Width = 170;

                dgvPeople.Columns[3].HeaderText = "مفكوكة؟";
                dgvPeople.Columns[3].Width = 110;

                dgvPeople.Columns[4].HeaderText = "المبلغ المدفوع";
                dgvPeople.Columns[4].Width = 110;


                dgvPeople.Columns[5].HeaderText = "تاريخ الفك";
                dgvPeople.Columns[5].Width = 110;


                dgvPeople.Columns[6].HeaderText = "الرقم الوطني";
                dgvPeople.Columns[6].Width = 110;

                dgvPeople.Columns[7].HeaderText = "رقم الرخصة";
                dgvPeople.Columns[7].Width = 110;

                dgvPeople.Columns[8].HeaderText = "رقم الفك";
                dgvPeople.Columns[8].Width = 110;




            }
        }

        public void RefreshDataGridView()
        {
            _dt = clsDetainedLicense.GetAllDetainedLicenses();
            dgvPeople.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvPeople.CurrentRow?.Cells["LicenseID"].Value;
        }


        //search
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
        
        string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "رقم الحجز":
                    FilterColumn = "DetainID";
                    break;

                case "رقم الرخصة":
                    FilterColumn = "LicenseID";
                    break;

                case "تاريخ الحجز":
                    FilterColumn = "DetainDate";
                    break;


                case "تاريخ الفك":
                    FilterColumn = "ReleaseDate";
                    break;


                case "الرقم الوطني":
                    FilterColumn = "NationalNo";
                    break;

                case "الاسم":
                    FilterColumn = "FullName";
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

            if (FilterColumn == "LicenseID" || FilterColumn== "DetainID")
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
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


 
        //minue
        private void تفاصيلالشخصToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPepoleDetails frm = new frmPepoleDetails(clsLicense.Find(GetPersonIDFromDGV()).DriverInfo.PersonID); ;
                frm.ShowDialog();
        }

        private void تفاصيلالرخصةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLicenseCard FRM=new frmLicenseCard(GetPersonIDFromDGV());
                FRM.ShowDialog();   
        }

        private void الرخصالتابعةلةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmShowPersonLicenseHistory FRM=new frmShowPersonLicenseHistory(clsLicense.Find(GetPersonIDFromDGV()).DriverInfo.PersonID); ;
            FRM.ShowDialog();
        }

        private void فكالرخصةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRelease frm=new frmRelease(GetPersonIDFromDGV());
            frm.ShowDialog();
            RefreshDataGridView();
        }

        private void guna2ContextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            فكالرخصةToolStripMenuItem.Enabled = !(bool)dgvPeople.CurrentRow.Cells[3].Value;
        }


        private void guna2CirclePictureBox2_Click(object sender, EventArgs e)
        {
            frmDetainLicenseApplication frm = new frmDetainLicenseApplication();
            frm.ShowDialog();
            RefreshDataGridView();

        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            frmRelease frm = new frmRelease();
            frm.ShowDialog();
            RefreshDataGridView();

        }
    }
}
