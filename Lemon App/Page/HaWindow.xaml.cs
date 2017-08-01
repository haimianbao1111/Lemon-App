using Microsoft.VisualBasic.Devices;
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
        private void UpdateNetworkInterface()
        {
            var myInfo = new Computer();
            var z = (double)myInfo.Info.TotalPhysicalMemory / 1024 / 1024;
            var k = z - (double)myInfo.Info.AvailablePhysicalMemory / 1024 / 1024;
            var l = k / z;
            pb.Value = 360 * l;
            if (l >= 0.65)
                Window_MouseDoubleClick(null, null);
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
            if (System.IO.File.Exists(He.Settings.UserImage))
            {
                var image = new System.Drawing.Bitmap(He.Settings.UserImage);
                TX.Background = new ImageBrush(image.ToImageSource());
            }
            RenderOptions.SetBitmapScalingMode(TX, BitmapScalingMode.Fant);
            Top = He.Settings.HaTop.y;
            Left = He.Settings.HaTop.x;
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
            var d = this.RestoreBounds;
            He.Settings.HaTop = new OnRect { x = d.X, y = d.Y, width = d.Width, height = d.Height };
            He.SaveSettings();
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
        [DllImport("psapi.dll")]
        static extern int EmptyWorkingSet(IntPtr hwProc);
        private async void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            osx = 9;
            var dt= new Computer().Info.AvailablePhysicalMemory;
            XT.BeginAnimation(OpacityProperty, new DoubleAnimation(0, TimeSpan.FromSeconds(0.2)));
            RotateTransform rtf = new RotateTransform();
            TX.RenderTransform = rtf;
            DoubleAnimation dbAscending = new DoubleAnimation(0, 3600, new Duration
            (TimeSpan.FromSeconds(3)));
            dbAscending.Completed += delegate { XT.BeginAnimation(OpacityProperty, new DoubleAnimation(1, TimeSpan.FromSeconds(0.2))); };
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
            await Task.Run(new Action(delegate {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Process[] processes = Process.GetProcesses();
                foreach (Process process in processes)
                {
                    if ((process.ProcessName == "System") && (process.ProcessName == "Idle"))
                        continue;
                    try { EmptyWorkingSet(process.Handle); } catch { }
                }
            }));
            var dtf = new Computer().Info.AvailablePhysicalMemory;
            var v = dtf - dt;
            await Task.Delay(3000);
            Tbi.Text = "已清理内存:"+HumanReadableFilesize(v);
            await Task.Delay(3000);
            Tbi.Text = "";
            osx = 0;
        }
        private String HumanReadableFilesize(double size)
        {
            String[] units = new String[] { "B", "KB", "MB", "GB", "TB", "PB" };
            double mod = 1024.0;
            int i = 0;
            while (size >= mod)
            {
                size /= mod;
                i++;
            }
            return Math.Round(size) + units[i];
        }
    }
}
