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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for AddProperty.xaml
    /// </summary>
    public partial class AddProperty : Window
    {
        jenningsdbEntitiesConnection context;
        PropertyAccess _propertyAccess;

        public AddProperty(jenningsdbEntitiesConnection context, PropertyAccess propertyAccess)
        {
            InitializeComponent();
            this.context = context;
            _propertyAccess = propertyAccess;

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

        /// <summary>
        /// Validating inputs
        /// </summary>
        /// <returns></returns>
        private Boolean Validate()
        {
            foreach (Control control in AddPropertyGrid.Children)
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
                property property = new property
                {
                    property_name = formName.Text,
                    address = formAddress.Text,
                    city = formCity.Text,
                    province = formProvince.Text,
                    country = formCountry.Text,
                    postal_code = formPostal.Text,
                    number_of_units = int.Parse(formNumberOfUnits.Text),
                    available_space = int.Parse(formAvailableSpace.Text),
                    rentable_area = int.Parse(formRentableArea.Text),
                    parking_spots = formParkingSpots.Text
                };

                _propertyAccess.AddProperty(property);

                Close();

                MessageBoxButton mbBtn = MessageBoxButton.OK;
                string header = "Add Property";
                string message = $"Property: {property.property_name} has been created.";
                MessageBoxImage icon = MessageBoxImage.Information;
                MessageBoxResult result = MessageBox.Show(message, header, mbBtn, icon);
            }
        }
    }
}
