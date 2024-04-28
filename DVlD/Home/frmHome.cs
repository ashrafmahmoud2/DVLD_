using DVlD.Appliction;
using DVlD.Dashboard;
using DVlD.Driver;
using DVlD.GenrelClass;
using DVlD.Login;
using DVlD.Payment;
using DVlD.People;
using DVlD.PriceApplication_Test;
using DVlD.User;
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
using static System.Net.Mime.MediaTypeNames;

namespace DVlD
{
    public partial class frmHome : Form
    {
        private Form currentChildForm;
        private Form _frmLoginForm;
      
        public frmHome(frmLogin frm)
        {
            InitializeComponent();
            _frmLoginForm = frm;
        }

        public void OpenChildForm(Form childForm)
        {
            //open only form
            if (currentChildForm != null)
            {
                currentChildForm.Close();
            }
            currentChildForm = childForm;
            //End
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            guna2CustomGradientPanel1.Controls.Add(childForm);
            guna2CustomGradientPanel1.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();

            if (childForm.Tag != null)
            {
                //lblTitleChildForm.Text = childForm.Tag.ToString();
            }
            else
            {
                // lblTitleChildForm.Text = childForm.Text;
            }

            //  RefreshUserInfo(this, clsGlobal.CurrentUser.UserID);
        }




        //btn 
        private void btnAppliction_Click(object sender, EventArgs e)
        {
            OpenChildForm(new frmApplcitions());
         
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
           
           OpenChildForm(new frmDashboard());

        }

        private void btnPeople_Click(object sender, EventArgs e)
        {

            OpenChildForm(new frmPeople());
        }

        private void btnDriber_Click(object sender, EventArgs e)
        {

            OpenChildForm(new frmDriver());
        }

        private void btnUser_Click(object sender, EventArgs e)
        {

           OpenChildForm(new frmPriceApplications_Tests());
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {

            OpenChildForm(new frmPayment());
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {

            OpenChildForm(new frmUser());
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
           
        }

        private void الخروجمعالنسيانToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            clsGlobal.ExitWithNonRemember();
            this.Hide();
            frmLogin frm=new frmLogin();
            frm.ShowDialog();
            
           

           
        }

        private void تسجيلالخروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            clsGlobal.CurrentUser = null;
            _frmLoginForm.Show();
            this.Close();
        }

        private void بيناتيToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUserDetails frm = new frmUserDetails(clsGlobal.CurrentUser.UserID);
            frm.Show();
        }

        private void تغيرالرمزToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(clsGlobal.CurrentUser.UserID);
            frm.Show();
        }
    }
}
