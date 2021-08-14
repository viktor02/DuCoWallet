using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using DataFormat = RestSharp.DataFormat;

namespace DCWallet
{
	public static class DucoApi
	{
		class UserLogin
		{
			public string result { get; set; }
			public bool success { get; set; }
		}

		public static string server_ip = "51.15.127.80";
		public static int server_port = 2813;

		public static bool auth_check(string username, string password)
		{
			var client = new RestClient("https://server.duinocoin.com/");

			/* Get login in result */
			string url = $"auth/?username={username}&password={password}";
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				return false;
			}

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
			if (response.StatusCode != System.Net.HttpStatusCode.OK)
			{
				JObject err = JObject.FromObject(new
				{
					err = response.StatusDescription
				});
				return err;
			}
			var json = response.Content;

			try
			{
				JObject account = JObject.Parse(json);
				return account;
			} catch (Exception e)
			{
				JObject err = JObject.FromObject(new
				{
					err = e.Message
				});
				return err;
			}
		}

		public static string[] send_duco(string username, string password, string recipient, decimal d_amount, string memo)
		{
			string amount = d_amount.ToString().Replace(',', '.');

			var client = new RestClient("https://server.duinocoin.com/");
			string url = $"transaction/?username={username}&password={password}&recipient={recipient}&amount={amount}&memo={memo}";

			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			if (response.StatusCode != HttpStatusCode.OK)
			{
				return new string[] { "ERR", $"Status code: {response.StatusCode}", "" };
			}

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
			catch (NullReferenceException)
			{
				string msg = send["message"].ToString();
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
		

		public static (string, int) GetPool()
		{
			var client = new RestClient("https://server.duinocoin.com/");
			var url = $"getPool";
			var request = new RestRequest(url, DataFormat.Json);
			var response = client.Get(request);
			var json = response.Content;

			JObject api = JObject.Parse(json);

			return ((string)api["ip"], (int)api["port"]);
		}
		
		public static decimal get_duco_price()
		{
			JObject api = get_common_api();
			decimal result = (decimal) api["Duco price"];
			return result;
		}
		
		public static void change_password(string username, string oldPassword, string newPassword)
		{
			IPAddress ipAddress = IPAddress.Parse(server_ip);
			IPEndPoint remoteEP = new IPEndPoint(ipAddress, server_port);

			Socket sender = new Socket(ipAddress.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);
			
			sender.Connect(remoteEP);
			byte[] bytes = new byte[128];
			
			// Receive server version
			int bytesRec = sender.Receive(bytes);
			
			// Send login request
			byte[] login_msg = Encoding.ASCII.GetBytes($"LOGI,{username},{oldPassword}\n");
			int login_bytesSent = sender.Send(login_msg);
			
			// Receive login answer
			bytesRec = sender.Receive(bytes);
			string ascii_answer = Encoding.ASCII.GetString(bytes, 0, bytesRec);
			
			// Send change password request
			byte[] msg = Encoding.ASCII.GetBytes($"CHGW,{oldPassword},{newPassword}\n");
			int bytesSent = sender.Send(msg);
			
			// Receive server answer
			int answerBytesRec = sender.Receive(bytes);
			string chpgwAnswer = Encoding.ASCII.GetString(bytes, 0, answerBytesRec);
			MessageBox.Show(chpgwAnswer);
		}
	}
}