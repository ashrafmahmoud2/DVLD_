using DVlD.GenrelClass;
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
using System.Web.Hosting;
using System.Windows.Forms;
using static DVLD_business.clsApplication;
using static System.Net.Mime.MediaTypeNames;

namespace DVlD.Appliction
{

    public partial class frmAddUpdateLocalDrivingLicesnseApplication : Form
    {
       
        private enum EnMode { AddNew = 1, Update = 2 }

        private  EnMode _mode;
        private  int _LocalDrivingLicebseApplicationID=-1;
        private  clsLocalDrivingLicenseApplication _LocalDrivingLicebseApplication;
        private  clsApplication _applications;
        private int _SelectedPersonID=-1;

        public delegate void DateBackEventHandler(object sender, int applicationID);
        public event DateBackEventHandler DateBack;

        public frmAddUpdateLocalDrivingLicesnseApplication()
        {
            InitializeComponent();
            _mode = EnMode.AddNew;
            _applications = new clsApplication();
        }

        public frmAddUpdateLocalDrivingLicesnseApplication(int localLicenseID)
        {
            InitializeComponent();
            _LocalDrivingLicebseApplicationID = localLicenseID;
            _mode = EnMode.Update;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (ucPersoneCardWithFilter1.PersonID != -1)
                guna2TabControl1.SelectedTab = tabPage2;
            else
                MessageBox.Show("قم بختيار أو إضافة الشخص أولاً", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void frmAddUpdateLocalDrivingLicesnseApplication_Load(object sender, EventArgs e)
        {
            ResetDefaultValues();
            if (_mode == EnMode.Update)
                LoadDataInUpdateForm();
        }

        private void ResetDefaultValues()
        {
            FillClassComboBox();
            if (_mode == EnMode.AddNew)
            {

                lblTitle.Text = "New Local Driving License Application";
                this.Text = "New Local Driving License Application";
               _LocalDrivingLicebseApplication = new clsLocalDrivingLicenseApplication();
                ucPersoneCardWithFilter1.FilterFocus();

                cbLicenseClass.SelectedIndex = 2;
                lblFees.Text =
                    clsApplicationType.Find((int)clsApplication
                    .enApplicationType.NewDrivingLicense).ApplicationFees.ToString();

                lblApplicationDate.Text = DateTime.Now.ToShortDateString();
                lblCreatedByUser.Text = clsGlobal.CurrentUser.UserName;
            }
            else
            {
                lblTitle.Text = "Update Local Driving License Application";
                this.Text = "Update Local Driving License Application";

                btnSave.Enabled = true;


            }
        }

        private void FillClassComboBox()
        {
            cbLicenseClass.Items.Clear();
            DataTable licenseClassesDataTable = clsLicenseClasses.GetAllLicenseClasses();

            foreach (DataRow row in licenseClassesDataTable.Rows)
                cbLicenseClass.Items.Add(row["ClassName"]);
        }

        private void LoadDataInUpdateForm()
        {
            ucPersoneCardWithFilter1.FilterEnabled = false;


            _LocalDrivingLicebseApplication =
                clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID
                (_LocalDrivingLicebseApplicationID);

            if (!string.IsNullOrEmpty(_LocalDrivingLicebseApplication.ApplicantFullName))
            {
                lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicebseApplication.LocalDrivingLicenseApplicationID.ToString();
                lblFees.Text = _LocalDrivingLicebseApplication.PaidFees.ToString();
                lblCreatedByUser.Text =
                    clsUser.Find(_LocalDrivingLicebseApplication.CreatedByUserID).UserName;
                lblApplicationDate.Text = _LocalDrivingLicebseApplication.ApplicationDate.ToString();
                ucPersoneCardWithFilter1.LoadPersonInfo(_LocalDrivingLicebseApplication.ApplicantPersonID);
                cbLicenseClass.SelectedIndex =
                    cbLicenseClass.FindString(_LocalDrivingLicebseApplication.
                    LicenseClassInfo.ClassName);
            }
            else
            {
                MessageBox.Show($"LocalLicense ID {_LocalDrivingLicebseApplicationID} not found");
            
            }
        }

        private void UpdateContactInformation()
        {
          

            int LicenseClassID = clsLicenseClasses.Find(cbLicenseClass.Text).LicenseClassID;

            int ActiveApplicationID =
           clsApplication.GetActiveApplicationIDForLicenseClass(_SelectedPersonID,
           clsApplication.enApplicationType.NewDrivingLicense, LicenseClassID);

            if (ActiveApplicationID != -1)
            {
                MessageBox.Show("Choose another License Class, the selected Person Already have an active application for the selected class with id=" + ActiveApplicationID, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cbLicenseClass.Focus();
                return;
            }

            if (clsLicense.IsLicenseExistByPersonID(ucPersoneCardWithFilter1.PersonID, LicenseClassID))
            {

                MessageBox.Show("Person already have a license with the same applied driving class, Choose diffrent driving class", "Not allowed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


        // clsLocalDrivingLicenseApplication _LocalDrivingLicebseApplication = new clsLocalDrivingLicenseApplication();
            _LocalDrivingLicebseApplication.ApplicantPersonID = ucPersoneCardWithFilter1.PersonID;
            _LocalDrivingLicebseApplication.ApplicationDate = DateTime.Now;
            _LocalDrivingLicebseApplication.ApplicationTypeID = cbLicenseClass.SelectedIndex;
            _LocalDrivingLicebseApplication.ApplicationStatus =clsApplication.enApplicationStatus.New;
            _LocalDrivingLicebseApplication.LastStatusDate = DateTime.Now;
            _LocalDrivingLicebseApplication.PaidFees =
                clsApplicationType.Find((int)enApplicationType.NewDrivingLicense).ApplicationFees;
            _LocalDrivingLicebseApplication.CreatedByUserID = clsGlobal.CurrentUser.UserID;
            _LocalDrivingLicebseApplication.ApplicationID = 114;
            _LocalDrivingLicebseApplication.LicenseClassID= LicenseClassID;

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UpdateContactInformation();

            float applicationFees = clsApplicationType.Find((int)enApplicationType.
                NewDrivingLicense).ApplicationFees;


            if (_mode == EnMode.AddNew || _mode == EnMode.Update)
            {
                try
                {
                
                    if (_LocalDrivingLicebseApplication.Save())
                        {
                        lblLocalDrivingLicebseApplicationID.Text =
                            _LocalDrivingLicebseApplication.LocalDrivingLicenseApplicationID.ToString();
                            MessageBox.Show($"LocalLicense {_mode} Successfully",
                                _mode == EnMode.AddNew ? "Add" : "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            lblLocalDrivingLicebseApplicationID.Text = _LocalDrivingLicebseApplication.ApplicationID.ToString();
                            this.Text = "تحديث";
                        _mode = EnMode.Update;

                        clsPayment payment = new clsPayment();
                        payment.PersoneID = _LocalDrivingLicebseApplication.ApplicantPersonID;
                        payment.Amount = (decimal)applicationFees;
                        payment.PaymentDate = DateTime.Now;
                        payment.Payfor = "طلب رخصة جديد";

                        if (payment._AddNewPayment())
                        {
                            MessageBox.Show("paymentID=" + payment.PaymentID);
                        }
                        else
                        {
                            MessageBox.Show("payment Add Failed");
                            return;
                        }

                        if (_LocalDrivingLicebseApplication == null)
                        {
                            MessageBox.Show($"LocalLicense is Null"
                            , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        }
                    }
                        else
                        {
                            MessageBox.Show($"LocalLicense {_mode} Failed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                   

                    if (_LocalDrivingLicebseApplication == null)
                    {
                        MessageBox.Show($"LocalLicense is Null"
                        , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ucPersoneCardWithFilter1_OnPersonSelecteD(int obj)
        {
            obj = _SelectedPersonID;
        }

        private void DataBackEvent(object sender, int PersoneID)
        {
            _SelectedPersonID = PersoneID;
            ucPersoneCardWithFilter1.LoadPersonInfo(PersoneID);
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}

    
