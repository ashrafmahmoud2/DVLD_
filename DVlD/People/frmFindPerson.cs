﻿using System;
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
    public partial class frmFindPerson : Form
    {

        public delegate void DateBackEventHendler(object sender, int Personeid);

        public event DateBackEventHendler DateBack;
        public frmFindPerson()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DateBack.Invoke(this, ucPersoneCardWithFilter1.PersonID);
        }
    }
}
