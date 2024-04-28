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
    public partial class frmPepoleDetails : Form
    {
     

        public frmPepoleDetails(int PersoneID)
        {
            InitializeComponent();
            PersoneID = PersoneID;
            ucPeopleCard1.LoadPersonInfo(PersoneID);
        }
        public frmPepoleDetails(string NationalNo)
        {
            InitializeComponent();
            NationalNo = NationalNo;
            ucPeopleCard1.LoadPersonInfo(NationalNo);
        }

   


    }
}
