using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Forms;
using mshtml;
using System.Diagnostics;

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
            wb.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html&hide_close_icon=1");
            wb.Navigated += NaAsync;
            RM.IsChecked = He.Settings.RNBM;
            var c = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(OpacityProperty, c);
            this.FontFamily = new FontFamily(He.Settings.FontFamilly);
            tr.Interval = 5000;
            tr.Tick += T;
            trs.Interval = 1000;
            trs.Tick += Trs;
            if (Console.CapsLock)
            {
                oldtext = rk.Text;
                rk.Text = "已开启大写锁定";
            }
            else { if (oldtext != "已开启大写锁定") rk.Text = oldtext; else { rk.Text = "";oldtext = ""; } }
        }
        int index = 0;
        private async void NaAsync(object sender, WebBrowserNavigatedEventArgs e)
        {
            if (index != 0)
            {
                if (wb.DocumentTitle == "我的QQ中心")
                {
                    op.IsOpen = false;
                    var qq = He.Text(wb.Document.Cookie, "uin=o", ";", 0);
                    var sl = He.Text(await Uuuhh.GetWebAsync("http://r.pengyou.com/fcg-bin/cgi_get_portrait.fcg?uins=" + qq, Encoding.Default), "portraitCallBack(", ")", 0);
                    JObject o = JObject.Parse(sl);
                    try
                    {
                        await Uuuhh.HttpDownloadFileAsync($"http://q2.qlogo.cn/headimg_dl?bs=qq&dst_uin={qq}&spec=100", AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg");
                        var image = new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg");
                        TX.Background = new ImageBrush(image.ToImageSource());
                    }
                    catch { }
                    He.Settings.RobotName = o[qq][6].ToString();
                    He.Settings.UserImage = AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg";
                    He.Settings.LemonAreeunIts = qq + "@qq.com";
                    He.Settings.RNBM = (Boolean)RM.IsChecked;
                    He.SaveSettings();
                    (Resources["OnLoaded1"] as Storyboard).Begin();
                    tr.Start();
                }
                else if (wb.DocumentText.Contains("安全验证"))
                {
                    op.IsOpen = true;
                    rk.Text = "请输入验证码";
                }
                else { rk.Text = "登录失败,请检查账号和密码."; op.IsOpen = false; }
            }else { index++; }
        }
        private System.Drawing.Image GetWebImage(System.Windows.Forms.WebBrowser WebCtl, HtmlElement ImgeTag)
        {
            HTMLDocument doc = (HTMLDocument)WebCtl.Document.DomDocument;
            HTMLBody body = (HTMLBody)doc.body;
            IHTMLControlRange rang = (IHTMLControlRange)body.createControlRange();
            IHTMLControlElement Img = (IHTMLControlElement)ImgeTag.DomElement; //图片地址  

            var oldImage = System.Windows.Forms.Clipboard.GetImage();
            rang.add(Img);
            rang.execCommand("Copy", false, null);  //拷贝到内存  
            var numImage = System.Windows.Forms.Clipboard.GetImage();
            try
            {
                System.Windows.Forms.Clipboard.SetImage(oldImage);
            }
            catch
            {
            }

            return numImage;
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
                wb.Dispose();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (He.Settings.RNBM)
            {
                (Resources["OnLoaded1"] as Storyboard).Begin();
                tr.Start();
            }
                var s = He.Settings.LemonAreeunIts;
                Email.Text = s.Remove(s.LastIndexOf("@qq.com"));
                if (System.IO.File.Exists(He.Settings.UserImage))
                {
                    var image = new System.Drawing.Bitmap(He.Settings.UserImage);
                    TX.Background = new ImageBrush(image.ToImageSource());
                }
                RM.IsChecked = He.Settings.RNBM;
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
                //    ns.Text = "发送成功";
                    DoubleAnimationUsingKeyFrames d = new DoubleAnimationUsingKeyFrames();
                    d.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                    d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(0.3)));
                    d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(3)));
                    d.AutoReverse = true;
                    po.BeginAnimation(OpacityProperty, d);
                }
            }catch
            {
       //        ns.Text = "发送失败" + es.Message;
                DoubleAnimationUsingKeyFrames d = new DoubleAnimationUsingKeyFrames();
                d.KeyFrames.Add(new LinearDoubleKeyFrame(0, TimeSpan.FromSeconds(0)));
                d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(0.3)));
                d.KeyFrames.Add(new LinearDoubleKeyFrame(1, TimeSpan.FromSeconds(3)));
                d.AutoReverse = true;
                po.BeginAnimation(OpacityProperty, d);
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
            c.Completed += delegate { Process.GetCurrentProcess().Kill(); };
            this.BeginAnimation(OpacityProperty, c);
        }
        private void Email_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Border_MouseDown_1(null, null);
            }
         }
        private bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        private void TextBlock_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            //He.Settings.RNBM = (bool)RM.IsChecked;
            //new QQLogin().Show();
            //Close();
            wb.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html&hide_close_icon=1");
            op.IsOpen = true;
            index = 0;
        }
     //   System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
        private async void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Email.Text != string.Empty || PSW.Password != string.Empty)
            {
                rk.Text = "登录中...";
                System.Windows.Forms.HtmlDocument doc = wb.Document;
                doc.GetElementById("switcher_plogin").InvokeMember("click");
                await Task.Delay(200);
                doc.GetElementById("u").InnerText = Email.Text;
                await Task.Delay(200);
                doc.GetElementById("p").InnerText = PSW.Password;
                await Task.Delay(200);
                doc.GetElementById("login_button").InvokeMember("click");
                await Task.Delay(1000);
                if (wb.DocumentTitle != "我的QQ中心"|| !wb.DocumentText.Contains("安全验证"))
                    rk.Text = "登录失败,请检查账号和密码.";
                else if(wb.DocumentText.Contains("安全验证"))
                {
                    op.IsOpen = true;
                    rk.Text = "请输入验证码";
                }
            }
        }

        private async void Grid_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                op.IsOpen = false;
                //    wb.Document..GetElementById("submit").InvokeMember("click");
                if (wb.DocumentTitle == "我的QQ中心")
                {
                    await Task.Delay(200);
                    var qq = He.Text(wb.Document.Cookie, "uin=o", ";", 0);
                    var sl = He.Text(await Uuuhh.GetWebAsync("http://r.pengyou.com/fcg-bin/cgi_get_portrait.fcg?uins=" + qq, Encoding.Default), "portraitCallBack(", ")", 0);
                    JObject o = JObject.Parse(sl);
                    try
                    {
                        await Uuuhh.HttpDownloadFileAsync($"http://q2.qlogo.cn/headimg_dl?bs=qq&dst_uin={qq}&spec=100", AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg");
                        var image = new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg");
                        TX.Background = new ImageBrush(image.ToImageSource());
                    }
                    catch { }
                    He.Settings.RobotName = o[qq][6].ToString();
                    He.Settings.UserImage = AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg";
                    He.Settings.LemonAreeunIts = qq + "@qq.com";
                    He.Settings.RNBM = (Boolean)RM.IsChecked;
                    He.SaveSettings();
                    (Resources["OnLoaded1"] as Storyboard).Begin();
                    tr.Start();
                }
                else { rk.Text = "登录失败,请检查账号和密码."; }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3)
                op.IsOpen = !op.IsOpen;
        }
        string oldtext = "";
        private void Window_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (Console.CapsLock)
            {
                oldtext = rk.Text;
                rk.Text = "已开启大写锁定";
            }
            else { if (oldtext != "已开启大写锁定") rk.Text = oldtext; else { rk.Text = ""; oldtext = ""; } }
        }

        private void Email_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(Email.Text.Count()>=5)
            {
                if (System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + Email.Text + ".jpg"))
                {
                    var image = new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + Email.Text + ".jpg");
                    TX.Background = new ImageBrush(image.ToImageSource());
                }
                else { TX.Background = new ImageBrush(new BitmapImage(new Uri("http://q2.qlogo.cn/headimg_dl?bs=qq&dst_uin={qq}&spec=100"))); }
                TX.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
            }
        }

        private void border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            tr.Stop();
            (Resources["OnLoaded1"] as Storyboard).Stop();
        }
    }
}
