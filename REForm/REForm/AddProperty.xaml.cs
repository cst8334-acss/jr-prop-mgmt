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
using REForm.Helpers;

namespace REForm
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class AddProperty : Window
    {
        public MySqlConnection connection { get; set; }
        public String PropertyName { get; set; }
        public String Address { get; set; }
        public String  city { get; set; }
        public String province { get; set; }
        public String country { get; set; }
        public String postal { get; set; }
        public Int16 numberOfUnits{ get; set; }
        public MainWindow mainWindow { get; set; }

        public AddProperty(MySqlConnection connection, MainWindow mainWindow)
        {
            this.connection = connection;
            this.mainWindow = mainWindow;
            InitializeComponent();
            DataContext = this;

            Button saveBtn = new Button()
            {
                Name = "SaveBtn"
            };
            saveBtn.Click += SaveBtn_Click;

            Button cancelBtn = new Button
            {
                Name = "CancelBtn"
            };
            cancelBtn.Click += CancelBtn_Click;
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            PropertyName = formPropertyName.Text;
            Address = formAddress.Text;
            city = formCity.Text;
            province = formProvince.Text;
            country = formCountry.Text;
            postal = formPostal.Text;
            numberOfUnits = Int16.Parse(formunits.Text);



            var addPropertyQuery = buildSaveQuery(Address, city, province, country, postal,numberOfUnits, PropertyName);

            //Here we execute the sql statements
            CRUDHelper.ExecuteQuery(addPropertyQuery, connection);
            MessageBox.Show("Property added");
            //this will refresh the grid for real-time refresh
            mainWindow.RefreshPropertyGrid();
            Close();

        }
   
        private String buildSaveQuery(String Address,String city,String province,String country,String postal,Int16 numberOfUnits, String PropertyName)
        {
            var command = "INSERT INTO property.property_details (address,city,postal_code,province,country,number_of_units,property_name)" +
                         $"VALUES ('{Address}', '{city}','{postal}','{province}','{country}','{numberOfUnits}','{PropertyName}');";
            return command;
        }
    }
}
