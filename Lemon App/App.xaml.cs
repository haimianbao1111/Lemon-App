using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

namespace Lemon_App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()//
        {if(Settings.Default.isWebProxy)
            if (Settings.Default.WebProxyUri != "")
            {
                He.proxy.Address = new Uri("http://127.0.0.1:8888");
                He.proxy.Credentials = new NetworkCredential(Settings.Default.WebProxyUser, Settings.Default.WebProxyPassWord);
            }
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
           AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {try
            {
                Toast.SetToastNotion("Lemon App:", ":( 噢！柠萌似乎遇到了一个异常：" + ((Exception)e.ExceptionObject).Message, "如果遇到不能使用请到用户反馈提出你的问题。").Show();
                //MailMessage m = new MailMessage()
                //{
                //    From = new MailAddress("lemon.app@qq.com", "Lemon团队")
                //};
                //m.To.Add(new MailAddress("cz241126@live.com"));
                //m.Subject = "Lemon App异常反馈";
                //m.SubjectEncoding = Encoding.UTF8;
                //string i ="柠萌账号:"+Settings.Default.RobotName+"\r\n柠萌版本:"+He.KMS+ ((Exception)e.ExceptionObject).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.ExceptionObject).Source + "\r\n 引发异常的方法:" + ((Exception)e.ExceptionObject).TargetSite + "\r\n  帮助链接:" + ((Exception)e.ExceptionObject).HelpLink + "\r\n 调用堆:" + ((Exception)e.ExceptionObject).StackTrace;
                //m.Body = He.EmailEorre.Replace("{ninini}",i);
                //m.BodyEncoding = Encoding.UTF8;
                //m.IsBodyHtml = true;
                //SmtpClient s = new SmtpClient()
                //{
                //    Host = "smtp.qq.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi")
                //};
                //s.Send(m);
            }
            catch { } }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {try
            {
                Toast.SetToastNotion("Lemon App:", ":( 噢！柠萌似乎遇到了一个异常：" + e.Exception.Message, "如果遇到不能使用请到用户反馈提出你的问题。").Show();
                e.Handled = true;
                //MailMessage m = new MailMessage()
                //{
                //    From = new MailAddress("lemon.app@qq.com", "Lemon团队")
                //};
                //m.To.Add(new MailAddress("cz241126@live.com"));
                //m.Subject = "Lemon App异常反馈";
                //m.SubjectEncoding = Encoding.UTF8;
                //string i = "柠萌账号:" + Settings.Default.RobotName + "\r\n柠萌版本:" + He.KMS+((Exception)e.Exception).Message + "\r\n 导致错误的对象名称:"+((Exception)e.Exception).Source+"\r\n 引发异常的方法:"+ ((Exception)e.Exception).TargetSite+"\r\n  帮助链接:"+ ((Exception)e.Exception).HelpLink+"\r\n 调用堆:"+ ((Exception)e.Exception).StackTrace;
                //m.Body = He.EmailEorre.Replace("{ninini}", i);
                //m.BodyEncoding = Encoding.UTF8;
                //m.IsBodyHtml = true;
                //SmtpClient s = new SmtpClient()
                //{
                //    Host = "smtp.qq.com",
                //    Port = 587,
                //    EnableSsl = true,
                //    Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi")
                //};
                //s.Send(m);
            }
            catch { }
        }
    }
}
