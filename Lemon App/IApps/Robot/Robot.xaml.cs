using System.Windows.Controls;
using System.Windows.Media;

namespace Lemon_App
{
    /// <summary>
    /// Robot.xaml 的交互逻辑
    /// </summary>
    public partial class Robot : UserControl
    {
        public Robot()
        {
            InitializeComponent();
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.Fant);
        }
        public Robot(string Text)
        {
            InitializeComponent();
            Te.Text = Text;
            RenderOptions.SetBitmapScalingMode(Image, BitmapScalingMode.Fant);
        }
    }
}
