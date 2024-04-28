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

namespace DVlD.PriceApplication_Test
{
    public partial class frmEditApplicationTypes : Form
    {
      
            private  int _ApplicationTypeID;
            private  clsApplicationType _applicationTypes;

            public frmEditApplicationTypes(int applicationTypeID)
            {
                InitializeComponent();
                _ApplicationTypeID = applicationTypeID;
                _applicationTypes = clsApplicationType.Find(applicationTypeID);
            }

            private void pictureBox1_Click(object sender, EventArgs e)
            {
            this.Close();            }

            private void frmEditApplicationTests_Load(object sender, EventArgs e)
            {
                DisplayApplicationTypeDetails();
            }
            private void DisplayApplicationTypeDetails()
        {
            if (_applicationTypes != null)
            {
                lblApplicationTypeID.Text = _applicationTypes.ApplicationTypeID.ToString();
                txtFees.Text = _applicationTypes.ApplicationFees.ToString();
                txtTitle.Text = _applicationTypes.ApplicationTypeTitle;
            }
            else
            {
                ShowErrorMessage("Application type not found.");
            }
        }

            private void btnSave_Click(object sender, EventArgs e)
            {
            if (_applicationTypes != null)
            {
                _applicationTypes.ApplicationFees =float.Parse(txtFees.Text);
                _applicationTypes.ApplicationTypeTitle = txtTitle.Text;
            }

            // Save changes
            if (_applicationTypes._UpdateApplicationTypes()) // Assuming you corrected the method name
                {
                    ShowSuccessMessage("Changes saved successfully.");
                }
                else
                {
                    ShowErrorMessage("Failed to save changes.");
                }
            }

            private void ShowSuccessMessage(string message)
            {
                MessageBox.Show(message, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            private void ShowErrorMessage(string message)
            {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            private decimal ParseDecimal(string input)
            {
                if (decimal.TryParse(input, out decimal result))
                {
                    return result;
                }
                else
                {
                    ShowErrorMessage("Invalid decimal value. Please enter a valid decimal number.");
                    return 0; // Return a default value or handle it based on your requirement
                }
            }

            
        }
    }

