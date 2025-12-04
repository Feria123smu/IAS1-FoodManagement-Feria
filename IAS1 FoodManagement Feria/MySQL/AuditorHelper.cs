using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using static IAS1_FoodManagement_Feria.SQLHelper;
using static IAS1_FoodManagement_Feria.Properties.Module;

namespace IAS1_FoodManagement_Feria.MySQL
{
    internal static class AuditorHelper
    {
        internal static void Log(string auditActivity)
        {
            Log(auditActivity, "");
        }

        internal static void Log(string auditActivity, string details)
        {
            Log(-1, GetLoggedInUsername(), "", auditActivity, details);
        }

        internal static void Log(int userid, string username, string role, string auditActivity, string details)
        {
            using (MySqlConnection connection = GetConn())
            {
                try
                {
                    connection.Open();
                    using (MySqlCommand command = new MySqlCommand("INSERT INTO audit_tbl (userid, username, role, logDate, auditActivity, details) VALUES (@userid, @username, @role, NOW(), @auditActivity, @details);", connection))
                    {
                        command.Parameters.AddWithValue("@userid", userid);
                        command.Parameters.AddWithValue("@username", username);
                        command.Parameters.AddWithValue("@role", role);
                        command.Parameters.AddWithValue("@auditActivity", auditActivity);
                        command.Parameters.AddWithValue("@details", details);
                        command.ExecuteNonQuery();
                    }
                }
                catch (MySqlException e)
                {
                    MessageBox.Show(e.Message, "System Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
