using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
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

namespace Lemon_App.IApps.Bing
{
    /// <summary>
    /// MapControl.xaml 的交互逻辑
    /// </summary>
    public partial class MapControl : UserControl
    {
        public MapControl()
        {
            InitializeComponent();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.map.Mode = new RoadMode();
        }

        private void Grid_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.map.Mode = new AerialMode(false);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            double latitude = 0;
            double longitude = 0;

            double.TryParse(j.Text, out latitude);
            double.TryParse(w.Text, out longitude);
            this.map.SetView(new Location(latitude, longitude), 5);
        }

        private void Grid_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if(jw.Opacity==0)
               jw.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
            else jw.BeginAnimation(OpacityProperty, new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2)));
        }

        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            if (map.ZoomLevel <= 15&&map.ZoomLevel >3) map.ZoomLevel--;
        }

        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            if (map.ZoomLevel >=3&&map.ZoomLevel<15) map.ZoomLevel++;
        }
    }
}
