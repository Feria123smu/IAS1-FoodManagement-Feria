using MySql.Data.MySqlClient;
using Mysqlx.Crud;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

using static IAS1_FoodManagement_Feria.SQLHelper;

namespace IAS1_FoodManagement_Feria.Authentication
{
    public partial class PopupForm : Form
    {
        private decimal cost;
        private decimal pay;

        private string[] foodNames;
        private int[] quantities;

        private Action TransactionComplete;

        public PopupForm(Action TransactionComplete, decimal cost, (string, int)[] foodItems)
        {
            InitializeComponent();
            this.TransactionComplete = TransactionComplete;
            this.cost = cost;
            txtCost.Text = cost.ToString();
            PopulateFoodItems(foodItems);
        }

        private void PopulateFoodItems((string, int)[] foodItems)
        {
            foodNames = new string[foodItems.Length];
            quantities = new int[foodItems.Length];

            string bastaPagkain = "";
            for (int i = 0; i < foodItems.Length; i++)
            {
                foodNames[i] = foodItems[i].Item1;
                quantities[i] = foodItems[i].Item2;
                bastaPagkain += foodNames[i].ToString() + " [" + quantities[i] +"]\n";
            }
            MessageBox.Show(bastaPagkain);
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                DataEntry();
                TransactionComplete();
                MessageBox.Show($"Received {pay}, change amounts to {pay - cost}.", "Transaction Completed", MessageBoxButtons.OK, MessageBoxIcon.Warning);


                //MessageBox.Show($"Congratu-lechon, may kupit kang pang ₱{pay - cost}.", "Transaction Completed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidateFields()
        {
            return FieldsAreEmpty() && IsPaymentValid() && IsCustomerNameValid();
        }

        private void DataEntry()
        {
            try
            {
                using (MySqlConnection conn = GetConn())
                {
                    conn.Open();

                    using (MySqlCommand cmd = new MySqlCommand("INSERT INTO receipts_tbl(payment, pay_change, customerName, deliveryAddress) VALUES (@payment, @pay_change, @customerName, @deliveryAddress);", conn))
                    {
                        cmd.Parameters.AddWithValue("@payment", pay);
                        cmd.Parameters.AddWithValue("@pay_change", pay - cost);
                        cmd.Parameters.AddWithValue("@customerName", txtCustomerName.Text);
                        cmd.Parameters.AddWithValue("@deliveryAddress", txtAddress.Text);
                        cmd.ExecuteNonQuery();
                    }

                    int receiptId = -1;

                    using (MySqlCommand cmd2 = new MySqlCommand("SELECT LAST_INSERT_ID() AS id;", conn))
                    {
                        MySqlDataReader reader = cmd2.ExecuteReader();
                        if (reader.HasRows)
                        {
                            reader.Read();
                            receiptId = reader.GetInt32("id");
                        }
                        reader.Close();
                    }

                    for (int i = 0; i < foodNames.Length; i++)
                    {
                        using (MySqlCommand cmd = new MySqlCommand("INSERT INTO receipt_item_tbl (receipt_id, food_item, quantity) VALUES (@receipt_id, @food_item, @quantity); ", conn))
                        {
                            cmd.Parameters.AddWithValue("@receipt_id", receiptId);
                            cmd.Parameters.AddWithValue("@food_item", foodNames[i]);
                            cmd.Parameters.AddWithValue("@quantity", quantities[i]);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Transaction Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool FieldsAreEmpty()
        {
            if (txtCustomerName.Text.Trim() == "" ||
                txtPayment.Text.Trim() == "" ||
                txtAddress.Text.Trim() == "")
            {
                MessageBox.Show("All fields must be filled.", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
        private bool IsCustomerNameValid()
        {
            if (HasNumber(txtCustomerName.Text))
                MessageBox.Show("Customer Name must not contain numbers.", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (!HasLettersWithOptionalPeriodOrSpaces(txtCustomerName.Text))
                MessageBox.Show("Customer Name must only contain letters, spaces, and periods.", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else if (!BeginsWithLetter(txtCustomerName.Text))
                MessageBox.Show("Customer Name must begin with a letter.", "Invalid Customer Name", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                return true;

            return false;
        }

        private bool IsPaymentValid()
        {
            try
            {
                pay = Convert.ToDecimal(txtPayment.Text);
                if (pay < 0)
                {
                    MessageBox.Show("Payment cannot be negative.", "Invalid Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (pay < cost)
                {
                    MessageBox.Show($"Insufficient payment. Still need ₱{cost - pay}", "Insufficient Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (pay >= cost)
                {
                    //MessageBox.Show("Gumana ka sana.", "Transaction Completed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return true;
                }
            }
            catch
            {
                MessageBox.Show("Payment is not a valid number.", "Invalid Payment", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return false;
        }

        // ================================

        private bool BeginsWithLetter(string text) { return Regex.IsMatch(text, "^[a-zA-Z]"); }

        private bool HasNumber(string text) { return Regex.IsMatch(text, @"\d"); }

        private bool HasLettersWithOptionalPeriodOrSpaces(string text)
        { return Regex.IsMatch(text, "^[a-zA-Z]+([. ]+[a-zA-Z]+)*([. ]?[a-zA-Z]*)$"); }
    }
}
