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
    /// Interaction logic for addTenantWindow.xaml
    /// </summary>
    public partial class addTenantWindow : Window
    {
        public MySqlConnection connection { get; set; }
        public String TenantName { get; set; }
        public Int16 UnitNumber { get; set; }
        public Double SquareFeet  { get; set; }
        public Double RentPerSF { get; set; }
        public Double MonthlyRent { get; set; }
        public Double AnnualRent { get; set; }
        public Int16 TenantPropertyId { get; set; }
        public DataGrid TenantGrid { get; set; }
        public MainWindow mainWindow { get; set; }


        public addTenantWindow(MySqlConnection connection, MainWindow mainWindow)
        {
            InitializeComponent();
            this.connection = connection;
            this.mainWindow = mainWindow;
            DataContext = this;

            Button saveBtn = new Button()
            {
                Name = "saveBtn"
            };
            saveBtn.Click += SaveBtn_Click;

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

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            TenantName = formTenantName.Text;
            UnitNumber = Int16.Parse(formUnitForm.Text);
            SquareFeet = Double.Parse(formSquareFeet.Text);
            RentPerSF = Double.Parse(formRent.Text);
            MonthlyRent = SquareFeet * RentPerSF;
            AnnualRent = MonthlyRent * 12;
            TenantPropertyId = 1;
            var addTenantQuery = buildSaveQuery(TenantName,UnitNumber,SquareFeet,RentPerSF,MonthlyRent,AnnualRent,TenantPropertyId);

            //Here we execute the sql statements
            CRUDHelper.ExecuteQuery(addTenantQuery, connection);
            MessageBox.Show("Tenant added");
            //this will refresh the grid for real-time refresh
            mainWindow.RefreshTenantGrid();
            Close();

        }
        /// <summary>
        /// This will build the query to insert the data in the database
        /// </summary>
        /// <param name="TenantName"></param>
        /// <param name="UnitNumber"></param>
        /// <param name="SquareFeet"></param>
        /// <param name="RentPerSF"></param>
        /// <param name="MonthlyRent"></param>
        /// <param name="AnnualRent"></param>
        /// <param name="TenantPropertyId"></param>
        /// <returns></returns>
        private String buildSaveQuery(String TenantName, Int16 UnitNumber, Double SquareFeet, Double RentPerSF, Double MonthlyRent, Double AnnualRent,Int16 TenantPropertyId)
        {
            var command = "INSERT INTO property.tenant (tenant_name,unit_number,square_feet,rent_per_sf,monthly_rent,annual_rent,tenant_propertyId)" +
                         $"VALUES ('{TenantName}', {UnitNumber},{SquareFeet},{RentPerSF},{MonthlyRent},{AnnualRent},{TenantPropertyId});";
            return command;
        }
    }
}
