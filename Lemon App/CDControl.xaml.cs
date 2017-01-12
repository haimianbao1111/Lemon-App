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
    /// CDControl.xaml 的交互逻辑
    /// </summary>
    public partial class CDControl : UserControl
    {
        public CDControl()
        {
            InitializeComponent();
        }
        public string ContentText
        {
            get { return Content.Text; }
            set { Content.Text = value; }
        }
        public Geometry ICONGEOMETRY
        {
            get { return ICON.Data; }
            set { ICON.Data = value; }
        }
        public  Brush ICONBackground
        {
            get { return ICON.Fill; }
            set { ICON.Fill = value; }
        }

        private void Grid_GotFocus(object sender, RoutedEventArgs e)
        {
            Back.Background = new SolidColorBrush(Color.FromArgb(5, 0, 0, 0));
        }

        private void Back_LostFocus(object sender, RoutedEventArgs e)
        {
            Back.Background = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
        }
    }
}
