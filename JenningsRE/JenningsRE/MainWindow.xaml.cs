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
        PropertyAccess _propertyAccess;
        TenantAccess _tenantAccess;

        int property_id;


        public MainWindow()
        {
            InitializeComponent();
            property_id = 0;

            Loaded += MainWindow_Loaded;

            _expenseAccess = new ExpenseAccess(context);
            _propertyAccess = new PropertyAccess(context, this);
            _tenantAccess = new TenantAccess(context);

            DataContext = this;


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

            #region PropertyTabLogic
            Button addPropertyBtn = new Button
            {
                Name = "addPropertyBtn"
            };
            addPropertyBtn.Click += AddPropertyBtn_Click;

            Button savePropertyBtn = new Button
            {
                Name = "savePropertyBtn"
            };
            savePropertyBtn.Click += SavePropertyBtn_Click;
            #endregion
        }

        /// <summary>
        /// Display Pop-up Add Property Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPropertyBtn_Click(object sender, RoutedEventArgs e)
        {
            AddProperty addWindow = new AddProperty(context, _propertyAccess);
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Updated Scoped Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SavePropertyBtn_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Are you sure you want to update this property?", 
                                      "Update Property", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                property property = (from p in context.properties
                                     where p.property_id == property_id
                                     select p).Single();

                property.address = formAddress.Text;
                property.city = formCity.Text;
                property.province = formProvince.Text;
                property.country = formCountry.Text;
                property.postal_code = formPostal.Text;
                property.number_of_units = int.Parse(formNumberOfUnits.Text);
                property.rentable_area = int.Parse(formRentableArea.Text);
                property.available_space = int.Parse(formAvailableSpace.Text);

                context.SaveChanges();

                LoadPropertyList();

            }
            else if (result == MessageBoxResult.No)
            {
                Hide();
            }
        }

        /// <summary>
        /// Delete Scoped Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deletePropertyBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this property?",
                                      "Delete Property", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _propertyAccess.DeleteProperty(property_id);
                ClearPropertyForm();
            }
            else if (result == MessageBoxResult.No)
            {
                Hide();
            }
        }

        #region ExpenseTabHandlers
        /// <summary>
        /// Display Pop-up Add Expense Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddExpense addWindow = new AddExpense(context,_expenseAccess, property_id);
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
        private void LoadGrid(int propId)
        {
            expDataGrid.ItemsSource = _expenseAccess.GetExpenses(propId);
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

        #region PropertyTabHandlers
        /// <summary>
        /// Scope to Selected Property,
        /// Populates data for manipulation in tenant
        /// and expense tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ListViewItem item = sender as ListViewItem;
            object obj = item.Content;

            //Set Global Property Id
            property_id = ((property)obj).property_id;

            //Set Text fields to parsed property members.
            formAddress.Text = ((property)obj).address;
            formCity.Text = ((property)obj).city;
            formProvince.Text = ((property)obj).province;
            formCountry.Text = ((property)obj).country;
            formPostal.Text = ((property)obj).postal_code;
            formNumberOfUnits.Text = ((property)obj).number_of_units.ToString();
            formRentableArea.Text = ((property)obj).rentable_area.ToString();
            formAvailableSpace.Text = ((property)obj).available_space.ToString();
            formParkingSpots.Text = ((property)obj).parking_spots.ToString();

            //Load property related expenses to Grid
            LoadGrid(property_id);
        }

        /// <summary>
        /// Executed on Window Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPropertyList();
            
        }

        /// <summary>
        /// Fetch Properties from Database
        /// </summary>
        public void LoadPropertyList()
        {
            propList.ItemsSource = _propertyAccess.GetProperties();
        }

        /// <summary>
        /// Clears the Forms in the Property Form Fields
        /// </summary>
        private void ClearPropertyForm()
        {
            formAddress.Clear();
            formCity.Clear();
            formProvince.Clear();
            formCountry.Clear();
            formPostal.Clear();
            formNumberOfUnits.Clear();
            formAvailableSpace.Clear();
            formRentableArea.Clear();
            formParkingSpots.Clear();
        }

        #endregion

        private void AddTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTenant Tenant = new AddTenant(context, _tenantAccess, property_id);
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
