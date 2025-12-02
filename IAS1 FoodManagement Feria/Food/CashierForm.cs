using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IAS1_FoodManagement_Feria.Food.MenuItems;

namespace IAS1_FoodManagement_Feria.Food
{
    public partial class CashierForm : Form
    {
        private Button[] buttons;

        private Bitmap[] bitmaps;
        private string[] names;
        private decimal[] prices;

        public CashierForm()
        {
            InitializeComponent();
            AssignMenuItemButtons();
            PopulateMenuItems();

            for (int i = 0; i < bitmaps.Length; i++)
            {
                buttons[i].BackgroundImage = bitmaps[i];
            }
        }

        private void PopulateMenuItems()
        {
            (string[], decimal[], Bitmap[]) items = MenuItems.GetMenuItems();

            names = items.Item1;
            prices = items.Item2;
            bitmaps = items.Item3;
        }

        private void AssignMenuItemButtons()
        {
            buttons = new Button[6];
            buttons[0] = btnItem0;
            buttons[1] = btnItem1;
            buttons[2] = btnItem2;
            buttons[3] = btnItem3;
            buttons[4] = btnItem4;
            buttons[5] = btnItem5;
        }

        private void ItemClick(int itemIndex)
        {

        }

        private void DisplayMenuItems(params MenuItems.Id[] menuItemIndices)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                if (i < menuItemIndices.Length)
                {
                    buttons[i].BackgroundImage = bitmaps[(int)menuItemIndices[i]];
                    buttons[i].Visible = true;
                }
                else
                {
                    buttons[i].Visible = false;
                }
            }
        }

        private void btnTabPizza_Click(object sender, EventArgs e)
        {
            DisplayMenuItems
            (
                MenuItems.Id.AllMeaty,
                MenuItems.Id.Barbecue,
                MenuItems.Id.BaconCheese
           );
        }

        private void btnTabMilkshake_Click(object sender, EventArgs e)
        {
            DisplayMenuItems
            (
                MenuItems.Id.StrawberryCheesecake,
                MenuItems.Id.CookiesCream,
                MenuItems.Id.ChocolateHeaven
           );
        }

        private void btnItem0_Click(object sender, EventArgs e) { ItemClick(0); }

        private void btnItem1_Click(object sender, EventArgs e)
        {
            ItemClick(1);
        }

        private void btnItem2_Click(object sender, EventArgs e)
        {
            ItemClick(2);
        }

        private void btnItem3_Click(object sender, EventArgs e)
        {
            ItemClick(3);
        }

        private void btnItem4_Click(object sender, EventArgs e)
        {
            ItemClick(4);
        }

        private void btnItem5_Click(object sender, EventArgs e)
        {
            ItemClick(5);
        }
    }
}
