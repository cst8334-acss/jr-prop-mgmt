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
using JenningsRE.DAL;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for AddTenant.xaml
    /// </summary>
    public partial class AddTenant : Window
    {
        private string TenantName;
        public AddTenant()
        {
            InitializeComponent();
            TenantName = formTenantName.Text;
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            TenantAccess tenant = new TenantAccess();
            tenant.AddNewTenant(1);
            MessageBox.Show("Tenant added");

            //tenant.GetAllTenants(1);

            //tenant.UpdateTenant(1);

            //tenant.DeleteTenant(1);
            //MessageBox.Show("Tenant Deleted");
        }
    }
}
