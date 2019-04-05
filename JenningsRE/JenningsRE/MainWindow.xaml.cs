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
        int property_id;
        
        jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection();
        public static DataGrid expenseDataGrid;

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
            property_id = 1;


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

            #region 
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
        }

        /// <summary>
        /// Delete Selected Row from DataGrid and DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            expense exp = expenseDataGrid.SelectedItem as expense;
            _expenseAccess.RemoveExpense(exp);

        }
        /// <summary>
        /// Load the Expenses DataGrid
        /// </summary>
        private void LoadGrid()
        {
            expDataGrid.ItemsSource = context.expenses.ToList();
            expenseDataGrid = expDataGrid;
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

        private void AddTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTenant Tenant = new AddTenant();
            Tenant.Show();
        }
        
    }

}
