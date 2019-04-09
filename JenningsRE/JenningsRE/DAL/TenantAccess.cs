using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace JenningsRE.DAL
{
    /// <summary>
    /// @author Vijay Abhichandani
    /// This Class has all the logic for tenant CRUD operation
    /// </summary>
    public class TenantAccess
    {
        private jenningsdbEntitiesConnection contex;

        public TenantAccess(jenningsdbEntitiesConnection contex)
        {
            this.contex = contex;
        }

        /// <summary>
        /// adding the new tenant
        /// </summary>
        /// <param name="tenant"></param>
        public void AddNewTenant(tenant tenant, int propertyId)
       {
            tenant.tenant_property_id = propertyId;
           contex.tenants.Add(tenant);
           contex.SaveChanges();
           MainWindow.TenantDataGrid.ItemsSource = contex.tenants.ToList();
       }

        /// <summary>
        /// getting all the tenant details based on property
        /// </summary>
        /// <param name="propertyId"></param>
        /// <returns></returns>
        public List<tenant> GetTenants(int propertyId)
        {
            var propTenants = from t in contex.tenants
               where t.tenant_property_id == propertyId
                select t;
            return propTenants.ToList();
        }

        /// <summary>
        /// Deleting the tenant
        /// </summary>
        /// <param name="tenant"></param>
        public void DeleteTenant(tenant tenant)
        {
            var delTenant = (from t in contex.tenants
                where t.tenant_id == tenant.tenant_id
                select t).Single();
            contex.tenants.Remove(delTenant);
            contex.SaveChanges();
            MainWindow.TenantDataGrid.ItemsSource = contex.tenants.ToList();
        }

    }
}
