#region L
using Lemon_App.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;

namespace Lemon_App
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        System.Windows.Forms.Timer Timer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer CatTimer = new System.Windows.Forms.Timer();
        //System.Windows.Forms.Timer Timerr = new System.Windows.Forms.Timer();
        private NotifyIcon notifyIcon;
        public MainWindow()//一只大猫咪，喵喵喵~~~
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
            System.Windows.Forms.MenuItem[] childen = new System.Windows.Forms.MenuItem[] { exit};
            notifyIcon.ContextMenu = new System.Windows.Forms.ContextMenu(childen);

            this.notifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler((o, e) =>
            {
                if (e.Button == MouseButtons.Left) this.Show(o, e);
            });
        }
        private void Show(object sender, EventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3)));
            gridhome.BeginAnimation(HeightProperty, new DoubleAnimation(0, 441, TimeSpan.FromSeconds(0.3)));
            this.ShowInTaskbar = true;
            this.Activate();
        }
        private void Close(object sender, EventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3)));
            gridhome.BeginAnimation(HeightProperty, new DoubleAnimation(441, 0, TimeSpan.FromSeconds(0.3)));
            Thread.Sleep(1000);
            Environment.Exit(0);
        }
        int GRIDI = 0;
        private void Border_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (GRIDI == 0)//跳呀跳，噗咪噗咪~
            {
                GRDIII.BeginAnimation(Grid.WidthProperty, new DoubleAnimation(43, 140, TimeSpan.FromSeconds(0.1)));
                GRDIII.Background = new SolidColorBrush(Color.FromRgb(45, 45, 48));
                GRIDI = 1;
            }
            else if (GRIDI == 1)
            {
                GRDIII.BeginAnimation(Grid.WidthProperty, new DoubleAnimation(140, 43, TimeSpan.FromSeconds(0.1)));
                GRDIII.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#0C000000"));
                GRIDI = 0;
            }
        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        
        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(1)));
            Environment.Exit(0);
        }
        private void Gwrinkde()
        {try
            {
                string o = He.request($"http://apis.baidu.com/acman/zhaiyanapi/tcrand/", "fangfa=json");
                var obj = JObject.Parse(o);
                Cat.Text = obj["taici"] + "\r\n——" + obj["source"];
            }
            catch { }
        }
        Dictionary<int, string> D = new Dictionary<int, string>();
        private int Get()
        {
            Random ra = new Random();
            return ra.Next(7);
            }
        private void StartVoid()
        {
            DoubleAnimationUsingKeyFrames s = new DoubleAnimationUsingKeyFrames();
            s.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            s.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            s.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1))));
            s.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(1.2))));
            KTEXT.BeginAnimation(OpacityProperty, s);
        }
        private void StartAd()
        {
            DoubleAnimationUsingKeyFrames s = new DoubleAnimationUsingKeyFrames();
            s.KeyFrames.Add(new LinearDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            s.KeyFrames.Add(new LinearDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.5))));
            KTEXT.BeginAnimation(OpacityProperty, s);
        }
        private async void Window_LoadedAsync(object sender, RoutedEventArgs e)
        {//
            new HaWindow(this).Show();
            D.Add(0, "(｡･∀･)ﾉﾞ嗨，{Name},你好啊");
            D.Add(1, "{Name}有何吩咐呢");
            D.Add(8, "{Name},你好呀");
            D.Add(7, "{Name},你来啦，我能为你做什么");
            D.Add(4, "(｡･∀･)ﾉﾞ嗨，{Name}在想什么呢");
            D.Add(5, "{Name}需要帮助吗");
            D.Add(6, "{Name}需要我帮你做些什么");
            D.Add(3, "{Name}，近来可好");
            D.Add(2, "别来无恙啊，{Name}");
            KTEXT.Text = D[Get()].Replace("{Name}", Settings.Default.RobotName);
            StartAd();
            //if (Uuuhh.Lalala("www.mi.com"))
            //{
            //    if (Settings.Default.RNBM == false)
            //    {
            //        Home.Visibility = Visibility.Collapsed;
            //        textBlock6.Text = "柠萌账号成长中";
            //        Load.Visibility = Visibility.Visible;
            //        Load.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
            //        Load.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
            //    }
            //    else
            //    {
            //        Load.Visibility = Visibility.Collapsed; Home.Visibility = Visibility.Visible; Load.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
            //        Load.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
            //    }
            //}
            //else { textBlock6.Text = "未链接到互联网"; Load.Visibility = Visibility.Collapsed; }

            Timer.Interval = 1000;
            Timer.Tick += Tick;
            //   Timer.Start();
            CatTimer.Interval = 3000;
            CatTimer.Tick += CatTick;
            CatTimer.Start();
            // Timerr.Interval = 5000;
            //Timerr.Tick += Tickk;
            //Timerr.Start();
            //   new HaWindow().Show();
                Window.FontFamily = new FontFamily(Settings.Default.Font);
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Theme.ltm"))
                {
                    JObject o = JObject.Parse(Read(AppDomain.CurrentDomain.BaseDirectory + "/Theme.ltm"));
                    byte[] a = (byte[])JsonToObject(o["ThemeImageUri"].ToString(), new byte[100000]);
                    WeatherItem.DataContext = Geometry.Parse(o["WeatherIcon"].ToString());
                    BingItem.DataContext = Geometry.Parse(o["BingIcon"].ToString());
                    FanyiItem.DataContext = Geometry.Parse(o["FanyiIcon"].ToString());
                    CodePath.DataContext = Geometry.Parse(o["QRIcon"].ToString());
                    CMDItem.DataContext = Geometry.Parse(o["CMDIcon"].ToString());
                    MusicItem.DataContext = Geometry.Parse(o["MusicIcon"].ToString());
                    UTFItem.DataContext = Geometry.Parse(o["UFIcon"].ToString());
                    SSItem.DataContext = Geometry.Parse(o["SSIcon"].ToString());
                    RBTItem.DataContext = Geometry.Parse(o["RBTIcon"].ToString());
                    NEWSItem.DataContext = Geometry.Parse(o["NEWSIcon"].ToString());
                    STItem.DataContext = Geometry.Parse(o["STIcon"].ToString());
                    byte[] ls = (byte[])JsonToObject(o["RobotImage"].ToString(), new byte[100000]);
                    He.RobotImage = ls.ToBitmapImage();
                    gridhome.Background = new ImageBrush(a.ToBitmapImage());
                }
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + "UserImage.bmp"))
            { HL.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "UserImage.bmp", UriKind.Absolute))); W.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + "UserImage.bmp", UriKind.Absolute))); }

            DoubleAnimation da = new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(1))
            {
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };
            textBlock6.BeginAnimation(OpacityProperty, da);
            textBlock5.Text = Settings.Default.RobotName;
            Gwrinkde();
            RNBM.IsChecked = Settings.Default.RNBM;
            if (Uuuhh.Lalala("www.mi.com"))
            {
                if (Settings.Default.RNBM == false)
                {
                    Home.Visibility = Visibility.Collapsed;
                    textBlock6.Text = "柠萌账号成长中";
                    Load.Visibility = Visibility.Visible;
                    Load.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
                    Load.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
                }
                else
                {
                    Load.Visibility = Visibility.Collapsed; Home.Visibility = Visibility.Visible; Load.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
                    Load.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
                }
            }
            else { textBlock6.Text = "未链接到互联网"; Load.Visibility = Visibility.Collapsed; }
            Thread T = new Thread(Start)
            {
                IsBackground = true
            };
            T.Start();
            if (await Lemon_Updata.IsLemonNew(He.KMS))
                new UpdataWindow().Show();
        }

        private void CatTick(object sender, EventArgs e)
        {
            RotateTransform rtf = new RotateTransform();
            CatIcon.RenderTransform = rtf;
            rtf.CenterX = 0.5;
            rtf.CenterY = 0.5;
            DoubleAnimationUsingKeyFrames D = new DoubleAnimationUsingKeyFrames();
            D.KeyFrames.Add(new LinearDoubleKeyFrame(0,KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0))));
            D.KeyFrames.Add(new LinearDoubleKeyFrame(20, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.2))));
            D.KeyFrames.Add(new LinearDoubleKeyFrame(-20, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(0.4))));
  //          D.RepeatBehavior = RepeatBehavior.Forever;
            D.AutoReverse = true;
            rtf.BeginAnimation(RotateTransform.AngleProperty, D);
        }

        private void Tickk(object sender, EventArgs e)
        {
            DoubleAnimation da = new DoubleAnimation(16, 5, TimeSpan.FromSeconds(0.2))
            {
                AutoReverse = true
            };
            BayMax.BeginAnimation(HeightProperty, da);
        }

        private void Tick(object sender, EventArgs e)
        {
         //   if (!Uuuhh.Lalala("www.mi.com"))
            //{
               // textBlock6.Text = "未链接到互联网"; Load.Visibility = Visibility.Collapsed;
            //}else { textBlock6.Text = "柠萌账号成长中"; }
            GC.Collect();
        }
        private void Start()
        {
#region 判断系统是否已启动

            Process[] myProcesses = Process.GetProcessesByName("Lemon App");
            if (myProcesses.Length > 1)
            {
                Toast.SetToastNotion("Lemon提示:", "Lemon已在运行！", "-----来自Lemon Toast").Show();
                Environment.Exit(0);
            }
#endregion
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

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {SSItem.Visibility = Visibility.Visible;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = D[Get()].Replace("{Name}", Settings.Default.RobotName);
            StartAd();
            this.SearchBox.Visibility = Visibility.Visible;
            UserF.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            this.SearchBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            this.SearchBox.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5,1, TimeSpan.FromSeconds(0.2)));
            //346
        }
        //348
        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Visible;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "柠萌机器人";
            StartAd();
            weather.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Visible;
            Note.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            SCCC.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            SCCC.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void Grid_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Visible;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "今日新闻";
            StartAd();
            SCCC.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Visible;
            SettingsControl.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Hidden;
            Note.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            Note.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void Grid_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Visible;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "柠萌设置";
            StartAd();
            weather.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Visible;
            UserF.Visibility = Visibility.Hidden;
            SettingsControl.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            SettingsControl.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }
        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            MUSIC.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Hidden;
                Bing.Visibility = Visibility.Hidden;
                Fanyi.Visibility = Visibility.Hidden;
                Code.Visibility = Visibility.Hidden;
                this.SearchBox.Visibility = Visibility.Hidden;
            KTEXT.Text = D[Get()].Replace("{Name}", Settings.Default.RobotName);
            StartAd();
            SCCC.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
                SettingsControl.Visibility = Visibility.Hidden;
                UserF.Visibility = Visibility.Hidden;
                HomeControl.Visibility = Visibility.Visible;
                HomeControl.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            HomeControl.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }
        private void Border_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void Gridhome_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void Grid_MouseDown_4(object sender, MouseButtonEventArgs e) => Process.Start("http://www.lemonapp.tk/wordpress");

        private void Border_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            if (PFBox.Width == 0)
            { PFBox.BeginAnimation(WidthProperty, new DoubleAnimation(0, 439, TimeSpan.FromSeconds(0.2))); /*Time.Visibility = Visibility.Hidden;*/ }
            else if (PFBox.Width == 439)
            { PFBox.BeginAnimation(WidthProperty, new DoubleAnimation(439, 0, TimeSpan.FromSeconds(0.2))); /*Time.Visibility = Visibility.Visible; */}
        }

        private void PF_DoWn(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/2528275249.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }catch { }
        }

        private void PF_DoWnw(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/2365237585.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnww(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://www.lemonapp.tk/Bing/bing.php")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnc(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/1641664773.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnv(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/546510605.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnl(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/2180230417.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/1131221776.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnx(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/3356568124.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWnm(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/891011727.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWng(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/616202690.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWoi(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/2045109352.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWop(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/802203027.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void PF_DoWol(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://tiankonguse.com/lab/cloudLink/baidupan.php?url=/36938860/3002360055.jpg")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }

        private void TextBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Visible;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "以下是今日天气";
            StartAd();
            Bing.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Visible;
            weather.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            weather.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void Grid_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Visible;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "这是今天的必应美图";
            StartAd();
            UserF.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Visible;
            Bing.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            Bing.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void Grid_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Visible;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "翻译&词典";
            StartAd();
            cmdcc.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Hidden; 
            Fanyi.Visibility = Visibility.Visible;
            MUSIC.Visibility = Visibility.Hidden;
            Fanyi.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            Fanyi.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void Grid_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Visible;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "二维码和短网址生成工具";
            StartAd();
            weather.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden; MUSIC.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Visible;
            Code.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            Code.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void WLO(object sender, MouseButtonEventArgs e)
        {
            try
            {
                gridhome.Background = (sender as Border).Background;
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(new Uri("http://www.lemonapp.tk/lemonappuiimage/XK.bmp")));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "/MetroImageForPF.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
            }
            catch { }
        }
        public string Read(string path)
        {
          string i= File.ReadAllText(path);return i;
        }
        private  void PF_DoWopo(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog()
            {
                DefaultExt = ".ltm",
                Filter = "柠檬主题|*.ltm"
            };
            if (ofd.ShowDialog() == true)
            {
                JObject o = JObject.Parse(Read(ofd.FileName));
                byte[] a = (byte[])JsonToObject(o["ThemeImageUri"].ToString(), new byte[int.MaxValue]);
                WeatherItem.DataContext = Geometry.Parse(o["WeatherIcon"].ToString());
                BingItem.DataContext = Geometry.Parse(o["BingIcon"].ToString());
                FanyiItem.DataContext = Geometry.Parse(o["FanyiIcon"].ToString());
                CodePath.DataContext = Geometry.Parse(o["QRIcon"].ToString());
                CMDItem.DataContext = Geometry.Parse(o["CMDIcon"].ToString());
                MusicItem.DataContext = Geometry.Parse(o["MusicIcon"].ToString());
                UTFItem.DataContext = Geometry.Parse(o["UFIcon"].ToString());
                SSItem.DataContext = Geometry.Parse(o["SSIcon"].ToString());
                RBTItem.DataContext = Geometry.Parse(o["RBTIcon"].ToString());
                NEWSItem.DataContext = Geometry.Parse(o["NEWSIcon"].ToString());
                STItem.DataContext = Geometry.Parse(o["STIcon"].ToString());
                byte[] ls = (byte[])JsonToObject(o["RobotImage"].ToString(), new byte[int.MaxValue]);
                He.RobotImage = ls.ToBitmapImage();
                gridhome.Background = new ImageBrush(a.ToBitmapImage());
                Window.FontFamily = new FontFamily(o["ThemeFont"].ToString());
                Settings.Default.Font = o["ThemeFont"].ToString();
                Settings.Default.Save();
                File.Copy(ofd.FileName, AppDomain.CurrentDomain.BaseDirectory + "/Theme.ltm", true);
            }
        }
        public static object JsonToObject(string jsonString, object obj)
        {//
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }
        private void Cat_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Gwrinkde();
        }
        int Fo = 0;
        private void Cat_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (Fo == 0)
            { Cat.Foreground = new SolidColorBrush(Colors.Transparent); Fo = 1; }
            else if (Fo == 1)
            { Cat.Foreground = new SolidColorBrush(Colors.White); Fo = 0; }
        }

        private void TextBlock9_Copy3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Collapsed;
            UTFItem.Visibility = Visibility.Visible;
            KTEXT.Text = "把你的意见提交给我们，以便改进柠萌";
            StartAd();
            MUSIC.Visibility = Visibility.Hidden;
            weather.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Visible;
            cmdcc.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            UserF.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            UserF.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }
        string ini;
        private void TextBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Email.Text != "")
            {
                Random ra = new Random();
                ini = ra.Next().ToString();
                MailMessage m = new MailMessage()
                {
                    From = new MailAddress("lemon.app@qq.com", "Lemon团队")
                };
                m.To.Add(new MailAddress(Email.Text));
                m.Subject = "Lemon App";
                m.SubjectEncoding = Encoding.UTF8;
                m.Body = He.EmailMessage.Replace("{ninini}", ini);
                m.BodyEncoding = Encoding.UTF8;
                m.IsBodyHtml = true;
                SmtpClient s = new SmtpClient()
                {
                    Host = "smtp.qq.com",
                    Port = 587,
                    EnableSsl = true,
                    Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi")
                };
                s.Send(m);
            }
        }

        private void TextBlock3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (key.Text.Trim() == ini)
            {
                Toast.SetToastNotion("Lemon App:", "你成功登录Lemon App", "-----来自Lemon App Toast").Show(); Settings.Default.LemonAreeunIts = Email.Text; Settings.Default.Save(); Load.Visibility = Visibility.Collapsed;
                Home.Visibility = Visibility.Visible;
                Home.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
                Home.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
            }
        }

        private void TextBlock4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void RNBM_Checked(object sender, RoutedEventArgs e)
        {try
            {
                if (Email.Text != "")
                    if (key.Text != ini)
                    {
                        Settings.Default.RNBM = (bool)RNBM.IsChecked;
                        Settings.Default.Save();
                    }
                    else { RNBM.IsChecked = false; }
            }
            catch { }
        }
        //
        private void TextBlock8_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void QR_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(AppDomain.CurrentDomain.BaseDirectory+@"/Note.exe");
        }



        private void TextBlock8_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3)));
            gridhome.BeginAnimation(HeightProperty, new DoubleAnimation(441, 0, TimeSpan.FromSeconds(0.3)));
            this.IsTabStop = false;
        }

        private void TextBlock11_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Uuuhh.Lalala("www.mi.com"))
            {
                if (Settings.Default.RNBM == false)
                {
                    Home.Visibility = Visibility.Collapsed;
                    ErrorIn.Visibility = Visibility.Collapsed;
                    textBlock6.Text = "未链接到互联网";
                    Load.Visibility = Visibility.Visible;
                    Load.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
                    Load.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
                }
                else { ErrorIn.Visibility = Visibility.Collapsed; Load.Visibility = Visibility.Collapsed; Home.Visibility = Visibility.Visible; Load.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
                    textBlock6.Text = "柠萌账号成长中"; Load.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
                }
            }
            else { ErrorIn.Visibility = Visibility.Visible; Load.Visibility = Visibility.Collapsed; Home.Visibility = Visibility.Collapsed; }
        }

        private void TextBlock6_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBlock6.Text == "未链接到互联网") {
                Home.Visibility = Visibility.Collapsed; ErrorIn.Visibility = Visibility.Visible; Load.Visibility = Visibility.Collapsed; ErrorIn.BeginAnimation(WidthProperty, new DoubleAnimation(620, 630, TimeSpan.FromSeconds(0.2)));
                ErrorIn.BeginAnimation(OpacityProperty, new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(0.2)));
            } else Toast.SetToastNotion("Lemon App:", "嘤嘤嘤", "别点啦/(ㄒoㄒ)/~~").Show();
        }

        private void Music_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            SSItem.Visibility = Visibility.Collapsed;
            RBTItem.Visibility = Visibility.Collapsed;
            NEWSItem.Visibility = Visibility.Collapsed;
            STItem.Visibility = Visibility.Collapsed;
            WeatherItem.Visibility = Visibility.Collapsed;
            BingItem.Visibility = Visibility.Collapsed;
            FanyiItem.Visibility = Visibility.Collapsed;
            CodeItem.Visibility = Visibility.Collapsed;
            CMDItem.Visibility = Visibility.Collapsed;
            MusicItem.Visibility = Visibility.Visible;
            UTFItem.Visibility = Visibility.Collapsed;
            KTEXT.Text = "柠萌音乐";
            StartAd();
            weather.Visibility = Visibility.Hidden;
            Bing.Visibility = Visibility.Hidden;
            Fanyi.Visibility = Visibility.Hidden;
            HomeControl.Visibility = Visibility.Hidden;
            this.SearchBox.Visibility = Visibility.Hidden;
            SCCC.Visibility = Visibility.Hidden;
            Note.Visibility = Visibility.Hidden;
            UserF.Visibility = Visibility.Hidden;
            cmdcc.Visibility = Visibility.Hidden;
            SettingsControl.Visibility = Visibility.Hidden;
            Code.Visibility = Visibility.Hidden;
            MUSIC.Visibility = Visibility.Visible;
            MUSIC.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(400, 94, 0, 0), new Thickness(43, 94, 0, 0), TimeSpan.FromSeconds(0.2)));
            MUSIC.BeginAnimation(OpacityProperty, new DoubleAnimation(0.5, 1, TimeSpan.FromSeconds(0.2)));
        }

        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}
#endregion 