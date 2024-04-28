using DVlD.People;
using DVlD.Properties;
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
    public partial class ucUserCard : UserControl
    {

        private clsUser User;
        private int _UserID = -1;

        public ucUserCard()
        {
            InitializeComponent();
        }

        public int UserID
        {
            get { return _UserID; }
        }




        public clsUser SelectedUserInfo => User;

        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            User = clsUser.Find(UserID);

            if (User == null )
            {
                ResetUserInformation();
                MessageBox.Show($"No User with UserID = {UserID}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            
           
            FillUserInfo();
        }

        private void FillUserInfo()
        {
            ucPeopleCard1.LoadPersonInfo(User.PersonID);
            lblUserID.Text = User.UserID.ToString();
            lblUserName.Text = User.UserName;
            lblIsActive.Text = (User.IsActive == true) ? "فعال" : "غير فعال";
        }

        public void ResetUserInformation()
        {
             
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
        }

     

        private void llEditPersonInfo_Click(object sender, EventArgs e)
        {
            frmAddEditUser frm = new frmAddEditUser(UserID);
            frm.ShowDialog();

            // Refresh
            LoadUserInfo(UserID);
        }
    }
}

