using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Windows;

namespace DCWallet
{
    public class FormAPI
    {
        public static string[] LoadCredentials()
        {
            if (File.Exists("creds.dcw"))
            {
                string[] creds = File.ReadAllText("creds.dcw").Split(":");
                return creds;
            }
            return new string[] {};

        }
        public static void SaveCredentials(string username, string password)
        {
            if ((username != "") & (password != ""))
            {
                File.WriteAllTextAsync("creds.dcw", $"{username}:{password}");
            }
        }
        
        public static void OpenHashInBrowser(string hash)
        {
            using Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.Arguments = $"/C start https://explorer.duinocoin.com/?search={hash}";
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
        }
        
        public static void OpenEditCreds()
        {
            using Process proc = new Process();
            proc.StartInfo.FileName = "notepad.exe";
            proc.StartInfo.Arguments = $"creds.dcw";
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
            MessageBox.Show("After change restart the app");
        }

        public static void LogBalance(decimal userBalance)
        {
            var currentTime = DateTime.Now.ToString("HH:mm:ss");
            var timestamp = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds();
            using(StreamWriter sw = File.AppendText("balance.log"))
            {
                sw.WriteLine($"{timestamp};{userBalance};{currentTime}");
            }
        }
    }
}