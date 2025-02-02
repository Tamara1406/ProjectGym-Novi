namespace Client.Forms.Group
{
    partial class UcAddGroup
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
            this.cmbCoach = new System.Windows.Forms.ComboBox();
            this.lblCoach = new System.Windows.Forms.Label();
            this.txtGroupName = new System.Windows.Forms.TextBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblGroupName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbCoach
            // 
            this.cmbCoach.FormattingEnabled = true;
            this.cmbCoach.Location = new System.Drawing.Point(235, 126);
            this.cmbCoach.Name = "cmbCoach";
            this.cmbCoach.Size = new System.Drawing.Size(135, 24);
            this.cmbCoach.TabIndex = 22;
            // 
            // lblCoach
            // 
            this.lblCoach.AutoSize = true;
            this.lblCoach.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCoach.Location = new System.Drawing.Point(59, 124);
            this.lblCoach.Name = "lblCoach";
            this.lblCoach.Size = new System.Drawing.Size(72, 24);
            this.lblCoach.TabIndex = 21;
            this.lblCoach.Text = "Trener:";
            // 
            // txtGroupName
            // 
            this.txtGroupName.Location = new System.Drawing.Point(235, 60);
            this.txtGroupName.Name = "txtGroupName";
            this.txtGroupName.Size = new System.Drawing.Size(135, 22);
            this.txtGroupName.TabIndex = 20;
            // 
            // btnSave
            // 
            this.btnSave.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSave.Location = new System.Drawing.Point(156, 225);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(130, 45);
            this.btnSave.TabIndex = 19;
            this.btnSave.Text = "Sačuvaj";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblGroupName
            // 
            this.lblGroupName.AutoSize = true;
            this.lblGroupName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroupName.Location = new System.Drawing.Point(59, 58);
            this.lblGroupName.Name = "lblGroupName";
            this.lblGroupName.Size = new System.Drawing.Size(116, 24);
            this.lblGroupName.TabIndex = 18;
            this.lblGroupName.Text = "Naziv grupe:";
            // 
            // UcAddGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cmbCoach);
            this.Controls.Add(this.lblCoach);
            this.Controls.Add(this.txtGroupName);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.lblGroupName);
            this.Name = "UcAddGroup";
            this.Size = new System.Drawing.Size(467, 314);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCoach;
        private System.Windows.Forms.Label lblCoach;
        private System.Windows.Forms.TextBox txtGroupName;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Label lblGroupName;
    }
}
