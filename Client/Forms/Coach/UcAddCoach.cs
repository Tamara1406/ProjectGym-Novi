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
    public partial class UcAddCoach : UserControl
    {
        public UcAddCoach()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            cmbEducation.Items.Clear();
            cmbEducation.DataSource = ClientController.Instance.GetAllEducations();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Domain.Coach coach = new Domain.Coach();
                if(txtFirstName.Text == "" || txtLastName.Text == "" || cmbEducation.SelectedIndex == -1)
                {
                    MessageBox.Show("Morate popuniti sva polja!\nSistem ne može da kreira trenera!");
                    return;
                }
                coach.FirstName = txtFirstName.Text;
                coach.LastName = txtLastName.Text;
                coach.Education = (Domain.Education)cmbEducation.SelectedItem;

                if (ClientController.Instance.CreateCoach(coach))
                {
                    MessageBox.Show("Sistem je kreirao trenera!");
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Sistem ne može da kreira trenera!");
                }
            }
            catch
            {
                MessageBox.Show("Sistem ne može da kreira trenera!");
            }
            
        }
    }
}
