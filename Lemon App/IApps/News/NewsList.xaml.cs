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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lemon_App
{
    /// <summary>
    /// NewsList.xaml 的交互逻辑
    /// </summary>
    public partial class NewsList : UserControl
    {
        public NewsList(string title,string time,string sousce,string url,string text)
        {
            InitializeComponent();
            this.title.Inlines.Clear();
            this.title.Inlines.Add(new Run(title));
            this.title.Inlines.Add(new LineBreak());
            this.title.Inlines.Add(new Run(text) { Foreground = new SolidColorBrush(Color.FromArgb(255, 99, 99, 99)) });
            this.Source.Text = sousce + "  " + time;
            uri = url;
        }
        string uri = "";
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start(uri);
        }
    }
}
