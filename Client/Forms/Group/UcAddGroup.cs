using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client.Forms.Group
{
    public partial class UcAddGroup : UserControl
    {
        public UcAddGroup()
        {
            InitializeComponent();
            FillComboBox();
        }

        private void FillComboBox()
        {
            cmbCoach.DataSource = ClientController.Instance.GetAllCoaches();
            cmbCoach.SelectedIndex = -1;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtGroupName.Text == "" || cmbCoach.SelectedIndex == -1)
                {
                    MessageBox.Show("Morate popuniti sva polja!\nSistem ne može da kreira novi termin!");
                    return;
                }
                Domain.Group group = new Domain.Group();
                group.GroupName = txtGroupName.Text;
                group.Coach = (Domain.Coach)cmbCoach.SelectedItem;

                if (ClientController.Instance.CreateGroup(group))
                {
                    MessageBox.Show("Sistem je kreirao novu grupu!");
                    Visible = false;
                }
                else
                {
                    MessageBox.Show("Sistem ne može da kreira novu grupu!");
                }
            }
            catch
            {
                MessageBox.Show("Sistem ne može da kreira novu grupu!");
            }
        }
    }
}
