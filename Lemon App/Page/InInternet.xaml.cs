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
    /// InInternet.xaml 的交互逻辑
    /// </summary>
    public partial class InInternet : Window
    {
        public InInternet()
        {
            InitializeComponent();
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                if (Settings.Default.IsFistRun)
                    if (!Settings.Default.RNBM)
                        new LoadWindow().Show();
                    else new lemon().Show();
                else new FistRun().Show();
                var c = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
                c.Completed += delegate { Close(); };
                this.BeginAnimation(OpacityProperty, c);
            }
            else if (e.ClickCount == 1)
            {
                if (Uuuhh.Lalala("www.mi.com"))
                {
                    if (Settings.Default.IsFistRun)
                        if (!Settings.Default.RNBM)
                            new LoadWindow().Show();
                        else new lemon().Show();
                    else new FistRun().Show();
                    var c = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
                    c.Completed += delegate { Close(); };
                    this.BeginAnimation(OpacityProperty, c);
                }
            }
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            c.Completed += delegate { Environment.Exit(0); };
            this.BeginAnimation(OpacityProperty, c);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var c = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(OpacityProperty, c);
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
