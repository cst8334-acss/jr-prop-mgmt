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

namespace JenningsRE
{
    /// <summary>
    /// @author Vijay Abhichandani
    /// Interaction logic for UpdateTenant.xaml
    /// </summary>
    public partial class UpdateTenant : Window
    {
        jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection();

        tenant tenant;

        public UpdateTenant()
        {
            InitializeComponent();
            tenant = GetSelectedTenant();
            SetFields(tenant);
        }

        /// <summary>
        /// Validating inputs
        /// </summary>
        /// <returns></returns>
        private Boolean Validate()
        {
            foreach (Control control in UpdateExpenseGrid.Children)
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
        /// gets the selected tenant details 
        /// </summary>
        /// <returns></returns>
        private tenant GetSelectedTenant()
        {
            return MainWindow.TenantDataGrid.SelectedItem as tenant;
        }

        /// <summary>
        /// sets the field based on previous details from database
        /// </summary>
        /// <param name="tenant"></param>
        private void SetFields(tenant tenant)
        {
            formTenantName.Text = tenant.tenant_name;
            formUnitNumber.Text = tenant.unit_number.ToString();
            formSquareFeet.Text = tenant.unit_size_sqft.ToString("F");
            formRent.Text = tenant.rent_per_sf.ToString("C");
            formStart.Text = tenant.lease_start.ToString("d");
            formEnd.Text = tenant.lease_end.ToString("d");
            formMonthsLeft.Text = tenant.months_left.ToString();
        }

        /// <summary>
        /// discards changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// This will update the details in database based on user inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            var validator = Validate();
            if (validator)
            {
                tenant editTenant = (from t in context.tenants
                    where t.tenant_id == tenant.tenant_id
                    select t).Single();

                editTenant.tenant_name = formTenantName.Text;
                editTenant.unit_number = int.Parse(formUnitNumber.Text);
                editTenant.unit_size_sqft = double.Parse(formSquareFeet.Text);
                editTenant.rent_per_sf = double.Parse(formRent.Text);
                editTenant.annual_rent = tenant.rent_per_sf;
                editTenant.monthly_rent = tenant.annual_rent / 12;
                editTenant.lease_start = DateTime.Parse(formStart.Text);
                editTenant.lease_end = DateTime.Parse(formEnd.Text);
                editTenant.months_left = int.Parse(formMonthsLeft.Text);

                context.SaveChanges();

                MainWindow.TenantDataGrid.ItemsSource = context.tenants.ToList();

                MessageBoxButton mbBtn = MessageBoxButton.OK;
                string header = "Update Tenant";
                string message = $"Tenant name:{editTenant.tenant_name} has been altered.";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);
                Close();
            }
        }
    }
}
