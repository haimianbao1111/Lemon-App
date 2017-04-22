using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
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
    /// FanyiBox.xaml 的交互逻辑
    /// </summary>
    public partial class FanyiBox : UserControl
    {
        public FanyiBox()
        {
            InitializeComponent();
        }
        public static string 取出中间文本(string 欲取全文本, string 前面文本, string 后面的文本, int 起始搜寻位置)
        {

            int 位置A = 欲取全文本.IndexOf(前面文本, 起始搜寻位置);
            int 位置B = 欲取全文本.IndexOf(后面的文本, 位置A + 1);
            if (位置A < 0 || 位置B < 0)
            {
                return "";
            }
            else
            {
                位置A = 位置A + 前面文本.Length;
                位置B = 位置B - 位置A;
                if (位置A < 0 || 位置B < 0)
                {
                    return "";
                }
                return 欲取全文本.Substring(位置A, 位置B);
            }

        }
        public static string DecodeUtf8(string sourceStr)
        {
            Regex regex = new Regex(@"\\u(\w{4})");
            string result = regex.Replace(sourceStr, delegate (Match m)
            {
                string hexStr = m.Groups[1].Value;
                string charStr = ((char)int.Parse(hexStr, System.Globalization.NumberStyles.HexNumber)).ToString();
                return charStr;
            });
            return result;
        }
        string to = "en";
        private async void button_Cli(object sender, MouseButtonEventArgs e)
        {
            if (textBox1.Text != "")
            {
                string q = Uri.EscapeDataString(textBox1.Text);
                string from = "auto";
                string sign = MD5.EncryptToMD5string("20151231000008489" + textBox1.Text + "2004112629Q3EQP1ay2cLKAMxs2gqa");
                if (Yyycombo.Text == "自动")
                {
                    from = "auto";
                }
                if (Yyycombo.Text == "中文")
                {
                    from = "zh";
                }
                if (Yyycombo.Text == "粤语")
                {
                    from = "yue";
                }
                if (Yyycombo.Text == "英语")
                {
                    from = "en";
                }
                if (Yyycombo.Text == "文言文")
                {
                    from = "wyw";
                }
                if (Yyycombo.Text == "日语")
                {
                    from = "jp";
                }
                if (Yyycombo.Text == "韩语")
                {
                    from = "kor";
                }
                if (Yyycombo.Text == "法语")
                {
                    from = "fra";
                }
                if (Yyycombo.Text == "西班牙语")
                {
                    from = "spa";
                }
                if (Yyycombo.Text == "俄语")
                {
                    from = "ru";
                }
                if (Yyycombo.Text == "泰语")
                {
                    from = "th";
                }
                if (Yyycombo.Text == "阿拉伯语")
                {
                    from = "ara";
                }
                if (Yyycombo.Text == "德语")
                {
                    from = "de";
                }
                if (Yyycombo.Text == "意大利语")
                {
                    from = "it";
                }
                if (Yyycombo.Text == "希腊语")
                {
                    from = "el";
                }
                if (Yyycombo.Text == "荷兰语")
                {
                    from = "nl";
                }
                if (Yyycombo.Text == "波兰语")
                {
                    from = "pl";
                }
                if (Yyycombo.Text == "保加利亚语")
                {
                    from = "bul";
                }
                if (Yyycombo.Text == "爱沙尼亚语")
                {
                    from = "auto";
                }
                if (Yyycombo.Text == "丹麦语")
                {
                    from = "dan";
                }
                if (Yyycombo.Text == "芬兰语")
                {
                    from = "fin";
                }
                if (Yyycombo.Text == "捷克语")
                {
                    from = "cs";
                }
                if (Yyycombo.Text == "罗马尼亚语")
                {
                    from = "rom";
                }
                if (Yyycombo.Text == "斯洛文尼亚语")
                {
                    from = "slo";
                }
                if (Yyycombo.Text == "瑞典语")
                {
                    from = "swe";
                }
                if (Yyycombo.Text == "匈牙利语")
                {
                    from = "hu";
                }
                if (Yyycombo.Text == "繁体中文")
                {
                    from = "cht";
                }
                if (HyycomboBox.Text == "中文")
                {
                    to = "zh";
                }
                if (HyycomboBox.Text == "粤语")
                {
                    to = "yue";
                }
                if (HyycomboBox.Text == "英语")
                {
                    to = "en";
                }
                if (HyycomboBox.Text == "文言文")
                {
                    to = "wyw";
                }
                if (HyycomboBox.Text == "日语")
                {
                    to = "jp";
                }
                if (HyycomboBox.Text == "韩语")
                {
                    to = "kor";
                }
                if (HyycomboBox.Text == "法语")
                {
                    to = "fra";
                }
                if (HyycomboBox.Text == "西班牙语")
                {
                    to = "spa";
                }
                if (HyycomboBox.Text == "俄语")
                {
                    to = "ru";
                }
                if (HyycomboBox.Text == "泰语")
                {
                    to = "th";
                }
                if (HyycomboBox.Text == "阿拉伯语")
                {
                    to = "ara";
                }
                if (HyycomboBox.Text == "德语")
                {
                    to = "de";
                }
                if (HyycomboBox.Text == "意大利语")
                {
                    to = "it";
                }
                if (HyycomboBox.Text == "希腊语")
                {
                    to = "el";
                }
                if (HyycomboBox.Text == "荷兰语")
                {
                    to = "nl";
                }
                if (HyycomboBox.Text == "波兰语")
                {
                    to = "pl";
                }
                if (HyycomboBox.Text == "保加利亚语")
                {
                    to = "bul";
                }
                if (HyycomboBox.Text == "爱沙尼亚语")
                {
                    to = "auto";
                }
                if (HyycomboBox.Text == "丹麦语")
                {
                    to = "dan";
                }
                if (HyycomboBox.Text == "芬兰语")
                {
                    to = "fin";
                }
                if (HyycomboBox.Text == "捷克语")
                {
                    to = "cs";
                }
                if (HyycomboBox.Text == "罗马尼亚语")
                {
                    to = "rom";
                }
                if (HyycomboBox.Text == "斯洛文尼亚语")
                {
                    to = "slo";
                }
                if (HyycomboBox.Text == "瑞典语")
                {
                    to = "swe";
                }
                if (HyycomboBox.Text == "匈牙利语")
                {
                    to = "hu";
                }
                if (HyycomboBox.Text == "繁体中文")
                {
                    to = "cht";
                }
                try
                {
                    string o = "http://api.fanyi.baidu.com/api/trans/vip/translate?q=" + q + "&from=" + from + "&to=" + to + "&appid=20151231000008489&salt=2004112629" + "&sign=" + sign;
                    JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://api.fanyi.baidu.com/api/trans/vip/translate?q=" + q + "&from=" + from + "&to=" + to + "&appid=20151231000008489&salt=2004112629" + "&sign=" + sign));
                    FanyiFromtoTextBox.Text = DecodeUtf8(obj["trans_result"][0]["dst"].ToString());
                }
                catch { }
            }
        }

        private  void button1_Click(object sender, MouseButtonEventArgs e)
        {
           
        }

        private  void label3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (FanyiFromtoTextBox.Text != "")
            {
                MediaPlayer p = new MediaPlayer();
                p.Open(new Uri($"http://fanyi.baidu.com/gettts?lan={to}&text={FanyiFromtoTextBox.Text}&spd=3&source=web"));
                p.Play();
                p.MediaEnded += delegate { p.Close(); };
            }
        }

        private async void FanyiFromtoTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!Uuuhh.Lalala("www.mi.com"))
            {
                textBlock.Text = "无法连接到互联网！";
            }
            else
            {
                string o = await Uuuhh.GetWebAsync("http://dict-co.iciba.com/api/dictionary.php?w=" + Uri.EscapeUriString(textBox1.Text) + "&key=04C75B1C14FAEA63A0DDA93FE527EA0A");
                textBlock.Text = "vi." + 取出中间文本(o, "<acceptation>", "</acceptation>", 0) + "\r\n" + 取出中间文本(o, "<sent><orig>", "</orig>", 0) + 取出中间文本(o, "<trans>", "</trans></sent>", 0);
            }
        }
        public class MD5
        {
            public static byte[] EncryptToMD5(string str)
            {
                MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
                byte[] str1 = System.Text.Encoding.UTF8.GetBytes(str);
                byte[] str2 = md5.ComputeHash(str1, 0, str1.Length);
                md5.Clear();
                (md5 as IDisposable).Dispose();
                return str2;
            }
            public static string EncryptToMD5string(string str)
            {
                byte[] bytHash = EncryptToMD5(str);
                string sTemp = "";
                for (int i = 0; i < bytHash.Length; i++)
                {
                    sTemp += bytHash[i].ToString("X").PadLeft(2, '0');
                }
                return sTemp.ToLower();
            }
        }
    }
}
