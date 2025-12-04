using IAS1_FoodManagement_Feria.Authentication;
using IAS1_FoodManagement_Feria.Properties;
using MySql.Data.MySqlClient;
using System;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    public partial class LoginForm : Form
    {
        private int loginAttempts = 0;
        private Action LoadRegisterForm;

        private MySqlConnection Conn = new MySqlConnection(
            "Server=localhost;Database=dbs_lab1 ;Uid=root;Pwd=root;"
        );

        public LoginForm()
        {
            InitializeComponent();
            InitializeCapsLockIndicator();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login();
        }

        internal void SetRegisterForm(Action LoadRegisterForm)
        {
            this.LoadRegisterForm = LoadRegisterForm;
        }

        private void InitializeCapsLockIndicator()
        {
            lblCapsWarning.Text = "";
            if (Control.IsKeyLocked(Keys.CapsLock))
                lblCapsWarning.Text = "Caps Lock is ON.";

            this.KeyDown += (sender, e) =>
            {
                lblCapsWarning.Text = Control.IsKeyLocked(Keys.CapsLock) ? "Caps Lock is ON." : "";
            };

            this.KeyPreview = true;
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = chkShowPassword.Checked ? '\0' : '•';
        }

        private void Login()
        {
            string query = "SELECT * FROM tbl_user WHERE username=@username AND password=@password";
            Console.WriteLine("Hashed Password \"" + Password.Hash(txtPassword.Text) + "\"");

            try
            {
                Conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, Conn);
                cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                cmd.Parameters.AddWithValue("@password", Password.Hash(txtPassword.Text));

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string status = reader.GetString("status");
                        Module.loggedInRole = reader.GetString("role");
                        string username = reader.GetString("username");

                        switch (status)
                        {
                            case "Pending":
                                MessageBox.Show("Your account is still pending approval.", "Account Pending");
                                return;

                            case "Inactive":
                                MessageBox.Show("Your account has been denied.", "Access Denied");
                                return;

                            case "Active":
                                Module.SetLoggedInUsername(username);
                                MessageBox.Show("Login successful.", "Access Granted");
                                break;

                            default:
                                MessageBox.Show("Invalid account status.", "Error");
                                return;
                        }

                        if (Module.loggedInRole == "User")
                        {
                            MessageBox.Show("Welcome user!", "Login Successful");
                        }
                        else if (Module.loggedInRole == "Admin")
                        {
                            AdminForm ad = new AdminForm();
                            ad.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Invalid role.", "Error");
                        }
                    }
                    else
                    {
                        loginAttempts++;

                        if (loginAttempts >= 3)
                        {
                            MessageBox.Show("Too many failed attempts.", "Login Locked", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            btnLogin.Enabled = false;
                        }
                        else
                        {
                            MessageBox.Show(
                                "Invalid username or password. Attempts left: " + (3 - loginAttempts),
                                "Access Denied",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning
                            );
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Conn.Close();
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblRegister_Click(object sender, EventArgs e)
        {
            LoadRegisterForm();
        }
    }
}
