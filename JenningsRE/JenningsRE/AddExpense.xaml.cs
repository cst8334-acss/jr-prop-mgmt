using JenningsRE.DAL;
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
    /// Interaction logic for AddExpense.xaml
    /// </summary>
    public partial class AddExpense : Window
    {
        #region Members
        jenningsdbEntitiesConnection context;
        ExpenseAccess _expenseAccess;
        int propertyId;
        #endregion

        public AddExpense(jenningsdbEntitiesConnection context, ExpenseAccess expenseAccess, int propertyId)
        {

            InitializeComponent();
            this.context = context;
            _expenseAccess = expenseAccess;
            this.propertyId = propertyId;


            //Add Expense Button
            Button applyBtn = new Button()
            {
                Name = "ApplyBtn"
            };
            applyBtn.Click += ApplyBtn_Click;

            //Cancel Transaction Button
            Button cancelBtn = new Button()
            {
                Name = "CancelBtn"
            };
            cancelBtn.Click += CancelBtn_Click;

        }

        /// <summary>
        /// Closes the window, disregards changes.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }


        /// <summary>
        /// Creates expense based on provided fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {

            //Map Textbox fields to new expense object
            expense expense = new expense
            {
                expense_name = formExpenseName.Text,
                expense_cost = Double.Parse(formExpenseCost.Text),
                expense_desc = formExpenseDescription.Text,
                contractor_name = formContractorName.Text,
                expense_type = expTypeOp.IsChecked == true ? expTypeOp.Content.ToString() : expTypeAdm.Content.ToString()
            };

            //Add expense to DataGrid and DB
            _expenseAccess.AddExpense(expense, propertyId);

            Close();

            MessageBoxButton mbBtn = MessageBoxButton.OK;
            string header = "Add Expense";
            string message = $"Expense: {expense.expense_name} has been created.";
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);
        }

    }
}
