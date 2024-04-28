namespace DVlD.User
{
    partial class frmUserDetails
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
            this.ucUserCard1 = new DVlD.User.ucUserCard();
            this.SuspendLayout();
            // 
            // ucUserCard1
            // 
            this.ucUserCard1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucUserCard1.Location = new System.Drawing.Point(0, 0);
            this.ucUserCard1.Name = "ucUserCard1";
            this.ucUserCard1.Size = new System.Drawing.Size(831, 325);
            this.ucUserCard1.TabIndex = 0;
            // 
            // frmUserDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 325);
            this.Controls.Add(this.ucUserCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUserDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "frmUserDetails";
            this.Load += new System.EventHandler(this.frmUserDetails_Load_1);
            this.ResumeLayout(false);

        }

        #endregion

        private ucUserCard ucUserCard1;
    }
}