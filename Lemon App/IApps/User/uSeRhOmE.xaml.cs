﻿using Lemon_App.IApps.User;
using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
    /// uSeRhOmE.xaml 的交互逻辑
    /// </summary>
    public partial class uSeRhOmE : UserControl
    {
        public uSeRhOmE()
        {
            InitializeComponent();
        }
        public Brush TXIMAGE
        {
            get { return TX.Background; }
            set { TX.Background = value; }
        }
        List<string> data = new List<string>();
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (System.IO.File.Exists(Settings.Default.UserImage))
            {
                var image = new System.Drawing.Bitmap(Settings.Default.UserImage);
                TX.Background = new ImageBrush(image.ToImageSource());
            }
            NM.Text = Settings.Default.RobotName;
        }

        private void TX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog o = new Microsoft.Win32.OpenFileDialog();
            if (o.ShowDialog() == true)
            {
                var image = new System.Drawing.Bitmap(o.FileName);
                TX.Background = new ImageBrush(image.ToImageSource());
                Settings.Default.UserImage = o.FileName;
                Settings.Default.Save();
            }
        }

        private void Mou(object sender, MouseButtonEventArgs e)
        {
            data.Remove((sender as Border).ToolTip as string);
            Settings.Default.LZone = JSON.ToJSON(data);
            Settings.Default.Save();
           // MessageBox.Show(JSON.ToJSON(data));
        }
    }
}
