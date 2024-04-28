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

namespace DVlD.User
{
    public partial class frmUser : Form
    {
        public frmUser()
        {
            InitializeComponent();
        }

        private static DataTable _dt = clsUser.GetAllUsers();

        private void frmUser_Load(object sender, EventArgs e)
        {
            _dt = clsUser.GetAllUsers();
            RefreshDataGridView();
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dgvUser.RowCount.ToString();
            HenderlRowsInDGV();
            
            
        }

        private void HenderlRowsInDGV()
        {

            if (dgvUser.RowCount > 0)
            {
                dgvUser.Columns[0].HeaderText = "UserID";
                dgvUser.Columns[0].Width = 80;


                dgvUser.Columns[1].HeaderText = "PersoneID";
                dgvUser.Columns[1].Width = 80;

                dgvUser.Columns[2].HeaderText = "الاسم كامل";
                dgvUser.Columns[2].Width = 170;


                dgvUser.Columns[3].HeaderText = "اسم المستخدم";
                dgvUser.Columns[3].Width = 110;

                dgvUser.Columns[4].HeaderText = "نشظ؟";
                dgvUser.Columns[4].Width = 110;

            }
        }

        public void RefreshDataGridView()
        {
            _dt = clsUser.GetAllUsers();
            dgvUser.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvUser.CurrentRow?.Cells["UserID"].Value;
        }



        //Serach

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            string filterColumn = "";

            // Map selected filter to real column name 
            switch (cbFilter.Text)
            {
                case "رقم الشخص":
                    filterColumn = "PersonID";
                    break;

                case "رقم المستخدم":
                    filterColumn = "UserID";
                    break;

                case "اسم الشخص":
                    filterColumn = "FullName";
                    break;

                case "اسم الستخدم":
                    filterColumn = "UserName";
                    break;

                case "فعال":
                    filterColumn = "IsActive";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }
            if (string.IsNullOrWhiteSpace(txtSearch.Text) || filterColumn == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvUser.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "IsActive")
            {
                // Assuming "IsActive" is a boolean column
                _dt.DefaultView.RowFilter = $"{filterColumn} = 1";
            }
            else if (IsNumericColumn(filterColumn))
            {
                // Allow only numbers if PersonID or UserID is selected
                if (!int.TryParse(txtSearch.Text.Trim(), out _))
                {
                    // Handle invalid input (optional)
                    txtSearch.Text = "";
                    return;
                }

                _dt.DefaultView.RowFilter = $"{filterColumn} = {txtSearch.Text.Trim()}";
            }
            else
            {
                _dt.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtSearch.Text.Trim()}%'";
            }

            lblRecordsCount.Text = dgvUser.Rows.Count.ToString();

        }

        private bool IsNumericColumn(string columnName)
        {
            // Add your logic to determine if the column is numeric
            // For simplicity, assume PersonID and UserID are numeric columns
            return columnName == "PersonID" || columnName == "UserID";
        }


        //ToolStripMenuItem
        private void Details_Click(object sender, EventArgs e)
        {

            frmUserDetails frm = new frmUserDetails(GetPersonIDFromDGV());
            frm.ShowDialog();


        }
      
        private void اضافهمستخدمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            RefreshDataGridView();
        }


        private void تعديلمستخدمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(GetPersonIDFromDGV());
            frm.ShowDialog();
            RefreshDataGridView();

        }

        private void مسحمستخدمToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("هل أنت متأكد من أنك تريد حذف هذا المستخدم؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                int UserID = GetPersonIDFromDGV();
                if (UserID > 0)
                {
                    try
                    {
                        if (clsUser.DeleteUser(UserID))
                        {
                            MessageBox.Show("تم حذف هذا المستخدم بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("لم يتم حذف هذا المستخدم.", "خطأ في الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void تعديلالرمزToolStripMenuItem_Click(object sender, EventArgs e)
        {
           frmChangePassword frm=new frmChangePassword(GetPersonIDFromDGV());
            frm.ShowDialog();
            RefreshDataGridView();

        }


        private void dgvUser_DoubleClick(object sender, EventArgs e)
        {
           
            frmUserDetails frm = new frmUserDetails(GetPersonIDFromDGV());
            frm.ShowDialog();
        }
        private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser();
            frm.ShowDialog();
            RefreshDataGridView();

        }


    }
}
