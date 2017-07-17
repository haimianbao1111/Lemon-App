using Lemon_App.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Media.Animation;
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
            try
            {
                JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://www.tuling123.com/openapi/api?key=0651b32a3a6c8f54c7869b9e62872796&info=" + Uri.EscapeUriString(textBox1.Text) + "&userid=" + Uri.EscapeUriString(Settings.Default.LemonAreeunIts)));
                if ((string)obj["code"] == "100000" || obj["code"].ToString() == "40002")
                {
                    User U = new User(textBox1.Text)
                    {
                        Width = Robot.ActualWidth,Opacity = 0
                    };
                    Robot Rb = new Robot((string)obj["text"])
                    {
                        Width = Robot.ActualWidth, Opacity = 0
                    };
                    Robot.Children.Add(U);
                    Robot.Children.Add(Rb);
                    var b = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                    U.BeginAnimation(OpacityProperty, b);
                    Rb.BeginAnimation(OpacityProperty, b);
                }
                else if ((string)obj["code"] == "200000")
                {
                    string i = (string)obj["text"];
                    User Uu = new User(textBox1.Text);
                    Uu.Opacity = 0;
                    Uu.Width = Robot.ActualWidth;
                    Lemon_App.Robot Rbu = new Lemon_App.Robot((string)obj["url"] + i);
                    Rbu.Opacity = 0;
                    Rbu.Width = Robot.ActualWidth;
                    Rbu.ToolTip = (string)obj["url"].ToString();
                    Rbu.MouseDown += Rbu_MouseDown;
                    Robot.Children.Add(Uu);
                    Robot.Children.Add(Rbu);
                    var b = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                    Uu.BeginAnimation(OpacityProperty, b);
                    Rbu.BeginAnimation(OpacityProperty, b);
                }
                else if ((string)obj["code"] == "308000")
                {
                    User Uu = new User(textBox1.Text);
                    Uu.Width = Robot.ActualWidth;
                    Uu.Opacity = 0;
                    Robot.Children.Add(Uu);
                    int i = 0;
                    var s = new List<string>();
                    var f = new List<string>();
                    var u = new List<string>();
                    while (i != 5)
                    {
                        s.Add(obj["list"][i]["name"].ToString());
                        f.Add(obj["list"][i]["info"].ToString());
                        u.Add(obj["list"][i]["detailurl"].ToString());
                        i++;
                    }
                    var c = new RobotHrSp(s, f, u);
                    c.Width = Robot.ActualWidth;
                    c.Opacity = 0;
                    Robot.Children.Add(c);
                    var b = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                    Uu.BeginAnimation(OpacityProperty, b);
                    c.BeginAnimation(OpacityProperty, b);
                }
            }
            catch
            {
                User U = new User(textBox1.Text)
                {
                    Width = Robot.ActualWidth
                    , Opacity = 0
                };
                Robot Rb = new Robot("小萌机器人似乎遇到了些问题")
                {
                    Width = Robot.ActualWidth, Opacity = 0
                };
                Robot.Children.Add(U);
                Robot.Children.Add(Rb);
                var b = new DoubleAnimation(1, TimeSpan.FromSeconds(0.2));
                U.BeginAnimation(OpacityProperty, b);
                Rb.BeginAnimation(OpacityProperty, b);
            }
            textBox1.Text = "";
        }

        private void Rbu_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start((sender as Grid).ToolTip.ToString());
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                label_MouseDown(null, null);
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
