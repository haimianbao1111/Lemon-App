using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
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
    /// UpdataWindow.xaml 的交互逻辑
    /// </summary>
    public partial class UpdataWindow : Window
    {
        public UpdataWindow()
        {
            InitializeComponent();
            this.FontFamily = new FontFamily(He.Settings.FontFamilly);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBlock1.Text != "下载更新中...")
            {
                WebClient dc = new WebClient();
                dc.DownloadFileAsync(new Uri("https://git.oschina.net/Twilight-Lemon/Updata/raw/master/Setup.exe"), AppDomain.CurrentDomain.BaseDirectory + @"Setup.exe");
             //   dc.Headers.Add(HttpRequestHeader.Cookie, "User=d2eb2545efba96cd3c9b6cf0cd07f135;");
                dc.Proxy = He.proxy;
                dc.DownloadFileCompleted += OK;
                dc.DownloadProgressChanged += DownloadFileCompleted;
                textBlock1.Text = "下载更新中...";
            }
        }

        private void DownloadFileCompleted(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void OK(object sender, AsyncCompletedEventArgs e)
        {
            if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Setup.exe"))
            { Process.Start(AppDomain.CurrentDomain.BaseDirectory + @"Setup.exe"); Environment.Exit(0); }
            else MessageBox.Show("下载失败");
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.K.Text = await Lemon_Updata.NewText(He.KMS);
            if(File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Setup.exe")) { File.Delete(AppDomain.CurrentDomain.BaseDirectory + @"Setup.exe"); }
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = Resources["c"] as Storyboard;
            c.Completed += delegate { Close(); };
            c.Begin();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
