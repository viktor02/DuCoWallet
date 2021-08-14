using System;
using System.Windows;
using System.IO;
using LiveCharts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;


namespace DCWallet
{
    public partial class BalanceGraph : Window
    {
        public BalanceGraph()
        {
            InitializeComponent();

            (double[] balances, string[] timestamps) = GetBalance();
            
            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Title = "Duco Balance",
                    Values = balances.AsChartValues()
                }
            };
 
            Labels = timestamps;
            YFormatter = value => value.ToString();

            DataContext = this;
        }
        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> YFormatter { get; set; }


        private (double[], string[]) GetBalance()
        {
            // read points
            var lines = File.ReadAllLines("balance.log");

            double[] balances = new double[lines.Length];
            string[] timestamps = new string[lines.Length];
            
            for (var i = 0; i < lines.Length; i += 1) {
                var line = lines[i];
                // Process line
                string[] arr = line.Split(";");
                
                string timestamp = arr[2];
                double balance = double.Parse(arr[1]);
                string datetime = arr[2];

                balances[i] = balance;
                timestamps[i] = timestamp;
            }

            return (balances, timestamps);
        }
    }
}