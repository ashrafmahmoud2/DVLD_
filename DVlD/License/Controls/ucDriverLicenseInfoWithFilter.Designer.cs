namespace DVlD.License
{
    partial class ucLicenseLicenseInfoWithFilter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.btnFind = new System.Windows.Forms.Button();
            this.gbFilters = new System.Windows.Forms.GroupBox();
            this.txtLicenseID = new System.Windows.Forms.TextBox();
            this.ucDriverLicenseInfo1 = new DVlD.License.ucDriverLicenseInfo();
            this.gbFilters.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(49, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 20);
            this.label1.TabIndex = 26;
            this.label1.Text = "LicenseID";
            // 
            // btnFind
            // 
            this.btnFind.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFind.Image = global::DVlD.Properties.Resources.SearchPerson;
            this.btnFind.Location = new System.Drawing.Point(327, 3);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(44, 37);
            this.btnFind.TabIndex = 27;
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.btnFind_Click_1);
            // 
            // gbFilters
            // 
            this.gbFilters.Controls.Add(this.label1);
            this.gbFilters.Controls.Add(this.txtLicenseID);
            this.gbFilters.Controls.Add(this.btnFind);
            this.gbFilters.Location = new System.Drawing.Point(183, 3);
            this.gbFilters.Name = "gbFilters";
            this.gbFilters.Size = new System.Drawing.Size(431, 44);
            this.gbFilters.TabIndex = 30;
            this.gbFilters.TabStop = false;
            this.gbFilters.Enter += new System.EventHandler(this.gbFilters_Enter);
            // 
            // txtLicenseID
            // 
            this.txtLicenseID.Location = new System.Drawing.Point(151, 13);
            this.txtLicenseID.Name = "txtLicenseID";
            this.txtLicenseID.Size = new System.Drawing.Size(170, 20);
            this.txtLicenseID.TabIndex = 29;
            this.txtLicenseID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtLicenseID_KeyPress_1);
            // 
            // ucDriverLicenseInfo1
            // 
            this.ucDriverLicenseInfo1.Location = new System.Drawing.Point(0, 48);
            this.ucDriverLicenseInfo1.Name = "ucDriverLicenseInfo1";
            this.ucDriverLicenseInfo1.Size = new System.Drawing.Size(847, 379);
            this.ucDriverLicenseInfo1.TabIndex = 0;
            // 
            // ucLicenseLicenseInfoWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.Controls.Add(this.ucDriverLicenseInfo1);
            this.Controls.Add(this.gbFilters);
            this.Name = "ucLicenseLicenseInfoWithFilter";
            this.Size = new System.Drawing.Size(847, 367);
            this.gbFilters.ResumeLayout(false);
            this.gbFilters.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ucDriverLicenseInfo ucDriverLicenseInfo1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.GroupBox gbFilters;
        private System.Windows.Forms.TextBox txtLicenseID;
    }
}
