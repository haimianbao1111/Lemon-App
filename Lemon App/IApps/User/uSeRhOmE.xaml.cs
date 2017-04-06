using Lemon_App.IApps.User;
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
            { TX.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
            NM.Text = Settings.Default.RobotName;
            if (File.Exists(AppDomain.CurrentDomain.BaseDirectory+Settings.Default.LemonAreeunIts+".data"))
            {
                data = (List<string>)JSON.JsonToObject(File.ReadAllText(AppDomain.CurrentDomain.BaseDirectory + Settings.Default.LemonAreeunIts + ".data"), data);
                for (int i = 0; i != data.Count; i++)
                {
                    var co = new LZoneItemControl();
                    co.QZoneData = data[i];
                    this.QzoneDataContent.Children.Insert(0, co);
                }
            }
        }

        private void TX_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog o = new Microsoft.Win32.OpenFileDialog();
            if (o.ShowDialog() == true)
            {
                TX.Background = new ImageBrush(new BitmapImage(new Uri(o.FileName, UriKind.Absolute)));
                Settings.Default.UserImage = o.FileName;
                Settings.Default.Save();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var co = new LZoneItemControl();
            co.QZoneData = so.Text;
            this.QzoneDataContent.Children.Insert(0, co);
            data.Add(so.Text);
            //   MessageBox.Show(JSON.ToJSON(data));
            File.AppendAllText(AppDomain.CurrentDomain.BaseDirectory + Settings.Default.LemonAreeunIts + ".data", JSON.ToJSON(data));
         }
    }
}
