using System;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using MySql.Data.MySqlClient;

namespace REForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var connectionString = "SERVER=localhost;PORT=3333;DATABASE=jr_prop;UID=root;PASSWORD=YOUR_PASS_HERE;";

            MySqlConnection connection = new MySqlConnection(connectionString);

            MySqlCommand cmd = new MySqlCommand("select * from expenses", connection);
            connection.Open();
            DataTable expensesDataTable = new DataTable();
            expensesDataTable.Load(cmd.ExecuteReader());
            connection.Close();

            ExpenseGrid.DataContext = expensesDataTable;
        }
    }
}
