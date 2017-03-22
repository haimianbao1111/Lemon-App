using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// CaptureWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CaptureWindow : Window
    {
        private double x;
        private double y;
        private double width;
        private double height;

        private bool isMouseDown = false;

        public CaptureWindow()
        {
            InitializeComponent();
        }

        private void CaptureWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            isMouseDown = true;
            x = e.GetPosition(null).X;
            y = e.GetPosition(null).Y;
        }

        private void CaptureWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (isMouseDown)
            {
                var rect = new Border();
                double dx = e.GetPosition(null).X;
                double dy = e.GetPosition(null).Y;
                double rectWidth = Math.Abs(dx - x);
                double rectHeight = Math.Abs(dy - y);
                SolidColorBrush brush = new SolidColorBrush(System.Windows.Media.Color.FromArgb(255,0,122,204));
                rect.Width = rectWidth;
                rect.Height = rectHeight;
                rect.BorderBrush = brush;
                rect.BorderThickness = new Thickness(2);
                if (dx < x)
                {
                    Canvas.SetLeft(rect, dx);
                    Canvas.SetTop(rect, dy);
                }
                else
                {
                    Canvas.SetLeft(rect, x);
                    Canvas.SetTop(rect, y);
                }

                CaptureCanvas.Children.Clear();
                CaptureCanvas.Children.Add(rect);

                if (e.LeftButton == MouseButtonState.Released)
                {
                    CaptureCanvas.Children.Clear();
                    CaptureCanvas.Visibility = Visibility.Collapsed;
                    this.Background = null;
                    width = Math.Abs(e.GetPosition(null).X - x);
                    height = Math.Abs(e.GetPosition(null).Y - y);

                    if (e.GetPosition(null).X > x)
                    {
                        CaptureCanvas.Children.Clear();
                        CaptureCanvas.Visibility = Visibility.Collapsed;
                        this.Background = null;
                        S(x, y, width, height);
                    }
                    else
                    {
                        CaptureCanvas.Children.Clear();
                        CaptureCanvas.Visibility = Visibility.Collapsed;
                        this.Background = null;
                        S(e.GetPosition(null).X, e.GetPosition(null).Y, width, height);
                    }


                    isMouseDown = false;
                    x = 0.0;
                    y = 0.0;
                    this.Close();
                }
            }
        }

        private void S(double x, double y, double width, double height)
        {
            CaptureCanvas.Children.Clear();
            CaptureCanvas.Visibility = Visibility.Collapsed;
            this.Background = null;

            int ix = Convert.ToInt32(x+2);
            int iy = Convert.ToInt32(y+2);
            int iw = Convert.ToInt32(width-4);
            int ih = Convert.ToInt32(height-4);

            System.Drawing.Bitmap bitmap = new Bitmap(iw, ih);
            using (System.Drawing.Graphics graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(ix, iy, 0, 0, new System.Drawing.Size(iw, ih));

                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "Png Files|*.png";
                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    bitmap.Save(dialog.FileName, ImageFormat.Png);
                }
                System.Windows.Forms.Clipboard.SetImage(bitmap);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.Height= SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.Left = 0;
            this.Top = 0;
        }
    }
}
