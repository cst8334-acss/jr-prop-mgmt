using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Media3D;

namespace JenningsRE.DAL
{
    class TenantAccess
    {
        public void AddNewTenant(int propertyid)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {
                tenant tenant = new tenant()
                {
                    tenant_name = "Vijay",
                    unit_number = 10,
                    unit_size_sqft = 1,
                    rent_per_sf = 12000,
                    monthly_rent = 1000,
                    annual_rent = 12000,
                    lease_start = DateTime.Today,
                    lease_end = DateTime.Today,
                    months_left = 12,
                    tenant_property_id = propertyid
                };
                context.tenants.Add(tenant);
                context.SaveChanges();
            }
        }

        public List<tenant> GetAllTenants(int propertyid)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {
                tenant tenant = context.tenants.FirstOrDefault(t => t.tenant_name == "Vijay");
                MessageBox.Show(tenant.lease_start.ToString());
            }
            return null;
        }

        public void UpdateTenant(int propertyid)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {
                tenant tenant = context.tenants.FirstOrDefault(t => t.tenant_property_id == 1);
                tenant.tenant_name = "Liam";
                context.SaveChanges();
                MessageBox.Show(tenant.tenant_name);
            }

        }
        public void DeleteTenant(int propertyid)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {
                tenant tenant = context.tenants.FirstOrDefault(t => t.tenant_name == "Vijay");
                context.tenants.Remove(tenant);
            }
        }

    }
}
