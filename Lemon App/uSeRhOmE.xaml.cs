using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// uSeRhOmE.xaml 的交互逻辑
    /// </summary>
    public partial class uSeRhOmE : UserControl
    {
        public uSeRhOmE()
        {
            InitializeComponent();
        }
        public Brush TXIMAGE
        {
            get { return TX.Background; }
            set { TX.Background = value; }
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(Settings.Default.UserImage))
            { TX.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
            NM.Text = Settings.Default.RobotName;
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
    }
}
