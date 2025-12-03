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
using static System.Net.Mime.MediaTypeNames;

namespace IAS1_FoodManagement_Feria.Authentication
{
    public partial class PopupForm : Form
    {
        private decimal cost;

        private decimal pay;
        public PopupForm(decimal cost)
        {
            InitializeComponent();
            this.cost = cost;
            txtCost.Text = cost.ToString();
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ValidateFields())
            {
                MessageBox.Show($"Congratu-lechon, may kupit kang pang ₱{pay - cost}.", "Transaction Completed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private bool ValidateFields()
        {
            return FieldsAreEmpty() && IsPaymentValid() && IsCustomerNameValid();
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
