using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using JenningsRE.DAL;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for AddTenant.xaml
    /// </summary>
    public partial class AddTenant : Window
    {
        private string tenant_name;
        private int unit_number;
        private double unit_size_sqft;
        private double rent_per_sf;
        private double monthly_rent;
        private double annual_rent;
        private DateTime lease_start;
        private DateTime lease_end;
        private int months_left;

        public AddTenant()
        {
            Clear();
            InitializeComponent();

            //Cancel Transaction Button
            Button cancelBtn = new Button()
            {
                Name = "CancelBtn"
            };
            cancelBtn.Click += CancelBtn_Click;

            //Add tenant button
            Button saveBtn = new Button()
            {
                Name = "SaveBtn"
            };
            saveBtn.Click += SaveBtn_Click;
        }


        /// <summary>
        /// Clears values from controls
        /// </summary>
        private void Clear()
        {
            foreach (UIElement element in TenantGrid.Children)
            {
                TextBox textbox = element as TextBox;
                if (textbox != null)
                {
                    textbox.Text = String.Empty;
                }
            }
        }


        /// <summary>
        /// Closes the window, disregards changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Saves the tenant to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            tenant_name = formTenantName.Text;
            unit_number = int.Parse(formUnitNumber.Text);
            unit_size_sqft = double.Parse(formSquareFeet.Text);
            rent_per_sf = annual_rent = double.Parse(formRent.Text);
            monthly_rent = (double.Parse(formRent.Text)/12);
            lease_start = DateTime.Parse(formStart.Text);
            lease_end = DateTime.Parse(formEnd.Text);
            months_left = int.Parse(formMonthsLeft.Text);

            TenantAccess tenant = new TenantAccess();
            tenant.AddNewTenant(1,tenant_name,unit_number,unit_size_sqft,rent_per_sf,monthly_rent,annual_rent,lease_start,lease_end,months_left);
            MessageBox.Show("Tenant added");
            Clear();

            //tenant.GetAllTenants(1);

            //tenant.UpdateTenant(1);

            //tenant.DeleteTenant(1);
            //MessageBox.Show("Tenant Deleted");
        }
    }
}
