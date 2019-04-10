using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenningsRE.DAL
{
public class AnalysisAccess
    {
        jenningsdbEntitiesConnection context;

        public AnalysisAccess(jenningsdbEntitiesConnection context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add New Expense to the Database
        /// </summary>
        /// <param name="ana_name"></param>
        /// <param name="ana_cost"></param>
        /// <param name="ana_desc"></param>
        /// <param name="contractor_name"></param>
        /// <param name="ana_type"></param>
        public void AddAnalysis(analysis ana)
        {
            //TODO SET PROPERTY_EXPENSE_ID HERE - LE

            context.analyses.Add(ana);
            context.SaveChanges();

            //MainWindow.analysisDataGrid.ItemsSource = context.analysis.ToList();

        }

        /// <summary>
        /// Gets a List of expenses related to the supplied property id.
        /// </summary>
        /// <param name="property_id"></param>
        /// <returns>List of type expense</returns>
        public List<analysis> GetAnalysis(int analysis_id)
        {
            var propAnalysis = from e in context.analyses select e;

            return propAnalysis.ToList();
        }

        /// <summary>
        /// Deletes the passed Expense object from DataGrid and DB
        /// </summary>
        /// <param name="exp"></param>
        public void RemoveAnalysis(analysis ana)
        {
            var delAna = (from an in context.analyses
                          where an.analysis_id == ana.analysis_id
                          select an).Single();

            context.analyses.Remove(delAna);
            context.SaveChanges();

            // MainWindow.analysisDataGrid.ItemsSource = context.analysis.ToList();
        }

    }
}
