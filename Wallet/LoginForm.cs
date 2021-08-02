using System;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;

namespace Wallet
{

	public partial class LoginForm : Form
	{
		string username;
		string password;

		public LoginForm()
		{
			InitializeComponent();
			InitializeTimer();
		}

		private void InitializeTimer()
		{
			// Call this procedure when the application starts.  
			// Set to 1 second.
			timerUpdater.Tick += new EventHandler(timerUpdateBalance);

			// Enable timer.  
			timerUpdater.Enabled = false;
		}

		private void timerUpdateBalance(object Sender, EventArgs e)
		{
			// Set the caption to the current time.

			buttonLogin_Click(null, null);
		}


		private void buttonLogin_Click(object sender, EventArgs e)
		{
			/* 
			 * Load info
			 */
			// TODO:
			// adaptive design
			

			username = textBoxUsername.Text;
			password = textBoxPassword.Text;

			if (username == "" | password == "")
			{
				MessageBox.Show("Empty login/password");
				return;
			}

			bool is_logged = API.auth_check(username, password);

			var account = API.get_account_info(username);

			if (account.ContainsKey("message"))
			{
				MessageBox.Show(account["message"].ToString());
				return;
			}
			JArray jminers = (JArray)account["result"]["miners"];
			JValue jbalance = (JValue)account["result"]["balance"]["balance"];
			JArray jtransactions = (JArray)account["result"]["transactions"];

			// Clear treeview and listview
			treeViewMiners.Nodes.Clear();
			treeViewTransactions.Nodes.Clear();

			// Fill balance label
			labelBalance.Text = jbalance + " DUCO";

			bool hasMiners = account["result"]["miners"].HasValues;
			if (hasMiners)
			{
				int i = 0;
				foreach (var x in jminers)
				{
					string identifier = x["identifier"].ToString();
					string software   = x["software"].ToString();
					string algorithm  = x["algorithm"].ToString();
					string diff       = x["diff"].ToString() + " diff";
					string hashrate   = x["hashrate"].ToString() + " H/s";
					string accepted   = x["accepted"].ToString() + " accepted";
					string rejected   = x["rejected"].ToString() + " rejected";

					treeViewMiners.Nodes.Add(software);
					treeViewMiners.Nodes[i].Nodes.Add(identifier);
					treeViewMiners.Nodes[i].Nodes.Add(algorithm);
					treeViewMiners.Nodes[i].Nodes.Add(hashrate);
					treeViewMiners.Nodes[i].Nodes.Add(diff);
					treeViewMiners.Nodes[i].Nodes.Add(accepted);
					treeViewMiners.Nodes[i].Nodes.Add(rejected);
					i++;
				}
				treeViewMiners.ExpandAll();
			}

			bool hasTransactions = account["result"]["transactions"].HasValues;
			if (hasTransactions)
			{
				int i = 0;
				foreach (var transaction in jtransactions)
				{
					string amount    = transaction["amount"].ToString();
					string datetime  = transaction["datetime"].ToString();
					string hash      = transaction["hash"].ToString();
					string memo      = transaction["memo"].ToString();
					string recipient = transaction["recipient"].ToString();
					string t_sender  = transaction["sender"].ToString();

					treeViewTransactions.Nodes.Add($"{t_sender} → {amount} → {recipient}");
					treeViewTransactions.Nodes[i].Nodes.Add(amount);
					treeViewTransactions.Nodes[i].Nodes.Add(datetime);
					treeViewTransactions.Nodes[i].Nodes.Add(hash);
					treeViewTransactions.Nodes[i].Nodes.Add(memo);
					i++;
				}
			}

			// Check login and password
			if (is_logged)
			{
				labelIsLogged.Text = $"Logged in as {username}";

				// TODO: Get info that require authentication 

				// Show panel with sending DUCO
				flowLayoutPanel3.Enabled = true;
			}
			else
			{
				labelIsLogged.Text = "Incorrect password. Only information that does not require authentication will be loaded";

				// Hide panel with sending DUCO
				flowLayoutPanel3.Enabled = false;
			}

			// Run auto-update timer
			timerUpdater.Enabled = true;
		}

		private void buttonSendDuco_Click(object sender, EventArgs e)
		{
			string recipient = textBoxRecepientDuco.Text;
			decimal d_amount = numericUpDownDuco.Value;

			MessageBox.Show($"Send {d_amount} to {recipient}");

			string[] result = API.send_duco(username, password, recipient, d_amount);

			string msg = result[1];
			string hash = result[2];

			MessageBox.Show($"{msg} Hash: {hash}");

			System.Diagnostics.Process.Start($"https://explorer.duinocoin.com/?search={hash}");
		}
	}
}
