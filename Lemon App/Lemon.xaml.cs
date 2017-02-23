﻿using Lemon_App.Properties;
using System;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using WinInterop = System.Windows.Interop;
using System.Windows.Shell;

namespace Lemon_App {
    /// <summary>
    /// lemon.xaml 的交互逻辑
    /// </summary>
    public partial class lemon : Window
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public lemon()
        {
            InitializeComponent();
            this.SourceInitialized += new EventHandler(win_SourceInitialized);
            t.Tick += T_Elapsed;
            t.Interval = 5000;
            t.Start();
        }

        private void T_Elapsed(object sender, EventArgs e)
        {
            GC.Collect();
            if (System.IO.File.Exists(Settings.Default.UserImage))
            { tx.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
        Boolean isopen = false;
        private void SSPath_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isopen)
            {
                TopPage.Height = 50;
                IContentPage.Margin = new Thickness(0, 50, 0, 0);
                Litt.Data = Geometry.Parse("M10.85,8.145L7.607,4.902C7.412,4.707 7.095,4.707 6.9,4.902 6.705,5.097 6.705,5.414 6.9,5.609L9.791,8.5 6.9,11.391C6.705,11.586 6.705,11.903 6.9,12.098 7.095,12.293 7.412,12.293 7.607,12.098L10.85,8.856C10.949,8.757 10.997,8.628 10.996,8.501 10.996,8.371 10.948,8.243 10.85,8.145z");
                isopen = true;
                BeginAnimation(WidthProperty, new DoubleAnimation(855, 310, TimeSpan.FromSeconds(0.1)));
                BeginAnimation(HeightProperty, new DoubleAnimation(610, 430, TimeSpan.FromSeconds(0.2)));
                Page.Clip = new RectangleGeometry() { RadiusX = 5, RadiusY = 5, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            }
            else
            {
                TopPage.Height = 55;
                IContentPage.Margin = new Thickness(0, 55, 0, 0);
                Litt.Data = Geometry.Parse("M7.209,8.5L10.1,5.609C10.295,5.414 10.295,5.097 10.1,4.902 9.905,4.707 9.588,4.707 9.393,4.902L6.15,8.145C6.052,8.243 6.004,8.371 6.004,8.5 6.004,8.629 6.052,8.757 6.15,8.855L9.393,12.097C9.588,12.292 9.905,12.292 10.1,12.097 10.295,11.902 10.295,11.585 10.1,11.39L7.209,8.5z");
                isopen = false;
                //  Width = 855;
                //Height = 610;
                BeginAnimation(WidthProperty, new DoubleAnimation(310, 855, TimeSpan.FromSeconds(0.1)));
                BeginAnimation(HeightProperty, new DoubleAnimation(430, 610, TimeSpan.FromSeconds(0.2)));
                Page.Clip = new RectangleGeometry() { RadiusX = 5, RadiusY = 5, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Page.Clip = new RectangleGeometry() { RadiusX = 5, RadiusY = 5, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            //    (IContentPage.Child as UserControl).Width = IContentPage.ActualWidth;
            // (IContentPage.Child as UserControl).Height = ActualHeight;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var ContentText = ((sender as Border).ToolTip as String);
            if (ContentText == "小萌机器人")
            {
                Robot.Visibility = Visibility.Visible;
                Music.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Collapsed;
                Robot.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 0, 50, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //IContentPage.Children.Clear();
                //IContentPage.Children.Add(new IMBOX());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(255, 31, 183, 245));
            }
            else if (ContentText == "小萌音乐")
            {
                Robot.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                Music.Visibility = Visibility.Visible;
                Music.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 0, 50, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //IContentPage.Children.Clear();
                //   IContentPage.Children.Add(new MusicControl());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(255, 31, 183, 245));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
            }
            else if (ContentText == "其他")
            {
                Robot.Visibility = Visibility.Collapsed;
                Music.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Visible;
                All.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-50, 0, 50, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //  IContentPage.Children.Clear();
                //IContentPage.Children.Add(new AllControl());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(255, 31, 183, 245));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
            }
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // FontFamily = new FontFamily(".PingFang SC");
            new HaWindow().Show();
            //         ZX.BeginAnimation(OpacityProperty, new DoubleAnimation(0.3, 1, TimeSpan.FromSeconds(1)) { AutoReverse = true });
            if (await Lemon_Updata.IsLemonNew(He.KMS))
                new UpdataWindow().Show();
            if (!Uuuhh.Lalala("www.mi.com"))
                ZX.Background = MIN.Background;
            else ZX.Background = MAX.Background;
            if (System.IO.File.Exists(Settings.Default.UserImage))
            { tx.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
            //DateTime tmCur = DateTime.Now;
            //if (tmCur.Hour > 18 && tmCur.Hour < 24)
            //    Toast.SetToastNotion("晚上好:", "欢迎回来" + Settings.Default.RobotName, "------早睡早起身体好").Show();
            //else if (tmCur.Hour >= 11 && tmCur.Hour < 12)
            //    Toast.SetToastNotion("中午好:", "欢迎回来" + Settings.Default.RobotName, "------中午啦~吃饭饭了~~").Show();
            //else if (tmCur.Hour > 1 && tmCur.Hour < 5)
            //    Toast.SetToastNotion("凌晨好:", "欢迎回来" + Settings.Default.RobotName, "-----不乖哦，还没有睡觉~").Show();
            //else if (tmCur.Hour > 6 && tmCur.Hour < 11)
            //    Toast.SetToastNotion("早上好:", "欢迎回来" + Settings.Default.RobotName, "-----一日之计在于晨，早上是最宝贵的时间哦~").Show();
            //else if (tmCur.Hour > 13 && tmCur.Hour < 17)
            //    Toast.SetToastNotion("下午好:", "欢迎回来" + Settings.Default.RobotName, "------祝你今天好运！").Show();
            //LemonWeather w = new LemonWeather(Settings.Default.WeatherInfo);
            //Toast.SetToastNotion($"今日{w.WeatherName}天气", w.WeatherMessage, "-----来自柠檬天气Toast").Show();
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
            User.NM.Text = Settings.Default.RobotName;
            Mus.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
            ALL.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
            Rbt.Fill = new SolidColorBrush(Color.FromArgb(255, 180, 180, 193));
        }

        private void MIN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            { this.WindowState = WindowState.Maximized; Page.Clip = new RectangleGeometry() { RadiusX = 0, RadiusY = 0, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } }; }
            else { this.WindowState = WindowState.Normal; Page.Clip = new RectangleGeometry() { RadiusX = 3, RadiusY = 3, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } }; }
        }

        private void MAX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ZX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new UFWindow().ShowDialog();
        }
        #region 最大化显示任务栏

        void win_SourceInitialized(object sender, EventArgs e)
        {
            System.IntPtr handle = (new WinInterop.WindowInteropHelper(this)).Handle;
            WinInterop.HwndSource.FromHwnd(handle).AddHook(new WinInterop.HwndSourceHook(WindowProc));
        }

        private static System.IntPtr WindowProc(
              System.IntPtr hwnd,
              int msg,
              System.IntPtr wParam,
              System.IntPtr lParam,
              ref bool handled)
        {
            switch (msg)
            {
                case 0x0024:
                    WmGetMinMaxInfo(hwnd, lParam);
                    handled = true;
                    break;
            }

            return (System.IntPtr)0;
        }

        private static void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));

            // Adjust the maximized size and position to fit the work area of the correct monitor
            int MONITOR_DEFAULTTONEAREST = 0x00000002;
            System.IntPtr monitor = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST);

            if (monitor != System.IntPtr.Zero)
            {

                MONITORINFO monitorInfo = new MONITORINFO();
                GetMonitorInfo(monitor, monitorInfo);
                RECT rcWorkArea = monitorInfo.rcWork;
                RECT rcMonitorArea = monitorInfo.rcMonitor;
                mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left);
                mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top);
                mmi.ptMaxSize.x = Math.Abs(rcWorkArea.right - rcWorkArea.left);
                mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top);
            }

            Marshal.StructureToPtr(mmi, lParam, true);
        }


        /// <summary>
        /// POINT aka POINTAPI
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            /// <summary>
            /// x coordinate of point.
            /// </summary>
            public int x;
            /// <summary>
            /// y coordinate of point.
            /// </summary>
            public int y;

            /// <summary>
            /// Construct a point of coordinates (x,y).
            /// </summary>
            public POINT(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MINMAXINFO
        {
            public POINT ptReserved;
            public POINT ptMaxSize;
            public POINT ptMaxPosition;
            public POINT ptMinTrackSize;
            public POINT ptMaxTrackSize;
        };



        /// <summary>
        /// </summary>
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
        public class MONITORINFO
        {
            /// <summary>
            /// </summary>            
            public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));

            /// <summary>
            /// </summary>            
            public RECT rcMonitor = new RECT();

            /// <summary>
            /// </summary>            
            public RECT rcWork = new RECT();

            /// <summary>
            /// </summary>            
            public int dwFlags = 0;
        }


        /// <summary> Win32 </summary>
        [StructLayout(LayoutKind.Sequential, Pack = 0)]
        public struct RECT
        {
            /// <summary> Win32 </summary>
            public int left;
            /// <summary> Win32 </summary>
            public int top;
            /// <summary> Win32 </summary>
            public int right;
            /// <summary> Win32 </summary>
            public int bottom;

            /// <summary> Win32 </summary>
            public static readonly RECT Empty = new RECT();

            /// <summary> Win32 </summary>
            public int Width
            {
                get { return Math.Abs(right - left); }  // Abs needed for BIDI OS
            }
            /// <summary> Win32 </summary>
            public int Height
            {
                get { return bottom - top; }
            }

            /// <summary> Win32 </summary>
            public RECT(int left, int top, int right, int bottom)
            {
                this.left = left;
                this.top = top;
                this.right = right;
                this.bottom = bottom;
            }


            /// <summary> Win32 </summary>
            public RECT(RECT rcSrc)
            {
                this.left = rcSrc.left;
                this.top = rcSrc.top;
                this.right = rcSrc.right;
                this.bottom = rcSrc.bottom;
            }

            /// <summary> Win32 </summary>
            public bool IsEmpty
            {
                get
                {
                    // BUGBUG : On Bidi OS (hebrew arabic) left > right
                    return left >= right || top >= bottom;
                }
            }
            /// <summary> Return a user friendly representation of this struct </summary>
            public override string ToString()
            {
                if (this == RECT.Empty) { return "RECT {Empty}"; }
                return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }";
            }

            /// <summary> Determine if 2 RECT are equal (deep compare) </summary>
            public override bool Equals(object obj)
            {
                if (!(obj is Rect)) { return false; }
                return (this == (RECT)obj);
            }

            /// <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
            public override int GetHashCode()
            {
                return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode();
            }


            /// <summary> Determine if 2 RECT are equal (deep compare)</summary>
            public static bool operator ==(RECT rect1, RECT rect2)
            {
                return (rect1.left == rect2.left && rect1.top == rect2.top && rect1.right == rect2.right && rect1.bottom == rect2.bottom);
            }

            /// <summary> Determine if 2 RECT are different(deep compare)</summary>
            public static bool operator !=(RECT rect1, RECT rect2)
            {
                return !(rect1 == rect2);
            }


        }

        [DllImport("user32")]
        internal static extern bool GetMonitorInfo(IntPtr hMonitor, MONITORINFO lpmi);

        /// <summary>
        /// 
        /// </summary>
        [DllImport("User32")]
        internal static extern IntPtr MonitorFromWindow(IntPtr handle, int flags);

        #endregion
    }
}