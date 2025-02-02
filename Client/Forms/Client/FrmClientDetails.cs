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

namespace Client
{
    public partial class FrmClientDetails : Form
    {
        Domain.Client client;
        public FrmClientDetails(Domain.Client client)
        {
            try
            {
                InitializeComponent();
                this.client = client;
                FillFields();
                MessageBox.Show("Sistem je učitao klijenta!");
            }
            catch
            {
                MessageBox.Show("Sistem ne može da učita klijenta!");
            }
            
        }

        public void FillFields()
        {
            txtFirstName.Text = client.FirstName;
            txtLastName.Text = client.LastName;
            txtGender.Text = client.Gender.ToString();
            txtHeigth.Text = client.Height.ToString();
            txtWeight.Text = client.Weight.ToString();
            cmbGroup.DataSource = ClientController.Instance.GetAllGroups();
            cmbGroup.Text = client.Group.GroupName;
        }

        private void btnDeleteClient_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show($"Obriši klijenta {client.Name}?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                List<Attendance> attendances = ClientController.Instance.GetAllAttendances();
                foreach (Attendance att in attendances)
                {
                    if (att.Client.ClientID == client.ClientID)
                        ClientController.Instance.DeleteAttendance(att);
                }
                ClientController.Instance.DeleteClient(client);
                this.Close();
                MessageBox.Show("Klijent je obrisan!");
            }
            catch
            {
                MessageBox.Show("Sistem ne moze da obrise klijenta!!");

            }
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                client.FirstName = txtFirstName.Text;
                client.LastName = txtLastName.Text;
                if (txtGender.Text == "Muski")
                {
                    client.Gender = Gender.Muski;
                }
                else if (txtGender.Text == "Zenski")
                {
                    client.Gender = Gender.Zenski;
                }
                client.Height = Int32.Parse(txtHeigth.Text);
                client.Weight = Int32.Parse(txtWeight.Text);
                if(cmbGroup.SelectedIndex == -1)
                {
                    MessageBox.Show("Sistem ne može da zapamti klijenta!");
                    return;
                }
                client.Group = (Group)cmbGroup.SelectedItem;


                if (ClientController.Instance.UpdateClient(client))
                {
                    MessageBox.Show("Sistem je zapamtio klijenta!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sistem ne može da zapamti klijenta!");
                }
            }
            catch
            {
                MessageBox.Show("Sistem ne može da zapamti klijenta!");
            }
            
        }
    }
}
