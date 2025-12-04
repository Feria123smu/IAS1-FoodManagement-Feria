using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAS1_FoodManagement_Feria.Properties
{
    public static class Module
    {
        private static string loggedInUser;
        internal static string loggedInRole;

        private static Action LogoutSwitchForm;

        public static void SetLoggedInUsername(string username)
        {
            loggedInUser = username;
        }

        public static string GetLoggedInUsername()
        {
            return loggedInUser;
        }

        public static void Logout()
        {
            loggedInUser = "";
            loggedInRole = "";
            LogoutSwitchForm();
        }

        public static void SetLogoutFormSwitcher(Action LogoutSwitchFormer)
        {
            LogoutSwitchForm = LogoutSwitchFormer;
        }
    }
}

