using System.Windows;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        public RegisterWindow()
        {
            InitializeComponent();
        }

        private void BtCreate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbUsername.Text) | string.IsNullOrWhiteSpace(pbPassword.Password) | string.IsNullOrWhiteSpace(pbConfirm.Password))
                lblValidation.Content = "Username/Password are required!";
            
            else if(pbPassword.Password != pbConfirm.Password)
                lblValidation.Content = "Password and Confirm must match!";
            
            else if(tbUsername.Text.Length > 15 | pbPassword.Password.Length > 15| pbConfirm.Password.Length > 15)
                lblValidation.Content = "Username/Password cannot be longer than 15 characters!";
            
            else
            {
                UserAccess.AddUser(tbUsername.Text, pbPassword.Password);
                LoginWindow loginWindow = new LoginWindow();
                loginWindow.Show();
                Close();
            }
        }
    }
}
