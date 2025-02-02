using Client.Forms.Coach;
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
    public partial class UcGetAllClients : UserControl
    {
        public UcGetAllClients()
        {
            InitializeComponent();
            GetAllClients();
            FillComboBox();
            btnDetailsClient.Visible = false;
            dgvGetAllClients.Visible = false;
        }

        public void GetAllClients()
        {
            dgvGetAllClients.DataSource = ClientController.Instance.GetAllClients();
        }


        public void FillComboBox()
        {
            cmbGroup.DataSource = ClientController.Instance.GetAllGroups();
            cmbGroup.SelectedIndex = -1;
        }

        private void btnSearchName_Click(object sender, EventArgs e)
        {
            dgvGetAllClients.Visible = true;
            btnDetailsClient.Visible = true;
            string searchStr = txtName.Text;
            dgvGetAllClients.DataSource = null;
            dgvGetAllClients.DataSource = ClientController.Instance.GetClientSearchedByName(searchStr);
            dgvGetAllClients.ClearSelection();
            if (dgvGetAllClients.RowCount == 0)
            {
                MessageBox.Show("Ne postoji klijent sa tim imenom! \n Sistem ne može da nadje klijente po zadatoj vrednosti.");
                txtName.Text = "";
                return;
            }
            MessageBox.Show("Sistem je našao klijente po zadatoj vrednosti!");
            txtName.Text = "";
        }

        private void btnSearchGroup_Click(object sender, EventArgs e)
        {
            try
            {
                dgvGetAllClients.Visible = true;
                btnDetailsClient.Visible = true;
                Domain.Group group = (Domain.Group)cmbGroup.SelectedItem;
                cmbGroup.SelectedIndex = -1;
                dgvGetAllClients.DataSource = null;
                if(group != null)
                {
                    dgvGetAllClients.DataSource = ClientController.Instance.GetAllClientsByGroup(group);
                    MessageBox.Show("Sistem je našao klijente po zadatoj vrednosti!");
                } else
                {
                    MessageBox.Show("Sistem ne može da nadje klijente po zadatoj vrednosti.");
                }
                
            }
            catch
            {
                MessageBox.Show("Sistem ne može da nadje klijente po zadatoj vrednosti.");
            }
            
        }

        private void btnDetailsClient_Click(object sender, EventArgs e)
        {
            try
            {
                Domain.Client clientToFind = (Domain.Client)dgvGetAllClients.SelectedRows[0].DataBoundItem;
                Domain.Client client = ClientController.Instance.GetClient(clientToFind);
                FrmClientDetails frm = new FrmClientDetails(client);
                frm.ShowDialog();
                dgvGetAllClients.DataSource = ClientController.Instance.GetAllClients();
            }
            catch
            {
                MessageBox.Show("Morate označiti ceo red!\nSistem ne može da učita klijenta!");
            }

        }
    }
}
