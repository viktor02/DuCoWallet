using System.Windows;

namespace DCWallet
{
    public partial class AboutWindow : Window
    {
        public AboutWindow()
        {
            InitializeComponent();

            string AppVersion = "v0.2.3";
            
            AppVersionLbl.Content = AppVersion;
            this.Title += $" {AppVersion}";
        }
    }
}