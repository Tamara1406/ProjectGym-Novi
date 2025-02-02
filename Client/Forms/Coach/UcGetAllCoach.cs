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

namespace Client.Forms.Coach
{
    public partial class UcGetAllCoach : UserControl
    {
        public UcGetAllCoach()
        {
            InitializeComponent();
            GetAllCoach();
            FillComboBox();
        }

        public void GetAllCoach()
        {
            dgvGetAllCoach.DataSource = ClientController.Instance.GetAllCoaches();
        }


        public void FillComboBox()
        {
            cmbEducation.DataSource = ClientController.Instance.GetAllEducations();
            cmbEducation.SelectedIndex = -1;
        }

        private void btnName_Click(object sender, EventArgs e)
        {
            try
            {
                string searchStr = txtName.Text;
                dgvGetAllCoach.DataSource = null;
                dgvGetAllCoach.DataSource = ClientController.Instance.GetCoachSearchedByName(searchStr);
                dgvGetAllCoach.ClearSelection();
                if (dgvGetAllCoach.RowCount == 0)
                {
                    MessageBox.Show("Ne postoji trener sa tim imenom!\nSistem ne može da nađe trenere po zadatoj vrednosti!");
                    txtName.Text = "";
                    return;
                }
                MessageBox.Show("Sistem je našao trenere po zadatoj vrednosti!");
                txtName.Text = "";
            }
            catch
            {
                MessageBox.Show("Sistem ne može da nađe trenere po zadatoj vrednosti!");
            }
            
        }

        private void btnEducation_Click(object sender, EventArgs e)
        {
            try
            {
                Education education = (Education)cmbEducation.SelectedItem;
                if (cmbEducation.SelectedIndex == -1)
                {
                    MessageBox.Show("Sistem ne može da nađe trenere po zadatoj vrednosti!");
                    return;
                }
                cmbEducation.SelectedIndex = -1;
                dgvGetAllCoach.DataSource = null;
                dgvGetAllCoach.DataSource = ClientController.Instance.GetAllCoachByEducation(education);
                MessageBox.Show("Sistem je našao trenere po zadatoj vrednosti!");
            }
            catch
            {
                MessageBox.Show("Sistem ne može da nađe trenere po zadatoj vrednosti!");
            }
            
        }

        private void btnDetails_Click(object sender, EventArgs e)
        {
            try
            {
                Domain.Coach coachToFind = (Domain.Coach)dgvGetAllCoach.SelectedRows[0].DataBoundItem;
                Domain.Coach coach = ClientController.Instance.GetCoach(coachToFind);
                FrmCoachDetails frm = new FrmCoachDetails(coach);
                frm.ShowDialog();
                dgvGetAllCoach.DataSource = ClientController.Instance.GetAllCoaches();
            } 
            catch
            {
                MessageBox.Show("Morate označiti ceo red!\nSIstem ne može da učita trenera!");
            }
            

        }

    }
}
