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
using MySql.Data.MySqlClient;


namespace REForm
{
    /// <summary>
    /// Interaction logic for AddExpenseWindow.xaml
    /// </summary>
    public partial class AddExpenseWindow : Window
    {
        public String ExpenseName { get; set; }
        public Double ExpenseCost { get; set; }
        public MySqlConnection connection { get; set; }

        public AddExpenseWindow(MySqlConnection connection)
        {
            this.connection = connection;

            InitializeComponent();
            DataContext = this;

            Button applyBtn = new Button()
            {
                Name = "applyBtn"
            };
            applyBtn.Click += ApplyBtn_Click;

            Button cancelBtn = new Button
            {
                Name = "cancelBtn"
            };
            cancelBtn.Click += CancelBtn_Click;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            ExpenseName = formExpenseName.Text;
            ExpenseCost = Double.Parse(formExpenseCost.Text);
            var addExpenseQuery = buildApplyQuery(ExpenseName, ExpenseCost);

            connection.Open();
            MySqlCommand cmd = new MySqlCommand(addExpenseQuery, connection);
            cmd.ExecuteReader();
            connection.Close();

            Close();
        }

        private String buildApplyQuery(String expName, Double expCost)
        {
            var command = "INSERT INTO EXPENSES (expense_name, expense_cost)" +
                         $"VALUES ('{ExpenseName}', {ExpenseCost.ToString()}); ";
            return command;
        }
    }
}
