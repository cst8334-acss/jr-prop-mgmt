using System;
using System.Windows;
using JenningsRE.ResultTransfer;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public String Username { get; set; }
        public String Password { get; set; }

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            btLogin.IsEnabled = false;
            lblValidation.Content = "";
            Username = logUser.Text;
            Password = logPass.Password;
            var result = UserAccess.VerifyUser(Username, Password);
            
            if(result is OkResult)
            {
                if (result.GetValue() == false.ToString())
                {
                    OpenWindow Open = new OpenWindow();
                    Open.Show();
                    Close();
                }
                else
                {
                    UserManagement userManagement = new UserManagement();
                    userManagement.Show();
                    Close();
                }
            }
            else
            {
                lblValidation.Content = result.GetValue();
            }
            btLogin.IsEnabled = true;
        }

        private void BtRegister_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow Register = new RegisterWindow();
            Register.Show();
            Close();
        }
    }
}
