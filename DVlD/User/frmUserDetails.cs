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
    public partial class frmUserDetails : Form
    {
       private int _UserID ;

        public frmUserDetails()
        {
            InitializeComponent();


        }
        public frmUserDetails(int UserID)
        {
            _UserID = UserID;

            InitializeComponent();
            ucUserCard1.LoadUserInfo(UserID);
        
        }

        private void frmUserDetails_Load(object sender, EventArgs e)
        {
            ucUserCard1.LoadUserInfo(_UserID);
        }

        private void frmUserDetails_Load_1(object sender, EventArgs e)
        {

        }
    }
}
