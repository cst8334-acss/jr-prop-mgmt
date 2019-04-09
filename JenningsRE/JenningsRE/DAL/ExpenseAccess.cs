using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenningsRE.DAL
{
    /// <summary>
    /// Class hosting Data Access Methods for Expenses.
    /// </summary>
    public class ExpenseAccess
    {
        jenningsdbEntitiesConnection context;

        public ExpenseAccess(jenningsdbEntitiesConnection context)
        {
            this.context = context;
        }

        /// <summary>
        /// Add New Expense to the Database
        /// </summary>
        /// <param name="prop_id"></param>
        /// <param name="exp_name"></param>
        /// <param name="exp_cost"></param>
        /// <param name="exp_desc"></param>
        /// <param name="contractor_name"></param>
        /// <param name="exp_type"></param>
        public void AddExpense(expense exp, int propertyId)
        {
            //TODO SET PROPERTY_EXPENSE_ID HERE - LE

            exp.expense_property_id = propertyId;
            context.expenses.Add(exp);
            context.SaveChanges();

            MainWindow.expenseDataGrid.ItemsSource = context.expenses.ToList();

        }

        /// <summary>
        /// Gets a List of expenses related to the supplied property id.
        /// </summary>
        /// <param name="property_id"></param>
        /// <returns>List of type expense</returns>
        public List<expense> GetExpenses(int property_id)
        {
            var propExpenses = from e in context.expenses
                               where e.expense_property_id == property_id
                               select e;

            return propExpenses.ToList();
        }

        /// <summary>
        /// Deletes the passed Expense object from DataGrid and DB
        /// </summary>
        /// <param name="exp"></param>
        public void RemoveExpense(expense exp)
        {
            var delExp = (from ex in context.expenses
                          where ex.expense_id == exp.expense_id
                          select ex).Single();

            context.expenses.Remove(delExp);
            context.SaveChanges();

            MainWindow.expenseDataGrid.ItemsSource = context.expenses.ToList();
        }

    }
}
