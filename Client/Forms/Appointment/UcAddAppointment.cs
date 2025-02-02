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

namespace Client.Forms.Term
{
    public partial class UcAddAppointment : UserControl
    {
        public UcAddAppointment()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            cmbGroup.DataSource = ClientController.Instance.GetAllGroups();
            cmbGroup.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if  (txtNumOfApp.Text == "" || dtpDate.Text == "" || cmbGroup.SelectedIndex == -1)
                {
                    MessageBox.Show("Morate popuniti sva polja!\nSistem ne može da kreira novi termin!");
                    return;
                }

                Domain.Appointment appointment = new Domain.Appointment();
                appointment.NumberOfAppointments = Int32.Parse(txtNumOfApp.Text);
                appointment.Time = DateTime.Parse(dtpDate.Value.ToString("dd/MM/yyyy"));
                appointment.Group = (Domain.Group)cmbGroup.SelectedItem;
                
                if (ClientController.Instance.CreateAppointment(appointment))
                {
                    MessageBox.Show("Sistem je kreirao novi termin!");
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Sistem ne može da kreira novi termin!");
                }
            }
            catch
            {
                MessageBox.Show("Sistem ne može da kreira novi termin!");
            }
            

        }
    }
}
