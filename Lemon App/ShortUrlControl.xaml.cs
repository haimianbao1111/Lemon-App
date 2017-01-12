using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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
    /// ShortUrlControl.xaml 的交互逻辑
    /// </summary>
    public partial class ShortUrlControl : UserControl
    {
        public ShortUrlControl()
        {
            InitializeComponent();
        }

        private void label1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (From.Text != "")
                {
                    HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create($"http://ni2.org/api/create?url={Uri.EscapeUriString(From.Text)}&user_key=f5f50d6ef60639954e24403e5e7167ad");
                    StreamReader sr = new StreamReader(hwr.GetResponse().GetResponseStream(),Encoding.UTF8);
                    To.Text = sr.ReadToEnd();
                }
            }
            catch { }
        }
    }
}
