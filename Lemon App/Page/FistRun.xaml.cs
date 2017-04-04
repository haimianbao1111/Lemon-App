using Lemon_App.Properties;
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
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// FistRun.xaml 的交互逻辑
    /// </summary>
    public partial class FistRun : Window
    {
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public FistRun()
        {
            InitializeComponent();
            t.Interval = 2500;
            t.Tick += Tick;
        }

        private void Tick(object sender, EventArgs e)
        {
            Settings.Default.IsFistRun = true;
            Settings.Default.Save();
            new LoadWindow().Show();
            t.Stop();
            t.Dispose();
            Close();
        }

        private void TX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog o = new Microsoft.Win32.OpenFileDialog();
            if (o.ShowDialog() == true)
            {
                TX.Background = new ImageBrush(new BitmapImage(new Uri(o.FileName, UriKind.Absolute)));
                Settings.Default.UserImage = o.FileName;
                Settings.Default.Save();
            }
        }

        private void NM_TextChanged(object sender, TextChangedEventArgs e)
        {
            Settings.Default.RobotName = NM.Text;
            Settings.Default.Save();
        }

        private void UrlBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (UrlBox.Text != "")
            {
                Settings.Default.SearchUrl = UrlBox.Text;
                Settings.Default.Save();
            }
        }

        private void border_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
         //   OnMouseDown2_BeginStoryboard.Storyboard.Begin();
            t.Start();
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
