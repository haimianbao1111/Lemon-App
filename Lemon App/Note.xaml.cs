using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

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
        private async Task LoadapisAsync(string ha="最新",int page=1)
        {
            jz.Visibility = Visibility.Visible;
            WP.Children.Clear();
            JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"https://route.showapi.com/109-35?&page={page}&showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&maxResult=10&channelName={ha}"));
            int i = 0;
            while (i != 9)
            {
                WP.Children.Add(new NewsList(o["showapi_res_body"]["pagebean"]["contentlist"][i]["title"].ToString(), o["showapi_res_body"]["pagebean"]["contentlist"][i]["pubDate"].ToString(), o["showapi_res_body"]["pagebean"]["contentlist"][i]["source"].ToString(), "", o["showapi_res_body"]["pagebean"]["contentlist"][i]["link"].ToString()) { Width=this.ActualWidth});
                i++;
            }
            jz.Visibility = Visibility.Collapsed;
            O.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0,86,0,0),new Thickness(0,36,0,0),TimeSpan.FromSeconds(0.5)));
        }
        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadapisAsync();
        }

        private async void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) if (textBox.Text != "") await LoadapisAsync(textBox.Text);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {foreach (var o in WP.Children){(o as UserControl).Width = this.ActualWidth;}}
        int Ipage = 1;
        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Ipage > 1) { Ipage--; await LoadapisAsync(textBox.Text, Ipage); }
        }
    
        private async void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            Ipage++;  await LoadapisAsync(textBox.Text, Ipage);
        }
    }
}
