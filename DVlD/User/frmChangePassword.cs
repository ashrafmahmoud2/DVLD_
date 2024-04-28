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
    public partial class frmChangePassword : Form
    {
        int _UserID = -1;
        public frmChangePassword(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
            ucUserCard1.LoadUserInfo(UserID);
        }
      
        private void HendelUpdatePassword()
        {
            clsUser _User = clsUser.Find(_UserID);

            if (_User.Password != txtNowPassword.Text)
            {
                MessageBox.Show($"{_UserID} الرقم غير الرقم السري للمستخدم رقم ");
            }
            else if (txtPassword.Text == txtConfirmPassword.Text)
            {
                if (clsUser.ChangePassword(_UserID,txtPassword.Text))
                {
                    MessageBox.Show($" {_User.Password}و اصبح {_UserID} تم تغير الرقم السري للمستخدم رقم ");
                }
            }
        }

        private void btnSave_Click_1(object sender, EventArgs e)
        {

            HendelUpdatePassword();
        }
    }
}
