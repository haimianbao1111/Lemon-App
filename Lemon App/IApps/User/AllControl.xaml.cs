using Lemon_App.IApps;
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
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
                var Sende = (sender as Border).ToolTip.ToString();
            info.Data = ((sender as Border).Child as Path).Data;
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
                else if (Sende == "设置")
                {
                    ContentPage.Children.Clear();
                    ContentPage.Children.Add(new SettingsControl());
                    ContentPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 40, 50, 0), new Thickness(0, 0, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
            Conten.BeginAnimation(WidthProperty, new DoubleAnimation(240, 0, TimeSpan.FromSeconds(0.2)));
            iso = false;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
        //    (ContentPage.Children[0] as UserControl).
        }

        private void UserControl_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        bool iso = false;
        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (!iso)
            {
                Conten.BeginAnimation(WidthProperty, new DoubleAnimation(0, 240, TimeSpan.FromSeconds(0.2)));
                iso = true;
            }
            else
            {
                Conten.BeginAnimation(WidthProperty, new DoubleAnimation(240, 0, TimeSpan.FromSeconds(0.2)));
                iso = false;
            }
        }
    }
}
