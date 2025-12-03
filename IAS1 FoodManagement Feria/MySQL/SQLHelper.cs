using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAS1_FoodManagement_Feria
{
    internal static class SQLHelper
    {
        internal static MySqlConnection GetConn()
        {
            return new MySqlConnection("server=localhost; userid=root; password=root; database=dbs_lab1;");
        }
    }
}