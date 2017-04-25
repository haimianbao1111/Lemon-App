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
            qm.Text = Settings.Default.qm;
            if (System.IO.File.Exists(Settings.Default.UserImage))
            { TX.Background = new ImageBrush(new BitmapImage(new Uri(Settings.Default.UserImage, UriKind.Absolute))); }
            NM.Text = Settings.Default.RobotName;
            if (Settings.Default.LZone!="null")
            {
                data = (List<string>)JSON.JsonToObject(Settings.Default.LZone, data);
                for (int i = 0; i != data.Count; i++)
                {
                    var co = new LZoneItemControl();
                    co.S.MouseDown += Mou;
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
            if (so.Text != string.Empty)
            {
                var co = new LZoneItemControl();
                co.S.MouseDown += Mou;
                co.QZoneData = so.Text;
                co.BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
                this.QzoneDataContent.Children.Insert(0, co);
                data.Add(so.Text);
                //   MessageBox.Show(JSON.ToJSON(data));
                Settings.Default.LZone = JSON.ToJSON(data);
                Settings.Default.Save();
                so.Text = "";
            }
         }

        private void Mou(object sender, MouseButtonEventArgs e)
        {
            data.Remove((sender as Border).ToolTip as string);
            Settings.Default.LZone = JSON.ToJSON(data);
            Settings.Default.Save();
           // MessageBox.Show(JSON.ToJSON(data));
        }

        private void qm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Settings.Default.qm = qm.Text;
                Settings.Default.Save();
            }
        }
        int iss = 0;
        private void Path_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (iss==0)
            {
                iss = 1;
                ss.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2)));
                st.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
            }else if(iss==1)
            {
                iss = 0;
                ss.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
                st.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2)));
            }
        }
    }
}
