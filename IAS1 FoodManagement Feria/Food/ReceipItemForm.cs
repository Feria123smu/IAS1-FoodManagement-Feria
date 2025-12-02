using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria.Food
{
    public partial class ReceipItemForm : Form
    {
        public ReceipItemForm()
        {
            InitializeComponent();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {

        }

        internal void SetText(string name, decimal price, int quantity)
        {
            lblName.Text = name;
            lblUnitPrice.Text = $"₱ {price}  x {quantity}";
            lblMultipliedPrice.Text = $"₱ {price * quantity}";
        }
    }
}
