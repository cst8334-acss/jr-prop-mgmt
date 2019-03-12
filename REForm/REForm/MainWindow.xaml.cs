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
using REForm.Helpers;
using REForm.Models;

namespace REForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MySqlConnection connection;
        
        //Declaration for expense table
        public DataTable expensesDataTable;
        public String GetExpensesQuery;

        //Declaration for tenant table
        public DataTable tenantDataTable;
        public String GetTenantQuery;
        public String DeleteTenantQuery;

        public MainWindow()
        {
            //Select queries to get data from database
            GetExpensesQuery = "select expense_name as Name, expense_cost as Cost from expenses";
            GetTenantQuery = "select tenant.tenantId as 'Id', tenant.tenant_name as 'Name', tenant.unit_number as 'Unit#', tenant.square_feet as 'Square Feet', tenant.rent_per_sf as 'Rent per SF', tenant.monthly_rent as 'Monthly Rent', tenant.annual_rent as 'Annual Rent' from property.tenant;";
            InitializeComponent();

            //Database Connection
            var connectionString = "SERVER=localhost;PORT=3306;DATABASE=property;UID=root;PASSWORD=1234;";
            connection = new MySqlConnection(connectionString);

            //Expenses Tab Logic
            expensesDataTable = new DataTable();
            CRUDHelper.LoadDataTable(expensesDataTable, GetExpensesQuery, connection);
            ExpenseGrid.DataContext = expensesDataTable;

            //Tenant Tab Logic
            tenantDataTable = new DataTable();
            CRUDHelper.LoadDataTable(tenantDataTable, GetTenantQuery, connection);
            TenantGrid.DataContext = tenantDataTable;

            
            //Add Expense Button
            Button addExpenseBtn = new Button
            {
                Name = "addExpenseBtn"
            };
            addExpenseBtn.Click += AddExpenseBtn_Click;

            //Delete Expense Button
            Button deleteExpenseButton = new Button
            {
                Name = "deleteExpenseBtn"
            };
            deleteExpenseButton.Click += DeleteExpenseButton_Click;

        }

        private void DeleteExpenseButton_Click(object sender, RoutedEventArgs e)
        {
            //DELETE FROM DATAGRID
        }

        //Adds new expense window on click event
        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddExpenseWindow addWindow = new AddExpenseWindow(connection, this);
            addWindow.Show();
        }
        //Performs real-time refresh here
        public void RefreshExpenseGrid()
        {
            CRUDHelper.LoadDataTable(expensesDataTable, GetExpensesQuery, connection);
            ExpenseGrid.DataContext = expensesDataTable;
        }

        //Adds new tenant window on click event
        private void AddTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            addTenantWindow addWindow = new addTenantWindow(connection, this);
            addWindow.Show();
        }
        //Performs real-time refresh here
        public void RefreshTenantGrid()
        {
            CRUDHelper.LoadDataTable(tenantDataTable, GetTenantQuery, connection);
            TenantGrid.DataContext = tenantDataTable;
        }
        //Delete the selected row from datagrid
        private void DeleteTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            tenantDataTable.Rows.RemoveAt(TenantGrid.SelectedIndex);
            MessageBox.Show("Tenant Deleted");
        }
       
        private void EditTenantBtn_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
