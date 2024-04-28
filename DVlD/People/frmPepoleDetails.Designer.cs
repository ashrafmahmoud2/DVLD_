namespace DVlD.People
{
    partial class frmPepoleDetails
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
            this.ucPeopleCard1 = new DVlD.People.ucPeopleCard();
            this.SuspendLayout();
            // 
            // ucPeopleCard1
            // 
            this.ucPeopleCard1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(229)))), ((int)(((byte)(255)))));
            this.ucPeopleCard1.Location = new System.Drawing.Point(3, 2);
            this.ucPeopleCard1.Name = "ucPeopleCard1";
            this.ucPeopleCard1.Size = new System.Drawing.Size(808, 349);
            this.ucPeopleCard1.TabIndex = 0;
            // 
            // frmPepoleDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 355);
            this.Controls.Add(this.ucPeopleCard1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "frmPepoleDetails";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmPepoleDetails";
            this.ResumeLayout(false);

        }

        #endregion

        private ucPeopleCard ucPeopleCard1;
    }
}