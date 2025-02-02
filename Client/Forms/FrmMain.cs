using Client.Forms.Attendance;
using Client.Forms.Coach;
using Client.Forms.Group;
using Client.Forms.Term;
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
    public partial class FrmMain : Form
    {
        User user;
        UcGetAllCoach ucGetAllCoach;
        UcAddCoach ucAddCoach;
        UcGetAllClients ucGetAllClients;
        UcAddClient ucAddClient;
        UcAddAppointment ucAddAppointment;
        UcAddGroup ucAddGroup;
        FrmAddAttendances frmAddAttendances;
        public FrmMain(User user)
        {
            InitializeComponent();
            this.user = ClientController.Instance.GetUserByUsername(user);
            user = ClientController.Instance.GetUserByUsername(user);

        }

        private void tsmiGetAllCoach_Click(object sender, EventArgs e)
        {
            ucGetAllCoach = new UcGetAllCoach();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucGetAllCoach);
        }

        private void tsmiAddCoach_Click(object sender, EventArgs e)
        {
            ucAddCoach = new UcAddCoach();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucAddCoach);

        }

        private void tsmiGetAllClient_Click(object sender, EventArgs e)
        {
            ucGetAllClients = new UcGetAllClients();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucGetAllClients);
        }

        private void tsmiAddClient_Click(object sender, EventArgs e)
        {
            ucAddClient = new UcAddClient();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucAddClient);
        }

        private void dodajTerminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ucAddAppointment = new UcAddAppointment();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucAddAppointment);
        }
        

        private void tsmiAddGroup_Click(object sender, EventArgs e)
        {
            ucAddGroup = new UcAddGroup();
            pnlMain.Controls.Clear();
            pnlMain.Controls.Add(ucAddGroup);
        }

        private void čekiranjeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddAttendances = new FrmAddAttendances();
            frmAddAttendances.ShowDialog();
        }
    }
}
