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

namespace Lemon_App
{
    /// <summary>
    /// MusicItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class MusicItemControl : UserControl
    {
        public MusicItemControl()
        {
            InitializeComponent();
        }
        public string MusicName
        {
            get {return Nm.Text; } set { Nm.Text = value; }
        }
        public new string Content
        {
            get { return MusicName + "-" + MusicGS; }
        }
        public string MusicGS
        {
            get { return GS.Text; } set { GS.Text = value; }
        }
        public string MusicZJ
        {
            get { return ZJ.Text; }
            set { ZJ.Text = value; }
        }
        public string Qt
        {
          get { return Q.Text; }
            set { if (value != "") if (value == "SQ") { Q.Text = value; RS.Visibility = Visibility.Visible; } else if (value == "HQ") { QC.Text = value; RC.Visibility = Visibility.Visible; } }
        }
        public Object M;
        public object Music { get { return M; } set { M = value; } }
    }
}
