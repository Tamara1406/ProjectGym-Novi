namespace Client.Forms
{
    partial class FrmMain
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
            this.pnlMain = new System.Windows.Forms.Panel();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.treneriToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetAllCoach = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddCoach = new System.Windows.Forms.ToolStripMenuItem();
            this.klijentiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiGetAllClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddClient = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAddGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.terminiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dodajTerminToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.čekiranjeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Location = new System.Drawing.Point(12, 63);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(922, 529);
            this.pnlMain.TabIndex = 0;
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.treneriToolStripMenuItem,
            this.klijentiToolStripMenuItem,
            this.terminiToolStripMenuItem,
            this.čekiranjeToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(946, 33);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // treneriToolStripMenuItem
            // 
            this.treneriToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGetAllCoach,
            this.tsmiAddCoach});
            this.treneriToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.treneriToolStripMenuItem.Name = "treneriToolStripMenuItem";
            this.treneriToolStripMenuItem.Size = new System.Drawing.Size(84, 29);
            this.treneriToolStripMenuItem.Text = "Treneri";
            // 
            // tsmiGetAllCoach
            // 
            this.tsmiGetAllCoach.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsmiGetAllCoach.Name = "tsmiGetAllCoach";
            this.tsmiGetAllCoach.Size = new System.Drawing.Size(224, 30);
            this.tsmiGetAllCoach.Text = "Prikaži trenere";
            this.tsmiGetAllCoach.Click += new System.EventHandler(this.tsmiGetAllCoach_Click);
            // 
            // tsmiAddCoach
            // 
            this.tsmiAddCoach.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsmiAddCoach.Name = "tsmiAddCoach";
            this.tsmiAddCoach.Size = new System.Drawing.Size(224, 30);
            this.tsmiAddCoach.Text = "Dodaj trenera";
            this.tsmiAddCoach.Click += new System.EventHandler(this.tsmiAddCoach_Click);
            // 
            // klijentiToolStripMenuItem
            // 
            this.klijentiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiGetAllClient,
            this.tsmiAddClient,
            this.tsmiAddGroup});
            this.klijentiToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.klijentiToolStripMenuItem.Name = "klijentiToolStripMenuItem";
            this.klijentiToolStripMenuItem.Size = new System.Drawing.Size(84, 29);
            this.klijentiToolStripMenuItem.Text = "Klijenti";
            // 
            // tsmiGetAllClient
            // 
            this.tsmiGetAllClient.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsmiGetAllClient.Name = "tsmiGetAllClient";
            this.tsmiGetAllClient.Size = new System.Drawing.Size(224, 30);
            this.tsmiGetAllClient.Text = "Prikaži klijente";
            this.tsmiGetAllClient.Click += new System.EventHandler(this.tsmiGetAllClient_Click);
            // 
            // tsmiAddClient
            // 
            this.tsmiAddClient.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.tsmiAddClient.Name = "tsmiAddClient";
            this.tsmiAddClient.Size = new System.Drawing.Size(224, 30);
            this.tsmiAddClient.Text = "Dodaj klijenta";
            this.tsmiAddClient.Click += new System.EventHandler(this.tsmiAddClient_Click);
            // 
            // tsmiAddGroup
            // 
            this.tsmiAddGroup.Name = "tsmiAddGroup";
            this.tsmiAddGroup.Size = new System.Drawing.Size(224, 30);
            this.tsmiAddGroup.Text = "Kreiraj grupu";
            this.tsmiAddGroup.Click += new System.EventHandler(this.tsmiAddGroup_Click);
            // 
            // terminiToolStripMenuItem
            // 
            this.terminiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.dodajTerminToolStripMenuItem});
            this.terminiToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.terminiToolStripMenuItem.Name = "terminiToolStripMenuItem";
            this.terminiToolStripMenuItem.Size = new System.Drawing.Size(88, 29);
            this.terminiToolStripMenuItem.Text = "Termini";
            // 
            // dodajTerminToolStripMenuItem
            // 
            this.dodajTerminToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dodajTerminToolStripMenuItem.Name = "dodajTerminToolStripMenuItem";
            this.dodajTerminToolStripMenuItem.Size = new System.Drawing.Size(224, 30);
            this.dodajTerminToolStripMenuItem.Text = "Dodaj termin";
            this.dodajTerminToolStripMenuItem.Click += new System.EventHandler(this.dodajTerminToolStripMenuItem_Click);
            // 
            // čekiranjeToolStripMenuItem
            // 
            this.čekiranjeToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.čekiranjeToolStripMenuItem.Name = "čekiranjeToolStripMenuItem";
            this.čekiranjeToolStripMenuItem.Size = new System.Drawing.Size(105, 29);
            this.čekiranjeToolStripMenuItem.Text = "Čekiranje";
            this.čekiranjeToolStripMenuItem.Click += new System.EventHandler(this.čekiranjeToolStripMenuItem_Click);
            // 
            // FrmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 625);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "FrmMain";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem treneriToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem klijentiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem terminiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetAllCoach;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddCoach;
        private System.Windows.Forms.ToolStripMenuItem tsmiGetAllClient;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddClient;
        private System.Windows.Forms.ToolStripMenuItem dodajTerminToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiAddGroup;
        private System.Windows.Forms.ToolStripMenuItem čekiranjeToolStripMenuItem;
    }
}