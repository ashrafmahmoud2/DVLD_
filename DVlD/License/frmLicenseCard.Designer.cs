namespace DVlD.License
{
    partial class frmLicenseCard
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
            this.ucLicenseCard1 = new DVlD.License.ucLicenseCard();
            this.SuspendLayout();
            // 
            // ucLicenseCard1
            // 
            this.ucLicenseCard1.Location = new System.Drawing.Point(0, 13);
            this.ucLicenseCard1.Name = "ucLicenseCard1";
            this.ucLicenseCard1.Size = new System.Drawing.Size(861, 307);
            this.ucLicenseCard1.TabIndex = 0;
            // 
            // frmLicenseCard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(861, 323);
            this.Controls.Add(this.ucLicenseCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmLicenseCard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmLicenseCard";
            this.Load += new System.EventHandler(this.frmLicenseCard_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ucLicenseCard ucLicenseCard1;
    }
}