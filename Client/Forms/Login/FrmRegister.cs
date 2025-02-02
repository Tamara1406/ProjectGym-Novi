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

namespace Client.Forms
{
    public partial class FrmRegister : Form
    {
        public FrmRegister()
        {
            InitializeComponent();
            GetAccount();
        }

        User newUser;

        public void GetAccount()
        {
            User user = new User
            {
                Username = ""
            };

            newUser = ClientController.Instance.GetUserByUsername(user);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            newUser.Username = txtUsername.Text;
            newUser.Password = txtPassword.Text;
            newUser.Email = txtEmail.Text;
            newUser.FirstName = txtFirstName.Text;
            newUser.LastName = txtLastName.Text;

            ValidatorClient validator = new ValidatorClient();

            if (!validator.CheckUserData(newUser))
            {
                MessageBox.Show("Uneli ste neispravne podatke!");
                return;
            }

            if (ClientController.Instance.SaveAccount(newUser))
            {
                MessageBox.Show("Nalog je kreiran!");
                this.Visible = false;
            }
            else
            {
                MessageBox.Show("Sistem ne može da kreira nalog");
            }
        }
    }
}
