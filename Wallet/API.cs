using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;

namespace Wallet
{
	class API
	{
		class UserLogin
		{
			public string result { get; set; }
			public bool success { get; set; }
		}

		public static bool auth_check(string username, string password)
		{
			var client = new RestClient("https://server.duinocoin.com/");

			/* Get login in result */
			string url = $"auth/?username={username}&password={password}";
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			UserLogin login = System.Text.Json.JsonSerializer.Deserialize<UserLogin>(response.Content);

			if (login.success)
			{
				return true;
			} else
			{
				return false;
			}
		}

		public static JObject get_account_info(string username)
		{
			/* Load base info */

			var client = new RestClient("https://server.duinocoin.com/");

			var url = $"users/{username}";
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			var json = response.Content;

			JObject account = JObject.Parse(json);
			
			return account;
		}

		public static string[] send_duco(string username, string password, string recipient, decimal d_amount)
		{
			string amount = d_amount.ToString().Replace(',', '.');

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

				return new string[] { status, msg, hash };
			}
			catch (IndexOutOfRangeException)
			{
				string msg = send["result"].ToString();
				return new string[] { "", msg, "" };
			}
			catch (Exception exc)
			{
				return new string[] { "", exc.Message, "" };
			}
		}

		public static JObject get_common_api()
		{
			var client = new RestClient("https://server.duinocoin.com/");

			var url = $"api.json";
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			var json = response.Content;

			JObject api = JObject.Parse(json);

			return api;
		}
	}
}
