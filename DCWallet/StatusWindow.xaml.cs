using System.Windows;
using System.Windows.Controls;
using Newtonsoft.Json.Linq;

namespace DCWallet
{
    public partial class StatusWindow : Window
    {
        public StatusWindow()
        {
            InitializeComponent();
            
            JObject japi = DucoApi.get_common_api();

            foreach (var key in japi)
            {
                if (key.Value.HasValues)
                {
                    TreeViewItem newChild = new TreeViewItem();
                    newChild.Header = key.Key;
                    foreach (var child_key in key.Value)
                    {
                        newChild.Items.Add(child_key.ToString());
                    }
                    StatusPage.Items.Add(newChild);
                }
                else
                {
                    string status = $"{key.Key} : {key.Value}";
                    StatusPage.Items.Add(status);
                }

            }
        }
    }
}