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
    /// lemon.xaml 的交互逻辑
    /// </summary>
    public partial class lemon : Window
    {
        public lemon()
        {
            InitializeComponent();
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
        Boolean isopen = false;
        private void SSPath_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!isopen)
            {
                isopen = true;
                Width = 270;
                Height = 335;
                Page.Clip = new RectangleGeometry() { RadiusX = 3, RadiusY = 3, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            }else
            {
                isopen = false;
                Width = 855;
                Height = 610;
                Page.Clip = new RectangleGeometry() { RadiusX = 3, RadiusY = 3, Rect = new Rect() { Width = Page.ActualWidth, Height = Page.ActualHeight } };
            }
        }
    }
}
