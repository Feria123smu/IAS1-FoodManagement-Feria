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
        private int index;
        private Action<int> RemoveClick;
        public ReceipItemForm()
        {
            InitializeComponent();
        }

        public void SetRemoveDependency(int index, Action<int> action)
        {
            this.index = index;
            RemoveClick = action;
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveClick(index);
        }

        internal void SetText(string name, decimal price, int quantity)
        {
            lblName.Text = name;
            lblUnitPrice.Text = $"₱ {price}  x {quantity}";
            lblMultipliedPrice.Text = $"₱ {price * quantity}";
        }
    }
}
