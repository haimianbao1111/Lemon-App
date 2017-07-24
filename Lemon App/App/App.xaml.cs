using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;
using System.Windows.Threading;
using Un4seen.Bass;

namespace Lemon_App
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public App()//
        {
            if (He.Settings.isWebProxy)
            if (He.Settings.WebProxyUri != "")
            {
                He.proxy.Address = new Uri(He.Settings.WebProxyUri);
                He.proxy.Credentials = new NetworkCredential(He.Settings.WebProxyUser, He.Settings.WebProxyPassWord);
            }
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + @"Settings.st"))
            {
                var data = File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + @"Settings.st");
                He.Settings = (SettingsData)JSON.JsonToObject(data, He.Settings);
            }
            else He.SaveSettings();
            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Startup += delegate {
                mutex = new Mutex(true, "Lemon App", out bool ret);
                if (!ret)
                    Environment.Exit(0);
            };
        }
        Mutex mutex;
        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(Keys bVk, byte bScan, uint dwFlags, uint dwExtraInfo);
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
                string i = "\r\n柠萌账号:" + He.Settings.RobotName + "\r\n柠萌版本:" + He.KMS +"\r\n"+ ((Exception)e.ExceptionObject).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.ExceptionObject).Source + "\r\n 引发异常的方法:" + ((Exception)e.ExceptionObject).TargetSite + "\r\n  帮助链接:" + ((Exception)e.ExceptionObject).HelpLink + "\r\n 调用堆:" + ((Exception)e.ExceptionObject).StackTrace;
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
                string i = "\r\n柠萌账号:" + He.Settings.RobotName + "\r\n柠萌版本:" + He.KMS +"\r\n"+ ((Exception)e.Exception).Message + "\r\n 导致错误的对象名称:" + ((Exception)e.Exception).Source + "\r\n 引发异常的方法:" + ((Exception)e.Exception).TargetSite + "\r\n  帮助链接:" + ((Exception)e.Exception).HelpLink + "\r\n 调用堆:" + ((Exception)e.Exception).StackTrace;
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
