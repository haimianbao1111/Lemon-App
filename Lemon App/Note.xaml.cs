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
            JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"https://route.showapi.com/109-35?&page={page}&showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&maxResult=10&channelName={ha}"));
            int i = 0;
            while (i != 10)
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
        public bool IsVerticalScrollBarAtButtom(ScrollViewer o)
        {
            bool isAtButtom = false;

            // get the vertical scroll position
            double dVer = o.VerticalOffset;

            //get the vertical size of the scrollable content area
            double dViewport = o.ViewportHeight;

            //get the vertical size of the visible content area
            double dExtent = o.ExtentHeight;

            if (dVer != 0)
            {
                if (dVer + dViewport == dExtent)
                {
                    isAtButtom = true;
                }
                else
                {
                    isAtButtom = false;
                }
            }
            else
            {
                isAtButtom = false;
            }

            if (o.VerticalScrollBarVisibility == ScrollBarVisibility.Disabled
                || o.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden)
            {
                isAtButtom = true;
            }

            return isAtButtom;
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

        private async void O_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsVerticalScrollBarAtButtom(O))
            { Ipage++; await LoadapisAsync(textBox.Text, Ipage); }
        }
    }
}
