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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App.IApps.User
{
    /// <summary>
    /// LZoneItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class LZoneItemControl : UserControl
    {
        public LZoneItemControl()
        {
            InitializeComponent();
        }
        public string QZoneData { set { CA.Text = value; S.ToolTip = CA.Text; } get { return CA.Text; } }

        private void S_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var da = new System.Windows.Media.Animation.DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            da.Completed += delegate
            {
                Visibility = Visibility.Collapsed;
            };
            BeginAnimation(OpacityProperty, da);
        }
    }
}
