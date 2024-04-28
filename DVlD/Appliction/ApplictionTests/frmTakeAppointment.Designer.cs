namespace DVlD.Tests.Applintment
{
    partial class frmTakeAppointment
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
            this.ucTakeAppointment1 = new DVlD.Tests.ucTakeAppointment();
            this.SuspendLayout();
            // 
            // ucTakeAppointment1
            // 
            this.ucTakeAppointment1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTakeAppointment1.Location = new System.Drawing.Point(0, 0);
            this.ucTakeAppointment1.Name = "ucTakeAppointment1";
            this.ucTakeAppointment1.Size = new System.Drawing.Size(537, 678);
            this.ucTakeAppointment1.TabIndex = 0;
            this.ucTakeAppointment1.TestTypeID = DVLD_business.clsTestType.enTestType.VisionTest;
            this.ucTakeAppointment1.Load += new System.EventHandler(this.ucTakeAppointment1_Load);
            // 
            // frmTakeAppointment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(537, 678);
            this.Controls.Add(this.ucTakeAppointment1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmTakeAppointment";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTakeApplintmetn";
            this.Load += new System.EventHandler(this.frmTakeAppointment_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucTakeAppointment ucTakeAppointment1;
    }
}