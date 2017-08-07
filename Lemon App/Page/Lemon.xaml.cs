using Lemon_App.Page;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using Un4seen.Bass;

namespace Lemon_App
{
    /// <summary>
    /// lemon.xaml 的交互逻辑
    /// </summary>
    public partial class lemon : Window
    {
        bool isc = false;
        System.Windows.Threading.DispatcherTimer t = new System.Windows.Threading.DispatcherTimer();
        public lemon()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(tx, BitmapScalingMode.Fant);
            FullScreenManager.RepairWpfWindowFullScreenBehavior(this);
            this.FontFamily = new FontFamily(He.Settings.FontFamilly);
            t.Tick += T_Elapsed;
            t.Interval = TimeSpan.FromSeconds(5);
            t.Start();
            var c = (Resources["l"] as Storyboard);
            c.Completed += delegate { isc = true; c.Stop(); };
            c.Begin();
        }
        /// <summary>
        /// 最大化
        /// </summary>
        private void MaxEX()
        {
            BorderThickness = new Thickness(0);
            c.ResizeBorderThickness = new Thickness(0);
            Page.Clip = new RectangleGeometry() { RadiusX = 0, RadiusY = 0, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            this.WindowState = WindowState.Maximized;
        }
        /// <summary>
        /// 还原
        /// </summary>
        private void EX()
        {
            BorderThickness = new Thickness(30);
            c.ResizeBorderThickness = new Thickness(30);
            Page.Clip = new RectangleGeometry() { RadiusX = 3, RadiusY = 3, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            this.WindowState = WindowState.Normal;
        }
        private void T_Elapsed(object sender, EventArgs e)
        {
            GC.Collect();
            if (System.IO.File.Exists(He.Settings.UserImage))
            {
                var image = new System.Drawing.Bitmap(He.Settings.UserImage);
                tx.Background = new ImageBrush(image.ToImageSource());
            }
        }

        public void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = Resources["c"] as Storyboard;
            c.Completed += delegate { Process.GetCurrentProcess().Kill(); };
            c.Begin();
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (isc == true)
            {
                if (this.WindowState == WindowState.Normal)
                {
                    Page.Width = Width - 60;
                    Page.Height = Height - 60;
                }else
                {
                    Page.Width = Width;
                    Page.Height = Height;
                }
                Page.Clip = new RectangleGeometry() { RadiusX = 5, RadiusY = 5, Rect = new Rect() { Width = Page.Width, Height = Page.Height } };
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ContentText = ((sender as Border).ToolTip as String);
            if (ContentText == "小萌机器人")
            {
                Robot.Visibility = Visibility.Visible;
                Music.Visibility = Visibility.Collapsed;
                _2048.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Collapsed;
                Robot.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, -50), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //IContentPage.Children.Clear();
                //IContentPage.Children.Add(new IMBOX());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(200, 157, 159, 167));
                Mus.StrokeThickness = 1;
                ALL.StrokeThickness = 1;
                Rbt.StrokeThickness = 0;
            }
            else if (ContentText == "小萌音乐")
            {
                Robot.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Collapsed;
                _2048.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                Music.Visibility = Visibility.Visible;
                Music.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, -50), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //IContentPage.Children.Clear();
                //   IContentPage.Children.Add(new MusicControl());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(200, 157, 159, 167));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                Mus.StrokeThickness = 0;
                ALL.StrokeThickness = 1;
                Rbt.StrokeThickness = 1;
            }
            else if (ContentText == "其他")
            {
                Robot.Visibility = Visibility.Collapsed;
                Music.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                _2048.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Visible;
                All.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, -50), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //  IContentPage.Children.Clear();
                //IContentPage.Children.Add(new AllControl());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(200, 157, 159, 167));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                Mus.StrokeThickness = 1;
                ALL.StrokeThickness = 0;
                Rbt.StrokeThickness = 1;
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BassNet.Registration("52pojie@qq.com", "2X211223140022");
            Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_CPSPEAKERS, new WindowInteropHelper(this).Handle);
            Rect rc = SystemParameters.WorkArea;
            this.MaxWidth = rc.Width;
            this.MaxHeight = rc.Height;
            IntPtr handle = new WindowInteropHelper(this).Handle;
            RegisterHotKey(handle, 124, (uint)4, (uint)System.Windows.Forms.Keys.Z);
            RegisterHotKey(handle, 125, (uint)4, (uint)System.Windows.Forms.Keys.S);
            RegisterHotKey(handle, 126, (uint)4, (uint)System.Windows.Forms.Keys.X);
            RegisterHotKey(handle, 127, (uint)4, (uint)System.Windows.Forms.Keys.A);
            RegisterHotKey(handle, 128, (uint)4, (uint)System.Windows.Forms.Keys.End);
            RegisterHotKey(handle, 129, (uint)4, (uint)System.Windows.Forms.Keys.Q);
            InstallHotKeyHook(this);
            // FontFamily = new FontFamily(".PingFang SC");
            Font.Items.Clear();
            foreach (FontFamily font in Fonts.SystemFontFamilies)
            {
                Font.Items.Add(new ListBoxItem() { Content = font.Source, FontFamily = font });
            }
            new WelcomeWindow().Show();
            new HaWindow().Show();
            //         ZX.BeginAnimation(OpacityProperty, new DoubleAnimation(0.3, 1, TimeSpan.FromSeconds(1)) { AutoReverse = true });
            if (await Lemon_Updata.IsLemonNew(He.KMS))
                new UpdataWindow().Show();
            if (!Uuuhh.Lalala("www.mi.com"))
                ZX.Background = MIN.Background;
            else ZX.Background = MAX.Background;
            if (System.IO.File.Exists(He.Settings.UserImage))
            {
                var image = new System.Drawing.Bitmap(He.Settings.UserImage);
                tx.Background = new ImageBrush(image.ToImageSource());
            }
        }

        private void tx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //if (Email.Text != "")
            //{
            //    Random ra = new Random();
            //    ini = ra.Next().ToString();
            //    MailMessage m = new MailMessage()
            //    {
            //        From = new MailAddress("lemon.app@qq.com", "Lemon团队")
            //    };
            //    m.To.Add(new MailAddress(Email.Text));
            //    m.Subject = "Lemon App";
            //    m.SubjectEncoding = Encoding.UTF8;
            //    m.Body = He.EmailMessage.Replace("{ninini}", ini);
            //    m.BodyEncoding = Encoding.UTF8;
            //    m.IsBodyHtml = true;
            //    SmtpClient s = new SmtpClient()
            //    {
            //        Host = "smtp.qq.com",
            //        Port = 587,
            //        EnableSsl = true,
            //        Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi")
            //    };
            //    s.Send(m);
            //}
            Robot.Visibility = Visibility.Collapsed;
            All.Visibility = Visibility.Collapsed;
            Music.Visibility = Visibility.Collapsed;
            User.Visibility = Visibility.Visible;
            User.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, -50), new Thickness(0), TimeSpan.FromSeconds(0.2)));
            User.NM.Text = He.Settings.RobotName;
            Mus.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            Rbt.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            Mus.StrokeThickness = 1;
            ALL.StrokeThickness = 1;
            Rbt.StrokeThickness = 1;
        }

        private void MIN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                EX();
            }
            else { MaxEX(); }
        }

        private void MAX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ZX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new WelcomeWindow().Show();
        }
        private void Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily = (Font.SelectedItem as ListBoxItem).FontFamily;
            He.Settings.FontFamilly = (Font.SelectedItem as ListBoxItem).FontFamily.Source;
            He.SaveSettings();
            popup.IsOpen = false;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public bool InstallHotKeyHook(Window window)
        {
            if (window == null)
                return false;
            System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(window);
            if (IntPtr.Zero == helper.Handle)
                return false;
            System.Windows.Interop.HwndSource source = System.Windows.Interop.HwndSource.FromHwnd(helper.Handle);
            if (source == null)
                return false;
            source.AddHook(this.HotKeyHook);
            return true;
        }
        private IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                if (wParam.ToInt32() == 124)
                {
                    new WelcomeWindow().Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate();
                }
                else if (wParam.ToInt32() == 125)
                    new SaerchWindow().Show();
                else if (wParam.ToInt32() == 126)
                    new CaptureWindow().Show();
                else if (wParam.ToInt32() == 127)
                    Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"/Note.exe");
                else if (wParam.ToInt32() == 128)
                    He.SaveControlImage(this, "data.bmp");
                else if (wParam.ToInt32() == 129)
                    new MG().Show();
            }
            return IntPtr.Zero;
        }
        private const int WM_HOTKEY = 0x0312;

        private void TopPage_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (_2048.Visibility == Visibility.Visible)
                {
                    if (He.isOpMod == false)
                    {
                        ConsoleManager.Show();
                        He.isOpMod = true;
                        Console.WriteLine("DEBUGMOD IS OPEN");
                        Console.Title = "DEBUGMOD";
                    }
                    else
                    {
                        He.isOpMod = false;
                        Console.WriteLine("DEBUGMOD IS CLOSE");
                        ConsoleManager.Hide();
                    }
                }
                else
                {
                    Robot.Visibility = Visibility.Collapsed;
                    Music.Visibility = Visibility.Collapsed;
                    User.Visibility = Visibility.Collapsed;
                    _2048.Visibility = Visibility.Visible;
                    All.Visibility = Visibility.Collapsed;
                    _2048.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                    //  IContentPage.Children.Clear();
                    //IContentPage.Children.Add(new AllControl());
                    Mus.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                    ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                    Rbt.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                    Mus.StrokeThickness = 1;
                    ALL.StrokeThickness = 1;
                    Rbt.StrokeThickness = 1;
                    _2048.a1.MouseDown += delegate
                    {
                        IContentPage.Children.Remove(_2048);
                        _2048 = null;
                        _2048 = new IApps._2048._2048Control()
                        {
                            Visibility = Visibility.Collapsed
                        };
                        IContentPage.Children.Add(_2048);
                        Robot.Visibility = Visibility.Collapsed;
                        All.Visibility = Visibility.Collapsed;
                        User.Visibility = Visibility.Collapsed;
                        Music.Visibility = Visibility.Visible;
                        Music.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                        //IContentPage.Children.Clear();
                        //   IContentPage.Children.Add(new MusicControl());
                        Mus.Fill = new SolidColorBrush(Color.FromArgb(200, 157, 159, 167));
                        ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                        Rbt.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
                        Mus.StrokeThickness = 0;
                        ALL.StrokeThickness = 1;
                        Rbt.StrokeThickness = 1;
                    };
                }
            }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            popup.IsOpen = !popup.IsOpen;
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ufp.IsOpen = !ufp.IsOpen;
        }
    }
}