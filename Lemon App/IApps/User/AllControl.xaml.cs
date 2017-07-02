using Lemon_App.IApps;
using Lemon_App.IApps.Bing;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// AllControl.xaml 的交互逻辑
    /// </summary>
    public partial class AllControl : UserControl
    {
        public AllControl()
        {
            InitializeComponent();
            ContentPage.Children.Clear();
            ContentPage.Children.Add(new Note());
            ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
                var Sende = (sender as Border).ToolTip.ToString();
                if (Sende == "新闻")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new Note());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
                else if (Sende == "天气")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new WeatherBox());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
                else if (Sende == "美图")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new BingDayImage());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
                else if (Sende == "翻译")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new FanyiBox());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
                else if (Sende == "便签")
                {
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"/Note.exe");
                }
                else if (Sende == "搜索")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new SearchBox());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
                else if (Sende == "地图")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new MapControl());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        //    (ContentPage.Children[0] as UserControl).
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
