using Lemon_App.Properties;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;

namespace Lemon_App
{
    /// <summary>
    /// TimeControl.xaml 的交互逻辑
    /// </summary>
    public partial class TimeControl : UserControl
    {
        public TimeControl()
        {
            InitializeComponent();
        }
        System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Timer.Interval = 1000;
            Timer.Tick += Tick;
            Timer.Start();
            DoubleAnimation da = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(1));
            da.AutoReverse = true;
            da.RepeatBehavior = RepeatBehavior.Forever;
            L.BeginAnimation(OpacityProperty, da);
            Time_S.Text = DateTime.Now.Hour.ToString();
            Time_F.Text= DateTime.Now.Minute.ToString();
            moonday.Content = DateTime.Now.Month.ToString() + "." + DateTime.Now.Day + "          " + DateTime.Now.DayOfWeek.ToString();
            if (Uuuhh.Lalala("www.mi.com"))
            {
                LemonWeather LW = new LemonWeather(Settings.Default.WeatherInfo);
                weather.Content = LW.WeatherName + "      " + LW.WeatherMessage;
                Icon.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{LW.WeatherIcon}.png"));
            }
        }

        private void Tick(object sender, EventArgs e)
        {
            GC.Collect();
            Time_S.Text = DateTime.Now.Hour.ToString();
            Time_F.Text= DateTime.Now.Minute.ToString();
            moonday.Content = DateTime.Now.Month.ToString() + "." + DateTime.Now.Day + "          " + DateTime.Now.DayOfWeek.ToString();
        }

        private void weather_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Uuuhh.Lalala("www.mi.com"))
            {
                LemonWeather LW = new LemonWeather(Settings.Default.WeatherInfo);
                weather.Content = LW.WeatherName + "      " + LW.WeatherMessage;
                Icon.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{LW.WeatherIcon}.png"));
            }
        }
    }
}
