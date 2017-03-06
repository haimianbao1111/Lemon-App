using System;
using System.Collections.Generic;
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

        private void textBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                MailMessage m = new MailMessage();
                m.From = new MailAddress("lemon.app@qq.com","小萌反馈");
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
                He.EmailUFMsg.Replace("{ninini}", body).Replace("{nihaoyouxiang}", textBox1.Text).Replace("{lalalala}", textBox.Text);
                m.Body = He.EmailUFMsg.Replace("{ninini}", body).Replace("{nihaoyouxiang}", textBox1.Text).Replace("{lalalala}", textBox.Text);
                m.BodyEncoding = Encoding.UTF8;
                m.IsBodyHtml = true;
                SmtpClient s = new SmtpClient();
                s.Host = "smtp.qq.com";
                s.Port = 587;
                s.EnableSsl = true;
                s.Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi");
                s.Send(m);
                label.Content = "成功发送你的反馈！";
            }
            catch (Exception ex)
            {
                textBox.Text = ex.Message;
                label.Content = "发送失败";
            }
        }
    }
}
