using DVlD.Properties;
using DVLD_business;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVlD.People
{
    public partial class ucPeopleCard : UserControl
    {

        //fix the valdetion in text; 
 
        private clsPerson _Person;

        private int _PersonID = -1;

        public int PersonID
        {
            get { return _PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return _Person; }
        }

        public ucPeopleCard()
        {
            InitializeComponent();
        }

        public void LoadPersonInfo(int PersonID)
        {
            _Person = clsPerson.Find(PersonID);
            if (_Person == null)
            {
                ResetPersonInfo();
                MessageBox.Show("No Person with PersonID. = " + PersonID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillPersonInfo();
        }

        public void LoadPersonInfo(string nationalNo)
        {
            _Person = clsPerson.Find(nationalNo);

            if (_Person != null && _Person.FirstName != null)
            {
                _FillPersonInfo();
            }
            else
            {
                ResetPersonInfo();
                MessageBox.Show($"No Person with National No. = {nationalNo}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void _LoadPersonImage()
        {
            if (_Person.Gender == 0)
                pbPersonImage.Image = Resources.Male_512;
            else
                pbPersonImage.Image = Resources.Female_512;

            string ImagePath = _Person.ImagePath;
            if (!string.IsNullOrEmpty(ImagePath))
            {
                ImagePath = ImagePath.Replace("\"", ""); // Remove double quotation marks

                if (File.Exists(ImagePath))
                {
                    try
                    {
                        pbPersonImage.ImageLocation = ImagePath;
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




        private void _FillPersonInfo()
        {
            llEditPersonInfo.Enabled = true;
            _PersonID = _Person.PersonID;
            lblPersonID.Text = _Person.PersonID.ToString();
            lblNationalNo.Text = _Person.NationalNo;
            lblFullName.Text = $"{_Person.FirstName} {_Person.SecondName} {_Person.ThirdName} {_Person.LastName}";
            lblGendor.Text = _Person.Gender == 0 ? "Male" : "Female";
            lblEmail.Text = _Person.Email;
            lblPhone.Text = _Person.Phone;
            lblDateOfBirth.Text = _Person.DateOfBirth.ToShortDateString();
            lblCountry.Text = clsCountries.Find(_Person.NationalityCountryID).CountryName;
            lblAddress.Text = _Person.Address;
            _LoadPersonImage();




        }

        public void ResetPersonInfo()
        {
            _PersonID = -1;
            lblPersonID.Text = "[????]";
            lblNationalNo.Text = "[????]";
            lblFullName.Text = "[????]";
            pbGendor.Image = Resources.Male_512;
            lblGendor.Text = "[????]";
            lblEmail.Text = "[????]";
            lblPhone.Text = "[????]";
            lblDateOfBirth.Text = "[????]";
            lblCountry.Text = "[????]";
            lblAddress.Text = "[????]";
            pbPersonImage.Image = Resources.Male_512;

        }

     
        private void llEditPersonInfo_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmAddEditPeople frm = new frmAddEditPeople(_PersonID);
            frm.ShowDialog();

            //refresh
            LoadPersonInfo(_PersonID);

        }

        private void ucPeopleCard_Load(object sender, EventArgs e)
        {

        }
    }
}
