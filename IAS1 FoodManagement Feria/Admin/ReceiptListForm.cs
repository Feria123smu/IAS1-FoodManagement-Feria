using IAS1_FoodManagement_Feria.MySQL;
using MySql.Data.MySqlClient;
using Mysqlx.Crud;
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

namespace IAS1_FoodManagement_Feria
{
    public partial class ReceiptListForm : Form
    {
        private int id = -1;
        public ReceiptListForm()
        {
            InitializeComponent();
        }

        private void dgvReceipts_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                using (MySqlConnection conn = GetConn())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM receipt_item_tbl WHERE receipt_id=@id;", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                using (MySqlConnection conn = GetConn())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("DELETE FROM receipts_tbl WHERE id=@id;", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        AuditorHelper.Log("Receipt deleted successfully", "id=\"" + id + "\"");
                        MessageBox.Show("Receipt deleted successfully.", "Receipt Deleted");
                        ListDownItems();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Receipt Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ReceiptListForm_Load(object sender, EventArgs e)
        {
            ListDownItems();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ListDownItems();
            
        }

        private void ListDownItems()
        {
            using (MySqlConnection conn = GetConn())
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM receipts_tbl", conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvReceipts.DataSource = table;
                }
            }

            using (MySqlConnection conn = GetConn())
            {
                conn.Open();

                using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM receipt_item_tbl", conn))
                {
                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable table = new DataTable();
                    adapter.Fill(table);
                    dgvReceiptItems.DataSource = table;
                }
            }
        }

        private void dgvReceipts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            id = Convert.ToInt32(dgvReceipts.Rows[dgvReceipts.CurrentCell.RowIndex].Cells["id"].Value);
            txtID.Text = "ID: " + id;

            //dgvReceipts.CurrentCell.RowIndex;

            //using (MySqlConnection conn = GetConn())
            //{
            //    conn.Open();

            //    using (MySqlCommand cmd = new MySqlCommand("SELECT * FROM receipts_tbl", conn))
            //    {
            //        MySqlDataReader reader = cmd.ExecuteReader();
            //        if (reader.HasRows)
            //        {
            //            txtID.Text = "ID: " + reader.GetInt32("id");
            //        }
            //    }
            //}
        }
    }
}