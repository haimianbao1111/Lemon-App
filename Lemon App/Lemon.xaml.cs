using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// Lemon.xaml 的交互逻辑
    /// </summary>
    public partial class Lemon : Window
    {
        public Lemon()
        {
            InitializeComponent();
            this.notifyIcon = new NotifyIcon()
            {
                BalloonTipText = "Lemon App在这里哦"
            };
            this.notifyIcon.ShowBalloonTip(2000);
            this.notifyIcon.Text = "哈哈哈，这是Lemon App的图标哦，点我打开Lemon App";
            this.notifyIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Windows.Forms.Application.ExecutablePath);
            this.notifyIcon.Visible = true;
            //退出菜单项
            System.Windows.Forms.MenuItem exit = new System.Windows.Forms.MenuItem("关闭Lemon App");
            exit.Click += new EventHandler(Close);
            //关联托盘控件
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exit };
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler((o, e) =>
            {
                if (e.Button == MouseButtons.Left) this.Show(o, e);
            });
        }
        NotifyIcon notifyIcon;
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(1))
            {AutoReverse = true,RepeatBehavior = RepeatBehavior.Forever};
            ZX.BeginAnimation(OpacityProperty, da);
            new HaWindow(this).Show();
            USERNAME.Text = Settings.Default.RobotName;
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "UserImage.bmp"))
            { USERTX.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "UserImage.bmp", UriKind.Absolute))); }
            if (Uuuhh.Lalala("www.mi.com"))
                ZX.Background = MAX.Background;
            else
                ZX.Background = MIN.Background;
            if (await Lemon_Updata.IsLemonNew(He.KMS))
                new UpdataWindow().Show();
            DateTime tmCur = DateTime.Now;
            if (tmCur.Hour > 18 && tmCur.Hour < 24)
                Toast.SetToastNotion("晚上好:", "欢迎回来" + Settings.Default.RobotName, "------早睡早起身体好").Show();
            else if (tmCur.Hour >= 11 && tmCur.Hour < 12)
                Toast.SetToastNotion("中午好:", "欢迎回来" + Settings.Default.RobotName, "------中午啦~吃饭饭了~~").Show();
            else if (tmCur.Hour > 1 && tmCur.Hour < 5)
                Toast.SetToastNotion("凌晨好:", "欢迎回来" + Settings.Default.RobotName, "-----不乖哦，还没有睡觉~").Show();
            else if (tmCur.Hour > 6 && tmCur.Hour < 11)
                Toast.SetToastNotion("早上好:", "欢迎回来" + Settings.Default.RobotName, "-----一日之计在于晨，早上是最宝贵的时间哦~").Show();
            else if (tmCur.Hour > 13 && tmCur.Hour < 17)
                Toast.SetToastNotion("下午好:", "欢迎回来" + Settings.Default.RobotName, "------祝你今天好运！").Show();
            LemonWeather w = new LemonWeather(Settings.Default.WeatherInfo);
            Toast.SetToastNotion($"今日{w.WeatherName}天气", w.WeatherMessage, "-----来自柠檬天气Toast").Show();
        }

        private void BackgroundPage_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void Show(object sender, EventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3)));
            BackgroundPage.BeginAnimation(HeightProperty, new DoubleAnimation(0, 570, TimeSpan.FromSeconds(0.3)));
            ShowInTaskbar = true;
            this.Activate();
        }
        private void Close(object sender, EventArgs e)
        {          
            Environment.Exit(0);
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MAX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3)));
            BackgroundPage.BeginAnimation(HeightProperty, new DoubleAnimation(570,0, TimeSpan.FromSeconds(0.3)));
            ShowInTaskbar = false;
        }

        private void CDControl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ContentPage.Children.Add(new WeatherBox() { Width=ContentPage.Width,Height=ContentPage.Height});
        }
    }
}
