using Lemon_App.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
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
    /// SettingsControl.xaml 的交互逻辑
    /// </summary>
    public partial class SettingsControl : UserControl
    {
        public SettingsControl()
        {
            InitializeComponent();
        }
        public void SetAutoRun(string fileName, bool isAutoRun)
        {
            RegistryKey reg = null;
            try
            {
                if (!System.IO.File.Exists(fileName))
                    throw new Exception("该文件不存在!");
                String name = fileName.Substring(fileName.LastIndexOf(@"\") + 1);
                reg = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                if (reg == null)
                    reg = Registry.LocalMachine.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                if (isAutoRun)
                    reg.SetValue(name, fileName);
                else
                    reg.SetValue(name, false);
            }
            catch
            {
                //throw new Exception(ex.ToString());  
            }
            finally
            {
                if (reg != null)
                    reg.Close();
            }
        }
        private void UrlBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (UrlBox.Text != "")
                {
                    Settings.Default.SearchUrl = UrlBox.Text;
                    Settings.Default.Save();
                }
        }

        private void UserNameBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (UserNameBox.Text != "")
                {
                    Settings.Default.RobotName = UserNameBox.Text;
                    Settings.Default.Save();
                }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            checkBox.IsChecked = Settings.Default.IsStart;
            UrlBox.Text = Settings.Default.SearchUrl;
            UserNameBox.Text = Settings.Default.RobotName;
            WeatherInfo.Text = Settings.Default.WeatherInfo;
            xyuri.Text = Settings.Default.WebProxyUri;
            xpuser.Text = Settings.Default.WebProxyUser;
            xypsw.Text = Settings.Default.WebProxyPassWord;
        }

        private void WeatherInfo_KeyDown(object sender, KeyEventArgs e)
        {
            if (WeatherInfo.Text != "")
                Settings.Default.WeatherInfo = WeatherInfo.Text;
        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            SetAutoRun(Process.GetCurrentProcess().MainModule.FileName, (bool)checkBox.IsChecked);
            Settings.Default.IsStart = (bool)checkBox.IsChecked;
            Settings.Default.Save();
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            if (xyuri.Text != "")
            {
                Settings.Default.IsStart = (bool)checkBox.IsChecked;
                Settings.Default.WeatherInfo = WeatherInfo.Text;
                Settings.Default.RobotName = UserNameBox.Text;
                Settings.Default.SearchUrl = UrlBox.Text;
                Settings.Default.isWebProxy = true;
                Settings.Default.WebProxyUri = xyuri.Text;
                Settings.Default.WebProxyUser = xpuser.Text;
                Settings.Default.WebProxyPassWord = xypsw.Text;
                Settings.Default.Save();
                He.proxy.Address = new Uri(Settings.Default.WebProxyUri);
                He.proxy.Credentials = new NetworkCredential(Settings.Default.WebProxyUser, Settings.Default.WebProxyPassWord);
            }
            else { Settings.Default.isWebProxy = false;Settings.Default.Save(); }
        }
    }
}
