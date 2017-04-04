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
    /// MusicSZ.xaml 的交互逻辑
    /// </summary>
    public partial class MusicSZ : UserControl
    {
        public MusicSZ()
        {
            InitializeComponent();
        }
        public new string Content
        {
            get { return nm.Text; }
            set { nm.Text = value; }
        }
        public new Brush Background
        {
            get { return ig.Background; }
            set { ig.Background = value; }
        }
    }
}
