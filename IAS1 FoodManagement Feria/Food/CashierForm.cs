using IAS1_FoodManagement_Feria.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static IAS1_FoodManagement_Feria.Food.MenuItem;

namespace IAS1_FoodManagement_Feria.Food
{
    public partial class CashierForm : Form
    {
        private Button[] buttons;
        private ReceipItemForm[] receiptItems;
        private Panel[] receiptItemPanels;

        private Bitmap[] bitmaps;
        private string[] names;
        private decimal[] prices;

        private int currentReceiptIndex = 0;

        private List<int> quantities = new List<int>();
        private List<MenuItem.Id> orderItems = new List<MenuItem.Id>();

        public CashierForm()
        {
            InitializeComponent();
            AssignMenuItemButtons();
            AssignReceiptItemPanels();
            PopulateMenuItems();

            for (int i = 0; i < bitmaps.Length; i++)
            {
                buttons[i].BackgroundImage = bitmaps[i];
            }

        }

        private void PopulateMenuItems()
        {
            (string[], decimal[], Bitmap[]) items = MenuItem.GetMenuItems();

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

        private void AssignReceiptItemPanels()
        {
            //Panel[] receiptItemPanels = new Panel[6];
            receiptItems = new ReceipItemForm[6];
            receiptItemPanels = new Panel[6];

            //receiptItemPanels[0] = panelReceipt0;
            //receiptItemPanels[1] = panelReceipt1;
            //receiptItemPanels[2] = panelReceipt2;
            //receiptItemPanels[3] = panelReceipt3;
            //receiptItemPanels[4] = panelReceipt4;
            //receiptItemPanels[5] = panelReceipt5;

            AddReceiptItem(0, panelReceipt0);
            AddReceiptItem(1, panelReceipt1);
            AddReceiptItem(2, panelReceipt2);
            AddReceiptItem(3, panelReceipt3);
            AddReceiptItem(4, panelReceipt4);
            AddReceiptItem(5, panelReceipt5);
            
            void AddReceiptItem(int index, Panel panel)
            {
                receiptItems[index] = new ReceipItemForm();
                receiptItemPanels[index] = panel;

                FormManagement.PlaceForm(receiptItems[index], panel);
                receiptItems[index].Show();
                receiptItems[index].Hide();
            }
        }

        private int tmpInt = 0;
        private void ItemClick(int itemIndex)
        {
            if (orderItems.Contains((MenuItem.Id)itemIndex))
            {
                for (int i = 0; i < orderItems.Count; i++)
                {
                    if (orderItems[i] == (MenuItem.Id)itemIndex)
                    {
                        quantities[i]++;

                        receiptItems[i].SetText(names[itemIndex], prices[itemIndex], quantities[i]);
                        break;
                    }

                }
            }
            else
            {
                receiptItems[orderItems.Count].SetText(names[itemIndex], prices[itemIndex], 1);
                receiptItems[orderItems.Count].SetRemoveDependency(orderItems.Count, ReceiptRemoveClicked);
                receiptItems[orderItems.Count].Show();
                receiptItemPanels[orderItems.Count].Visible = true;

                orderItems.Add((MenuItem.Id)itemIndex);
                quantities.Add(1);
            }
            UpdateTotalPrice();
        }

        private decimal GetTotalPrice()
        {
            decimal total = 0;
            for (int i = 0; i < orderItems.Count; i++)
            {
                total += prices[(int)orderItems[i]] * quantities[i];
            }
            return total;
        }

        private void UpdateTotalPrice()
        {
            lblTotal.Text = GetTotalPrice().ToString();
        }

        private void ReceiptRemoveClicked(int index)
        {
            orderItems.RemoveAt(index);
            quantities.RemoveAt(index);

            RefreshReceipt();
            //int currentItemIndex;

            //for (int i = 0; i < 6; i++)
            //{
            //    if (i < orderItems.Count)
            //    {
            //        currentItemIndex = (int)orderItems[i];
            //        receiptItems[i].SetText(names[currentItemIndex], prices[currentItemIndex], quantities[i]);
            //        receiptItemPanels[i].Visible = true;
            //    }
            //    else
            //    {
            //        receiptItemPanels[i].Visible = false;
            //    }
            //}
            UpdateTotalPrice();
        }

        private void RefreshReceipt()
        {
            int currentItemIndex;

            for (int i = 0; i < 6; i++)
            {
                if (i < orderItems.Count)
                {
                    currentItemIndex = (int)orderItems[i];
                    receiptItems[i].SetText(names[currentItemIndex], prices[currentItemIndex], quantities[i]);
                    receiptItemPanels[i].Visible = true;
                }
                else
                {
                    receiptItemPanels[i].Visible = false;
                }
            }
        }

        private void DisplayMenuItems(params MenuItem.Id[] menuItemIndices)
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
                MenuItem.Id.AllMeaty,
                MenuItem.Id.Barbecue,
                MenuItem.Id.BaconCheese
           );
        }

        private void btnTabMilkshake_Click(object sender, EventArgs e)
        {
            DisplayMenuItems
            (
                MenuItem.Id.StrawberryCheesecake,
                MenuItem.Id.CookiesCream,
                MenuItem.Id.ChocolateHeaven
           );
        }

        private void btnItem0_Click(object sender, EventArgs e) { ItemClick(0); }
        private void btnItem1_Click(object sender, EventArgs e) { ItemClick(1); }
        private void btnItem2_Click(object sender, EventArgs e) { ItemClick(2); }
        private void btnItem3_Click(object sender, EventArgs e) { ItemClick(3); }
        private void btnItem4_Click(object sender, EventArgs e) { ItemClick(4); }
        private void btnItem5_Click(object sender, EventArgs e) { ItemClick(5); }

        private void btnPlaceOrder_Click(object sender, EventArgs e)
        {
            PopupForm popup = new PopupForm(TransactionCompleted, GetTotalPrice(), GetFoodItems()); 
            popup.Show(); 
        }

        private (string, int)[] GetFoodItems()
        {
            (string, int)[] items = new (string, int)[orderItems.Count];

            for (int i = 0; i < items.Length; i++)
            {
                items[i].Item1 = names[(int)orderItems[i]];
                items[i].Item2 = quantities[i];
            }
            return items;
        }

        private void TransactionCompleted()
        {
            orderItems.Clear();
            quantities.Clear();
            RefreshReceipt();
        }
    }
}