using IAS1_FoodManagement_Feria.Properties;
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

namespace IAS1_FoodManagement_Feria.Authentication
{
    public partial class RegisterForm : Form
    {
        public RegisterForm()
        {
            InitializeComponent();
        }
        //     private void btnRegister_Click(object sender, EventArgs e)
        //    {
        //        if (String.IsNullOrWhiteSpace(txtUserName.Text) || String.IsNullOrWhiteSpace(txtPassword.Text) || String.IsNullOrWhiteSpace(txtConfirmPassword.Text))
        //        {
        //            MessageBox.Show("Please fill in all fields.", "Empty Fields");
        //            return;
        //        }

        //        if (txtPassword.Text != txtConfirmPassword.Text)
        //        {
        //            MessageBox.Show("Passwords do not match.", "Password Mismatch");
        //            return;
        //        }


        //        string query = "INSERT INTO tbl_user(username, password, status) VALUES (@username, @password, 'Pending')";

        //        try
        //        {
        //            using (MySqlConnection conn = IAS1_FoodManagement_Feria.SQLHelper.GetConn())
        //            {
        //                conn.Open();
        //                using (MySqlCommand cmd = new MySqlCommand(query, conn))
        //                {
        //                    cmd.Parameters.AddWithValue("@username", txtUserName.Text.Trim());
        //                    cmd.Parameters.AddWithValue("@password", Password.Hash(txtPassword.Text));
        //                    cmd.ExecuteNonQuery();

        //                    MessageBox.Show("User registered successfully!");
        //                    this.Close();
        //                }
        //            }
        //        }
        //        catch (MySqlException ex)
        //        {
        //            if (ex.Number == 1062)
        //            {
        //                MessageBox.Show("Username already exists.", "Duplicate Username", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //            else
        //            {
        //                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        }
        //        finally
        //        {
        //            Conn.Close();
        //        }
        //    }

        //    private void btnExit_Click(object sender, EventArgs e)
        //    {

        //        this.Close();
        //    }

        //    private void btnRegister_Click_1(object sender, EventArgs e)
        //    {

        //    }
    }
}
