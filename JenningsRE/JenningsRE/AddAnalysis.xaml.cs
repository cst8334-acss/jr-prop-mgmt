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
using JenningsRE.DAL;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for AddAnalysis.xaml
    /// </summary>
    public partial class AddAnalysis : Window
    {
        jenningsdbEntitiesConnection context;
        AnalysisAccess _AnalysisAccess;

        public AddAnalysis(jenningsdbEntitiesConnection context, AnalysisAccess analysisAccess)
        {
            InitializeComponent();
            this.context = context;
            _AnalysisAccess = analysisAccess;

            Button applyBtn = new Button
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

        private Boolean Validate()
        {
            foreach (Control control in AddAnalysisGrid.Children)
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

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void ApplyBtn_Click(object sender, RoutedEventArgs e)
        {
            var validator = Validate();
            if (validator)
            {
                analysis analysis = new analysis
                {
                    analysis_name = formName.Text,
                    land_acquisition_price = 0,
                    acquisition_leasing_fee = 0,
                    acquisition_leasing_percentage = 0,
                    syndication_fee = 0,
                    syndication_percentage = 0,
                    land_transfer_tax = 0,
                    land_transfer_percentage = 0,
                    legal_purchase_lease_fee = 0,
                    contingency_tenant_improvement_fee = 0,
                    environmental_fee = 0,
                    construction_fee = 0,
                    mortgage_fee = 0,
                    bcr_fee = 0,
                    appraisal_fee = 0,
                    total_closing_fees = 0,
                    total_acquisition_cost = 0,
                    mortgage_ltv_cost = 0,
                    mortgage_ltv_percentage = 0,
                    twenty_five_year_interest_rate = 0,
                    twenty_five_year_interest_rate_percentage = 0,
                    annual_debt_service_twenty_five_years = 0
                };

                _AnalysisAccess.AddAnalysis(analysis);

                Close();

                MessageBoxButton mbBtn = MessageBoxButton.OK;
                string header = "Add Property";
                string message = $"Property: {analysis.analysis_name} has been created.";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);
            }
        }
    }
}
