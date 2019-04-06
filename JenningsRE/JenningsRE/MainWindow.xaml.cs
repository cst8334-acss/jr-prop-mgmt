using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Data.Entity;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using JenningsRE.DAL;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        //Initialization of connection
        jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection();

        //Datagrids for each tab
        public static DataGrid ExpenseDataGrid;
        public static DataGrid TenantDataGrid;

        ExpenseAccess _expenseAccess;
        TenantAccess _tenantAccess;

        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();

            _expenseAccess = new ExpenseAccess(context);
            _tenantAccess = new TenantAccess(context);

            DataContext = this;

            //TODO Set this Dynamically.


            #region ExpenseTabLogic

            //Add Expense Button
            Button addExpenseBtn = new Button
            {
                Name = "addExpenseBtn"
            };
            addExpenseBtn.Click += AddExpenseBtn_Click;

            //Update Expense Button
            Button updateExpenseBtn = new Button
            {
                Name = "updateExpenseBtn"
            };
            updateExpenseBtn.Click += UpdateExpenseBtn_Click;

            //Delete Expense Button
            Button deleteExpenseBtn = new Button
            {
                Name = "deleteExpenseBtn"
            };
            deleteExpenseBtn.Click += DeleteExpenseBtn_Click;

            #endregion

            #region TenantTabLogic

            //Add Expense Button
            Button addTenantBtn = new Button
            {
                Name = "addExpenseBtn"
            };
            addExpenseBtn.Click += AddTenantBtn_Click;

            //Update Expense Button
            Button updateTenantBtn = new Button
            {
                Name = "updateExpenseBtn"
            };
            updateExpenseBtn.Click += UpdateTenantBtn_Click;

            //Delete Expense Button
            Button deleteTenantBtn = new Button
            {
                Name = "deleteExpenseBtn"
            };
            deleteExpenseBtn.Click += DeleteTenantBtn_Click;

            #endregion

        }


        #region ExpenseTabHandlers
        /// <summary>
        /// Display Pop-up Add Expense Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddExpense addWindow = new AddExpense(context,_expenseAccess);
            addWindow.ShowDialog();
            expDataGrid.Items.Refresh();
        }

        /// <summary>
        /// Update Selected Row from DataGrid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateExpense upWindow = new UpdateExpense();
            upWindow.ShowDialog();
            expDataGrid.Items.Refresh();
        }

        /// <summary>
        /// Delete Selected Row from DataGrid and DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            expense exp = ExpenseDataGrid.SelectedItem as expense;
            _expenseAccess.RemoveExpense(exp);
            expDataGrid.Items.Refresh();
        }
        /// <summary>
        /// Load the DataGrids for each tab
        /// </summary>
        private void LoadGrid()
        {
            expDataGrid.ItemsSource = context.expenses.ToList();
            ExpenseDataGrid = expDataGrid;

            TenantGrid.ItemsSource = context.tenants.ToList();
            TenantDataGrid = TenantGrid;
        }

        #endregion
        /// <summary>
        /// Switch view to Analysis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchScreen(object sender, RoutedEventArgs e)
        {
            AnalysisWindow Analysis = new AnalysisWindow();
            Analysis.Show();
            Close();
        }

        /// <summary>
        /// This will open the add new tenant window on the click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTenant Tenant = new AddTenant(context, _tenantAccess);
            Tenant.ShowDialog();
            TenantGrid.Items.Refresh();
        }

        /// <summary>
        /// This will delete the tenant from database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            tenant tenant = TenantDataGrid.SelectedItem as tenant;
            _tenantAccess.DeleteTenant(tenant);
            MessageBox.Show("Tenant Deleted");
            TenantGrid.Items.Refresh();
        }

        /// <summary>
        /// This will update the tenant in database and in data grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            UpdateTenant tenant = new UpdateTenant();
            tenant.ShowDialog();
            TenantGrid.Items.Refresh();
        }
    }

}
