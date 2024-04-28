using DVLD_business;
using DVLD_DateAccess;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace DVlD.People
{
    public partial class frmPeople : Form
    {
    
       

        //fix gender search

        private static DataTable _dt = clsPerson.GetAllPeople();

        public frmPeople()
        {
            InitializeComponent();
        }

        private void frmPeople_Load(object sender, EventArgs e)
        {
           _dt = clsPerson.GetAllPeople();
            RefreshDataGridView();
           // PopulateFilterComboBox();
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text=dgvPeople.RowCount.ToString();
            HenderlRowsInDGV();


        }
       private void HenderlRowsInDGV()
        {

            //stop here 
            if(dgvPeople.RowCount > 0)
            {
                dgvPeople.Columns[0].HeaderText = "ID";
                dgvPeople.Columns[0].Width = 80;

                dgvPeople.Columns[1].HeaderText = "No:";
                dgvPeople.Columns[1].Width = 80;

                dgvPeople.Columns[2].HeaderText = "الاسم كامل";
                dgvPeople.Columns[2].Width = 170;

                dgvPeople.Columns[3].HeaderText = "تاريخ الميلاد";
                dgvPeople.Columns[3].Width = 110;

                dgvPeople.Columns[4].HeaderText = "الجنس";
                dgvPeople.Columns[4].Width = 110;


                dgvPeople.Columns[5].HeaderText = "العنوان";
                dgvPeople.Columns[5].Width = 110;


                dgvPeople.Columns[6].HeaderText = "الهاتف";
                dgvPeople.Columns[6].Width = 110;

                dgvPeople.Columns[7].HeaderText = "الايميل";
                dgvPeople.Columns[7].Width = 110;

                dgvPeople.Columns[8].HeaderText = "الدولة";
                dgvPeople.Columns[8].Width = 110;

                


            }
        }
        public  void RefreshDataGridView()
        {
            _dt=clsPerson.GetAllPeople();
            dgvPeople.DataSource = _dt;
        }
         private int GetPersonIDFromDGV()
        {
            return (int)dgvPeople.CurrentRow?.Cells["PersonID"].Value;
        }

        //search
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 
            switch (cbFilter.Text)
            {
                case "رقم الشخص":
                    FilterColumn = "PersonID";
                    break;

                case "الرقم الوطني":
                    FilterColumn = "NationalNo";
                    break;

                case "الاسم":
                    FilterColumn = "FullName";
                    break;

               
                case "الجنسية":
                    FilterColumn = "CountryName";
                    break;

                case "الجنس":
                    FilterColumn = "Gender";
                    break;
                   

                case "الهاتف":
                    FilterColumn = "Phone";
                    break;

                case "البريد الإلكتروني":
                    FilterColumn = "Email";
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

            if (FilterColumn == "PersonID")
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

        private void cbGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbGender.Visible = true;
            string FilterColumn = "";
            switch (cbFilter.Text)
            {
                case "ذكر":
                    FilterColumn = "NationalNo";
                    break;

                case "انثي":
                    FilterColumn = "NationalNo";
                    break;
                case "الكل":
                    FilterColumn = "NationalNo";
                    break;


            }
            _dt.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtSearch.Text.Trim());

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


        //StripMenuItem
        private void مسحToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من أنك تريد حذف هذا الشخص؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int personID = GetPersonIDFromDGV();
                if (personID > 0)
                {
                    try
                    {
                        if (clsPerson.DeletePerson(personID))
                        {
                            MessageBox.Show("تم حذف هذا الشخص بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("لم يتم حذف هذا الشخص.", "خطأ في الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error deleting person: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a person to delete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
             
            }
           
        }

        private void اظهارالتفاصيلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenDetailsForm();

        }
     
        private void اضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPeople frmAddEditPeople = new frmAddEditPeople();
            frmAddEditPeople.ShowDialog();
            RefreshDataGridView();
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditPeople frmAddEditPeople = new frmAddEditPeople(GetPersonIDFromDGV());
            frmAddEditPeople.ShowDialog();
            RefreshDataGridView();
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            OpenDetailsForm();
        }

        private void OpenDetailsForm()
        {
            frmPepoleDetails frm = new frmPepoleDetails(GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            frmAddEditPeople frmAddEditPeople = new frmAddEditPeople();
            frmAddEditPeople.ShowDialog();
            RefreshDataGridView();
        }

       
    }

}

