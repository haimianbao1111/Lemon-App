using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Un4seen.Bass;

namespace Lemon_App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public App()//
        {
                    if (!Settings.Default.RNBM)
                        StartupUri = new Uri("/Lemon App;component/Page/LoadWindow.xaml", UriKind.Relative);
                    else StartupUri = new Uri("/Lemon App;component/Page/Lemon.xaml", UriKind.Relative);
            if (Settings.Default.isWebProxy)
            if (Settings.Default.WebProxyUri != "")
            {
                He.proxy.Address = new Uri(Settings.Default.WebProxyUri);
                He.proxy.Credentials = new NetworkCredential(Settings.Default.WebProxyUser, Settings.Default.WebProxyPassWord);
            }
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {try
            {
                //string i = "";
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Log.log"))
                //{
                //    var a = (EX)JSON.JsonToObject(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Log.log"), new EX());
                //    a.Exception.Add(new ExceptionItem()
                //    {
                //        UserName = Settings.Default.RobotName,
                //        KMS = He.KMS,
                //        Message = ((Exception)e.ExceptionObject).Message,
                //        Source = ((Exception)e.ExceptionObject).Source,
                //        TargetSite = ((Exception)e.ExceptionObject).TargetSite.ToString(),
                //        HelpLink = ((Exception)e.ExceptionObject).HelpLink,
                //        StackTrace = ((Exception)e.ExceptionObject).Message
                //    });
                //    i = JSON.ToJSON(a);
                //}
                //else
                //{
                //    var a = new EX();
                //    a.Exception.Add(new ExceptionItem()
                //    {
                //        UserName = Settings.Default.RobotName,
                //        KMS = He.KMS,
                //        Message = ((Exception)e.ExceptionObject).Message,
                //        Source = ((Exception)e.ExceptionObject).Source,
                //        TargetSite = ((Exception)e.ExceptionObject).TargetSite.ToString(),
                //        HelpLink = ((Exception)e.ExceptionObject).HelpLink,
                //        StackTrace = ((Exception)e.ExceptionObject).Message
                //    });
                //    i = JSON.ToJSON(a);
                //}
                string i = "\r\n柠萌账号:" + Settings.Default.RobotName + "\r\n柠萌版本:" + He.KMS +"\r\n"+ ((Exception)e.ExceptionObject).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.ExceptionObject).Source + "\r\n 引发异常的方法:" + ((Exception)e.ExceptionObject).TargetSite + "\r\n  帮助链接:" + ((Exception)e.ExceptionObject).HelpLink + "\r\n 调用堆:" + ((Exception)e.ExceptionObject).StackTrace;
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+@"Log.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(i);
                sw.Flush();
                sw.Close();
                fs.Close();
                if (He.isOpMod == true)
                {
                    Console.WriteLine(i);
                }
            }
            catch { } }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {try
            {
                e.Handled = true;
                //string i = "";
                //if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Log.log"))
                //{
                //    var a = (EX)JSON.JsonToObject(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Log.log"), new EX());
                //    a.Exception.Add(new ExceptionItem()
                //    {
                //        UserName = Settings.Default.RobotName,
                //        KMS = He.KMS,
                //        Message = ((Exception)e.Exception).Message,
                //        Source = ((Exception)e.Exception).Source,
                //        TargetSite = ((Exception)e.Exception).TargetSite.ToString(),
                //        HelpLink = ((Exception)e.Exception).HelpLink,
                //        StackTrace = ((Exception)e.Exception).Message
                //    });
                //    i = JSON.ToJSON(a);
                //}
                //else
                //{
                //    var a = new EX();
                //    a.Exception.Add(new ExceptionItem()
                //    {
                //        UserName = Settings.Default.RobotName,
                //        KMS = He.KMS,
                //        Message = ((Exception)e.Exception).Message,
                //        Source = ((Exception)e.Exception).Source,
                //        TargetSite = ((Exception)e.Exception).TargetSite.ToString(),
                //        HelpLink = ((Exception)e.Exception).HelpLink,
                //        StackTrace = ((Exception)e.Exception).Message
                //    });
                //    i = JSON.ToJSON(a);
                //}
                string i = "\r\n柠萌账号:" + Settings.Default.RobotName + "\r\n柠萌版本:" + He.KMS +"\r\n"+ ((Exception)e.Exception).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.Exception).Source + "\r\n 引发异常的方法:" + ((Exception)e.Exception).TargetSite + "\r\n  帮助链接:" + ((Exception)e.Exception).HelpLink + "\r\n 调用堆:" + ((Exception)e.Exception).StackTrace;
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Log.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(i);
                sw.Flush();
                sw.Close();
                fs.Close();
                if (He.isOpMod == true)
                {
                    Console.WriteLine(i);
                }
            }
            catch { }
        }
    }
}
