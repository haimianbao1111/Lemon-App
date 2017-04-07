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
    /// DeskLyricWin.xaml 的交互逻辑
    /// </summary>
    public partial class DeskLyricWin : Window
    {
        public DeskLyricWin()
        {
            InitializeComponent();
            MouseDown += delegate(object sender, MouseButtonEventArgs e)
             {
                 if(e.ClickCount>=2)
                    Clipboard.SetText(textBlockDeskLyricFore.Text);
             };
        }
        /// <summary>
        /// 鼠标进入窗体显示背景矩形框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseEnter(object sender, MouseEventArgs e)
        {
            rectangleDeskLyricBack.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// 鼠标出了窗体隐藏背景矩形框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseLeave(object sender, MouseEventArgs e)
        {
            rectangleDeskLyricBack.Visibility = Visibility.Hidden;
        }

        /// <summary>
        /// 可以拖动窗口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (e.ButtonState == System.Windows.Input.MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            }
            catch { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
