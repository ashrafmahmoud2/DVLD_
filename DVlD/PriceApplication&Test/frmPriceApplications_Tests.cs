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
    public partial class frmPriceApplications_Tests : Form
    {

        private enum enMode { Test = 0, Application = 1 };
        private enMode Mode;

        public frmPriceApplications_Tests()
        {
            InitializeComponent();
            lblTitel.Text = "الطلبات و الاختبارات";
        }

        private void PanelTests_DoubleClick(object sender, EventArgs e)
        {
            SwitchPanelVisibility(false);
            ShowTestsTypesScreen();
            Mode = enMode.Test;
        }

        private void PanelApplictions_DoubleClick(object sender, EventArgs e)
        {
            SwitchPanelVisibility(false);
            ShowApplictionTypesScreen();
            Mode = enMode.Application;
        }

        private void SwitchPanelVisibility(bool isVisible)
        {
            PanelApplictions.Visible = isVisible;
            PanelTests.Visible = isVisible;
            dgvPeople.Visible = !isVisible;
            btnGoBack.Visible = !isVisible;
            lblTitel.Text = "الطلبات و الاختبارات";
        }

        private void ShowTestsTypesScreen()
        {
            lblTitel.Text = "انواع الاختبارات";
            dgvPeople.DataSource = clsTestType.GetAllTestTypes();
        }

        private void ShowApplictionTypesScreen()
        {
            lblTitel.Text = "انواع الطلبات";
            dgvPeople.DataSource = clsApplicationType.GetAllApplicationTypes();
        }

        private int GetIDFromDGV()
        {
            string columnName = (Mode == enMode.Test) ? "TestTypeID" : "ApplicationTypeID";
            return Convert.ToInt32(dgvPeople.CurrentRow.Cells[columnName].Value);
        }

        private void dgvPeople_DoubleClick(object sender, EventArgs e)
        {
            OpenEditForm();
        }

        private void OpenEditForm()
        {
            if (Mode == enMode.Application)
            {
                OpenApplicationEditForm();
            }
            else
            {
                OpenTestEditForm();
            }
        }

        private void OpenApplicationEditForm()
        {
            using (frmEditApplicationTypes frm = new frmEditApplicationTypes(GetIDFromDGV()))
            {
                frm.ShowDialog();
            }
        }

        private void OpenTestEditForm()
        {
            //using (frmEditTestType frm = new frmEditTestType(GetIDFromDGV()))
            //{
            //    frm.ShowDialog();
            //}
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            SwitchPanelVisibility(true);
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenEditForm();
            RefreshDataGridView();
        }

        private void RefreshDataGridView()
        {
            if (Mode == enMode.Application)
            {
                dgvPeople.DataSource = clsApplicationType.GetAllApplicationTypes();
            }
            else
            {
                dgvPeople.DataSource = clsTestType.GetAllTestTypes();
            }
        }


    }
}
