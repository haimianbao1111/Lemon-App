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
        public HaWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(Settings.Default.UserImage))
            { TX.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute)));}
            DoubleAnimation da = new DoubleAnimation(0.2, 1, TimeSpan.FromSeconds(1));
            da.AutoReverse = true;
            NM.Text = Settings.Default.RobotName;
            da.RepeatBehavior = RepeatBehavior.Forever;
            Js.BeginAnimation(OpacityProperty, da);
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
            Settings.Default.Hatop = this.RestoreBounds;
            Settings.Default.Save();
        }

        private void TX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (XT.Width == 85)
                XT.BeginAnimation(WidthProperty, new DoubleAnimation(85, 0, TimeSpan.FromSeconds(0.2)));
            else if (XT.Width == 0)
            {
                NM.Text = Settings.Default.RobotName;
                if (System.IO.File.Exists(Settings.Default.UserImage))
                { TX.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
                XT.BeginAnimation(WidthProperty, new DoubleAnimation(0, 85, TimeSpan.FromSeconds(0.2)));
            }
        }
    }
}
