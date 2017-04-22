using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Media.Imaging;
//using Windows.UI.Notifications;
using Lemon_App.Properties;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Runtime.Serialization.Json;
using System.Web.Script.Serialization;
using System.Net.Http;
using System.Threading.Tasks;

//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂
//                                                                                          🙂
//                        by：Twilight                                               🙂
//                                                                                          🙂
//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂

namespace Lemon_App
{
    public class PopopHelper
    {
        public static DependencyObject GetPopupPlacementTarget(DependencyObject obj)
        {
            return (DependencyObject)obj.GetValue(PopupPlacementTargetProperty);
        }

        public static void SetPopupPlacementTarget(DependencyObject obj, DependencyObject value)
        {
            obj.SetValue(PopupPlacementTargetProperty, value);
        }

        // Using a DependencyProperty as the backing store for PopupPlacementTarget.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupPlacementTargetProperty =
            DependencyProperty.RegisterAttached("PopupPlacementTarget", typeof(DependencyObject), typeof(PopopHelper), new PropertyMetadata(null, OnPopupPlacementTargetChanged));

        private static void OnPopupPlacementTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue != null)
            {
                DependencyObject popupPopupPlacementTarget = e.NewValue as DependencyObject;
                Popup pop = d as Popup;

                var w = Window.GetWindow(popupPopupPlacementTarget);
                if (null != popupPopupPlacementTarget)
                {
                    w.LocationChanged += delegate
                    {
                        var offset = pop.HorizontalOffset;
                        pop.HorizontalOffset = offset + 1;
                        pop.HorizontalOffset = offset;
                    };
                }
            }
        }

    }
    public class ExceptionItem
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 版本号
        /// </summary>
        public string KMS { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 对象
        /// </summary>
        public string Source { get; set; }
        /// <summary>
        /// 方法
        /// </summary>
        public string TargetSite { get; set; }
        /// <summary>
        /// 链接
        /// </summary>
        public string HelpLink { get; set; }
        /// <summary>
        /// 堆
        /// </summary>
        public string StackTrace { get; set; }
    }

    public class EX
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ExceptionItem> Exception { get; set; }
    }
    public class JSON
    {
        public static object JsonToObject(string jsonString, object obj)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(obj.GetType());
            MemoryStream mStream = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
            return serializer.ReadObject(mStream);
        }

        public static string ToJSON(object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;
            return serializer.Serialize(obj);
        }
    }
    public class LemonWeather
    {

        public LemonWeather(string info)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create($"https://api.heweather.com/v5/now?city={Uri.EscapeUriString(info)}&key=f97e6a6ad4cd49babd0538747c86b88d");
                if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
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
        public static string KMS = "3.5.6.3";
        public static int MS = 0;
        public static string on = "";

       /* public static string EmailEorre = @"<table dir=""ltr"">
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
</table>";*/
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
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("http://api.lemonapp.tk/Updata.php");
                hwr.Headers.Add("Cookie", "User=d2eb2545efba96cd3c9b6cf0cd07f135;");
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
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("http://api.lemonapp.tk/UpdataText.php");
            hwr.Headers.Add("Cookie", "User=d2eb2545efba96cd3c9b6cf0cd07f135;");
            hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream());
            var st= await sr.ReadToEndAsync();
            return st.Replace(".", "\r\n");
        }
    }
    //public class ToastNotion
    //{
    //    private string doc = "";
    //    public ToastNotion(string I)
    //    {
    //        doc = I;
    //    }
    //    public void Show()
    //    {
    //        string os = Environment.OSVersion.Version.Major.ToString() +"."+ Environment.OSVersion.Version.Minor.ToString();
    //        if (os == "10.0" || os == "6.3" || os == "6.2")
    //        {
    //            Windows.Data.Xml.Dom.XmlDocument xml = new Windows.Data.Xml.Dom.XmlDocument();
    //            xml.LoadXml(doc);
    //            ToastNotification toast = new ToastNotification(xml);
    //            ToastNotificationManager.CreateToastNotifier("Lemon App").Show(toast);
    //        }
    //    } 
    //    public override string ToString()
    //    {
    //        return doc;
    //    }
    //}
    //public class Toast
    //{
    //    public static ToastNotion SetToastNotion(string i, string ii, string iii)
    //    {
    //        string doc = "<toast lang=\"zh-CN\">" +
    //          "<visual>" +
    //              "<binding template=\"ToastGeneric\">" +
    //                  "<text>" + i + "</text>" +
    //                  "<text>" + ii + "</text>" +
    //                  "<text>" + iii + "</text>" +
    //              "</binding>" +
    //          "</visual>" +
    //       "</toast>";
    //        return new ToastNotion(doc);
    //    }
    //}


    public class Uuuhh
    {

        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Timeout = 20000;
                if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream(), Encoding.UTF8);
                var st = await sr.ReadToEndAsync();
                sr.Dispose();
                return st;
            }
            catch { return ""; }
        }
        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url,Encoding c)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Timeout = 20000;
                if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream(), c);
                var st = await sr.ReadToEndAsync();
                sr.Dispose();
                return st;
            }
            catch { return ""; }
        }
        public static async Task<string> PostWebJSONAsync(string url, string param)
        {
            string strURL = url;
            System.Net.HttpWebRequest request;
            request = (System.Net.HttpWebRequest)WebRequest.Create(strURL);
            request.Method = "POST";
            request.ContentType = "application/json;charset=UTF-8";
            string paraUrlCoded = param;
            byte[] payload;
            payload = System.Text.Encoding.UTF8.GetBytes(paraUrlCoded);
            request.ContentLength = payload.Length;
            Stream writer =await request.GetRequestStreamAsync();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)await request.GetResponseAsync();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate =await Reader.ReadLineAsync()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }
        public static async Task<string> PostWebAsync(string url,string idata)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(idata);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream =await request.GetRequestStreamAsync())
            {
              await  stream.WriteAsync(data, 0, data.Length);
            }

            var response = (HttpWebResponse) await request.GetResponseAsync();

            var r = new StreamReader(response.GetResponseStream());
        //    System.Windows.MessageBox.Show(await r.ReadToEndAsync());
            return await r.ReadToEndAsync();
        }
        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url, bool isOpen)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Headers.Add(HttpRequestHeader.UserAgent, " Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36");
                hwr.Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                hwr.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch, br");
                hwr.Headers.Add(HttpRequestHeader.AcceptLanguage, " zh-CN,zh;q=0.8");
                hwr.Headers.Add(HttpRequestHeader.Cookie, "tvfe_boss_uuid=c3db0dcc4d677c60; pac_uid=1_2728578956; ts_refer=ADTAGYQQ; qq_slist_autoplay=on; yq_index=0; pgv_pvi=5341831168; RK=gLPObA2/3O; ptui_loginuin=2728578956; o_cookie=2728578956; ptcz=897c17d7e17ae9009e018ebf3f818355147a3a26c6c67a63ae949e24758baa2d; pt2gguin=o2728578956; pgv_pvid=5107924810; ts_uid=996779984");
          //      hwr.Headers.Add(HttpRequestHeader.IfModifiedSince, "");
                hwr.Timeout = 20000;
                if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream());
                var st = await sr.ReadToEndAsync();
                sr.Dispose();
                return st;
            }
            catch { return ""; }
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
        public Music ItemText { get; set; }
    }

    public class ListJson
    {
        public List<ListItem> List { get; set; }
    }

    public class Music
    {
        public Music() { }
     /// <summary>
    /// 歌曲名称
    /// </summary>
        public string MusicName { set; get; }
        /// <summary>
        /// 歌手
        /// </summary>
        public string Singer { set; get; }
        /// <summary>
        /// 用于播放的音乐ID
        /// </summary>
        public string MusicID { set; get; }
        /// <summary>
        /// 用于获取图像的ID
        /// </summary>
        public string ImageID { set; get; }
        /// <summary>
        /// 专辑名称
        /// </summary>
        public string   ZJ { set; get; }
        /// <summary>
        /// SQ品质
        /// </summary>
        public string Fotmat { set; get; }
        /// <summary>
        /// HQ品质
        /// </summary>
        public string HQFOTmat { set; get; }
        /// <summary>
        /// 歌词ID
        /// </summary>
        public string GC { set; get; }
        /// <summary>
        /// mv ID
        /// </summary>
        public string MV { set; get; }
        /// <summary>
        /// 是否为排行榜歌单
        /// </summary>
        public Boolean IsDF { set; get; }
        /// <summary>
        /// 排行榜歌单的标准品质uri
        /// </summary>
        public string DFSONGURI { set; get; }
        /// <summary>
        /// 排行榜歌HQ
        /// </summary>
        public string DFSONGURI_HQ { set; get; }
    }
}
