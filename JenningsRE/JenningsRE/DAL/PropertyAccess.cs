using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenningsRE.DAL
{
    /// <summary>
    /// Class hosting data access Methods for Properties.
    /// </summary>
    public class PropertyAccess
    {
        jenningsdbEntitiesConnection context;
        MainWindow _mw;

        public PropertyAccess(jenningsdbEntitiesConnection context, MainWindow mainWindow)
        {
            this.context = context;
            _mw = mainWindow;
        }

        /// <summary>
        /// Get all properties from DB
        /// </summary>
        /// <returns>A List of type properties</returns>
        public List<property> GetProperties()
        {
            var properties = from p in context.properties
                             select p;

            return properties.ToList();
        }

        /// <summary>
        /// Add property object to the Database
        /// </summary>
        /// <param name="prop"></param>
        public void AddProperty(property prop)
        {
            context.properties.Add(prop);
            context.SaveChanges();

            _mw.LoadPropertyList();
        }

        /// <summary>
        /// Remove Property with Corresponding ID from the Database
        /// </summary>
        /// <param name="id"></param>
        public void DeleteProperty(int id)
        {
            var prop = (from p in context.properties
                       where p.property_id == id
                       select p).Single();

            context.properties.Remove(prop);
            context.SaveChanges();

            _mw.LoadPropertyList();
        }
    }
}
