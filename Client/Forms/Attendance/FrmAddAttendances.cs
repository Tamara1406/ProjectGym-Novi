using Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms.Attendance
{
    public partial class FrmAddAttendances : Form
    {
        Domain.Group group;
        Appointment appointment;
        public FrmAddAttendances()
        {
            InitializeComponent();
            FillComboBox();
            cmbAppointment.Visible = false;
            dgvClients.Visible = false;
            btnSave.Visible = false;
            lblAppointment.Visible = false;
            
        }


        private void FillComboBox()
        {
            cmbGroup.DataSource = ClientController.Instance.GetAllGroups();
            cmbGroup.SelectedIndex = -1;
        }

        private bool HaveAttendances(Domain.Client client)
        {
            List<Domain.Attendance> attendances = ClientController.Instance.GetAllAttendances();
            foreach (Domain.Attendance attendance in attendances)
            {
                if (attendance.Appointment.AppointmentID == ((Appointment)cmbAppointment.SelectedItem).AppointmentID && attendance.Client.ClientID == client.ClientID)
                {
                    return true;
                }
            }
            return false;
        }

        private void cmbGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cmbGroup.SelectedIndex == -1)
                    return;
                group = cmbGroup.SelectedItem as Domain.Group;
                cmbAppointment.Visible = true;
                lblAppointment.Visible=true;
                dgvClients.Visible = false;
                btnSave.Visible = false;
                cmbAppointment.SelectedIndex = -1;
                cmbAppointment.DataSource = ClientController.Instance.GetAllAppointmentsByGroup(group);
            }
            catch
            {
                MessageBox.Show("Sistem ne može da nađe termine za ovu grupu.");
            }
            
        }

        private void cmbAppointment_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbGroup.SelectedIndex != -1 && cmbAppointment.SelectedIndex != -1)
            {
                appointment = cmbAppointment.SelectedItem as Domain.Appointment;
                dgvClients.Visible = true;
                btnSave.Visible = true;
                List<Domain.Client> clients = ClientController.Instance.GetAllClientsByGroup(group);

                
                dgvClients.Columns.Clear();
                dgvClients.DataSource = clients;

                DataGridViewCheckBoxColumn attendancess = new DataGridViewCheckBoxColumn();
                attendancess.HeaderText = "Prisustvo";
                attendancess.Name = "Prisustvo";
                dgvClients.Columns.Add(attendancess);

                dgvClients.Columns["Weight"].Visible = false;
                dgvClients.Columns["Height"].Visible = false;
                dgvClients.Columns["Gender"].Visible = false;

                List<Domain.Attendance> attendances = ClientController.Instance.GetAllAttendances();
                foreach (DataGridViewRow row in dgvClients.Rows)
                {
                    
                    Domain.Client client = row.DataBoundItem as Domain.Client;
                    Domain.Attendance attendance = attendances.FirstOrDefault(a => a.Client.ClientID == client.ClientID && a.Appointment.AppointmentID == appointment.AppointmentID);
                    if (attendance != null)
                    {
                        DataGridViewCheckBoxCell cell = row.Cells["Prisustvo"] as DataGridViewCheckBoxCell;
                        if (cell != null)
                        {
                            cell.Value = attendance.IsAttend;
                        }
                    }
                }
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            List<Domain.Client> clients = ClientController.Instance.GetAllClientsByGroup(group);
            List<Domain.Attendance> attendances = new List<Domain.Attendance>();
            int i = 0;
            foreach (DataGridViewRow row in dgvClients.Rows)
            {
                Domain.Attendance attendance = new Domain.Attendance();
                attendance.Client = clients[i++];
                attendance.Appointment = appointment;
                DataGridViewCheckBoxCell cell = row.Cells["Prisustvo"] as DataGridViewCheckBoxCell;
                if (Convert.ToBoolean(cell.Value))
                {
                    attendance.IsAttend = true;
                }
                else if (!Convert.ToBoolean(row.Cells["Prisustvo"].Value))
                {
                    attendance.IsAttend = false;
                }

                attendances.Add(attendance);
            }
            foreach (Domain.Client client in clients)
            {
                if (HaveAttendances(client))
                {
                    MessageBox.Show("Prisustva su već bila zabeležena!");
                    Visible = false;
                    return;
                }
            }
            if (ClientController.Instance.CreateAttendances(attendances))
            {
                MessageBox.Show("Prisustva su zabeležena!");
                Visible = false;
            }
            else
            {
                MessageBox.Show("Neuspešno obeležavanje prisustva!");
            }
        }
    }
}
