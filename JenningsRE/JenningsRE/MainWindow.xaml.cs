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

        #region PropertyTabHandlers
        /// <summary>
        /// Validating inputs
        /// </summary>
        /// <returns></returns>
        private Boolean Validate()
        {
            foreach (Control control in UpdatePropertyGrid.Children)
            {
                string controlType = control.GetType().ToString();
                if (controlType == "System.Windows.Controls.TextBox")
                {
                    TextBox txtBox = (TextBox)control;
                    if (string.IsNullOrEmpty(txtBox.Text))
                    {
                        MessageBox.Show(txtBox.Name + " Can not be empty");
                        return false;
                    }
                }
            }
            return true;
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
            var validator = Validate();
            if (validator)
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
        }

        /// <summary>
        /// Delete Scoped Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeletePropertyBtn_Click(object sender, RoutedEventArgs e)
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

        private void FilterExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            FilterExpenseGrid(property_id);
        }
        /// <summary>
        /// Load the DataGrids for each tab
        /// </summary>
        private void LoadGrid(int propId)
        {
            expDataGrid.ItemsSource = _expenseAccess.GetExpenses(propId);
            ExpenseDataGrid = expDataGrid;

            TenantGrid.ItemsSource = _tenantAccess.GetTenants(propId);
            TenantDataGrid = TenantGrid;
        }

        /// <summary>
        /// Reload Expense DataGrid without filters.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearExpenseFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            ExpenseDataGrid.ItemsSource = _expenseAccess.GetExpenses(property_id);
        }

        /// <summary>
        /// Filters the items found within the expense datagrid
        /// </summary>
        private void FilterExpenseGrid(int propId)
        {

            var filteredExpenses = new List<expense>();

            var filterType = formExpenseFilterComboBox.SelectedIndex;
            

            switch (filterType)
            {
                case 1:
                    {
                        var fe = (from e in context.expenses
                                  where e.expense_name.Contains(formExpenseFilter.Text)
                                  && e.expense_property_id == propId
                                  select e).ToList();

                        filteredExpenses.AddRange(fe);

                        break;
                    }
                case 2:
                    {
                        var fe = (from e in context.expenses
                                  where e.contractor_name.Contains(formExpenseFilter.Text)
                                  && e.expense_property_id == propId
                                  select e).ToList();

                        filteredExpenses.AddRange(fe);

                        break;
                    }
                case 3:
                    {
                        var fe = (from e in context.expenses
                                  where e.expense_id.ToString() == formExpenseFilter.Text
                                  && e.expense_property_id == propId
                                  select e).ToList();

                        filteredExpenses.AddRange(fe);

                        break;
                    }
                default:
                    break;

            };

            ExpenseDataGrid.ItemsSource = filteredExpenses;
            

        }

        #endregion
        
        #region TenantTabHandlers
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

        /// <summary>
        /// Filter Tenant Grid by Input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FilterTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            var filteredTenants = new List<tenant>();

            var filterType = formTenantFilterComboBox.SelectedIndex;

            switch (filterType)
            {
                case 0:
                    {
                        var ft = (from t in context.tenants
                                  where t.tenant_name.Contains(formTenantFilter.Text)
                                  select t).ToList();

                        filteredTenants.AddRange(ft);

                        break;
                    }
                case 1:
                    {
                        var unitNum = int.Parse(formTenantFilter.Text);

                        var ft = (from t in context.tenants
                                  where t.unit_number == unitNum
                                  select t).ToList();

                        filteredTenants.AddRange(ft);

                        break;
                    }
                default:
                    break;
            }

            TenantDataGrid.ItemsSource = filteredTenants;
        }

        /// <summary>
        /// Clear Filters from Tenant Grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearTenantFilterBtn_Click(object sender, RoutedEventArgs e)
        {
            TenantDataGrid.ItemsSource = _tenantAccess.GetTenants(property_id);
        }

        #endregion

        /// <summary>
        /// Switch view to Analysis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SwitchScreen(object sender, RoutedEventArgs e)
        {
            AnalysisWindow Analysis = new AnalysisWindow();
            Analysis.Show();
            Close();
        }
    }
}
