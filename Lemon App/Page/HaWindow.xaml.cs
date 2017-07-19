using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
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
    /// HaWindow.xaml 的交互逻辑
    /// </summary>
    public partial class HaWindow : Window
    {
        private NetworkInterface[] nicArr;
        NetworkInterface nic;
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        public HaWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 初始化网卡
        /// </summary>
        private void InitNetworkInterface()
        {
            nicArr = NetworkInterface.GetAllNetworkInterfaces();
            for (int i = 0; i < nicArr.Length; i++)
            {
                if (nicArr[i].OperationalStatus== OperationalStatus.Up)
                { nic = nicArr[i]; return; }
            }
        }

        /// <summary>
        /// 初始化计时器
        /// </summary>
        private void InitializeTimer()
        {
            timer.Interval = 1000;
            timer.Tick += delegate {
                this.Dispatcher.Invoke(() =>
                {
                    UpdateNetworkInterface();
                });
            };
            timer.Start();
        }
        double bt = 0;
        double br = 0;
        /// <summary>
        /// 获取网络数据并更新到UI
        /// </summary>
        private void UpdateNetworkInterface()
        {
            IPv4InterfaceStatistics interfaceStats = nic.GetIPv4Statistics();
            int bytesSentSpeed = (int)(interfaceStats.BytesSent - bt) / 1024;
            int bytesReceivedSpeed = (int)(interfaceStats.BytesReceived - br) / 1024;
            bt = interfaceStats.BytesSent;
            br = interfaceStats.BytesReceived;
            up.Text = bytesSentSpeed.ToString() + " KB/s";
            down.Text = bytesReceivedSpeed.ToString() + " KB/s";
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Left > SystemParameters.WorkArea.Height - 200)
                (Resources["l"] as Storyboard).Begin();
            else (Resources["r"] as Storyboard).Begin();
            InitNetworkInterface();
            InitializeTimer();
            if (System.IO.File.Exists(Settings.Default.UserImage))
            {
                var image = new System.Drawing.Bitmap(Settings.Default.UserImage);
                TX.Background = new ImageBrush(image.ToImageSource());
            }
            RenderOptions.SetBitmapScalingMode(TX, BitmapScalingMode.Fant);
            Rect bounds = Properties.Settings.Default.Hatop;
            Top = bounds.Top;
            Left = bounds.Left;
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }
        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            if (Left > SystemParameters.WorkArea.Height - 200)
                (Resources["l"] as Storyboard).Begin();
            else (Resources["r"] as Storyboard).Begin();
            Settings.Default.Hatop = this.RestoreBounds;
            Settings.Default.Save();
        }
        int osx = 0;
        private void TX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (osx == 0)
            { XT.BeginAnimation(OpacityProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(0.2))); osx = 1; }
            else if (osx == 1)
            {
                osx = 0;
                XT.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(0.2)));
            }
        }
    }
}
