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


        public AnalysisWindow()
        {
            InitializeComponent();
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
            var validator = Valid();

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
        }

        private void NumberValid(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !IsTextAllowed(e.Text);
        }
    }
}
