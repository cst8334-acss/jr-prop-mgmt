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
        private jenningsdbEntitiesConnection contex;

        public TenantAccess(jenningsdbEntitiesConnection contex)
        {
            this.contex = contex;
        }

       /// <summary>
       /// Preforms Create operation for Database
       /// </summary>
       /// <param name="propertyid"></param>
       /// <param name="tenant_name"></param>
       /// <param name="unit_number"></param>
       /// <param name="unit_size_sqft"></param>
       /// <param name="rent_per_sf"></param>
       /// <param name="monthly_rent"></param>
       /// <param name="annual_rent"></param>
       /// <param name="lease_start"></param>
       /// <param name="lease_end"></param>
       /// <param name="months_left"></param>
        public void AddNewTenant(int propertyid, string tenant_name, int unit_number, double unit_size_sqft, double rent_per_sf, double monthly_rent, double annual_rent, DateTime lease_start, DateTime lease_end, int months_left)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {
                tenant tenant = new tenant()
                {
                    tenant_name = tenant_name,
                    unit_number = unit_number,
                    unit_size_sqft = unit_size_sqft,
                    rent_per_sf = rent_per_sf,
                    monthly_rent = monthly_rent,
                    annual_rent = annual_rent,
                    lease_start = lease_start,
                    lease_end = lease_end,
                    months_left = months_left,
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
        public void DeleteTenant(int propertyid, string tenant_name, int unit_number, double unit_size_sqft, double rent_per_sf, double monthly_rent, double annual_rent, string lease_start, string lease_end, int months_left)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {
                tenant tenant = context.tenants.Find(propertyid);
                context.tenants.Remove(tenant);
            }
        }

    }
}
