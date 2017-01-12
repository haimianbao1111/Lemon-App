using Lemon_App.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// HomeControl.xaml 的交互逻辑
    /// </summary>
    public partial class HomeControl : UserControl
    {
        public HomeControl()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            LC.Text ="柠萌账号:" +Settings.Default.LemonAreeunIts;
            textBox.Text = Settings.Default.Qianmin;
            op.IsChecked = Settings.Default.RNBM;
            Namec.Text = Settings.Default.RobotName;
            if(System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory+"UserImage.bmp"))
                Image.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory+"UserImage.bmp", UriKind.Absolute)));
        }

        private void Name_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (Namec.Text != "")
                {
                    Settings.Default.RobotName = Namec.Text;
                    Settings.Default.Save();
                }
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog ofd = new Microsoft.Win32.OpenFileDialog();
            if (ofd.ShowDialog() == true)
            {
                try
                {
                    Image.Background = new ImageBrush(new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute)));
                    BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(new BitmapImage(new Uri(ofd.FileName, UriKind.Absolute))));
                    FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory+"UserImage.bmp", FileMode.Create, FileAccess.ReadWrite);
                    encoder.Save(fileStream);
                    fileStream.Close();
                }
                catch { Toast.SetToastNotion("Lemon App:", "此文件不是有效图像文件！", "-----Lemon App Toast").Show(); }
            }
        }

        private void Image_SizeChanged(object sender, SizeChangedEventArgs e)
        {
           
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
        //    if (e.Key == Key.Enter)
         //       if (textBox.Text != "")
            //    { Settings.Default.Qianmin = textBox.Text; Settings.Default.Save(); }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            A.Visibility = Visibility.Collapsed;
            B.Visibility = Visibility.Visible;
        }

        private void TextBlock3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            B.Visibility = Visibility.Collapsed;
            A.Visibility = Visibility.Visible;
        }
        string ini;
        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                if(textBox1.Text!="")
                {
                    Random ra = new Random();
                    ini = ra.Next().ToString();
                    MailMessage m = new MailMessage()
                    {
                        From = new MailAddress("2728578956@qq.com")
                    };
                    m.To.Add(new MailAddress(textBox1.Text));
                    m.Subject = "Lemon App";
                    m.SubjectEncoding = Encoding.UTF8;
                    m.Body = He.EmailMessage.Replace("{ninini}", ini);
                    m.BodyEncoding = Encoding.UTF8;
                    m.IsBodyHtml = true;
                    SmtpClient s = new SmtpClient()
                    {
                        Host = "smtp.qq.com",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("2728578956@qq.com", "qtmiqibczofmddbi")
                    };
                    s.Send(m);
                }
        }

        private void TextBlock4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBox2.Text.Trim() == ini)
            { Toast.SetToastNotion("Lemon App:", "你成功登录Lemon App", "-----来自Lemon App Toast").Show(); Settings.Default.LemonAreeunIts = textBox1.Text; Settings.Default.Save();LC.Text = "柠萌账号:" + textBox1.Text; B.Visibility = Visibility.Collapsed;
                A.Visibility = Visibility.Visible;
            }
        }

        private void Op_Checked(object sender, RoutedEventArgs e)
        {
            Settings.Default.RNBM = (bool)op.IsChecked;
            Settings.Default.Save();
        }
    }
}
