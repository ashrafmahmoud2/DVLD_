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

namespace DVlD.Payment
{
    public partial class frmPayment : Form
    {
        public frmPayment()
        {
            InitializeComponent();
        }

        private void frmPayment_Load(object sender, EventArgs e)
        {
            _dt = clsPayment.GetAllPayments();
            RefreshDataGridView();
            // PopulateFilterComboBox();
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.RowCount.ToString();
            HenderlRowsInDGV();

        }
        private static DataTable _dt = clsPayment.GetAllPayments();
        private void HenderlRowsInDGV()
        {

            if (dgvPeople.RowCount > 0)
            {
                dgvPeople.Columns[0].HeaderText = "رقم الدفع";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "رقم الشخص";
                dgvPeople.Columns[1].Width = 80;

                dgvPeople.Columns[2].HeaderText = "المبلغ";
                dgvPeople.Columns[2].Width = 170;

                dgvPeople.Columns[3].HeaderText = "تاريخ الدفع";
                dgvPeople.Columns[3].Width = 110;

                dgvPeople.Columns[4].HeaderText = "النوع";
                dgvPeople.Columns[4].Width = 110;








            }
        }

        public void RefreshDataGridView()
        {
            _dt = clsPayment.GetAllPayments();
            dgvPeople.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvPeople.CurrentRow?.Cells["PaymentID"].Value;
        }

        //search

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "رقم الدفع":
                    FilterColumn = "PaymentID";
                    break;

                case "رقم الشخص":
                    FilterColumn = "PersoneID";
                    break;

                case "تاريخ الدفع":
                    FilterColumn = "PaymentDate";
                    break;

                case "النوع":
                    FilterColumn = "Payfor";
              
                    break;

                default:
                    FilterColumn = "None";
                    break;

            }

            if (string.IsNullOrWhiteSpace(txtSearch.Text) || FilterColumn == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PaymentID")
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

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
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

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {

            //we allow number incase person id is selected.
            if (cbFilter.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }



    }
}
