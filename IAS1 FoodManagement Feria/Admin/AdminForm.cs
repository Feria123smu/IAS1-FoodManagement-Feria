using IAS1_FoodManagement_Feria.Food;
using IAS1_FoodManagement_Feria.MySQL;
using IAS1_FoodManagement_Feria.Properties;
using System;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    public partial class AdminForm : Form
    {
        // Pre-load cashier form so it stays persistent
        private CashierForm cashier = new CashierForm();

        public AdminForm()
        {
            InitializeComponent();
        }

        private void AdminForm_Load(object sender, EventArgs e)
        {
            // Default screen when admin logs in
            FormManagement.PlaceForm(cashier, panel1);
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupRestore.BackupDatabase();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackupRestore.RestoreDatabase();
        }

        private void receiptsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptListForm form = new ReceiptListForm();
            FormManagement.PlaceForm(form, panel1);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // Show Cashier screen again
            FormManagement.PlaceForm(cashier, panel1);
        }

        private void auditTrailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // If you have an Audit Trail Form:
            // AuditTrailForm audit = new AuditTrailForm();
            // FormManagement.PlaceForm(audit, panel1);
            AuditForm form = new AuditForm();
            FormManagement.PlaceForm(form, panel1);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Module.Logout();
        }
    }
}
