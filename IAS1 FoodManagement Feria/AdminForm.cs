using IAS1_FoodManagement_Feria.Food;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            CashierForm form = new CashierForm();
            FormManagement.PlaceForm(form, panel1);
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupRestore.BackupDatabase();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupRestore.RestoreDatabase();
        }
    }
}
