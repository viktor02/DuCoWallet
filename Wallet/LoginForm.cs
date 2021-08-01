using System;
using System.Windows.Forms;
using RestSharp;
using Newtonsoft.Json.Linq;

namespace Wallet
{

	public partial class LoginForm : Form
	{
		string username;
		string password;
		class UserLogin
		{
			public string result { get; set; }
			public bool success { get; set; }
		}


		public LoginForm()
		{
			InitializeComponent();
		}

		private void buttonLogin_Click(object sender, EventArgs e)
		{
			var client = new RestClient("https://server.duinocoin.com/");

			username = textBoxUsername.Text;
			password = textBoxPassword.Text;

			if (username == "" | password == "")
			{
				MessageBox.Show("Empty login/password");
				return;
			}

			/* Get log in result */
			string url = $"auth/?username={username}&password={password}";
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			UserLogin login = System.Text.Json.JsonSerializer.Deserialize<UserLogin>(response.Content);

			/* Load base info */

			url = $"users/{username}";
			request = new RestRequest(url, DataFormat.Json);
			response = client.Get(request);
			var json = response.Content;

			JObject account = JObject.Parse(json);


			// Fill balance label
			labelBalance.Text = account["result"]["balance"]["balance"].ToString() + " DUCO";

			// Clear treeview and listview
			treeViewMiners.Nodes.Clear();
			treeViewTransactions.Nodes.Clear();

			bool hasMiners = account["result"]["miners"].HasValues;
			if (hasMiners)
			{
				int i = 0;
				foreach (var x in account["result"]["miners"])
				{
					string identifier = x["identifier"].ToString();
					string software   = x["software"].ToString();
					string algorithm  = x["algorithm"].ToString();
					string diff       = x["diff"].ToString() + " diff";
					string hashrate   = x["hashrate"].ToString() + " H/s";
					string accepted   = x["accepted"].ToString() + " accepted";
					string rejected   = x["rejected"].ToString() + " rejected";

					treeViewMiners.Visible = true;
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
				var transactions = account["result"]["transactions"];
				int i = 0;
				foreach (var transaction in transactions)
				{
					string amount = transaction["amount"].ToString();
					string datetime = transaction["datetime"].ToString();
					string hash = transaction["hash"].ToString();
					string memo = transaction["memo"].ToString();
					string recipient = transaction["recipient"].ToString();
					string t_sender = transaction["sender"].ToString();

					treeViewTransactions.Visible = true;
					treeViewTransactions.Nodes.Add($"{t_sender} => {recipient}");
					treeViewTransactions.Nodes[i].Nodes.Add(amount);
					treeViewTransactions.Nodes[i].Nodes.Add(datetime);
					treeViewTransactions.Nodes[i].Nodes.Add(hash);
					treeViewTransactions.Nodes[i].Nodes.Add(memo);
					i++;
				}
			}

			/* Check login and pass
			 * 
			 *
			 */


			if (login.success)
			{
				labelIsLogged.Text = $"Logged in as {username}";

				// TODO: Get info that require authentication 

				// Show panel with sending DUCO
				flowLayoutPanel3.Visible = true;
			}
			else
			{
				labelIsLogged.Text = "Incorrect password. Only information that does not require authentication will be loaded";

				// Hide panel with sending DUCO
				flowLayoutPanel3.Visible = false;
			}
		}

		private void buttonSendDuco_Click(object sender, EventArgs e)
		{
			string recipient = textBoxRecepientDuco.Text;
			decimal d_amount = numericUpDownDuco.Value;
			string amount = d_amount.ToString().Replace(',', '.');

			MessageBox.Show($"Send {amount} to {recipient}");

			var client = new RestClient("https://server.duinocoin.com/");
			string url = $"transaction/?username={username}&password={password}&recipient={recipient}&amount={amount}";
			
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			var json = response.Content;

			JObject send = JObject.Parse(json);

			try
			{
				string[] subs = send["result"].ToString().Split(',');
				string status = subs[0];
				string msg = subs[1];
				string hash = subs[2];

				MessageBox.Show($"{msg} Hash: {hash}");

				System.Diagnostics.Process.Start($"https://explorer.duinocoin.com/?search={hash}");
			} 
			catch (IndexOutOfRangeException)
			{
				string msg = send["result"].ToString();
				MessageBox.Show(msg);
			}
			catch (Exception exc)
			{
				MessageBox.Show(exc.Message);
			}
		}
	}
}
