using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JenningsRE.DAL
{
    public class ExpenseAccess
    {

        public ExpenseAccess()
        {
            
        }

        public void AddExpense(int prop_id, string exp_name, double exp_cost, string exp_desc, string contractor_name, string exp_type)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection())
            {

                expense exp = new expense()
                {
                    expense_property_id = prop_id,
                    expense_name = exp_name,
                    expense_cost = exp_cost,
                    expense_desc = exp_desc,
                    contractor_name = contractor_name,
                    expense_type = exp_type
                };

                context.expenses.Add(exp);
                context.SaveChanges();

            }
        }


        public List<expense> GetExpenses(int property_id)
        {
            using (jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection()){

            }


            return null;
        }

    }
}
