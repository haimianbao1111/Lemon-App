using Lemon_App.Properties;
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
            this.FontFamily = new FontFamily(Settings.Default.FontFamilly);
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
                Robot.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //IContentPage.Children.Clear();
                //IContentPage.Children.Add(new IMBOX());
                Mus.Fill = new SolidColorBrush(Color.FromArgb(0, 255,255, 255));
                ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255,255));
                Rbt.Fill = new SolidColorBrush(Color.FromArgb(200,157, 159, 167));
                Mus.StrokeThickness = 1;
                ALL.StrokeThickness = 1;
                Rbt.StrokeThickness = 0;
            }
            else if (ContentText == "小萌音乐")
            {
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
            }
            else if (ContentText == "其他")
            {
                Robot.Visibility = Visibility.Collapsed;
                Music.Visibility = Visibility.Collapsed;
                User.Visibility = Visibility.Collapsed;
                All.Visibility = Visibility.Visible;
                All.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 50, 0, 0), new Thickness(0), TimeSpan.FromSeconds(0.2)));
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
            IntPtr handle = new WindowInteropHelper(this).Handle;
            RegisterHotKey(handle, 124, (uint)4, (uint)System.Windows.Forms.Keys.Z);
            RegisterHotKey(handle, 125, (uint)4, (uint)System.Windows.Forms.Keys.S);
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
            if (System.IO.File.Exists(Settings.Default.UserImage))
            { tx.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
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
            Mus.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            ALL.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            Rbt.Fill = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
            Mus.StrokeThickness = 1;
            ALL.StrokeThickness = 1;
            Rbt.StrokeThickness = 1;
        }

        private void MIN_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
            { BorderThickness = new Thickness(0); this.WindowState = WindowState.Maximized; Page.Clip = new RectangleGeometry() { RadiusX = 0, RadiusY = 0, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } }; }
            else { BorderThickness = new Thickness(10); this.WindowState = WindowState.Normal; Page.Clip = new RectangleGeometry() { RadiusX = 3, RadiusY = 3, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } }; }
        }

        private void MAX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void ZX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            new WelcomeWindow().Show();
        }
        #region 最大化显示任务栏

        void win_SourceInitialized(object sender, EventArgs e)
        {
            IntPtr handle = (new WindowInteropHelper(this)).Handle;
            HwndSource.FromHwnd(handle).AddHook(new HwndSourceHook(WindowProc));
        }

        private  System.IntPtr WindowProc(
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

        private void WmGetMinMaxInfo(System.IntPtr hwnd, System.IntPtr lParam)
        {

            MINMAXINFO mmi = (MINMAXINFO)Marshal.PtrToStructure(lParam, typeof(MINMAXINFO));
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
                mmi.ptMinTrackSize.x = Math.Abs((int)MinWidth);
                mmi.ptMinTrackSize.y = Math.Abs((int)MinHeight);
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
        private void Font_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FontFamily = (Font.SelectedItem as ListBoxItem).FontFamily;
            Settings.Default.FontFamilly = (Font.SelectedItem as ListBoxItem).FontFamily.Source;
        }

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, uint controlKey, uint virtualKey);

        [System.Runtime.InteropServices.DllImport("user32")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        public bool InstallHotKeyHook(Window window)
        {
            //判断组件是否有效
            if ( null == window )
            {
                //如果无效，则直接返回
                return false;
            }

            //获得窗体的句柄
            System.Windows.Interop.WindowInteropHelper helper = new System.Windows.Interop.WindowInteropHelper(window);

            //判断窗体句柄是否有效
            if (IntPtr.Zero == helper.Handle )
            {
                //如果句柄无效，则直接返回
                return false;
            }

            //获得消息源
            System.Windows.Interop.HwndSource source = System.Windows.Interop.HwndSource.FromHwnd(helper.Handle);

            //判断消息源是否有效
            if ( null == source )
            {
                //如果消息源无效，则直接返回
                return false;
            }

            //挂接事件
            source.AddHook(this.HotKeyHook );
            return true;
        }
        private IntPtr HotKeyHook(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY )
            {
                if (wParam.ToInt32() == 124)
                {
                    new WelcomeWindow().Show();
                    this.WindowState = WindowState.Normal;
                    this.Activate();
                }
                else if (wParam.ToInt32() == 125)
                {
                    new SaerchWindow().Show();
                }
            }
            return IntPtr.Zero;
        }
        private const int WM_HOTKEY = 0x0312;
    }
}