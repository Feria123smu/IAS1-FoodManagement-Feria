using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IAS1_FoodManagement_Feria
{
    internal static class FormManagement
    {
        internal static void PlaceForm(Form form, Panel panel, bool isLogin = false)
        {
            form.Dock = DockStyle.Fill;
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopLevel = false;

            if (isLogin==true)
            {
                form.Size = new System.Drawing.Size(600, 450);
            }
            else
                form.Size= new System.Drawing.Size(1246, 767);

                panel.Controls.Clear();
            panel.Controls.Add(form);
            form.Show();
        }
    }
}