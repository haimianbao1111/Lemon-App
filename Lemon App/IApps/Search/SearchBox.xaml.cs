﻿using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Lemon_App
{
    /// <summary>
    /// SearchBox.xaml 的交互逻辑
    /// </summary>
    public partial class SearchBox : UserControl
    {
        public SearchBox()
        {
            InitializeComponent();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                if (textBox1.Text != "" && os != 1) 
                    Process.Start(Uri.EscapeUriString(He.Settings.SearchUrl.Replace("%2a", textBox1.Text)));
            if (textBox1.Text == "搜索"&&e.Key!=Key.Enter)
                  textBox1.Text = "";
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (textBox1.Text != ""&&os!=1)
                Process.Start(Uri.EscapeUriString(He.Settings.SearchUrl.Replace("%2a", textBox1.Text)));
        }

        private void UserControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if(os!=1)
                    if (textBox1.Text != "")
                {
                    if (textBox1.Text != "搜索")
                    {
                        S.BeginAnimation(HeightProperty, new DoubleAnimation(400, TimeSpan.FromSeconds(0.3)));
                        HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("http://suggestion.baidu.com/su?wd=" + Uri.EscapeDataString(textBox1.Text) + "&action=opensearch");
                        hwr.Proxy = He.proxy;
                        string html6 = "";
                        Stream sr = hwr.GetResponse().GetResponseStream();
                        byte[] b = new byte[1024];
                        sr.Read(b, 0, 1024);
                        html6 = Encoding.Default.GetString(b);
                        html6 = html6.Replace("\0", "");
                        string Htp = html6.Substring(html6.LastIndexOf(",[")).Replace("]]", "").Replace(",[", "").Replace(",", "");
                        string[] aa = Htp.Split(new char[] { '\"' }, StringSplitOptions.RemoveEmptyEntries);
                        listBox.Items.Clear();
                        foreach (var item in aa)
                        {
                            listBox.Items.Add(new ListBoxItem() { Content=item,Height =35});
                        }
                    }
                }else { S.BeginAnimation(HeightProperty, new DoubleAnimation(30, TimeSpan.FromSeconds(0.3))); listBox.Items.Clear(); }
            }
            catch { }
        }

        private void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textBox1.Text = (listBox.SelectedItem as ListBoxItem).Content.ToString();
            Process.Start(Uri.EscapeUriString(He.Settings.SearchUrl.Replace("%2a", textBox1.Text)));
        }
        int os = 0;
        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (os == 0)
            {
                os = 1;
                RotateTransform rtf = new RotateTransform();
                (sender as Border).RenderTransform = rtf;
                DoubleAnimation dbAscending = new DoubleAnimation(0, 170, TimeSpan.FromSeconds(0.3));
                rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
                textBox1.Text = He.Settings.SearchUrl;
            }
            else
            {
                os = 0;
                RotateTransform rtf = new RotateTransform();
                (sender as Border).RenderTransform = rtf;
                DoubleAnimation dbAscending = new DoubleAnimation(170, 0, TimeSpan.FromSeconds(0.3));
                rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
                He.Settings.SearchUrl = textBox1.Text;
                He.SaveSettings();
                textBox1.Text = "搜索";
            }
        }
    }
}
