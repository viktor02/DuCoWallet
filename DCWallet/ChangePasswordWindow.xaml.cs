using System.Windows;

namespace DCWallet
{
    public partial class ChangePasswordWindow : Window
    {
        private string Username;
        private string Password;
        public ChangePasswordWindow(string username, string password)
        {
            Username = username;
            Password = password;
            InitializeComponent();
        }

        private void ChangePasswordButton(object sender, RoutedEventArgs e)
        {
            string newPassword = ChangePasswordTb.Text;
            DucoApi.change_password(Username, Password, newPassword);
        }
    }
}