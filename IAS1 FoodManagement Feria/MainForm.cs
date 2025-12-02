using IAS1_FoodManagement_Feria.Authentication;

using IAS1_FoodManagement_Feria.Food;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            LoadMenu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        private void LoadMenu()
        {
            //MenuForm Menu = new MenuForm();
            //FormManagement.PlaceForm(Menu, panel1);

            CashierForm cashier = new CashierForm();
            FormManagement.PlaceForm(cashier, panel1);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
