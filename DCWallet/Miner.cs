using System;
using System.ComponentModel;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Windows;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;

namespace DCWallet
{
	public class Miner
	{
		string server_ip;
		//string server_ip = "51.15.127.80";
		int server_port;
		//int server_port = 2811;
		public string username;
		public string identifier;
		public string diff;
		public string program_name = "DuCoWallet_Miner";

		public Miner()
		{
			try
			{
				(string ip, int port) = DucoApi.GetPool();
				server_ip = ip;
				server_port = port;
			}
			catch
			{
				// Fallback to pulse pool
				server_ip = "149.91.88.18";
				server_port = 6000;
			}
			
		}

		private int MineDUCOS1_slow(string last_hash, string expected_hash, int diff)
		{
			byte[] temp_hash;
			var sha1 = new SHA1CryptoServiceProvider();
			var sb = new StringBuilder();
			string compute_sha1(string input)
			{
				temp_hash = sha1.ComputeHash(Encoding.ASCII.GetBytes(input));
				foreach (byte b in temp_hash) sb.AppendFormat("{0:x2}", b);
				return sb.ToString();
			}

			string hash;
			for (int nonce=0; nonce < (diff*100)+1; nonce++)
			{
				hash = compute_sha1(last_hash + nonce.ToString());
				if (hash.ToString() == expected_hash)
				{
					return nonce;
				}	
			}
			return -1;
		}


		[DllImport("duinolib.dll", CallingConvention = CallingConvention.Cdecl)]
		private static extern int mine_duinos1(string last_hash, string expected_hash, int diff);

		byte[] bytes = new byte[128];
		public void MineMain(object t_sender, DoWorkEventArgs e)
		{
			// Connect to server
			// Get version
			// LOOP START
			// Send request JOB,{USERNAME},{DIFF}
			// Get job {last_block_hash, expected_hash, {DIFF}}
			// Mine
			// Send result 
			// LOOP END

			// For cancellation pending
			BackgroundWorker worker = t_sender as BackgroundWorker;
			
			IPAddress ipAddress = IPAddress.Parse(server_ip);
			IPEndPoint remoteEP = new IPEndPoint(ipAddress, server_port);

			Socket sender = new Socket(ipAddress.AddressFamily,
				SocketType.Stream, ProtocolType.Tcp);

			if (this.username is null)
			{
				MessageBox.Show("Username is empty");
			}
			
			sender.Connect(remoteEP);
			byte[] msg = Encoding.ASCII.GetBytes($"JOB,{this.username},{this.diff}\n");
			// Receive server version
			int bytesRec = sender.Receive(bytes);

			bool mine = true;
			while (mine)
			{
				try
				{
					if (worker.CancellationPending)
					{
						mine = false;
						MessageBox.Show("Miner is stopping");
					}
					// Send job request
					int bytesSent = sender.Send(msg);

					// Receive job
					bytesRec = sender.Receive(bytes);

					// Parse job
					string ascii_job = Encoding.ASCII.GetString(bytes, 0, bytesRec);

					if (ascii_job[0] == 'N')
					{
						MessageBox.Show(ascii_job);
					}

					string[] lasthash_exphash_diff = ascii_job.Split(',');

					string last_hash = lasthash_exphash_diff[0];
					string expected_hash = lasthash_exphash_diff[1];
					int diff = int.Parse(lasthash_exphash_diff[2]);

					// Mine nonce
					Stopwatch sw = new Stopwatch();

					sw.Start();
					//int nonce = MineDUCOS1_slow(last_hash, expected_hash, diff);
					int nonce = mine_duinos1(last_hash, expected_hash, diff);
					sw.Stop();

					long hashrate = nonce / (sw.ElapsedMilliseconds * 1000);

					// Send nonce
					string result = $"{nonce},{hashrate},{program_name},{identifier}";
					bytesSent = sender.Send(Encoding.ASCII.GetBytes(result));

					// Receive answer
					bytesRec = sender.Receive(bytes);
					//MessageBox.Show(Encoding.ASCII.GetString(bytes, 0, bytesRec));
				}
				catch (Exception exc)
				{
					MessageBox.Show(exc.Message);
				}
			}
			// Close connection
			sender.Shutdown(SocketShutdown.Both);
			sender.Close();
			MessageBox.Show("Miner stopped");
		}
	}
}