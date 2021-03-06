﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.AccessControl;
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
    /// @author Vijay Abhichandani
    /// This class will add the tenant into the database using mouse click events from the UI
    /// </summary>
    public partial class AddTenant : Window
    {

        #region Members
        jenningsdbEntitiesConnection context;
        TenantAccess _tenantAccess;
        int propertyId;
        #endregion

        public AddTenant(jenningsdbEntitiesConnection context, TenantAccess  tenantAccess, int propertyId)
        {
            InitializeComponent();
            this.context = context;
            this.propertyId = propertyId;
            _tenantAccess = tenantAccess;

            //Cancel Transaction Button
            Button cancelBtn = new Button()
            {
                Name = "CancelBtn"
            };
            cancelBtn.Click += CancelBtn_Click;

            //Add tenant button
            Button saveBtn = new Button()
            {
                Name = "SaveBtn"
            };
            saveBtn.Click += SaveBtn_Click;
        }

        /// <summary>
        /// Clears values from controls
        /// </summary>
        private void Clear()
        {
            foreach (UIElement element in TenantGrid.Children)
            {
                TextBox textbox = element as TextBox;
                if (textbox != null)
                {
                    textbox.Text = String.Empty;
                }
            }
        }

        /// <summary>
        /// Validating inputs
        /// </summary>
        /// <returns></returns>
        private Boolean Validate()
        {
            foreach (Control control in TenantGrid.Children)
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
        /// Saves the tenant to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            
            var validator = Validate();
            if (validator)
            {
                property property = (from p in context.properties
                                      where p.property_id == propertyId
                                      select p).FirstOrDefault();

                var annualRent = (double.Parse(formRent.Text)) * (double.Parse(formSquareFeet.Text));

                var unitSize = int.Parse(formSquareFeet.Text);
                var availableSpace = property.available_space;
                var leaseStart = DateTime.Parse(formStart.Text);
                var leaseEnd = DateTime.Parse(formEnd.Text);
                var monthsLeft = (leaseEnd.Month + leaseEnd.Year * 12) - (leaseStart.Month + leaseStart.Year * 12);

                if (unitSize <= availableSpace)
                {
                    //Map Text box fields to new tenant object
                    tenant tenant = new tenant
                    {
                        tenant_name = formTenantName.Text,
                        unit_number = int.Parse(formUnitNumber.Text),
                        unit_size_sqft = double.Parse(formSquareFeet.Text),
                        rent_per_sf = double.Parse(formRent.Text),
                        annual_rent = annualRent,
                        monthly_rent = annualRent / 12,
                        lease_start = leaseStart,
                        lease_end = leaseEnd,
                        months_left = monthsLeft,
                        tenant_property_id = propertyId
                    };
                    property.available_space = availableSpace - unitSize;
                    _tenantAccess.AddNewTenant(tenant, propertyId);
                    context.SaveChanges();
                    Close();

                    MessageBoxButton mbBtn = MessageBoxButton.OK;
                    string header = "Add Expense";
                    string message = $"Tenant: {tenant.tenant_name} has been added.";
                    MessageBoxImage icon = MessageBoxImage.Information;
                    MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);
                    Clear();
                }
            }
        }
    }
}
