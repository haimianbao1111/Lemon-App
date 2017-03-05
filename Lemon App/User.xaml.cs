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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// User.xaml 的交互逻辑
    /// </summary>
    public partial class User : UserControl
    {
        public User()
        {
            InitializeComponent();
        }
        public User(String Text)
        {
            InitializeComponent();
            Te.Text = Text;
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(Settings.Default.UserImage))
                bd.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute)));
            UName.Text = Settings.Default.RobotName;
        }
    }
}
