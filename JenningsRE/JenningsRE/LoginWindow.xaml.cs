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

namespace JenningsRE
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public String Username { get; set; }
        public String Password { get; set; }
        public String CorrectUser { get; set; }
        public String CorrectPass { get; set; }
        public LoginWindow()
        {
            InitializeComponent();
            CorrectUser = "admin";
            CorrectPass = "admin";

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            Username = logUser.Text;
            Password = logPass.Text;

            if (Username == CorrectUser && Password == CorrectPass)
            {
                OpenWindow Open = new OpenWindow();
                Open.Show();
                Close();
            }
        }
    }
}
