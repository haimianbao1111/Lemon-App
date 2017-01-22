using Lemon_App.Properties;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// Note.xaml 的交互逻辑
    /// </summary>
    public partial class Note : UserControl
    {
        public Note()
        {
            InitializeComponent();
        }
        private async Task LoadapisAsync(string ha="最新")
        {
            jz.Visibility = Visibility.Visible;
            WP.Children.Clear();
            JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"https://route.showapi.com/109-35?&page=1&showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&maxResult=100&channelName={ha}"));
            int i = 0;
            while (i != 100)
            {
                WP.Children.Add(new NewsList(o["showapi_res_body"]["pagebean"]["contentlist"][i]["title"].ToString(), o["showapi_res_body"]["pagebean"]["contentlist"][i]["pubDate"].ToString(), o["showapi_res_body"]["pagebean"]["contentlist"][i]["source"].ToString(), "", o["showapi_res_body"]["pagebean"]["contentlist"][i]["link"].ToString()));
                i++;
            }
            jz.Visibility = Visibility.Collapsed;
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadapisAsync();
        }

        private async void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) if (textBox.Text != "") await LoadapisAsync(textBox.Text);
        }
    }
}
