using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace REForm
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        string GetPropertyQuery;
        public Window1()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //This is my connection string i have assigned the database file address path  
                string MyConnection = "datasource=localhost;port=3306;username=local;password=yA907191";
                //This is my insert query in which i am taking input from the user through windows forms  
                GetPropertyQuery = "insert into property.property_details(address,city,postal_code,province,country,number_of_units,property_name,property_tenantId,property_expenseId) values('" + p_address.Text + "','" + p_city.Text + "','" + p_postal.Text + "','" + p_province.Text + "','" + p_country.Text + "','" + p_units.Text + "','" + p_name.Text + "','" + 1 + "','" + 2 + "');";
                //This is  MySqlConnection here i have created the object and pass my connection string.  
                MySqlConnection Mycon = new MySqlConnection(MyConnection);
                //This is command class which will handle the query and connection object.  
                MySqlCommand MyCommand2 = new MySqlCommand(GetPropertyQuery, Mycon);
                MySqlDataReader MyReader2;
                Mycon.Open();
                MyReader2 = MyCommand2.ExecuteReader();     // Here our query will be executed and data saved into the database.  
                MessageBox.Show("Save Data");
                while (MyReader2.Read())
                {
                }
                Mycon.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
