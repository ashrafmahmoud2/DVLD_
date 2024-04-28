using DVlD.GenrelClass;
using DVlD.Properties;
using DVLD_business;
using DVLD_DateAccess;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.Design;
using System.Windows.Forms;

namespace DVlD.People
{
    public partial class frmAddEditPeople : Form
    {
        


        public delegate void DateBackEventHandler(object sender, int PersoneID);
        public event DateBackEventHandler DateBack;

        public enum EnMode { AddNew = 1, Update = 2 }
        public EnMode PersonMode = EnMode.AddNew;


        private EnMode _Mode;
        private int _PersonID = -1;
        clsPerson _Person;


        public frmAddEditPeople()
        {
            InitializeComponent();
         
            _Mode = EnMode.AddNew;
        }
        public frmAddEditPeople(int personID)
        {
            InitializeComponent();
            _PersonID = personID;
            _Mode = EnMode.Update;
        }


        private void frmAddEditPeople_Load(object sender, EventArgs e)
        {
            _ResetDefaultValues();

            if (_Mode == EnMode.Update)
                _LoadDataInUpdateForm();
            label1.Text = "PersoneID";

        }

        private void _ResetDefaultValues()
        {
            _FillComboCountries();

            if (_Mode == EnMode.AddNew)
            {
                _Person = new clsPerson();
                lblTitle.Text = "اضافة";
                this.Text = "اضافة";
                cbCountry.SelectedIndex = cbCountry.FindString("Egypt");
            }
            else
            {
                lblTitle.Text = "تعديل";

            }

            if (rbMale.Checked)
            {
                PersoneImagge.Image = Resources.Male_512;
            }
            else
            {
                PersoneImagge.Image = Resources.Female_512;
            }



            dtpDateOfBirth.MaxDate = DateTime.Now.AddYears(-18);
            dtpDateOfBirth.MinDate = DateTime.Now.AddYears(-100);

            cbCountry.SelectedIndex = cbCountry.FindString("Egypt");
            txtFirstName.Text = "";
            txtSecondName.Text = "";
            txtThirdName.Text = "";
            txtLastName.Text = "";
            txtNationalNo.Text = "";
           // rbMale.Checked = true;
            txtPhone.Text = "";
            txtEmail.Text = "";
            txtAddress.Text = "";
        }

        private void _FillComboCountries()
        {
            cbCountry.Items.Clear();
            DataTable countriesDataTable = clsCountries.GetAllCountries();

            foreach (DataRow row in countriesDataTable.Rows)
            {
                cbCountry.Items.Add(row["CountryName"]);
            }
        }

        private void _LoadDataInUpdateForm()
        {
            _Person = clsPerson.Find(_PersonID);

            if ( _Person.FirstName != "")
            {
              //  MessageBox.Show(" Person  ID = " + _PersonID);
                lblPersonID.Text = _Person.PersonID.ToString();
                txtFirstName.Text = _Person.FirstName;
                txtSecondName.Text = _Person.SecondName;
                txtThirdName.Text = _Person.ThirdName;
                txtLastName.Text = _Person.LastName;
                txtAddress.Text = _Person.Address;
                txtPhone.Text = _Person.Phone;
                dtpDateOfBirth.Value = _Person.DateOfBirth;
                PersoneImagge.ImageLocation = _Person.ImagePath;
                txtNationalNo.Text = _Person.NationalNo;
                txtEmail.Text = _Person.Email;
                _LoadPersonImage();
                //rbMale.Checked = (_Person.Gender == 0);
                //rbFemale.Checked = (_Person.Gender != 0);

                // Select the country in the combo box based on the person's country info
                cbCountry.SelectedIndex = cbCountry.FindString(_Person.CuntryInfo.CountryName);

                return;
            }
            else
                MessageBox.Show(" Person  ID no found = " + _PersonID);




        }
        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                PersoneImagge.Image = Resources.Male_512;
            else
                PersoneImagge.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (!string.IsNullOrEmpty(ImagePath))
            {
                // Remove double quotes from the beginning and end of the string
                ImagePath = ImagePath.Trim('\"');

                if (File.Exists(ImagePath))
                {
                    try
                    {
                        PersoneImagge.ImageLocation = ImagePath;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Could not find the image: {ImagePath}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Image path is empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void UpdateContactInformation()
        {
            try
            {
                _Person = _Person ?? new clsPerson(); // Initialize _Person if not already done

                int NationalityCountryid = -1;

                // Attempt to find the country, handle exception if it occurs
                try
                {
                    var country = clsCountries.Find(cbCountry.Text);
                    if (country != null)
                    {
                        NationalityCountryid = country.CountryID;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error finding country: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Exit the method if an error occurs during country lookup
                }

                _Person.FirstName = txtFirstName.Text.Trim();
                _Person.SecondName = txtSecondName.Text.Trim();
                _Person.ThirdName = txtThirdName.Text.Trim();
                _Person.LastName = txtLastName.Text.Trim();
                _Person.Address = txtAddress.Text.Trim();
                _Person.Phone = txtPhone.Text.Trim();
                _Person.DateOfBirth = dtpDateOfBirth.Value;
                _Person.ImagePath = PersoneImagge.ImageLocation;
              //  _Person.ImagePath = "path/to/image.jpg";

                // Set default values if corresponding text fields are empty
                _Person.NationalNo = string.IsNullOrWhiteSpace(txtNationalNo.Text) ? "DefaultNationalNo" : txtNationalNo.Text;
                _Person.Email = string.IsNullOrWhiteSpace(txtEmail.Text) ? "DefaultEmail" : txtEmail.Text;

                _Person.NationalityCountryID = NationalityCountryid;
                _Person.Gender = (byte)(rbMale.Checked ? 0 : 1);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
           }

        private void btnSave_Click_1(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!_HandlePersonImage())
                return;


            UpdateContactInformation();

            if (_Mode == EnMode.AddNew || _Mode == EnMode.Update)
            {
                try
                {

                    if (_Person.Save())
                    {
                        MessageBox.Show($"Person {(_Mode == EnMode.AddNew ? "Added" : "Updated")} Successfully",
                            _Mode == EnMode.AddNew ? "Add" : "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lblPersonID.Text = _Person.PersonID.ToString();
                        lblTitle.Text = "تحديث";
                        DateBack?.Invoke(this, _Person.PersonID);
                       
                    }
                    else
                    {
                        // Display more information about the person in case of failure
                        string errorMessage = $"Person {(_Mode == EnMode.AddNew ? "Add" : "Update")} Failed\n";


                        MessageBox.Show(errorMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }


        //Image
        private bool _HandlePersonImage()
        {
            if (_Person.ImagePath != PersoneImagge.ImageLocation)
            {
                if (_Person.ImagePath != "")
                {
                    try
                    {
                        File.Delete(_Person.ImagePath);
                    }
                    catch (IOException)
                    { }
                }
                if (PersoneImagge.ImageLocation != null)
                {
                    //then we copy the new image to the image folder after we rename it
                    string SourceImageFile = PersoneImagge.ImageLocation.ToString();

                    if (clsUtil.CopyImageToProjectImagesFolder(ref SourceImageFile))
                    {
                        PersoneImagge.ImageLocation = SourceImageFile;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Error Copying Image File", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            return true;


        }

        private void llSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg;*.gif;*.bmp)|*.png;*.jpg;*.jpeg;*.gif;*.bmp|All Files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PersoneImagge.Image = new Bitmap(openFileDialog.FileName);
                        _Person.ImagePath = openFileDialog.FileName;
                        //_ImagePath = openFileDialog.FileName;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void llSetImage_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
            openFileDialog1.FilterIndex = 1;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // Process the selected file
                string selectedFilePath = openFileDialog1.FileName;
                PersoneImagge.Load(selectedFilePath);
                llRemoveImage.Visible = true;
                // ...
            }
        }

        private void llRemoveImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            PersoneImagge.ImageLocation = null;



            if (rbMale.Checked)
                PersoneImagge.Image = Resources.Male_512;
            else
                PersoneImagge.Image = Resources.Female_512;

            llRemoveImage.Visible = false;
        }

        private void rbMale_Click(object sender, EventArgs e)
        {
            if (PersoneImagge.ImageLocation == null)
                PersoneImagge.Image = Resources.Male_512;
        }

        private void rbFemale_Click(object sender, EventArgs e)
        {
            if (PersoneImagge.ImageLocation == null)
                PersoneImagge.Image = Resources.Female_512;
        }


        //Validate
        private void txtEmail_Validating(object sender, CancelEventArgs e)
        {
          if(!  clsValidationHelper.IsEmailValid(txtEmail.Text))
            {
                errorProvider1.SetError(txtEmail, "الايميل غير صالح");
                e.Cancel = true;
            }
            else
            {
                // Clear the error if validation is successful
                errorProvider1.SetError(txtEmail, "");
            }

        }
 
        private void ValidateNumberInput(object sender, CancelEventArgs e)
        {
            bool IsMore = false;

            int requiredDigits = 5; 

            if (sender == txtPhone)
            {
                 IsMore = true;
                requiredDigits = 6;
            }
            TextBox textBox = sender as TextBox; 

            if (textBox != null && !clsValidationHelper.ValidateNumberInput(textBox, requiredDigits, IsMore))
            {
                errorProvider1.SetError(textBox, $"الرجاء إدخال قيمة رقمية صالحة مكونة من أكثر من {requiredDigits} أرقام");
                e.Cancel = true;
            }
            else
            {
                // Clear the error if validation is successful
                errorProvider1.SetError(textBox, "");
            }
        }

        private void ValidateStringInput(object sender, CancelEventArgs e)
        {
            int requiredWords = 5;
            bool IsMore = false;

            if (sender == txtAddress)
            {
                requiredWords = 3;
                IsMore = true;
            }
            else if (sender == txtNationalNo)
            {
                if ( txtNationalNo.Text.Trim() !=_Person.NationalNo &&
                    clsPerson.DoesPersonExist(txtNationalNo.Text.Trim()))
                    requiredWords = 1;
                else
                    return;
            }
            else if (sender == txtFirstName || sender == txtSecondName || sender == txtThirdName || sender == txtLastName)
            {
                requiredWords = 1;
            }

            TextBox textBox = sender as TextBox;

            if (textBox != null && !clsValidationHelper.ValidateStringInput(textBox, requiredWords, IsMore))
            {
                errorProvider1.SetError(textBox, $"الرجاء إدخال قيمة نصية صالحة مكونة من {requiredWords} كلمات بالضبط وبدون أرقام أو مسافات.");
                e.Cancel = true;
            }
            else
            {
                // Clear the error if validation is successful
                errorProvider1.SetError(textBox, "");
            }
        }

      
    }
}

