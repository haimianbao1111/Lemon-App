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
using System.Windows.Threading;

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
                StartupUri = new Uri("LoadWindow.xaml", UriKind.Relative);
        else
                StartupUri = new Uri("Lemon.xaml", UriKind.Relative);
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
                string i = "\r\n柠萌账号:" + Settings.Default.RobotName + "\r\n柠萌版本:" + He.KMS + ((Exception)e.ExceptionObject).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.ExceptionObject).Source + "\r\n 引发异常的方法:" + ((Exception)e.ExceptionObject).TargetSite + "\r\n  帮助链接:" + ((Exception)e.ExceptionObject).HelpLink + "\r\n 调用堆:" + ((Exception)e.ExceptionObject).StackTrace;
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory+@"Log.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(i);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch { } }

        private void Current_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {try
            {
                e.Handled = true;
                string i = "\r\n柠萌账号:" + Settings.Default.RobotName + "\r\n柠萌版本:" + He.KMS + ((Exception)e.Exception).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.Exception).Source + "\r\n 引发异常的方法:" + ((Exception)e.Exception).TargetSite + "\r\n  帮助链接:" + ((Exception)e.Exception).HelpLink + "\r\n 调用堆:" + ((Exception)e.Exception).StackTrace;
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + @"Log.log", FileMode.Append);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(i);
                sw.Flush();
                sw.Close();
                fs.Close();
            }
            catch { }
        }
    }
}
