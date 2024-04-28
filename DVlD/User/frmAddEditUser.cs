using DVlD.Properties;
using DVLD_business;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace DVlD.User
{
    public partial class frmAddEditUser : Form
    {
        public delegate void DateBackEventHandler(object sender, int UsereID);
        public event DateBackEventHandler DateBack;

        public enum EnMode { AddNew = 1, Update = 2 }
        public EnMode UserMode = EnMode.AddNew;


        private EnMode _Mode;
        private int _UserID = -1;
        clsUser _User;

        public frmAddEditUser()
        {
            InitializeComponent();
            _Mode = EnMode.AddNew;
        }
        public frmAddEditUser(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            _Mode = EnMode.Update;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ucPersoneCardWithFilter1.PersonID != -1)
            {
                // Assuming guna2TabControl is the name of your Guna2TabControl
                guna2TabControl1.SelectedTab = tabPage2;
            }
            else
            {
                MessageBox.Show("قم بختيار أو إضافة الشخص أولاً", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void frmAddEditUser_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == EnMode.Update)
                _LoadDataInUpdateForm();
            label1.Text = "UsereID";

        }
        private void _ResetDefaultValues()
        {

            if (_Mode == EnMode.AddNew)
            {
                _User = new clsUser();
                this.Text = "اضافة";
            }
            else
            {

            }

           
            txtConfirmPassword.Text = "";
            txtPassword.Text = "";
            txtUserName.Text = "";
            chkIsActive.Checked = true;
         
        }

        private void _LoadDataInUpdateForm()
        {
            _User = clsUser.Find(_UserID);

            if (_User.FirstName != "")
            {
              //  MessageBox.Show(" User  ID = " + _UserID);
              lblUserID.Text=_User.UserID.ToString();
                txtUserName.Text = _User.UserName;
                txtPassword.Text = _User.Password;
                txtConfirmPassword.Text = _User.Password;
                ucPersoneCardWithFilter1.LoadPersonInfo(_User.PersonID);
                chkIsActive.Checked = _User.IsActive;
               
               

                // Select the country in the combo box based on the User's country info

                return;
            }
            else
                MessageBox.Show(" User  ID no found = " + _UserID);




        }

        private void UpdateContactInformation()
        {
            try
            {
                _User = _User ?? new clsUser();



                //if(ucPersoneCardWithFilter1.PersonID == 0)
                //{
                //    MessageBox.Show("the are no perseoe;");
                //}



                _User.PersonID = ucPersoneCardWithFilter1.PersonID;
                _User.UserName = txtUserName.Text;
                _User.Password = txtPassword.Text.Trim();
                _User.IsActive = chkIsActive.Checked;
              

                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


           
            


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Some fileds");

        }

        private bool _HendelConfirmPassword()
        {
            if(txtPassword.Text==txtConfirmPassword.Text)
            {
return true;
            }
            else
               
            return false;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {


            //if (!this.ValidateChildren())
            //{
            //    MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    return;
            //}

            if (!_HendelConfirmPassword())
            {
                MessageBox.Show("الرقم  غير مظابق");
                return;
            }

            UpdateContactInformation();

            if (_Mode == EnMode.AddNew || _Mode == EnMode.Update)
            {
                try
                {

                    if (_User.Save())
                    {
                        MessageBox.Show($"User {(_Mode == EnMode.AddNew ? "Added" : "Updated")} Successfully",
                            _Mode == EnMode.AddNew ? "Add" : "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblUserID.Text = _User.UserID.ToString();
                        DateBack?.Invoke(this, _User.UserID);

                    }
                    else
                    {
                        // Display more information about the User in case of failure
                        string errorMessage = $"User {(_Mode == EnMode.AddNew ? "Add" : "Update")} Failed\n";


                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }
    }
}
