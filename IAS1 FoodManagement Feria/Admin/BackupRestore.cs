using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    internal class BackupRestore
    {
        private static string GetMysqlsdumpLocation() { return "C:/Program Files/MySQL/MySQL Server 8.0/bin/mysqldump.exe"; }
        private static string Database() { return "dbs_lab1"; }

        internal static void RestoreDatabase()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SQL Files (*.sql)|*.sql";
            ofd.Title = "Select SQL Backup File";


            if (ofd.ShowDialog() != DialogResult.OK)
                return;


            string sqlFile = ofd.FileName;
            string script = System.IO.File.ReadAllText(sqlFile);
            string[] commands = script.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

            string connString = "server=localhost; userid=root; password=root; database=" + Database() + ";";

            using (MySqlConnection conn = new MySqlConnection(connString))
            {
                conn.Open();
                foreach (string cmdText in commands)
                {
                    string cleanCmd = cmdText.Trim();

                    if (cleanCmd != "")
                    {
                        using (MySqlCommand cmd = new MySqlCommand(cleanCmd, conn))
                        {

                            try
                            {
                                cmd.ExecuteNonQuery();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Error running command: " + cleanCmd + "\t" + ex.Message, "Database Restore Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                        //AuditLogging.AddEntry("Database restore error", ex.Message)
                            }
                        }
                    }
                    //AuditLogging.AddEntry("Database restored", "")
                }
                MessageBox.Show("Database was restored successfully. ", "Database Restore Successful");
            }
        }

        public static void BackupDatabase()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "SQL Files (*.sql)|*.sql";
            sfd.Title = "Save MySQL Backup";
            sfd.FileName = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.sql";

            if (sfd.ShowDialog() != DialogResult.OK)
                return;

            string backupFile = sfd.FileName;
            string arguments = "--user=root --password=root --databases " + Database() + " --result-file=\"" + backupFile + "\"";

            try
            {
                Process p = new Process();
                p.StartInfo.FileName = GetMysqlsdumpLocation();
                p.StartInfo.Arguments = arguments;
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;

                p.Start();
                p.WaitForExit();
                //AuditLogging.AddEntry("Database backed up", "")
                MessageBox.Show("Database was succesfully backuped. Saved to: " + backupFile, "Database Backup Successful");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Database Backup Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //AuditLogging.AddEntry("Database backed up error", ex.Message)
            }
        }
    }
}
