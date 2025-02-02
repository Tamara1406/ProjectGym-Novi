using System;
using Domain;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms.Coach
{
    public partial class FrmCoachDetails : Form
    {
        Domain.Coach coach;
        public FrmCoachDetails(Domain.Coach coach)
        {
            try
            {
                InitializeComponent();
                this.coach = coach;
                FillFields();
                MessageBox.Show("Sistem je učitao trenera!");
            }
            catch
            {
                MessageBox.Show("Sistem ne može da učita trenera!");
            }
        }


        public void FillFields()
        {
            txtFirstName.Text = coach.FirstName;
            txtLastName.Text = coach.LastName;
            cmbEducation.DataSource = ClientController.Instance.GetAllEducations();
            cmbEducation.Text = coach.Education.Qualifications;
        }

        private void btnDeleteCoach_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show($"Obriši trenera {coach.Name}?", "CONFIRMATION", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                List<Domain.Group> groups = ClientController.Instance.GetAllGroups();
                if (result == DialogResult.Yes)
                {
                    foreach (Domain.Group group in groups)
                    {
                        if (group.Coach.CoachID == coach.CoachID)
                        {
                            MessageBox.Show("Sistem ne može da obriše trenera!\nNije moguće obrisati trenera koji ima grupu vežbača!");
                            return;
                        }
                    }
                    ClientController.Instance.DeleteCoach(coach);
                    this.Close();
                    MessageBox.Show("Sistem je obrisao trenera!");
                }
            }
            catch
            {
                MessageBox.Show("Sistem ne može da obriše trenera!");
            }
            
            
            
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtFirstName.Text == "" || txtLastName.Text == "" || cmbEducation.SelectedIndex == -1)
                {
                    MessageBox.Show("Morate popuniti sva polja!\nSistem ne može da zapamti trenera!");
                    return;
                }
                coach.FirstName = txtFirstName.Text;
                coach.LastName = txtLastName.Text;
                coach.Education = (Education)cmbEducation.SelectedItem;


                if (ClientController.Instance.UpdateCoach(coach))
                {
                    MessageBox.Show("Sistem je zapamtio trenera!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Sistem ne može da zapamti trenera!");
                }
            }
            catch
            {
                MessageBox.Show("Sistem ne može da zapamti trenera!");
            }
            
        }
    }
}
