namespace Client.Forms
{
    partial class UcGetAllClients
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
            this.btnDetailsClient = new System.Windows.Forms.Button();
            this.dgvGetAllClients = new System.Windows.Forms.DataGridView();
            this.btnSearchGroup = new System.Windows.Forms.Button();
            this.btnSearchName = new System.Windows.Forms.Button();
            this.cmbGroup = new System.Windows.Forms.ComboBox();
            this.txtName = new System.Windows.Forms.TextBox();
            this.lblGroup = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGetAllClients)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDetailsClient
            // 
            this.btnDetailsClient.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetailsClient.Location = new System.Drawing.Point(553, 439);
            this.btnDetailsClient.Name = "btnDetailsClient";
            this.btnDetailsClient.Size = new System.Drawing.Size(145, 43);
            this.btnDetailsClient.TabIndex = 16;
            this.btnDetailsClient.Text = "Detalji";
            this.btnDetailsClient.UseVisualStyleBackColor = true;
            this.btnDetailsClient.Click += new System.EventHandler(this.btnDetailsClient_Click);
            // 
            // dgvGetAllClients
            // 
            this.dgvGetAllClients.AllowUserToAddRows = false;
            this.dgvGetAllClients.AllowUserToDeleteRows = false;
            this.dgvGetAllClients.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGetAllClients.Location = new System.Drawing.Point(27, 138);
            this.dgvGetAllClients.Name = "dgvGetAllClients";
            this.dgvGetAllClients.ReadOnly = true;
            this.dgvGetAllClients.RowHeadersWidth = 51;
            this.dgvGetAllClients.RowTemplate.Height = 24;
            this.dgvGetAllClients.Size = new System.Drawing.Size(692, 276);
            this.dgvGetAllClients.TabIndex = 15;
            // 
            // btnSearchGroup
            // 
            this.btnSearchGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchGroup.Location = new System.Drawing.Point(424, 27);
            this.btnSearchGroup.Name = "btnSearchGroup";
            this.btnSearchGroup.Size = new System.Drawing.Size(135, 37);
            this.btnSearchGroup.TabIndex = 14;
            this.btnSearchGroup.Text = "Prikazi";
            this.btnSearchGroup.UseVisualStyleBackColor = true;
            this.btnSearchGroup.Click += new System.EventHandler(this.btnSearchGroup_Click);
            // 
            // btnSearchName
            // 
            this.btnSearchName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearchName.Location = new System.Drawing.Point(424, 80);
            this.btnSearchName.Name = "btnSearchName";
            this.btnSearchName.Size = new System.Drawing.Size(135, 39);
            this.btnSearchName.TabIndex = 13;
            this.btnSearchName.Text = "Nadji klijenta";
            this.btnSearchName.UseVisualStyleBackColor = true;
            this.btnSearchName.Click += new System.EventHandler(this.btnSearchName_Click);
            // 
            // cmbGroup
            // 
            this.cmbGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbGroup.FormattingEnabled = true;
            this.cmbGroup.Location = new System.Drawing.Point(251, 32);
            this.cmbGroup.Name = "cmbGroup";
            this.cmbGroup.Size = new System.Drawing.Size(167, 30);
            this.cmbGroup.TabIndex = 12;
            // 
            // txtName
            // 
            this.txtName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtName.Location = new System.Drawing.Point(251, 85);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(167, 28);
            this.txtName.TabIndex = 11;
            // 
            // lblGroup
            // 
            this.lblGroup.AutoSize = true;
            this.lblGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGroup.Location = new System.Drawing.Point(114, 32);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(131, 25);
            this.lblGroup.TabIndex = 10;
            this.lblGroup.Text = "Izaberi grupu:";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.Location = new System.Drawing.Point(51, 88);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(194, 24);
            this.lblName.TabIndex = 9;
            this.lblName.Text = "Pretrazi prema imenu:";
            // 
            // UcGetAllClients
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnDetailsClient);
            this.Controls.Add(this.dgvGetAllClients);
            this.Controls.Add(this.btnSearchGroup);
            this.Controls.Add(this.btnSearchName);
            this.Controls.Add(this.cmbGroup);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.lblGroup);
            this.Controls.Add(this.lblName);
            this.Name = "UcGetAllClients";
            this.Size = new System.Drawing.Size(750, 560);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGetAllClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnDetailsClient;
        private System.Windows.Forms.DataGridView dgvGetAllClients;
        private System.Windows.Forms.Button btnSearchGroup;
        private System.Windows.Forms.Button btnSearchName;
        private System.Windows.Forms.ComboBox cmbGroup;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label lblGroup;
        private System.Windows.Forms.Label lblName;
    }
}
