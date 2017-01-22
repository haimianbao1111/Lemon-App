using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Windows.UI.Notifications;
using System.Drawing;
using ZXing;
using ZXing.QrCode;
using ZXing.Common;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;
using Lemon_App.Properties;
using System.Windows.Controls;

//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂
//                                                                                          🙂
//                        by：Twilight                                               🙂
//                                                                                          🙂
//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂

namespace Lemon_App
{
    public class Theme
    {
        public string ThemeName { get; set; }
        public string ThemeImageUri { get; set; }
        public string ThemeFont { get; set; }
        public string A { get; set; }
        public string WeatherIcon { get; set; }
        public string BingIcon { get; set; }
        public string FanyiIcon { get; set; }
        public string QRIcon { get; set; }
        public string UFIcon { get; set; }
        public string RobotImage { get; set; }
        public string CMDIcon { get; set; }
        public string MusicIcon { get; set; }
        public string SSIcon { get; set; }
        public string RBTIcon { get; set; }
        public string NEWSIcon { get; set; }
        public string STIcon { set; get; }
    }

    public class LemonWeather
    {

        public LemonWeather(string info)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create($"https://api.heweather.com/v5/now?city={Uri.EscapeUriString(info)}&key=f97e6a6ad4cd49babd0538747c86b88d");
                hwr.Proxy = He.proxy;
                StreamReader sr = new StreamReader(hwr.GetResponse().GetResponseStream());
                string html5 = sr.ReadToEnd().Replace("HeWeather5", "Weather");
                JObject obj = JObject.Parse(html5);
                WeatherName = obj["Weather"][0]["basic"]["city"].ToString();
                WeatherMessage = obj["Weather"][0]["now"]["tmp"] + "℃   " + obj["Weather"][0]["now"]["cond"]["txt"];
                WeatherIcon = obj["Weather"][0]["now"]["cond"]["code"].ToString();
                sr.Dispose();
            }
            catch { }
        }
        public string WeatherMessage { get; private set; }
        public string WeatherName { get; private set; }
        public string WeatherIcon { get; private set; }
    }
    public class He
    {
        public static  string Text(string all, string r, string l, int t)
        {

            int A = all.IndexOf(r, t);
            int B =all.IndexOf(l, A + 1);
            if (A < 0 || B < 0)
            {
                return null;
            }
            else
            {
                A = A + r.Length;
                B = B - A;
                if (A < 0 || B < 0)
                {
                    return null;
                }
                return all.Substring(A, B);
            }

        }
        public static WebProxy proxy = new WebProxy();
        public static string Url = "";
        public static string KMS = "3.4.6.0";
        public static int MS = 0;
        public static string on = "";
        public static string EmailUFMsg = @"<table dir=""ltr"">
    <tbody>
        <tr>
            <td id = ""i1"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:17px; color:#707070;"">
                Lemon App 用户反馈
            </td>
        </tr>
        <tr>
            <td id = ""i2"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:41px; color:#2672ec;"">
                用户反馈
            </td>
        </tr>
        <tr>
            <td id = ""i3"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                新的Lemon App用户反馈
            </td>
        </tr>
        <tr>
            <td id = ""i4"" style= ""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"" >
                状态：
                <b>
                    {ninini} &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;邮箱</b></td><td id = ""i4"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">：
                <b>
                    {nihaoyouxiang}
                </b>
            </td>
        </tr>
        <tr>
            <td id = ""i5"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                {lalalala}
            </td>
        </tr>
        <tr>
            <td id = ""i6"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                谢谢!
            </td>
        </tr>
        <tr>
            <td id = ""i7"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                Lemon App 用户反馈团队
            </td>
        </tr>
    </tbody>
</table>";
        public static string EmailMessage = @"<table dir=""ltr"">
    <tbody>
        <tr>
            <td id = ""i1"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:17px; color:#707070;"">
                Lemon App 帐户
            </td>
        </tr>
        <tr>
            <td id = ""i2"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:41px; color:#2672ec;"">
                验证码
            </td>
        </tr>
        <tr>
            <td id = ""i3"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                请为 Lemon App 帐户
                使用以下验证码登录。
            </td>
        </tr>
        <tr>
            <td id = ""i4"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                验证码：
                <b>
                    {ninini}
                </b>
            </td>
        </tr>
        <tr>
            <td id = ""i5"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                如果你无法识别此Lemon App 帐户，可以忽略此电子邮件
            </td>
        </tr>
        <tr>
            <td id = ""i6"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                谢谢!
            </td>
        </tr>
        <tr>
            <td id = ""i7"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                Lemon App 帐户团队
            </td>
        </tr>
    </tbody>
</table>";

        public static string EmailEorre = @"<table dir=""ltr"">
    <tbody>
        <tr>
            <td id = ""i1"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:17px; color:#707070;"">
                Lemon App 异常报告
            </td>
        </tr>
        <tr>
            <td id = ""i2"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:41px; color:#2672ec;"">
                异常:
            </td>
        </tr>
        <tr>
            <td id = ""i3"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                在Lemon App中引发了异常
            </td>
        </tr>
        <tr>
            <td id = ""i4"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                异常码：
                <b>
                    {ninini}
                </b>
            </td>
        </tr>
        <tr>
            <td id = ""i6"" style=""padding:0; padding-top:25px; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                此邮件由LemonApp自动发出!
            </td>
        </tr>
        <tr>
            <td id = ""i7"" style=""padding:0; font-family:'Microsoft Yahei', Verdana, Simsun, sans-serif; font-size:14px; color:#2a2a2a;"">
                Lemon App 团队
            </td>
        </tr>
    </tbody>
</table>";
        public static BitmapImage RobotImage = null;
        /// <summary>
        /// 发送HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="param">请求的参数</param>
        /// <returns>请求结果</returns>
        public static string request(string url, string param)
        { try
            {
                string strURL = url + '?' + param;
                System.Net.HttpWebRequest request;
                request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
                request.Method = "GET";
                request.Proxy = He.proxy;
                // 添加header
                request.Headers.Add("apikey", "d2eb2545efba96cd3c9b6cf0cd07f135");
                System.Net.HttpWebResponse response;
                response = (System.Net.HttpWebResponse)request.GetResponse();
                System.IO.Stream s;
                s = response.GetResponseStream();
                string StrDate = "";
                string strValue = "";
                StreamReader Reader = new StreamReader(s, Encoding.UTF8);
                while ((StrDate = Reader.ReadLine()) != null)
                {
                    strValue += StrDate + "\r\n";
                }
                return strValue;
            }
            catch { return null; }
            }
    }

    public class Lemon_Updata
    {
        public static async System.Threading.Tasks.Task<bool> IsLemonNew(string con)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("http://cname.lemonapp.tk/Updata.txt");
                hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream());
                string html5 =await sr.ReadToEndAsync();
                if (con == html5)
                    return false;
                else return true;
            }
            catch { return false; }
        }
        public static async System.Threading.Tasks.Task<string> NewText(string con)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("http://cname.lemonapp.tk/UpdataTetx.txt");
            hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream());
            var st= await sr.ReadToEndAsync();
            return st.Replace(".", "\r\n");
        }

    }
    public class ToastNotion
    {
        private string doc = "";
        public ToastNotion(string I)
        {
            doc = I;
        }
        public void Show()
        {
                Windows.Data.Xml.Dom.XmlDocument xml = new Windows.Data.Xml.Dom.XmlDocument();
                xml.LoadXml(doc);
                ToastNotification toast = new ToastNotification(xml);
                ToastNotificationManager.CreateToastNotifier("Lemon App").Show(toast);
        } 
        public override string ToString()
        {
            return doc;
        }
    }
    public class Toast
    {
        public static ToastNotion SetToastNotion(string i, string ii, string iii)
        {
            string doc = "<toast lang=\"zh-CN\">" +
              "<visual>" +
                  "<binding template=\"ToastGeneric\">" +
                      "<text>" + i + "</text>" +
                      "<text>" + ii + "</text>" +
                      "<text>" + iii + "</text>" +
                  "</binding>" +
              "</visual>" +
           "</toast>";
            return new ToastNotion(doc);
        }
    }

    public class QRCode
    {
        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);
        public static ImageSource GetQRCode(String content, int width, int height, System.Drawing.Color c, BarcodeFormat BF= BarcodeFormat.QR_CODE )
        {
            EncodingOptions options;
            BarcodeWriter write = null;
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = width,
                Height = height,
                Margin = 0
            };
            write = new BarcodeWriter();
            write.Renderer = new ZXing.Rendering.BitmapRenderer { Background = System.Drawing.Color.Transparent, Foreground =c };
            write.Format = BF;
            write.Options = options;
            Bitmap bitmap = write.Write(content);
            IntPtr ip = bitmap.GetHbitmap();
            BitmapSource bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty,
            BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }
        
    }

    public class Uuuhh
    {
        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            if(Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream(),Encoding.UTF8);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }
        public static async System.Threading.Tasks.Task<string> GetWebUAsync(string url)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream(), Encoding.Default);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }

        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url,Encoding c)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream(), c);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }
        public static string GetWeb(string url)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
            StreamReader sr = new StreamReader(hwr.GetResponse().GetResponseStream(), Encoding.Default);
            var st = sr.ReadToEnd();
            sr.Dispose();
            return st;
        }
        public static string PingErrorMsg = "";
        public static bool Lalala(string ip)
        {try
            {
                Ping ping = new Ping();
                PingReply La = ping.Send(ip);
                PingErrorMsg = La.Status.ToString();
                if (La.Status == IPStatus.Success)
                    return true;
                else
                    return false;
            }
            catch { return false; }
        }
    }
    public class ListItem
    {
        public string[] ItemText { get; set; }
    }

    public class ListJson
    {
        public List<ListItem> List { get; set; }
    }
}
