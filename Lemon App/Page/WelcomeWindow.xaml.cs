using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// WelcomeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class WelcomeWindow : Window
    {
        public WelcomeWindow()
        {
            InitializeComponent();
            this.FontFamily = new FontFamily(He.Settings.FontFamilly);
            this.Left = SystemParameters.WorkArea.Width - this.Width+10;
            this.EndTop = SystemParameters.WorkArea.Height - this.Height+10;
            this.Top = SystemParameters.WorkArea.Height;
        }
        public double EndTop { get; set; }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            while (this.Top > EndTop)
            {
                this.Top -= 5;
            }
            Welcome.Inlines.Clear();
            Weather.Inlines.Clear();
            DateTime tmCur = DateTime.Now;
            if (tmCur.Hour >= 18 && tmCur.Hour <= 24)
            {
                b.Background = new ImageBrush(new BitmapImage(new Uri("http://i9.download.fd.pchome.net/t_960x600/g1/M00/10/15/ooYBAFWoU_mIZ8UZAAL0iXuHXcgAACmNwIIMlcAAvSh377.jpg")));
                Welcome.Inlines.Add(new Run($"晚上好呀！"));
                Welcome.Inlines.Add(new LineBreak());
                Welcome.Inlines.Add(new Run("早睡早起身体好") { FontSize = 14 }); }
            else if (tmCur.Hour >= 11 && tmCur.Hour <= 12)
            {
                b.Background = new ImageBrush(new BitmapImage(new Uri("http://i9.download.fd.pchome.net/t_960x600/g1/M00/10/15/ooYBAFWoU_mIZ8UZAAL0iXuHXcgAACmNwIIMlcAAvSh377.jpg")));
                Welcome.Inlines.Add(new Run($"午安！"));
                Welcome.Inlines.Add(new LineBreak());
                Welcome.Inlines.Add(new Run("中午啦~吃饭饭惹~") { FontSize = 14
            });
        }
        else if (tmCur.Hour >= 1 && tmCur.Hour <= 5)
            {
                b.Background = new ImageBrush(new BitmapImage(new Uri("http://img.kutoo8.com//upload/thumb/012066/f36a5eff700ba80a626fe54eb1bce043_320x480.jpg")));
                Welcome.Inlines.Add(new Run($"凌晨好呀！"));
                Welcome.Inlines.Add(new LineBreak());
                Welcome.Inlines.Add(new Run("不乖哦~还没有睡觉惹~")
                {
                    FontSize = 14
                });
            }
        else if (tmCur.Hour >= 6 && tmCur.Hour <= 11)
            {
                b.Background = new ImageBrush(new BitmapImage(new Uri("http://img.kutoo8.com//upload/thumb/583419/e47164658e5c23cf2424bbb72ef56b65_320x480.jpg")));
                Welcome.Inlines.Add(new Run($"早呀！"));
                Welcome.Inlines.Add(new LineBreak());
                Welcome.Inlines.Add(new Run("一日之计在于晨，早上是最宝贵的时间哦~")
                {
                    FontSize = 14
                });
            }
        else if (tmCur.Hour >= 13 && tmCur.Hour <= 17)
            {
                b.Background = new ImageBrush(new BitmapImage(new Uri("http://img.pconline.com.cn/images/upload/upc/tx/wallpaper/1207/30/c1/12613028_1343631802292.jpg")));
                Welcome.Inlines.Add(new Run($"下午好呀！"));
                Welcome.Inlines.Add(new LineBreak());
                Welcome.Inlines.Add(new Run("有什么新鲜事吗？")
                {
                    FontSize = 14
                });
            }
        LemonWeather w = new LemonWeather(He.Settings.WeatherInfo);
            Weather.Inlines.Add(new Run($"今日{w.WeatherName}天气"));
            Weather.Inlines.Add(new LineBreak());
            Weather.Inlines.Add(new Run(w.WeatherMessage));
            DoubleAnimationUsingKeyFrames d = new DoubleAnimationUsingKeyFrames();
            d.KeyFrames.Add(new LinearDoubleKeyFrame(1,TimeSpan.FromSeconds(0.2)));
            d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(10)));
            d.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(10.2)));
            d.Completed += delegate { this.Close(); };
            BeginAnimation(OpacityProperty, d);
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DoubleAnimationUsingKeyFrames d = new DoubleAnimationUsingKeyFrames();
            d.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0.2)));
            d.Completed += delegate { this.Close(); };
            BeginAnimation(OpacityProperty, d);
        }
    }
}
