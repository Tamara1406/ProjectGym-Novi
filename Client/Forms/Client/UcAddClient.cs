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
    public partial class UcAddClient : UserControl
    {
        public UcAddClient()
        {
            InitializeComponent();
            FillComboBox();
        }

        public void FillComboBox()
        {
            cmbGroup.DataSource = ClientController.Instance.GetAllGroups();
            cmbGroup.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Domain.Client client = new Domain.Client();
                client.FirstName = txtFirstName.Text;
                client.LastName = txtLastName.Text;
                if (rdoFemale.Checked)
                {
                    client.Gender = Gender.Zenski;
                }
                else
                {
                    client.Gender = Gender.Muski;
                }
                client.Height = Int32.Parse(txtHeight.Text);
                client.Weight = Int32.Parse(txtWeight.Text);
                client.Group = (Domain.Group)cmbGroup.SelectedItem;

                if(cmbGroup.SelectedIndex == -1 || client.FirstName == null || client.FirstName == "" || client.LastName == null || client.LastName == "" || client.Height == 0 || client.Weight == 0)
                {
                    MessageBox.Show("Sistem ne može da kreira novog klijenta!");
                    return;
                }

                if (ClientController.Instance.CreateClient(client))
                {
                    MessageBox.Show("Sistem je kreirao novog klijenta!");
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Sistem ne može da kreira novog klijenta!");
                }
            } 
            catch
            {
                MessageBox.Show("Neuspešno kreiranje klijenta!");
            }
            
        }
    }
}
