using Newtonsoft.Json.Linq;
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
    /// MIDITEM.xaml 的交互逻辑
    /// </summary>
    public partial class MIDITEM : UserControl
    {
        public MIDITEM()
        {
            InitializeComponent();
        }
        public string MID { get; set; }
        public async void SETMIDAsync(string MID)
        {
            this.MID = MID;
            var s = await Uuuhh.GetWebDatacAsync($"https://c.y.qq.com/qzone/fcg-bin/fcg_ucc_getcdinfo_byids_cp.fcg?type=1&json=1&utf8=1&onlysong=0&disstid={MID}&format=json&g_tk=5381&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0", Encoding.UTF8);
            JObject o = JObject.Parse(s);
            MIDNAME.Text = o["cdlist"][0]["dissname"].ToString();
            MIDIMAGE.Background = new ImageBrush(new BitmapImage(new Uri(o["cdlist"][0]["logo"].ToString())));
        }

        private void border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            He.Settings.MIDLIST.Remove(this.MID);
            He.SaveSettings();
        }
    }
}
