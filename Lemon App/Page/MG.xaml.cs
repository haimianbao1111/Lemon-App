using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemon_App.Page
{
    /// <summary>
    /// MG.xaml 的交互逻辑
    /// </summary>
    public partial class MG : Window
    {
        public MG()
        {
            InitializeComponent();
            var c = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(OpacityProperty, c);
        }
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = Resources["c"] as Storyboard;
            c.Completed += delegate { Close(); };
            c.Begin();
        }
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (but.Text == "开始")
            {
                but.Text = "停止";
                pl.IsReadOnly = true;
                tx.IsReadOnly = true;
                int i = 1000;
                int ont = 0;
                TimeSpan t = new TimeSpan();
                int.TryParse(pl.Text, out i);
                if (lx.Text == "毫秒")
                    t = TimeSpan.FromMilliseconds(i);
                else if (lx.Text == "秒")
                    t = TimeSpan.FromSeconds(i);
                else if (lx.Text == "分")
                    t = TimeSpan.FromMinutes(i);
                else if (lx.Text == "小时")
                    t = TimeSpan.FromHours(i);
                if (t <= TimeSpan.FromMilliseconds(100))
                { t = TimeSpan.FromMilliseconds(100); pl.Text = "100"; tit.Text = "最低只能使用100毫秒"; }
                Action a = new Action(async delegate
                {
                    while (true)
                    {
                        if (but.Text == "开始")
                            break;
                        System.Windows.Clipboard.SetText(tx.Text);
                        if (on.IsChecked == true && tw.IsChecked == false)
                        {
                            keybd_event(Keys.ControlKey, 0, 0, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.V, 0, 0, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.ControlKey, 0, 2, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.Enter, 0, 0, 0);
                        }
                        else
                        {
                            keybd_event(Keys.ControlKey, 0, 0, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.V, 0, 0, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.ControlKey, 0, 2, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.ControlKey, 0, 0, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.Enter, 0, 0, 0);
                            await Task.Delay(10);
                            keybd_event(Keys.ControlKey, 0, 2, 0);
                        }
                        ont++;
                        tit.Text = "已刷屏次数: " + ont;
                        await Task.Delay(t);
                    }
                });
                a();
            }
            else
            {
                pl.IsReadOnly = false;
                tx.IsReadOnly = false;
                but.Text = "开始";
            }
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (lx.Text == "毫秒")
                lx.Text = "秒";
            else if (lx.Text == "秒")
                lx.Text = "分";
            else if (lx.Text == "分")
                lx.Text = "小时";
            else if (lx.Text == "小时")
                lx.Text = "毫秒";
        }

        private void textBox1_Pasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!isNumberic(text))
                { e.CancelCommand(); }
            }
            else { e.CancelCommand(); }
        }
        private void textBox1_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Space)
                e.Handled = true;
        }
        private void textBox1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!isNumberic(e.Text))
            {
                e.Handled = true;
            }
            else
                e.Handled = false;
        }
        public static bool isNumberic(string _string)
        {
            if (string.IsNullOrEmpty(_string))
                return false;
            foreach (char c in _string)
            {
                if (!char.IsDigit(c))

                    return false;
            }
            return true;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            keybd_event(Keys.ControlKey, 0, 2, 0);
        }
    }
}
