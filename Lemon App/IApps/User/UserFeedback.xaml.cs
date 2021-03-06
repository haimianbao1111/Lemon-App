﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Lemon_App
{
    /// <summary>
    /// UserFeedback.xaml 的交互逻辑
    /// </summary>
    public partial class UserFeedback : UserControl
    {
        public UserFeedback()
        {
            InitializeComponent();
        }
        private bool IsValidEmail(string strIn)
        {
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)" + @"|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }
        private async void textBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (textBox1.Text != string.Empty && IsValidEmail(textBox1.Text) && textBox.Text != string.Empty)
                {
                    MailMessage m = new MailMessage();
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Log.log"))
                    {
                        m.Attachments.Add(new Attachment(AppDomain.CurrentDomain.BaseDirectory + @"Log.log"));
                    }
                    m.From = new MailAddress("lemon.app@qq.com", "小萌反馈");
                    m.To.Add(new MailAddress("cz241126@live.com"));
                    m.Subject = "Lemon App用户反馈";
                    m.SubjectEncoding = Encoding.UTF8;
                    string body = "";
                    if (r.IsChecked == true)
                        body = "喜欢";
                    else if (rr.IsChecked == true)
                        body = "建议";
                    else if (rrr.IsChecked == true)
                        body = "不喜欢";
                    m.Body = @"<table dir=""ltr"">
    <tbody>
        <tr>
            <td id = ""i1"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:17px; color:#707070;"">
                Lemon App 用户反馈
            </td>
        </tr>
        <tr>
            <td id = ""i2"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:41px; color:#2672ec;"">
                用户反馈
            </td>
        </tr>
        <tr>
            <td id = ""i3"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                新的Lemon App用户反馈
            </td>
        </tr>
        <tr>
            <td id = ""i4"" style= ""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"" >
                状态：
                <b>
                    {ninini} &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;邮箱</b></td><td id = ""i4"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">：
                <b>
                    {nihaoyouxiang}
                </b>
            </td>
        </tr>
        <tr>
            <td id = ""i5"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                {lalalala}
            </td>
        </tr>
        <tr>
            <td id = ""i6"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                谢谢!
            </td>
        </tr>
        <tr>
            <td id = ""i7"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                Lemon App 用户反馈团队
            </td>
        </tr>
    </tbody>
</table>".Replace("{ninini}", body).Replace("{nihaoyouxiang}", textBox1.Text).Replace("{lalalala}", textBox.Text);
                    m.BodyEncoding = Encoding.UTF8;
                    m.IsBodyHtml = true;
                    SmtpClient s = new SmtpClient();
                    s.Host = "smtp.qq.com";
                    s.Port = 587;
                    s.EnableSsl = true;
                    s.Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi");
                    await s.SendMailAsync(m);
                    label.Content = "成功发送你的反馈！";
                }
                else { label.Content = "请输入合法的Email地址"; }
            }
            catch (Exception ex)
            {
                textBox.Text = ex.Message;
                label.Content = "发送失败";
            }
        }
    }
}
