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
        public MySqlConnection connection;

        public MainWindow()
        {
            var GetExpensesQuery = "select expense_name as Name, expense_cost as Cost from expenses";

            InitializeComponent();

            //Database Connection
            var connectionString = "SERVER=localhost;PORT=3333;DATABASE=jr_prop;UID=root;PASSWORD=mypass4sql;";
            this.connection = new MySqlConnection(connectionString);

            //Expenses Tab Logic
            MySqlCommand cmd = new MySqlCommand(GetExpensesQuery, connection);
            connection.Open();
            DataTable expensesDataTable = new DataTable();
            expensesDataTable.Load(cmd.ExecuteReader());
            connection.Close();

            ExpenseGrid.DataContext = expensesDataTable;

            Button addExpenseBtn = new Button
            {
                Name = "addExpenseBtn"
            };
            addExpenseBtn.Click += AddExpenseBtn_Click;

        }

        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddExpenseWindow addWindow = new AddExpenseWindow(connection);
            addWindow.Show();
        }
    }
}
