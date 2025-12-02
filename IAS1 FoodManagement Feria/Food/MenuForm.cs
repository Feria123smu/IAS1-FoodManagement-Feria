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
    internal class MenuItem
    {
        internal Bitmap bitmap;
        internal string name;
        internal decimal price;

        public MenuItem(Bitmap bitmap, string name, decimal price)
        {
            this.bitmap = bitmap;
            this.name = name;
            this.price = price;
        }

        internal static MenuItem Item(Bitmap bitmap, string name, decimal price)
        {
            return new MenuItem(bitmap, name, price);
        }
    }


    public partial class MenuForm : Form
    {
        ReceipItemForm[] receiptItemForms = new ReceipItemForm[4];
        Panel[] receiptItems = new Panel[4];

        Bitmap[] itemImages;
        string[] itemNames;
        decimal[] itemPrices;

        private void CreateMenuItem(params MenuItem[] items)
        {
            List<Bitmap> bitmaps = new List<Bitmap>();
            List<string> names = new List<string>();
            List<decimal> prices = new List<decimal>();

            foreach (MenuItem item in items)
            {
                bitmaps.Add(item.bitmap);
                names.Add(item.name);
                prices.Add(item.price);
            }
            itemImages = bitmaps.ToArray();
            itemNames = names.ToArray();
            itemPrices = prices.ToArray();
        }

        public MenuForm()
        {
            InitializeComponent();

            CreateMenuItem
            (
                new MenuItem(Properties.Resources.p_bacon_cheese, "Bacon Cheese", 50),
                new MenuItem(Properties.Resources.p_bacon_cheese, "Bacon Cheese", 50)
            );

            //receiptItems[0] = panelReceiptItem0;
            //receiptItems[1] = panelReceiptItem1;
            //receiptItems[2] = panelReceiptItem2;
            //receiptItems[3] = panelReceiptItem3;

            //receiptItemForms[0] = new ReceipItemForm();
            //receiptItemForms[1] = new ReceipItemForm();
            //receiptItemForms[2] = new ReceipItemForm();
            //receiptItemForms[3] = new ReceipItemForm();

            //FormManagement.PlaceForm(receiptItemForms[0], panelReceiptItem0);
        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        private enum MenuType
        {
            Pizza, Milkshake
        }

        private void MenuForm_Load(object sender, EventArgs e)
        {
            btnP_AllMeaty.BackgroundImage = Properties.Resources.p_bacon_cheese;
            //MessageBox.Show(btnP_AllMeaty.BackgroundImage.ToString());
            //ToggleMenuItemButtons
            //(
            //    false,
            //    btnP_AllMeaty, btnP_BBQ, btnP_BaconCheese,
            //    btnM_ChocolateHeaven, btnM_StrawberryCheesecake, btnM_CookiesNCream
            //);
        }

        private void ToggleMenuItemButtons(bool isVisible, params Button[] buttons)
        {
            foreach (Button button in buttons)
            {
                button.Visible = isVisible;
            }
        }

        private void AlignMenuItems(bool isVisible, Button button1, Button button2, Button button3)
        {
            button1.Location = new Point(25, 99);
            button2.Location = new Point(126, 99);
            button3.Location = new Point(225, 99);

            button1.Visible = isVisible;
            button2.Visible = isVisible;
            button3.Visible = isVisible;
        }

        private void btnTabMilkShake_Click(object sender, EventArgs e)
        {
            ToggleMenuItemButtons(true,
                btnM_ChocolateHeaven, btnM_StrawberryCheesecake, btnM_CookiesNCream);
        }


        private void btnTabPizza_Click(object sender, EventArgs e)
        {
            ToggleMenuItemButtons(true,
                btnP_AllMeaty, btnP_BBQ, btnP_BaconCheese);
        }
    }
}
