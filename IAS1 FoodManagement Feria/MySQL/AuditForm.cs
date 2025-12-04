using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static IAS1_FoodManagement_Feria.SQLHelper;

namespace IAS1_FoodManagement_Feria.MySQL
{
    public partial class AuditForm : Form
    {
        public AuditForm()
        {
            InitializeComponent();
        }

        private void AuditForm_Load(object sender, EventArgs e)
        {
            FillTable();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            FillTable();
        }

        private void FillTable()
        {
            using (MySqlConnection con = GetConn())
            {
                con.Open();
                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM audit_tbl", con))
                {
                    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dgvAudit.DataSource = dt;
                }
            }
        }
    }
}
