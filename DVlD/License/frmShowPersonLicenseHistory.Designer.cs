namespace DVlD.License
{
    partial class frmShowPersonLicenseHistory
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ucDriverLicensesHistory1 = new DVlD.License.ucDriverLicensesHistory();
            this.ucPersoneCardWithFilter1 = new DVlD.People.ucPersoneCardWithFilter();
            this.SuspendLayout();
            // 
            // ucDriverLicensesHistory1
            // 
            this.ucDriverLicensesHistory1.Location = new System.Drawing.Point(-2, 282);
            this.ucDriverLicensesHistory1.Name = "ucDriverLicensesHistory1";
            this.ucDriverLicensesHistory1.Size = new System.Drawing.Size(813, 345);
            this.ucDriverLicensesHistory1.TabIndex = 1;
            // 
            // ucPersoneCardWithFilter1
            // 
            this.ucPersoneCardWithFilter1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ucPersoneCardWithFilter1.FilterEnabled = true;
            this.ucPersoneCardWithFilter1.Location = new System.Drawing.Point(-2, -1);
            this.ucPersoneCardWithFilter1.Name = "ucPersoneCardWithFilter1";
            this.ucPersoneCardWithFilter1.ShowAddPerson = true;
            this.ucPersoneCardWithFilter1.Size = new System.Drawing.Size(812, 285);
            this.ucPersoneCardWithFilter1.TabIndex = 2;
            this.ucPersoneCardWithFilter1.OnPersonSelecteD += new System.Action<int>(this.ucPersoneCardWithFilter1_OnPersonSelecteD);
            // 
            // frmShowPersonLicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 617);
            this.Controls.Add(this.ucPersoneCardWithFilter1);
            this.Controls.Add(this.ucDriverLicensesHistory1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmShowPersonLicenseHistory";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmShowPersonLicenseHistory";
            this.Load += new System.EventHandler(this.frmShowPersonLicenseHistory_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private ucDriverLicensesHistory ucDriverLicensesHistory1;
        private People.ucPersoneCardWithFilter ucPersoneCardWithFilter1;
    }
}