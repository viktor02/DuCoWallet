using System;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Windows;
using Newtonsoft.Json.Linq;
using System.Linq;
using System.Timers;
using System.Windows.Controls;
using Brushes = System.Windows.Media.Brushes;

namespace DCWallet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string Username;
        private string Password;
        private decimal UserBalance;

        public bool AuthStatus;
        BackgroundWorker worker = new BackgroundWorker();
        
        public MainWindow(string username, string password, bool authStatus)
        {
	        InitializeComponent();
            this.Username = username;
            this.Password = password;
            this.AuthStatus = authStatus;

            if (AuthStatus == false)
            {
                this.Title = "DuCoWallet - You are not logged in";
                SendCoinsButton.IsEnabled = false;
            }
            
            var dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(UpdateForm);
            dispatcherTimer.Interval = new TimeSpan(0,0,10);
            dispatcherTimer.Start();

            UpdateForm(null, null);
        }

        public void RenameBalanceFile()
        {
	        int count = 1;

	        string fullPath = Path.GetFullPath("balance.log");
	        
	        string fileNameOnly = Path.GetFileNameWithoutExtension(fullPath);
	        string extension = Path.GetExtension(fullPath);
	        string path = Path.GetDirectoryName(fullPath);
	        string newFullPath = fullPath;

	        while(File.Exists(newFullPath)) 
	        {
		        string tempFileName = string.Format("{0}({1})", fileNameOnly, count++);
		        newFullPath = Path.Combine(path, tempFileName + extension);
	        }
        }
        
		public void UpdateForm(object source, EventArgs e)
		{
			var account = DucoApi.get_account_info(Username);

			
			
			if (account.ContainsKey("message"))
			{
				MessageBox.Show(account["message"].ToString());
				return;
			}
			if (account.ContainsKey("err"))
			{
				MessageBox.Show(account["err"].ToString());
				return;
			}
			JArray jminers = (JArray)account["result"]["miners"];
			JValue jbalance = (JValue)account["result"]["balance"]["balance"];
			JArray jtransactions = (JArray)account["result"]["transactions"];

			// Clear treeview and listview
			MinerTreeView.DataContext = null;
			MinerTreeView.Items.Clear();
			TransactionsTreeView.DataContext = null;
			TransactionsTreeView.Items.Clear();
			UserBalance = (decimal)jbalance;

			// Fill hello and balance label
			HelloLabel.Content = $"Hello, {this.Username}";
			BalanceLabel.Content = $"{UserBalance} DUCO";
			decimal price = DucoApi.get_duco_price();
			BalanceLabel.Content = BalanceLabel.Content + $" ~${decimal.Round(price*UserBalance, 2)}";
			
			bool hasMiners = account["result"]["miners"].HasValues;
			if (hasMiners)
			{
				foreach (var x in jminers)
				{
					string identifier = x["identifier"].ToString();
					string software   = x["software"].ToString();
					string algorithm  = x["algorithm"].ToString();
					string diff       = x["diff"] + " diff";
					string hashrate   = $"{(int)x["hashrate"] * 1000} kH/s";
					string accepted   = x["accepted"] + " accepted";
					string rejected   = x["rejected"] + " rejected";

					TreeViewItem newChild = new TreeViewItem();
					newChild.Header = $"{identifier} - {software}";
					newChild.Items.Add(hashrate);
					newChild.Items.Add(diff);
					newChild.Items.Add(algorithm);
					newChild.Items.Add(accepted);
					newChild.Items.Add(rejected);
					MinerTreeView.Items.Add(newChild);
				}
			} else
				MinerTreeView.Items.Add("Your miners will be here");

			bool hasTransactions = account["result"]["transactions"].HasValues;
			if (hasTransactions)
			{
				foreach (var transaction in jtransactions.Reverse())
				{
					string amount    = transaction["amount"].ToString();
					string datetime  = transaction["datetime"].ToString();
					string hash      = transaction["hash"].ToString();
					string memo      = transaction["memo"].ToString();
					string recipient = transaction["recipient"].ToString();
					string tSender  = transaction["sender"].ToString();

					if (tSender == Username)
					{
						TreeViewItem newChildTr = new TreeViewItem();
						newChildTr.Header = $"Sent {amount} to {recipient}";
						newChildTr.Foreground = Brushes.Firebrick;
						newChildTr.Items.Add(amount + " DUCO");
						newChildTr.Items.Add(datetime);
						newChildTr.Items.Add(hash);
						newChildTr.Items.Add(memo);
						TransactionsTreeView.Items.Add(newChildTr);
					} else
					{
						TreeViewItem newChildTr = new TreeViewItem();
						newChildTr.Header = $"Received {amount} from {tSender}";
						newChildTr.Foreground = Brushes.Green;
						newChildTr.Items.Add(amount + " DUCO");
						newChildTr.Items.Add(datetime);
						newChildTr.Items.Add(hash);
						newChildTr.Items.Add(memo);
						TransactionsTreeView.Items.Add(newChildTr);
					}
				}
			} else
				TransactionsTreeView.Items.Add("Your transactions will be here");
			
			// Log current balance
			FormAPI.LogBalance(UserBalance);
		}
        private void SendCoinsButton_OnClick(object sender, RoutedEventArgs e)
        {
	        string recipient = SendCoinsRecipientBox.Text;
	        decimal amount = decimal.Parse(SendCoinsAmountBox.Text, CultureInfo.InvariantCulture);
	        string memo = SendCoinsMemo.Text;
	        
	        MessageBoxResult isWannaSend = MessageBox.Show($"Send {amount} to {recipient}?", "DuCoWallet", 
		        MessageBoxButton.YesNo, MessageBoxImage.Information);
	        if (isWannaSend == MessageBoxResult.No)
	        {
		        return;
	        }
	        
	        string[] result = DucoApi.send_duco(Username, Password, recipient, amount, memo);
	        try
	        {
		        string status = result[0];
		        string msg = result[1];
		        string hash = result[2];

		        MessageBox.Show($"{status} - {msg} \nHash: {hash}");
		        FormAPI.OpenHashInBrowser(hash);
	        }
	        catch (Exception exc)
	        {
		        MessageBox.Show(result + exc.Message);
	        }

        }

        private void MenuItemServerStats_OnClick(object sender, RoutedEventArgs e)
        {
	        StatusWindow statusPage = new StatusWindow();
	        statusPage.Show();
        }

        private void MinerStartButton_OnClick(object sender, RoutedEventArgs e)
        {
	        string diff = MinerDiff.SelectedItem.ToString();
	        string identifier = MinerIdentifierTb.Text;

	        Miner miner = new Miner
	        {
		        username = Username,
		        diff = diff,
		        identifier = identifier
	        };

	        MinerStopButton.IsEnabled = true;
	        MinerStartButton.IsEnabled = false;
	        
	        // start in new thread
	        worker.DoWork += miner.MineMain;
	        worker.WorkerSupportsCancellation = true;
	        worker.RunWorkerAsync();
        }

        private void MenuItemChangeUsername_OnClick(object sender, RoutedEventArgs e)
        {
            FormAPI.OpenEditCreds();
        }

        private void MenuItemAbout_OnClick(object sender, RoutedEventArgs e)
        {
	        //string text = "DuCoWallet 1.1 wpf\ncreated by mendabex\nwww.vitka-k.ru";
	        //MessageBox.Show(text, "DuCoWallet");
	        AboutWindow aboutUs = new AboutWindow();
	        aboutUs.Show();
        }

        private void MenuItemChangePassword_OnClick(object sender, RoutedEventArgs e)
        {
	        ChangePasswordWindow changePasswordWindow = new ChangePasswordWindow(Username, Password);
	        changePasswordWindow.Show();
        }

        private void TransactionCopy(object sender, RoutedEventArgs e)
        {
	        TreeViewItem selectedItem = TransactionsTreeView.SelectedItem as TreeViewItem;
	        MessageBox.Show(selectedItem.ToString());
        }

        private void MinerStopButton_OnClick(object sender, RoutedEventArgs e)
        {
	        worker.CancelAsync();
	        MinerStartButton.IsEnabled = true;
	        MinerStopButton.IsEnabled = false;
        }

        private void MenuItemUpdateForm_OnClick(object sender, RoutedEventArgs e)
        {
	        UpdateForm(null, null);
        }

        private void MenuItemDrawBalance_OnClick(object sender, RoutedEventArgs e)
        {
	        BalanceGraph graphWindow = new BalanceGraph();
	        graphWindow.Show();
        }
    }
}