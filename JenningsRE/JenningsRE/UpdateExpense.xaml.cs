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

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for UpdateExpense.xaml
    /// </summary>
    public partial class UpdateExpense : Window
    {

        jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection();

        expense expense;

        /// <summary>
        /// Update Expense Window for Expense CRUD Operations.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="expName"></param>
        /// <param name="expCost"></param>
        /// <param name="ctrName"></param>
        /// <param name="expDesc"></param>
        /// <param name="expType"></param>
        public UpdateExpense()
        {
            InitializeComponent();

            expense = GetSelectedExpense();
            SetFields(expense);
        }

        /// <summary>
        /// Validating inputs
        /// </summary>
        /// <returns></returns>
        private Boolean Validate()
        {
            foreach (Control control in UpdateExpenseGrid.Children)
            {
                string controlType = control.GetType().ToString();
                if (controlType == "System.Windows.Controls.TextBox")
                {
                    TextBox txtBox = (TextBox)control;
                    if (string.IsNullOrEmpty(txtBox.Text))
                    {
                        MessageBox.Show(txtBox.Name + " Can not be empty");
                        return false;
                    }
                }
            }
            return true;
        }

        private expense GetSelectedExpense()
        {
          return MainWindow.ExpenseDataGrid.SelectedItem as expense;
        }

        /// <summary>
        /// Set window fields based on select row
        /// </summary>
        private void SetFields(expense expense)
        {

            formExpenseName.Text = expense.expense_name;
            formExpenseCost.Text = expense.expense_cost.ToString();
            formContractorName.Text = expense.contractor_name;
            formExpenseDescription.Text = expense.expense_desc;

            if(expense.expense_type == "Operational")
            {
                expTypeOp.IsChecked = true;
            }
            else if (expense.expense_type == "Administrative")
            {
                expTypeAdm.IsChecked = true;
            }
        }

        /// <summary>
        /// Close the update Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Fetch selected row from Database, update Grid and DB
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtn_Click(object sender, RoutedEventArgs e)
        {
            var validator = Validate();
            if (validator)
            {
                expense editExp = (from ex in context.expenses
                    where ex.expense_id == this.expense.expense_id
                    select ex).Single();

                editExp.expense_name = formExpenseName.Text;
                editExp.expense_cost = Double.Parse(formExpenseCost.Text);
                editExp.contractor_name = formContractorName.Text;
                editExp.expense_desc = formExpenseDescription.Text;
                editExp.expense_type = expTypeOp.IsChecked == true
                    ? expTypeOp.Content.ToString()
                    : expTypeAdm.Content.ToString();

                context.SaveChanges();
                MainWindow.ExpenseDataGrid.ItemsSource = context.expenses.ToList();

                MessageBoxButton mbBtn = MessageBoxButton.OK;
                string header = "Update Expense";
                string message = $"Expense id:{editExp.expense_id} has been altered.";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);

                Close();
            }
        }
    }
}
