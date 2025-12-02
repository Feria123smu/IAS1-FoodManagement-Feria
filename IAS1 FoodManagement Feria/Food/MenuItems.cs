using IAS1_FoodManagement_Feria.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IAS1_FoodManagement_Feria.Food
{
    internal class MenuItem
    {

        internal enum Id
        {
            AllMeaty,
            Barbecue,
            BaconCheese,
            StrawberryCheesecake,
            CookiesCream,
            ChocolateHeaven
        }

        internal static (string[], decimal[], Bitmap[]) GetMenuItems()
        {
            return Populate
            (
                Add("All Meaty", 450, Properties.Resources.p_all_meaty),
                Add("Barbecue", 400, Properties.Resources.p_bbq),
                Add("BaconCheese", 430, Properties.Resources.p_bacon_cheese),
                Add("StrawberryCheesecake", 160, Properties.Resources.m_strawberry_cheesecake),
                Add("CookiesCream", 150, Properties.Resources.m_cookies_n_cream),
                Add("ChocolateHeaven", 170, Properties.Resources.m_chocolate_heaven)

            //Add("ChocolateHeaven", 170, Properties.Resources.m_chocolate_heaven),
            //Add("CookiesCream", 150, Properties.Resources.m_cookies_n_cream),
            //Add("StrawberryCheesecake", 160, Properties.Resources.m_strawberry_cheesecake),
            //Add("BaconCheese", 430, Properties.Resources.p_bacon_cheese),
            //Add("Barbecue", 400, Properties.Resources.p_bbq),
            //Add("All Meaty", 450, Properties.Resources.p_all_meaty)
            );
        }


        private static (string[], decimal[], Bitmap[]) Populate(params (string, decimal, Bitmap)[] items)
        {
            List<string> names = new List<string>();
            List<decimal> prices = new List<decimal>();
            List<Bitmap> bitmaps = new List<Bitmap>();

            for (int i = 0; i < items.Length; i++)
            {
                names.Add(items[i].Item1);
                prices.Add(items[i].Item2);
                bitmaps.Add(items[i].Item3);
            }
            return (names.ToArray(), prices.ToArray(), bitmaps.ToArray());
        }

        private static (string, decimal, Bitmap) Add(string name, decimal price, Bitmap bitmap)
        {
            return (name, price, bitmap);
        }
    }

    
}