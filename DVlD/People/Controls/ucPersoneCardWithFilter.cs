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

namespace DVlD.People
{
    public partial class ucPersoneCardWithFilter : UserControl
    {
        //OnPersonSelecteD
        // Define a custom event handler delegate with parameters
        public event Action<int> OnPersonSelecteD;
        // Create a protected method to raise the event with a parameter
        protected virtual void PersonSelected(int PersonID)
        {
            Action<int> handler = OnPersonSelecteD;
            if (handler != null)
            {
                handler(PersonID); // Raise the event with the parameter
            }
        }


        private bool _ShowAddPerson = true;
        public bool ShowAddPerson
        {
            get
            {
                return _ShowAddPerson;
            }
            set
            {
                _ShowAddPerson = value;
                btnAddNewPerson.Visible = _ShowAddPerson;
            }
        }

        private bool _FilterEnabled = true;
        public bool FilterEnabled
        {
            get
            {
                return _FilterEnabled;
            }
            set
            {
                _FilterEnabled = value;
                gbFilters.Enabled = _FilterEnabled;
            }
        }

        public ucPersoneCardWithFilter()
        {
            InitializeComponent();
        }


        private int _PersonID = -1;


      


        public int PersonID
        {
            get { return ucPeopleCard1.PersonID; }
        }

        public clsPerson SelectedPersonInfo
        {
            get { return ucPeopleCard1.SelectedPersonInfo; }
        }

        public void LoadPersonInfo(int PersonID)
        {

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            FindNow();

        }

        private void FindNow()
        {
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    ucPeopleCard1.LoadPersonInfo(int.Parse(txtFilterValue.Text));

                    break;

                case "National No.":
                    ucPeopleCard1.LoadPersonInfo(txtFilterValue.Text);
                    break;

                default:
                    break;
            }

            if (OnPersonSelecteD != null && FilterEnabled)
                // Raise the event with a parameter
                OnPersonSelecteD(ucPeopleCard1.PersonID);
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Text = "";
            txtFilterValue.Focus();
        }

      

        private void ucPersoneCardWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterValue.Focus();

        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }

    

        private void DataBackEvent(object sender, int PersonID)
        {
            // Handle the data received

            cbFilterBy.SelectedIndex = 1;
            txtFilterValue.Text = PersonID.ToString();
            ucPeopleCard1.LoadPersonInfo(PersonID);
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Check if the pressed key is Enter (character code 13)
            if (e.KeyChar == (char)13)
            {

                btnFind.PerformClick();
            }

            //this will allow only digits if person id is selected
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);



        }

        private void btnFind_Click_1(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!," +
                    " put the mouse over the red icon(s) to see the erro", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;

            }

            FindNow();
        }

        private void btnAddNewPerson_Click_1(object sender, EventArgs e)
        {
            frmAddEditPeople frm1 = new frmAddEditPeople();
            frm1.DateBack += DataBackEvent;
            frm1.ShowDialog();
        }

        private void gbFilters_Enter(object sender, EventArgs e) 
        {

        }
    }
}
