using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
using ZXing;

namespace Lemon_App
{
    /// <summary>
    /// QRCodeControl.xaml 的交互逻辑
    /// </summary>
    public partial class QRCodeControl : UserControl
    {
        public QRCodeControl()
        {
            InitializeComponent();
        }
        private void label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(textBox.Text!="")
            { image.Source= QRCode.GetQRCode(textBox.Text, 160, 160,c);
            }
        }

        private void label_Copy_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(image.Source!=null)
            {
                System.Windows.Forms.SaveFileDialog op = new System.Windows.Forms.SaveFileDialog();
                op.Filter = "BMP Files (*.BMP)|*.BMP";
                if (op.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    BitmapImage iimage = (BitmapImage)image.Source;
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(iimage));
                    FileStream fileStream = new FileStream(op.FileName, FileMode.Create, FileAccess.ReadWrite);
                    encoder.Save(fileStream);
                    fileStream.Close();
                }
            }
        }

        private void image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            ofd.DefaultExt = ".*";
            ofd.Filter = "二维码图像|*.*";
            if (ofd.ShowDialog() == true)
            {
                image.Stretch = Stretch.Uniform;
                image.Source = new BitmapImage(new Uri(ofd.FileName,UriKind.Absolute));
                BarcodeReader reader = new BarcodeReader();
                BitmapSource m = new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute));
                System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(m.PixelWidth, m.PixelHeight, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                System.Drawing.Imaging.BitmapData data = bmp.LockBits(new System.Drawing.Rectangle(System.Drawing.Point.Empty, bmp.Size), System.Drawing.Imaging.ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
                m.CopyPixels(Int32Rect.Empty, data.Scan0, data.Height * data.Stride, data.Stride);
                bmp.UnlockBits(data);
                Result result = reader.Decode(bmp);
                textBox.Text = "识别的文本:"+result.ToString();
            }
        }
        System.Drawing.Color c = System.Drawing.Color.Black;
        private void Colorks_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
            {
               c= ColorTranslator.FromHtml(Colorks.Text);
                if (textBox.Text != "")                
                    image.Source = QRCode.GetQRCode(textBox.Text, 160, 160, c);                
            }
        }
    }
}
