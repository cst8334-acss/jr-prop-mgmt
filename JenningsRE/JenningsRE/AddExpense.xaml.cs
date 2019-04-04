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
        int property_id;
        string expenseName, expenseDesc, contractorName, expenseType;
        double expenseCost;

        public AddExpense()
        {

            InitializeComponent();

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
            this.Close();
        }


        /// <summary>
        /// Creates expense based on provided fields.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {

            expenseName = formExpenseName.Text;
            expenseCost = Double.Parse(formExpenseCost.Text);
            expenseDesc = formExpenseDescription.Text;
            contractorName = formContractorName.Text;
            expenseType = expTypeOp.IsChecked == true ? expTypeOp.Content.ToString() : expTypeAdm.Content.ToString();

            ExpenseAccess expenseAccess = new ExpenseAccess();
            expenseAccess.AddExpense(1, expenseName, expenseCost, expenseDesc, contractorName, expenseType);

            Close();

            MessageBoxButton mbBtn = MessageBoxButton.OK;
            string header = "Add Expense";
            string message = $"Expense: {expenseName} has been created.";
            MessageBoxImage icon = MessageBoxImage.Information;
            MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);

            
            
        }

    }
}
