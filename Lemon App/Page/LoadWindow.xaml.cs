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
using WPFMediaKit.DirectShow.Controls;

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
            wb.Navigated += NaAsync;
            RM.IsChecked = He.lsd.RNBM;
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
            else { if (oldtext != "已开启大写锁定") rk.Text = oldtext; else { rk.Text = ""; oldtext = ""; } }
            (Resources["l"] as Storyboard).Begin();

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
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory +qq+ @"@qq.com.st"))
                        He.Settings = (SettingsData)JSON.JsonToObject(Encoding.Default.GetString(Convert.FromBase64String(He.TextDecrypt(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + qq+@"@qq.com.st"), FanyiBox.MD5.EncryptToMD5string(qq+"@qq.com.st")))), He.Settings);
                    else He.SaveSettings(qq+"@qq.com");
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
                    He.lsd.NAME = qq;
                    He.lsd.RNBM = (Boolean)RM.IsChecked;
                    He.lsd.TX= AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg";
                    He.SaveLoadSettings();
                    (Resources["OnLoaded1"] as Storyboard).Begin();
                    tr.Start();
                }
                else if (wb.DocumentText.Contains("安全验证"))
                {
                    op.IsOpen = true;
                    rk.Text = "请输入验证码";
                }
                else { rk.Text = "登录失败,请检查账号和密码."; op.IsOpen = false; }
            }
            else { index++; }
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
            if (He.lsd.RNBM)
            {
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + He.lsd.NAME + "@qq.com.st"))
                    He.Settings = (SettingsData)JSON.JsonToObject(Encoding.Default.GetString(Convert.FromBase64String(He.TextDecrypt(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + He.lsd.NAME + "@qq.com.st"), FanyiBox.MD5.EncryptToMD5string(He.lsd.NAME + "@qq.com.st")))), He.Settings);
                else He.SaveSettings(He.lsd.NAME+"@qq.com");
                (Resources["OnLoaded1"] as Storyboard).Begin();
                tr.Start();
            }
            Email.Text = He.lsd.NAME;
            if (System.IO.File.Exists(He.lsd.TX))
            {
                var image = new System.Drawing.Bitmap(He.lsd.TX);
                TX.Background = new ImageBrush(image.ToImageSource());
            }
            RM.IsChecked = He.lsd.RNBM;
        }
        string ini = "";
        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var c = (Resources["c"] as Storyboard);
            c.Completed += delegate { Process.GetCurrentProcess().Kill(); };
            c.Begin();
        }
        private void Email_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Border_MouseDown_1(null, null);
        }
        private bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        //   System.Windows.Forms.WebBrowser wb = new System.Windows.Forms.WebBrowser();
        private async void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (Email.Text != string.Empty || PSW.Password != string.Empty)
            {
                wb.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html&hide_close_icon=1");
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
                if (wb.DocumentTitle != "我的QQ中心" || !wb.DocumentText.Contains("安全验证"))
                    rk.Text = "登录失败,请检查账号和密码.";
                else if (wb.DocumentText.Contains("安全验证"))
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
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + qq + @"@qq.com.st"))
                        He.Settings = (SettingsData)JSON.JsonToObject(Encoding.Default.GetString(Convert.FromBase64String(He.TextDecrypt(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + qq + @"@qq.com.st"), FanyiBox.MD5.EncryptToMD5string(qq + "@qq.com.st")))), He.Settings);
                    else He.SaveSettings(qq + "@qq.com");
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
                    He.lsd.NAME = qq;
                    He.lsd.RNBM = (Boolean)RM.IsChecked;
                    He.lsd.TX = AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg";
                    He.SaveLoadSettings();
                    (Resources["OnLoaded1"] as Storyboard).Begin();
                    tr.Start();
                }
                else { rk.Text = "登录失败,请检查账号和密码."; }
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 3)
            {
                op.IsOpen = !op.IsOpen;
                wb.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html&hide_close_icon=1");
            }
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
            if (Email.Text.Count() >= 5)
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
            (Resources["FXC"] as Storyboard).Begin();
        }
        private async void qrcode_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wb.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html&hide_close_icon=1");
            await Task.Delay(1000);
            string str = wb.Document.Body.OuterHtml;
            MatchCollection matches;
            matches = Regex.Matches(str, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            var t = matches[1].Value.ToString();
            Regex reg = new Regex(@"<img.*?src=""(?<src>[^""]*)""[^>]*>", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(t);
            var content = mc[0].Groups["src"].Value;
            t = He.Text(content+"\"", "t=", "\"", 0);
            qrcode.Background = new ImageBrush(new BitmapImage(new Uri(content)));
            //       op.IsOpen = true;
            index = 0;
        }

        private async void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            wb.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html&hide_close_icon=1");
            await Task.Delay(1000);
            string str = wb.Document.Body.OuterHtml;
            MatchCollection matches;
            matches = Regex.Matches(str, @"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
            var t = matches[1].Value.ToString();
            Regex reg = new Regex(@"<img.*?src=""(?<src>[^""]*)""[^>]*>", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(t);
            var content = mc[0].Groups["src"].Value;
            qrcode.Background = new ImageBrush(new BitmapImage(new Uri(content)));
            //       op.IsOpen = true;
            index = 0;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                vce.Play();
                RenderTargetBitmap bmp = new RenderTargetBitmap(
                    (int)vce.ActualWidth,
                    (int)vce.ActualHeight,
                    96, 96, PixelFormats.Default);
                bmp.Render(vce);
                BitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bmp));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    byte[] a = ms.ToArray();
                    byte[] b = Convert.FromBase64String(He.TextDecrypt(File.ReadAllText(He.lsd.NAME + ".FaceData"), FanyiBox.MD5.EncryptToMD5string(He.lsd.NAME + ".FaceData")));
                    vce.Stop();
                    var client = new Baidu.Aip.Face.Face("75bl82qIt9Rtly6Na6wqYUmm", "pMO9ZSQSsZFNvMMnXy5L3GaQbpWG6Fyw");
                    var images = new byte[][] { a, b };
                    var result = double.Parse(client.FaceMatch(images).First.First.Last.Last.First.ToString());
                    if (result >= 90)
                    {
                        if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + He.lsd.NAME + "@qq.com.st"))
                            He.Settings = (SettingsData)JSON.JsonToObject(Encoding.Default.GetString(Convert.FromBase64String(He.TextDecrypt(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + He.lsd.NAME + "@qq.com.st"), FanyiBox.MD5.EncryptToMD5string(He.lsd.NAME + "@qq.com.st")))), He.Settings);
                        else He.SaveSettings(He.lsd.NAME + "@qq.com");
                        (Resources["OnLoaded1"] as Storyboard).Begin();
                        tr.Start();
                    }
                    else txb.Text = "识别失败";
                }
            }
            catch { txb.Text = "识别失败"; }
            }

        private void face_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Email.Text != "QQ账号" && File.Exists(Email.Text + ".FaceData"))
            {
               vce.VideoCaptureSource = MultimediaUtil.VideoInputNames[0];
                (Resources["FACESTAR"] as Storyboard).Begin();
            }
            else { rk.Text = "没有录入面部数据"; }
        }
    }
}
