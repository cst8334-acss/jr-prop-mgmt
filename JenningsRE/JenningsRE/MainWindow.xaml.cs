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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();


            //Expense Tab Logic

            //Add Expense Button
            Button addExpenseBtn = new Button
            {
                Name = "addExpenseBtn"
            };
            addExpenseBtn.Click += AddExpenseBtn_Click;
        }

        /// <summary>
        /// Display Pop-up Add Expense Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddExpenseBtn_Click(object sender, RoutedEventArgs e)
        {
            AddExpense addWindow = new AddExpense();
            addWindow.ShowDialog();
        }

        /// <summary>
        /// Switch view to Analysis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void switchScreen(object sender, RoutedEventArgs e)
        {
            AnalysisWindow Analysis = new AnalysisWindow();
            Analysis.Show();
            Close();
        }

        private void AddTenantBtn_Click(object sender, RoutedEventArgs e)
        {
            AddTenant Tenant = new AddTenant();
            Tenant.Show();
        }
    }
}
