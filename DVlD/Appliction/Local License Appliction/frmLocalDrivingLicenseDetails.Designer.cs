namespace DVlD.Appliction.Local_Driving_License
{
    partial class frmLocalDrivingLicenseDetails
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
            this.ucApplictionCard1 = new DVlD.Appliction.ucApplictionCard();
            this.SuspendLayout();
            // 
            // ucApplictionCard1
            // 
            this.ucApplictionCard1.Location = new System.Drawing.Point(-5, 0);
            this.ucApplictionCard1.Name = "ucApplictionCard1";
            this.ucApplictionCard1.Size = new System.Drawing.Size(865, 242);
            this.ucApplictionCard1.TabIndex = 0;
            // 
            // frmLocalDrivingLicenseDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(860, 236);
            this.Controls.Add(this.ucApplictionCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmLocalDrivingLicenseDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLocalDrivingLicenseDetails";
            this.Load += new System.EventHandler(this.frmLocalDrivingLicenseDetails_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucApplictionCard ucApplictionCard1;
    }
}