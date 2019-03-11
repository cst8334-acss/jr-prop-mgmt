using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace REForm.Helpers
{
    public static class CRUDHelper
    {

        public static void LoadDataTable(DataTable dataTable, String query, MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            connection.Open();
            dataTable.Load(cmd.ExecuteReader());
            connection.Close();

        }

        public static void ExecuteQuery(String query, MySqlConnection connection)
        {
            MySqlCommand cmd = new MySqlCommand(query, connection);
            connection.Open();
            cmd.ExecuteReader();
            connection.Close();
        }


    }
}
