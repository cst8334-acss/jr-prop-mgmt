using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for UserManagement.xaml
    /// </summary>
    public partial class UserManagement : Window
    {
        public UserManagement()
        {
            InitializeComponent();
            dgUsers.ItemsSource = UserAccess.GetUsers().ToList();
        }

        private void DgUsers_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            var selectedUser = (user)dgUsers.SelectedItem;
            tbID.Text = selectedUser.id.ToString();
            tbUsername.Text = selectedUser.username;
            cbIsAdmin.IsChecked = selectedUser.isAdmin;
            cbIsEnabled.IsChecked = selectedUser.isEnabled;
        }

        private void BtUpdate_Click(object sender, RoutedEventArgs e)
        {
            var updateUser = new user()
            {
                id = int.Parse(tbID.Text),
                username = tbUsername.Text,
                isAdmin = cbIsAdmin.IsChecked,
                isEnabled = cbIsEnabled.IsChecked
            };
            UserAccess.UpdateUser(updateUser);
            dgUsers.ItemsSource = UserAccess.GetUsers().ToList();
        }

        private void Window_Closed(object sender, System.EventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void BtDelete_Click(object sender, RoutedEventArgs e)
        {
            var deleteUser = new user()
            {
                id = int.Parse(tbID.Text),
                username = tbUsername.Text,
                isAdmin = cbIsAdmin.IsChecked,
                isEnabled = cbIsEnabled.IsChecked
            };
            UserAccess.DeleteUser(deleteUser);
            dgUsers.ItemsSource = UserAccess.GetUsers().ToList();
        }
    }
}
