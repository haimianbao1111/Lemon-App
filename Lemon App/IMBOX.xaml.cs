using Lemon_App.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// IMBOX.xaml 的交互逻辑
    /// </summary>
    public partial class IMBOX : UserControl
    {
        public IMBOX()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            Robot.Width = this.ActualWidth;
        }

        private async void label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://www.tuling123.com/openapi/api?key=0651b32a3a6c8f54c7869b9e62872796&info=" + Uri.EscapeUriString(textBox1.Text) + "&userid=" + Uri.EscapeUriString(Settings.Default.LemonAreeunIts)));
            User U = new User(textBox1.Text)
            {
                Width = Robot.ActualWidth
            };
            Robot Rb = new Robot((string)obj["text"])
            {
                Width = Robot.ActualWidth
            };
            Robot.Children.Add(U);
            Robot.Children.Add(Rb);
            if ((string)obj["code"] == "200000")
            {
                string i = (string)obj["text"];
                User Uu = new User(textBox1.Text);
                U.Width = Robot.ActualWidth;
                Lemon_App.Robot Rbu = new Lemon_App.Robot((string)obj["url"] + i);
                Rb.Width = Robot.ActualWidth;
                Robot.Children.Add(Uu);
                Robot.Children.Add(Rbu);
            }

        }

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://www.tuling123.com/openapi/api?key=0651b32a3a6c8f54c7869b9e62872796&info=" + Uri.EscapeUriString(textBox1.Text) + "&userid=" + Uri.EscapeUriString(Settings.Default.LemonAreeunIts)));
                User U = new User(textBox1.Text);
                U.Width = Robot.ActualWidth;
                Lemon_App.Robot Rb = new Lemon_App.Robot((string)obj["text"]);
                Rb.Width = Robot.ActualWidth;
                Robot.Children.Add(U);
                Robot.Children.Add(Rb);
                if ((string)obj["code"] == "200000")
                {
                    string i = (string)obj["text"];
                    User Uu = new User(textBox1.Text);
                    U.Width = Robot.ActualWidth;
                    Lemon_App.Robot Rbu = new Lemon_App.Robot((string)obj["url"] + i);
                    Rb.Width = Robot.ActualWidth;
                    Robot.Children.Add(Uu);
                    Robot.Children.Add(Rbu);
                }
            }
        }

        private void Robot_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            double d = this.Sllv.ActualHeight + this.Sllv.ViewportHeight + this.Sllv.ExtentHeight;
            this.Sllv.ScrollToVerticalOffset(d);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Robot.Width = this.ActualWidth;
            foreach(var o in Robot.Children)
            {
                (o as UserControl).Width = this.ActualWidth;
            }
        }
    }
}
