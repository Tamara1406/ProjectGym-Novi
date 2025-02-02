using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms.Login
{
    public partial class FrmLogin : Form
    {
        ClientCommunication communication;
        public FrmLogin()
        {
            InitializeComponent();
            communication = new ClientCommunication();
            communication.ConnectToServer();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            User user = new User
            {
                Username = txtUsername.Text,
                Password = txtPassword.Text,
            };
            if (ClientController.Instance.LoginClient(user) == 1)
            {
                MessageBox.Show($"Dobrodošli {user.Username}!");
                this.Visible = false;
                FrmMain frmMain = new FrmMain(user);
                frmMain.ShowDialog();
                this.Visible = true;
            }
            else
            {
                MessageBox.Show("Neuspešna prijava!");
            }
        }

        private void btnRegister_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Da li ste sigurni da želite da kreirate novi nalog?", "Potvrda za kreiranje naloga", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                ClientController.Instance.CreateAccount(new User());
                this.Visible = false;
                FrmRegister frm = new FrmRegister();
                frm.ShowDialog();
                this.Visible = true;
            }
        }
    }
}
