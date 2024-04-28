using DVlD.GenrelClass;
using DVlD.License;
using DVlD.Tests;
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
using static DVLD_business.clsTestType;
using static System.Net.Mime.MediaTypeNames;

namespace DVlD.Appliction.Local_Driving_License
{
    public partial class frmLocalDrivingLicenseApplictions : Form
    {
        private static DataTable _dt;

        private clsLocalDrivingLicenseApplication _localDrivingLicenseApplication;

        public frmLocalDrivingLicenseApplictions()
        {
            InitializeComponent();

            _dt = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
        }

        private void frmLocalDrivingLicense_Load(object sender, EventArgs e)
        {

            //clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications()
            _dt = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            RefreshDataGridView();
            cbFilter.SelectedIndex = 0;
            lblRecordsCount.Text = dgvLocarLicense.RowCount.ToString();
            HenderlRowsInDGV();

        }
        private void HenderlRowsInDGV()
        {
            if (dgvLocarLicense.RowCount > 0)
            {
                dgvLocarLicense.Columns[0].HeaderText = "رقم الطلب";
                dgvLocarLicense.Columns[0].Width = 80;


                dgvLocarLicense.Columns[1].HeaderText = "الفئة";
                dgvLocarLicense.Columns[1].Width = 80;

                dgvLocarLicense.Columns[2].HeaderText = "الرقم الوطني";
                dgvLocarLicense.Columns[2].Width = 170;


                dgvLocarLicense.Columns[3].HeaderText = "الاسم كامل";
                dgvLocarLicense.Columns[3].Width = 110;

                dgvLocarLicense.Columns[4].HeaderText = "تاريخ الطلب";
                dgvLocarLicense.Columns[4].Width = 110;

                dgvLocarLicense.Columns[5].HeaderText = "الاختبارات الناجحة";
                dgvLocarLicense.Columns[5].Width = 110;

                dgvLocarLicense.Columns[6].HeaderText = "الحالة";
                dgvLocarLicense.Columns[6].Width = 110;



            }
        }

        public void RefreshDataGridView()
        {
            _dt = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplications();
            dgvLocarLicense.DataSource = _dt;
        }

        private int GetPersonIDFromDGV()
        {
            return (int)dgvLocarLicense.CurrentRow?.Cells["LocalDrivingLicenseApplicationID"].Value;
        }

        //search
        private void txtSearch_TextChanged_1(object sender, EventArgs e)
        {
            string filterColumn = "";

            // Map selected filter to real column name 
            switch (cbFilter.Text)
            {
                case "رقم الطلب":
                    filterColumn = "LocalDrivingLicenseApplicationID";
                    break;

                case "الفئة":
                    filterColumn = "ClassName";
                    break;

                case "اسم الشخص":
                    filterColumn = "FullName";
                    break;

                case "الرقم الوطني":
                    filterColumn = "NationalNo";
                    break;

                case "تاريخ الطلب":
                    filterColumn = "ApplicationDate";
                    break;

                case "الحالة":
                    filterColumn = "Status";
                    break;

                default:
                    filterColumn = "None";
                    break;
            }

            if (string.IsNullOrWhiteSpace(txtSearch.Text) || filterColumn == "None")
            {
                _dt.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvLocarLicense.Rows.Count.ToString();
                return;
            }

            if (filterColumn == "ApplicationDate" && DateTime.TryParse(txtSearch.Text, out var dateValue))
            {
                // Assuming "ApplicationDate" is a DateTime column
                string formattedDate = dateValue.ToString("yyyy-MM-dd"); // Adjust the format as needed
                _dt.DefaultView.RowFilter = $"{filterColumn} = #{formattedDate}#";
            }
            else
            {
                if (filterColumn == "Status" && !IsNumericColumn(filterColumn))
                {
                    // Assuming "Status" is an enumeration or string column
                    _dt.DefaultView.RowFilter = $"{filterColumn} = '{txtSearch.Text.Trim()}'";
                }
                else if (IsNumericColumn(filterColumn))
                {
                    // Allow only numbers if it's a numeric column
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
                    // Use a suitable condition for non-numeric and non-date columns
                    _dt.DefaultView.RowFilter = $"{filterColumn} LIKE '{txtSearch.Text.Trim()}%'";
                }
            }

            lblRecordsCount.Text = dgvLocarLicense.Rows.Count.ToString();

        }

        private bool IsNumericColumn(string columnName)
        {
            // Add your logic to determine if the column is numeric
            // For simplicity, assume PersonID and UserID are numeric columns
            return columnName == "PersonID" || columnName == "UserID";
        }


        //ToolStripMenuItem
        private void dgvLocarLicense_DoubleClick(object sender, EventArgs e)
        {

            frmLocalDrivingLicenseDetails frm = new frmLocalDrivingLicenseDetails(GetPersonIDFromDGV());
            frm.ShowDialog();
        }
        private void Details_Click(object sender, EventArgs e)
        {
            frmLocalDrivingLicenseDetails frm = new frmLocalDrivingLicenseDetails(GetPersonIDFromDGV());
            frm.ShowDialog();
        }

        private void AddToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new
               frmAddUpdateLocalDrivingLicesnseApplication();
            frm.ShowDialog();
            RefreshDataGridView();
        }

        private void EdittoolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdateLocalDrivingLicesnseApplication frm = new
                          frmAddUpdateLocalDrivingLicesnseApplication(GetPersonIDFromDGV());
            frm.ShowDialog();
            RefreshDataGridView();
        }

        private void DeleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication ll =
                clsLocalDrivingLicenseApplication.
                FindByLocalDrivingAppLicenseID(GetPersonIDFromDGV());


            if (MessageBox.Show("هل أنت متأكد من أنك تريد حذف هذا الطلب؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ll != null)
                {
                    try
                    {
                        if (ll.Delete())
                        {
                            MessageBox.Show("تم حذف هذا الطلب بنجاح.", "تم الحذف", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("لم يتم حذف هذا الطلب.", "خطأ في الحذف", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void CancelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsLocalDrivingLicenseApplication ll =
                            clsLocalDrivingLicenseApplication.
                            FindByLocalDrivingAppLicenseID(GetPersonIDFromDGV());


            if (MessageBox.Show("هل أنت متأكد من أنك تريد الغاء هذا الطلب؟", "تأكيد الغاء", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (ll != null)
                {
                    try
                    {
                        if (ll.Cancel())
                        {
                            MessageBox.Show("تم الغاء هذا الطلب بنجاح.", "تم الغاء", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            RefreshDataGridView();
                        }
                        else
                        {
                            MessageBox.Show("لم يتم الغاء هذا الطلب.", "خطأ في الغاء", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void PersoneHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
              (GetPersonIDFromDGV());
            frmShowPersonLicenseHistory frm = new
                frmShowPersonLicenseHistory(_localDrivingLicenseApplication.ApplicantPersonID);
            frm.ShowDialog();
        }

        private void LicenceDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.
                FindByLocalDrivingAppLicenseID(GetPersonIDFromDGV());
            int num =
               clsLicense.GetActiveLicenseIDByPersonID(_localDrivingLicenseApplication.ApplicantPersonID, _localDrivingLicenseApplication.LicenseClassID);
            frmLicence FRM = new frmLicence(num);
            FRM.ShowDialog();
        }

        private void اصداررخصةالقيادةلأولمرةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueDriverLicenseFirstTime frm = 
                new frmIssueDriverLicenseFirstTime(GetPersonIDFromDGV());
            frm.ShowDialog();
            RefreshDataGridView();

        }


        private void TestsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _localDrivingLicenseApplication = clsLocalDrivingLicenseApplication
                 .FindByLocalDrivingAppLicenseID(GetPersonIDFromDGV());

            // Get the number of passed tests

            // Determine the test type based on the number of passed tests
            clsTestType.enTestType testType = GetTestType(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);

            if (!clsTest.PassedAllTests(GetPersonIDFromDGV()))
            {
                MessageBox.Show(GetPersonIDFromDGV().ToString());
                frmTests frm = new frmTests(GetPersonIDFromDGV(), testType);
                frm.ShowDialog();
                RefreshDataGridView();

            }
            else if (clsLicense.IsLicenseExistByPersonID(_localDrivingLicenseApplication.ApplicantPersonID, _localDrivingLicenseApplication.LicenseClassID))
            {
                MessageBox.Show("لقد تم اصدار الرخصة بالفعل!!");
            }
            else
            {
                DialogResult result = MessageBox.Show("تم الانتهاء من الاختبارات. هل تريد اصدار الرخصة؟", "Confirmation", MessageBoxButtons.OKCancel);

                if (result == DialogResult.OK)
                {
                    AddLicense();
                    clsApplication application = clsApplication.FindBaseApplication(_localDrivingLicenseApplication.ApplicationID);

                    if (application != null)
                    {
                        application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
                        application.Mode = clsApplication.enMode.Update;

                        if (!application.Save())
                        {
                            MessageBox.Show("ApplicationStatus error");
                        }
                    }
                }
                RefreshDataGridView();

            }
        }
        //private clsTestType.enTestType GetTestType(int LocalDrivingLicenseApplicationID)
        //{
        //    int passedTestCount = 
        //        clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);

        //    switch (passedTestCount)
        //    {
        //        case 0:
        //            return clsTestType.enTestType.VisionTest;
        //        case 1:
        //            return clsTestType.enTestType.WrittenTest;
        //        case 2:
        //            return clsTestType.enTestType.StreetTest;
        //        default:
        //            return clsTestType.enTestType.VisionTest;
        //    }
        //}



        private void AddLicense()
    {
        //_localDrivingLicenseApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
        //    (GetPersonIDFromDGV());
        //clsDrivers _driver = new clsDrivers
        //{
        //    PersonID = _localDrivingLicenseApplication.ApplicantPersonID,
        //    CreatedDate = DateTime.Now,
        //    CreatedByUserID = clsGlobal.CurrentUser.UserID
        //};

        //if (_driver.Save())
        //{
        //    MessageBox.Show("Driver Added Successfully");
        //}
        //else
        //{
        //    MessageBox.Show("Driver Add Failed");
        //}

        //clsLicense _licenses = new clsLicense
        //{
        //    ApplicationID = _localDrivingLicenseApplication.ApplicationID,
        //    DriverID= _driver.DriverID,
        //    LicenseClass = _localDrivingLicenseApplication.LicenseClassID,
        //    IssueDate = DateTime.Now,
        //    ExpirationDate = DateTime.Now.AddYears(_localDrivingLicenseApplication.LicenseClassInfo.DefaultValidityLength),
        //    Notes = "",
        //    PaidFees = clsApplicationType.Find(_localDrivingLicenseApplication.ApplicationTypeID).ApplicationFees,
        //    IsActive = true,
        //    IssueReason = clsLicense.enIssueReason.FirstTime,
        //    CreatedByUserID = clsGlobal.CurrentUser.UserID
        //};

        //if (_licenses.Save())
        //{
        //    MessageBox.Show("License Added Successfully");
        //}
        //else
        //{
        //    MessageBox.Show("License Add Failed");
        //}

        //frmLicenseCard frm = new frmLicenseCard(_localDrivingLicenseApplication.LocalDrivingLicenseApplicationID);
        //frm.ShowDialog();
    }

         private void guna2CirclePictureBox1_Click(object sender, EventArgs e)
    {
        frmAddUpdateLocalDrivingLicesnseApplication frm = new
                       frmAddUpdateLocalDrivingLicesnseApplication();
        frm.ShowDialog();
        RefreshDataGridView();
    }


}
}
    

