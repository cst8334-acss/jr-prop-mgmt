using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for AnalysisWindow.xaml
    /// </summary>
    public partial class AnalysisWindow : Window
    {
        private static readonly Regex _regex = new Regex("[^0-9. ]+"); //regex that matches disallowed text
        private static bool IsTextAllowed(string text)
        {
            return !_regex.IsMatch(text);
        }

        jenningsdbEntitiesConnection context = new jenningsdbEntitiesConnection();

        AnalysisAccess _analysisAccess;

        public bool validator;

        private double varlandAcquisitionCost;
        private double varAcquisitionLeasing;
        private double varSyndication;
        private double varLandTransfer;
        private double varLegalPurchaseLease;
        private double varEnvironmental;
        private double varContigency;
        private double varMortgage;
        private double varConstruction;
        private double varAppraisal;
        private double varBCR;
        private double varMortgageLTV;
        private double varInterest25;
        private double varAnnualDebt;
        private double varLandTransferTax;
        private double varSyndicationFee;
        private double varAcquisitionFee;
        private double varClosingFee;
        private double varAcquisitionCost;
        private double varMortgageLTVCost;
        private double varInterestRatePer;

        int analysis_id;

        public AnalysisWindow()
        {
            InitializeComponent();

            _analysisAccess = new AnalysisAccess(context, this);

            analysis_id = 1;

            Loaded += AnalysisWindow_Loaded;

            DataContext = this;

           
        }

        /// <summary>
        /// Display Pop-up Add Property Window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddAnalysisBtn_Click(object sender, RoutedEventArgs e)
        {
            AddAnalysis addWindow = new AddAnalysis(context, _analysisAccess);
            addWindow.ShowDialog();
        }



        /// <summary>
        /// Fetch Properties from Database
        /// </summary>
        public void LoadPropertyList()
        {
            analysisList.ItemsSource = _analysisAccess.GetAnalysis();
        }


        private void SwitchScreen(object sender, RoutedEventArgs e)
        {
            MainWindow Main = new MainWindow();
            Main.Show();
            Close();
        }

        private Boolean Valid()
        {
            // Check to see if all textbox's have values
            foreach (Control control in AnalysisGrid.Children)
            {
                string controlType = control.GetType().ToString();
                if (controlType == "System.Windows.Controls.TextBox")
                {
                    TextBox txtBox = (TextBox)control;
                    if (string.IsNullOrEmpty(txtBox.Text))
                    {
                        MessageBox.Show("No Textboxes Can Be Empty");
                        return false;
                    }
                }
            }
            return true;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            validator = Valid();

            if (validator)
            {
                varlandAcquisitionCost = double.Parse(landAcquisitionCost.Text);
                varAcquisitionLeasing = double.Parse(AcquisitionLeasing.Text);
                varSyndication = double.Parse(Syndication.Text);
                varLandTransfer = double.Parse(LandTransfer.Text);
                varLegalPurchaseLease = double.Parse(LegalPurchaseLease.Text);
                varEnvironmental = double.Parse(Environmental.Text);
                varContigency = double.Parse(Contigency.Text);
                varMortgage = double.Parse(Mortgage.Text);
                varConstruction = double.Parse(Construction.Text);
                varAppraisal = double.Parse(Appraisal.Text);
                varBCR = double.Parse(BCR.Text);
                varMortgageLTV = double.Parse(MortgageLTV.Text);
                varInterest25 = double.Parse(Interest25.Text);
                varAnnualDebt = double.Parse(AnnualDebt.Text);
                varLandTransferTax = varLandTransfer * varlandAcquisitionCost;
                varSyndicationFee = varSyndication * varlandAcquisitionCost;
                varAcquisitionFee = varAcquisitionLeasing * varlandAcquisitionCost;
                varClosingFee = varAcquisitionLeasing + varSyndication + varLandTransfer + varLegalPurchaseLease
                    + varEnvironmental + varContigency + varMortgage + varConstruction + varConstruction
                    + varAppraisal + varBCR;
                varAcquisitionCost = varClosingFee + varlandAcquisitionCost;
                varMortgageLTVCost = varMortgageLTV + varlandAcquisitionCost;
                varInterestRatePer = varInterest25 + varlandAcquisitionCost;

                LandTransferTax.Content = varLandTransferTax.ToString();
                SyndicationFee.Content = varSyndicationFee.ToString();
                AcquisitionFee.Content = varAcquisitionFee.ToString();
                ClosingFee.Content = varClosingFee.ToString();
                AcquisitionCost.Content = varAcquisitionCost.ToString();
                MortgageLTVCost.Content = varMortgageLTVCost.ToString();
                InterestRatePer.Content = varInterestRatePer.ToString();

            }
            
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            
            
            Update_Click(sender, e);
            if (validator)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure you want to save this property?",
                    "Update Property", MessageBoxButton.YesNo);

                if (result == MessageBoxResult.Yes)
                {
                    analysis analysis = (from a in context.analyses
                                         where a.analysis_id == analysis_id
                                         select a).First();

                    analysis.land_acquisition_price = varlandAcquisitionCost;
                    analysis.acquisition_leasing_fee = varAcquisitionFee;                    
                    analysis.acquisition_leasing_percentage = varAcquisitionLeasing;
                    analysis.syndication_fee = varSyndicationFee;
                    analysis.syndication_percentage = varSyndication;
                    analysis.land_transfer_tax = varLandTransferTax;
                    analysis.land_transfer_percentage = varLandTransfer;
                    analysis.legal_purchase_lease_fee = varLegalPurchaseLease;
                    analysis.contingency_tenant_improvement_fee = varContigency;
                    analysis.environmental_fee = varEnvironmental;
                    analysis.construction_fee = varConstruction;
                    analysis.mortgage_fee = varMortgage;
                    analysis.bcr_fee = varBCR;
                    analysis.appraisal_fee = varAppraisal;
                    analysis.total_closing_fees = varClosingFee;
                    analysis.total_acquisition_cost = varAcquisitionCost;
                    analysis.mortgage_ltv_cost = varMortgageLTVCost;
                    analysis.mortgage_ltv_percentage = varMortgageLTV;
                    analysis.twenty_five_year_interest_rate = varInterest25;
                    analysis.twenty_five_year_interest_rate_percentage = varInterestRatePer;
                    analysis.annual_debt_service_twenty_five_years = varAnnualDebt;

                    context.SaveChanges();


                    LoadPropertyList();
                }
                else if (result == MessageBoxResult.No)
                {
                    Hide();
                }
            }

        }

        /// <summary>
        /// Delete Scoped Property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteAnalysisBtn_Click(object sender, RoutedEventArgs e)
        {

            MessageBoxResult result = MessageBox.Show("Are you sure you want to delete this Analysis?",
                                      "Delete Analysis", MessageBoxButton.YesNo);

            if (result == MessageBoxResult.Yes)
            {
                _analysisAccess.RemoveAnalysis(analysis_id);
                ClearPropertyForm();
            }
            else if (result == MessageBoxResult.No)
            {
                Hide();
            }
        }

        /// <summary>
        /// Clears the Forms in the Property Form Fields
        /// </summary>
        private void ClearPropertyForm()
        {
            landAcquisitionCost.Clear();
            AcquisitionLeasing.Clear();
            Syndication.Clear();
            LandTransfer.Clear();
            LegalPurchaseLease.Clear();
            Environmental.Clear();
            Contigency.Clear();
            Mortgage.Clear();
            Construction.Clear();
            Appraisal.Clear();
            BCR.Clear();
            MortgageLTV.Clear();
            Interest25.Clear();
            AnnualDebt.Clear();
        }

        /// <summary>
        /// Executed on Window Load
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AnalysisWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadPropertyList();

        }

        private void NumberValid(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }


        private void AnalysisList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // ListViewItem item = sender as ListViewItem;
            // object obj = item.Content;

            ListViewItem viewI = sender as ListViewItem;
            object obje = viewI.Content;

            //Set Global Property Id
            this.analysis_id = ((analysis)obje).analysis_id;

            //Set Text fields to parsed property members.
            landAcquisitionCost.Text = ((analysis)obje).land_acquisition_price.ToString();
            AcquisitionLeasing.Text = ((analysis)obje).acquisition_leasing_percentage.ToString();
            Syndication.Text = ((analysis)obje).syndication_percentage.ToString();
            LandTransfer.Text = ((analysis)obje).land_transfer_percentage.ToString();
            LegalPurchaseLease.Text = ((analysis)obje).legal_purchase_lease_fee.ToString();
            Environmental.Text = ((analysis)obje).environmental_fee.ToString();
            Contigency.Text = ((analysis)obje).contingency_tenant_improvement_fee.ToString();
            Mortgage.Text = ((analysis)obje).mortgage_fee.ToString();
            Construction.Text = ((analysis)obje).construction_fee.ToString();
            Appraisal.Text = ((analysis)obje).appraisal_fee.ToString();
            BCR.Text = ((analysis)obje).bcr_fee.ToString();
            MortgageLTV.Text = ((analysis)obje).mortgage_ltv_percentage.ToString();
            Interest25.Text = ((analysis)obje).twenty_five_year_interest_rate_percentage.ToString();
            AnnualDebt.Text = ((analysis)obje).annual_debt_service_twenty_five_years.ToString();




        }
    }
}
