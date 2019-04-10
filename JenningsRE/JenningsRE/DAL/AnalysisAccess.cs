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
        AnalysisWindow _aw;

        public AnalysisAccess(jenningsdbEntitiesConnection context, AnalysisWindow analysisWindow)
        {
            this.context = context;
            _aw = analysisWindow;
        }

        /// <summary>
        /// Add New Analysis to the Database
        /// </summary>
        /// <param name="ana"></param>
        public void AddAnalysis(analysis ana)
        {
            //TODO SET PROPERTY_EXPENSE_ID HERE - LE

            context.analyses.Add(ana);
            context.SaveChanges();

            _aw.LoadPropertyList();
            //MainWindow.analysisDataGrid.ItemsSource = context.analysis.ToList();

        }

        /// <summary>
        /// Gets a list of all analyses
        /// </summary>
        /// <param name="analysis_id"></param>
        /// <returns>List of type analysis</returns>
        public List<analysis> GetAnalysis()
        {
            return context.analyses.ToList();
        }

        /// <summary>
        /// Deletes the passed Expense object from DataGrid and DB
        /// </summary>
        /// <param name="exp"></param>
        public void RemoveAnalysis(int id)
        {
            var delAna = (from an in context.analyses
                          where an.analysis_id == id
                          select an).Single();

            context.analyses.Remove(delAna);
            context.SaveChanges();

            _aw.LoadPropertyList();
            // MainWindow.analysisDataGrid.ItemsSource = context.analysis.ToList();
        }

    }
}
