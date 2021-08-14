using System;
using System.Windows;

namespace DCWallet
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();

            string[] creds = FormAPI.LoadCredentials();
            if (creds != null || creds.Length > 0)
            {
                try
                {
                    string username = creds[0];
                    string password = creds[1];

                    bool auth = DucoApi.auth_check(username, password);
                    MainWindow mainWindow = new MainWindow(username, password, auth);
                    mainWindow.Show();

                    this.Close();
                }
                catch (IndexOutOfRangeException)
                {
                    MessageBox.Show("Error loading credentials. Enter your login and password");
                }
                catch (Exception exc)
                {
                    MessageBox.Show("Error loading credentials. " + exc.Message);
                }
            }
        }

        private void LoginButton_OnClick(object sender, RoutedEventArgs e)
        {
            string username = LoginTb.Text;
            string password = PasswordTb.Text;
            if ((password == String.Empty) | (username == String.Empty))
            {
                MessageBox.Show("Password or login is empty.", "DuCoWallet");
                return;
            }

            FormAPI.SaveCredentials(username, password);

            bool auth = DucoApi.auth_check(username, password);
            
            // If password not correct, give choice
            if (!auth)
            {
                MessageBoxResult isWannaSend = MessageBox.Show($"Incorrect password or an error occurred. Only information that does not require authentication will be loaded. Continue?", "DuCoWallet", 
                    MessageBoxButton.YesNo, MessageBoxImage.Information);
                if (isWannaSend == MessageBoxResult.No)
                {
                    return;
                }
            }
            MainWindow mainWindow = new MainWindow(username, password, auth);
            mainWindow.Show();
            this.Close();
        }
    }
}