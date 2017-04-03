using Lemon_App.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// QQLogin.xaml 的交互逻辑
    /// </summary>
    public partial class QQLogin : Window
    {
        public QQLogin()
        {
            InitializeComponent();
            web.Navigate("http://ui.ptlogin2.qq.com/cgi-bin/login?appid=1006102&s_url=http://id.qq.com/index.html");
        }
        private async void web_Navigated_1(object sender, System.Windows.Forms.WebBrowserNavigatedEventArgs e)
        {
            if (web.DocumentTitle == "我的QQ中心")
            {
                var qq = He.Text(web.Document.Cookie, "uin=o", ";", 0);
                web.Dispose();
                var sl = He.Text(await Uuuhh.GetWebAsync("http://r.pengyou.com/fcg-bin/cgi_get_portrait.fcg?uins=" + qq,Encoding.Default), "portraitCallBack(", ")", 0);
                JObject o = JObject.Parse(sl);
                if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\" + qq + ".jpg"))
                {
                    System.Net.WebClient x = new System.Net.WebClient();
                    x.DownloadFileAsync(new Uri($"http://q2.qlogo.cn/headimg_dl?bs=qq&dst_uin={qq}&spec=100"), AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg");
                }
                Settings.Default.RobotName = o[qq][6].ToString();
                Settings.Default.UserImage = AppDomain.CurrentDomain.BaseDirectory + qq + ".jpg";
                Settings.Default.LemonAreeunIts = qq + "@qq.com";
                Settings.Default.Save();
                new lemon().Show();
                this.Close();
            }
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
