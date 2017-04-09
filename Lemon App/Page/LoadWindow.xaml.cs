using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    /// LoadWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadWindow : Window
    {
        System.Windows.Forms.Timer tr = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer trs = new System.Windows.Forms.Timer();
        public LoadWindow()
        {
            InitializeComponent();
            var c = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(OpacityProperty, c);
            this.FontFamily = new FontFamily(Settings.Default.FontFamilly);
            tr.Interval = 4000;
            tr.Tick += T;
            trs.Interval = 4000;
            trs.Tick += Trs;
        }

        private void Trs(object sender, EventArgs e)
        {
            OS.Visibility = Visibility.Visible;
            RM.Visibility = Visibility.Visible;
            rk.Text = "验证码错误";
            trs.Stop();
        }

        private void T(object sender, EventArgs e)
        {
                new lemon().Show();
                this.Close();
                tr.Stop();
                tr.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.RNBM&&Settings.Default.LemonAreeunIts!=string.Empty)
            {
                new lemon().Show();
                this.Close();
            }
            else
            {
                Email.Text = Settings.Default.LemonAreeunIts;
                if (System.IO.File.Exists(Settings.Default.UserImage))
                { TX.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
                RM.IsChecked = Settings.Default.RNBM;
            }
        }
        string ini = "";
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (Email.Text != "" && IsValidEmail(Email.Text))
                {
                    Random ra = new Random();
                    ini = ra.Next(1000, 9999).ToString();
                    MailMessage m = new MailMessage()
                    {
                        From = new MailAddress("lemon.app@qq.com", "Lemon团队")
                    };
                    m.To.Add(new MailAddress(Email.Text));
                    m.Subject = "Lemon App";
                    m.SubjectEncoding = Encoding.UTF8;
                    m.Body = @"<table dir=""ltr"">
    <tbody>
        <tr>
            <td id = ""i1"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:17px; color:#707070;"">
                Lemon App 帐户
            </td>
        </tr>
        <tr>
            <td id = ""i2"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:41px; color:#2672ec;"">
                验证码
            </td>
        </tr>
        <tr>
            <td id = ""i3"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                请为 Lemon App 帐户
                使用以下验证码登录。
            </td>
        </tr>
        <tr>
            <td id = ""i4"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                验证码：
                <b>
                    {ninini}
                </b>
            </td>
        </tr>
        <tr>
            <td id = ""i5"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                如果你无法识别此Lemon App 帐户，可以忽略此电子邮件
            </td>
        </tr>
        <tr>
            <td id = ""i6"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                谢谢!
            </td>
        </tr>
        <tr>
            <td id = ""i7"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                Lemon App 帐户团队
            </td>
        </tr>
    </tbody>
</table>".Replace("{ninini}", ini);
                    m.BodyEncoding = Encoding.UTF8;
                    m.IsBodyHtml = true;
                    SmtpClient s = new SmtpClient()
                    {
                        Host = "smtp.qq.com",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi")
                    };
                    s.Send(m);
                    ns.Text = "发送成功";
                    DoubleAnimationUsingKeyFrames d = new DoubleAnimationUsingKeyFrames();
                    d.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                    d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(0.3)));
                    d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(3)));
                    d.AutoReverse = true;
                    po.BeginAnimation(OpacityProperty, d);
                }
            }catch(Exception es)
            {
                ns.Text = "发送失败" + es.Message;
                DoubleAnimationUsingKeyFrames d = new DoubleAnimationUsingKeyFrames();
                d.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(0.3)));
                d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(3)));
                d.AutoReverse = true;
                po.BeginAnimation(OpacityProperty, d);
            }
        }

        private void PSW_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PSW.Password.Length == 4)
                if (PSW.Password == ini)
                {
                    Settings.Default.RNBM = (Boolean)RM.IsChecked;
                    Settings.Default.LemonAreeunIts = Email.Text;
                    Settings.Default.Save();
                    OS.Visibility = Visibility.Collapsed;
                    RM.Visibility = Visibility.Collapsed;
                    ThicknessAnimationUsingKeyFrames t = new ThicknessAnimationUsingKeyFrames();
                    t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, -45, 0, 0), TimeSpan.FromSeconds(0)));
                    t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, 50, 0, 0), TimeSpan.FromSeconds(0.3)));
                    t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, 50, 0, 0), TimeSpan.FromSeconds(3)));
                    TX.BeginAnimation(MarginProperty,t);
                    tr.Start();
                }else
                {
                    OS.Visibility = Visibility.Collapsed;
                    RM.Visibility = Visibility.Collapsed;
                    ThicknessAnimationUsingKeyFrames t = new ThicknessAnimationUsingKeyFrames();
                    t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, -45, 0, 0), TimeSpan.FromSeconds(0)));
                    t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, 50, 0, 0), TimeSpan.FromSeconds(0.3)));
                    t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, 50, 0, 0), TimeSpan.FromSeconds(2)));
                    t.AutoReverse = true;
                    TX.BeginAnimation(MarginProperty, t);
                    trs.Start();
                }
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            c.Completed += delegate { Environment.Exit(0); };
            this.BeginAnimation(OpacityProperty, c);
        }

        private void PSW_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex re = new Regex("[^0-9.-]+");
            e.Handled = re.IsMatch(e.Text);
        }

        private void Email_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Border_MouseDown(null, null);
            }
         }
        private bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        private void TextBlock_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if(Email.Text!=""&& IsValidEmail(Email.Text))
            {
                Settings.Default.RNBM = (Boolean)RM.IsChecked;
                Settings.Default.LemonAreeunIts = Email.Text;
                Settings.Default.Save();
                OS.Visibility = Visibility.Collapsed;
                RM.Visibility = Visibility.Collapsed;
                ThicknessAnimationUsingKeyFrames t = new ThicknessAnimationUsingKeyFrames();
                t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, -45, 0, 0), TimeSpan.FromSeconds(0)));
                t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, 50, 0, 0), TimeSpan.FromSeconds(0.3)));
                t.KeyFrames.Add(new LinearThicknessKeyFrame(new Thickness(0, 50, 0, 0), TimeSpan.FromSeconds(3)));
                TX.BeginAnimation(MarginProperty, t);
                tr.Start();
            }
            else { rk.Text = "游客访问必须输入你的邮箱"; }
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            Settings.Default.RNBM = (bool)RM.IsChecked;
            new QQLogin().Show();
            Close();
        }
    }
}
