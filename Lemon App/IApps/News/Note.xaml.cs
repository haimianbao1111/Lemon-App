using Newtonsoft.Json.Linq;
using System;
using System.Text;
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
        string ass = "";
        private async Task LoadapisAsync(string info= "news_hot")
        {
            try { 
            jz.Text = "加载中";
            jz.Visibility = Visibility.Visible;
                var s = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            ass = await Uuuhh.GetNewsDataAsync($"https://www.toutiao.com/api/pc/feed/?category={info}&utm_source=toutiao&widen=1&tadrequire=true&max_behot_time={s}&max_behot_time_tmp={s}&as=A1156950D5231C8&cp=59051331BC08CE1", Encoding.UTF8);
            JObject o = JObject.Parse(ass);
            int i = 0;
            while (i++ != 5)
            {
                if (o["data"][i].ToString().Contains("广告")==true)
                    continue;
                string title = o["data"][i]["title"].ToString();
                string time = He.StampToDateTime(o["data"][i]["behot_time"].ToString()).ToString();
                string sousce = o["data"][i]["source"].ToString();
                string url = "http://www.toutiao.com" + o["data"][i]["source_url"].ToString();
                string text = "";
                 if (o["data"][i].ToString().Contains("abstract") == true)
                    text = o["data"][i]["abstract"].ToString();
                WP.Children.Add(new NewsList(title, time, sousce, url,text) { Width = this.ActualWidth });

            }
            jz.Visibility = Visibility.Collapsed;
            O.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 86, 0, 0), new Thickness(0, 36, 0, 0), TimeSpan.FromSeconds(0.2)));
              }
            catch(Exception ex)
            { jz.Text = "加载失败"; Console.WriteLine(ex.Message); Console.WriteLine(ass); }
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
        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        { foreach (var o in WP.Children) { (o as UserControl).Width = this.ActualWidth; } }

        private async void O_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (IsVerticalScrollBarAtButtom(O))
            {  await LoadapisAsync(dindex); }
        }
        string dindex = "";
        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WP.Children.Clear();
            dindex = (sender as Border).ToolTip.ToString();
            await LoadapisAsync(dindex);
        }
    }
}
