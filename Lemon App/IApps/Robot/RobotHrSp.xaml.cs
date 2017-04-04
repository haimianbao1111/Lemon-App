using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// RobotHrSp.xaml 的交互逻辑
    /// </summary>
    public partial class RobotHrSp : UserControl
    {
        public RobotHrSp(List<string>name,List<string>infos,List<string>url)
        {
            try
            {
                InitializeComponent();
                IName.Text = name[0];
                IName1.Text = name[1];
                IName2.Text = name[2];
                IName3.Text = name[3];
                IName4.Text = name[4];
                info.Text = infos[0];
                info1.Text = infos[1];
                info2.Text = infos[2];
                info3.Text = infos[3];
                info4.Text = infos[4];
                c.ToolTip = url[0];
                c1.ToolTip = url[1];
                c2.ToolTip = url[2];
                c3.ToolTip = url[3];
                c4.ToolTip = url[4];
            }
            catch { }
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start((sender as Grid).ToolTip.ToString());
        }
    }
}
