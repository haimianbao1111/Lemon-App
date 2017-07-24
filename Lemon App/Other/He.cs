using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Media.Imaging;
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
using System.Security;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Windows.Data;
using System.Globalization;

//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂
//                                                                                          🙂
//                        by：Twilight                                               🙂
//                                                                                          🙂
//🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂🙂

namespace Lemon_App
{

    public class PercentToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var percent =double.Parse( value.ToString());
            if (percent >= 1) return 360.0D;
            return percent * 360;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class ThicknessToDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var thickness = (Thickness)value;
            return thickness.Left;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DependencyProperty.UnsetValue;
        }
    }
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
    public class SettingsData
    {
        public string SearchUrl { get; set; } = "https://www.baidu.com/s?tn=mswin_oem_dg&ie=utf-16&word=%2a";
        public string RobotName { get; set; } = "LemonUser";
        public string WeatherInfo { get; set; } = "北京";
        public bool RNBM { get; set; } = false;
        public bool IsStart { get; set; } = false;
        public string MusicList { get; set; } = "{\"List\":[]}";
        public string WebProxyUri { get; set; } = "";
        public string WebProxyUser { get; set; } = "";
        public string WebProxyPassWord { get; set; } = "";
        public bool isWebProxy { get; set; } = false;
        public OnRect HaTop { get; set; } = new OnRect();
        public string UserImage { get; set; } = "";
        public string ZJid { get; set; } = "2591355982";
        public string LemonAreeunIts { get; set; } = "你的QQ";
        public int sx { get; set; } = 0;
        public string FontFamilly { get; set; } = ".PingFang SC,Microsoft Yahei UI";
        public string MusicGD { get; set; } = "{\"List\":[]}";
        public string MusicSearch { get; set; } = "null";
    }
    public class OnRect
    {
        public double x = 0;
        public double y = 0;
        public double width = 0;
        public double height = 0;
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
            JavaScriptSerializer serializer = new JavaScriptSerializer()
            {
                MaxJsonLength = Int32.MaxValue
            };
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
                if (He.Settings.isWebProxy) hwr.Proxy = He.proxy;
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
    public static class ConsoleManager
    {
        private const string Kernel32_DllName = "kernel32.dll";

        [DllImport(Kernel32_DllName)]
        private static extern bool AllocConsole();

        [DllImport(Kernel32_DllName)]
        private static extern bool FreeConsole();

        [DllImport(Kernel32_DllName)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport(Kernel32_DllName)]
        private static extern int GetConsoleOutputCP();

        public static bool HasConsole
        {
            get { return GetConsoleWindow() != IntPtr.Zero; }
        }

        public static void Show()
        {
            if (!HasConsole)
            {
                AllocConsole();
                InvalidateOutAndError();
            }
        }

        public static void Hide()
        {
            if (HasConsole)
            {
                SetOutAndErrorNull();
                FreeConsole();
            }
        }

        public static void Toggle()
        {
            if (HasConsole)
            {
                Hide();
            }
            else
            {
                Show();
            }
        }

        static void InvalidateOutAndError()
        {
            Type type = typeof(System.Console);

            System.Reflection.FieldInfo _out = type.GetField("_out",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            System.Reflection.FieldInfo _error = type.GetField("_error",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            System.Reflection.MethodInfo _InitializeStdOutError = type.GetMethod("InitializeStdOutError",
                System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);

            Debug.Assert(_out != null);
            Debug.Assert(_error != null);

            Debug.Assert(_InitializeStdOutError != null);

            _out.SetValue(null, null);
            _error.SetValue(null, null);

            _InitializeStdOutError.Invoke(null, new object[] { true });
        }

        static void SetOutAndErrorNull()
        {
            Console.SetOut(TextWriter.Null);
            Console.SetError(TextWriter.Null);
        }
        static void SetOut(string A)
        {

        }
    }
    public class He
    {
        public static void SaveControlImage(FrameworkElement ui, string fileName)
        {

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create);
            RenderTargetBitmap bmp = new RenderTargetBitmap((int)ui.Width, (int)ui.Height, 96d, 96d,
            PixelFormats.Pbgra32);
            bmp.Render(ui);
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
            encoder.Save(fs);
            fs.Close();
        }
        public static void SaveSettings()
        {
            var data = JSON.ToJSON(Settings);
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"Settings.st", data);
        }

        public static string Text(string all, string r, string l, int t)
        {

            int A = all.IndexOf(r, t);
            int B = all.IndexOf(l, A + 1);
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
        public static DateTime StampToDateTime(string timeStamp)
        {
            DateTime dateTimeStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
            long lTime = long.Parse(timeStamp + "0000000");
            TimeSpan toNow = new TimeSpan(lTime);

            return dateTimeStart.Add(toNow);
        }
        public static bool isOpMod = false;
        public static SettingsData Settings = new SettingsData();
        public static WebProxy proxy = new WebProxy();
        public static string Url = "";
        public static string KMS = "4.0.0.0";
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
        {
            try
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
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("https://github.com/TwilightLemon/Updata/blob/master/UNVTX");
                hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream());
                string html5 = He.Text(await sr.ReadToEndAsync(), "----UVN-START---", "----UVN-STOP---", 0);
                if (con == html5)
                    return false;
                else return true;
                
            }
            catch { return false; }
        }
        public static async System.Threading.Tasks.Task<string> NewText(string con)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("https://github.com/TwilightLemon/Updata/blob/master/UNVTX");
            hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream());
            var st = He.Text(await sr.ReadToEndAsync(), "----UTX-START---", "----UTX-STOP---", 0);
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
        public static string BaiduInfoToBingInfo(string info)
        {
            switch (info)
            {
                case "jp":
                    return "ja";
                case "en":
                    return "en";
                case "kor":
                    return "ko";
                default:
                    return "";
            }
        }
        public static async Task HttpDownloadFileAsync(string url, string path)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            HttpWebResponse response = await request.GetResponseAsync() as HttpWebResponse;
            Stream responseStream = response.GetResponseStream();
            Stream stream = new FileStream(path, FileMode.Create);
            byte[] bArr = new byte[1024];
            int size =await  responseStream.ReadAsync(bArr, 0, bArr.Length);
            while (size > 0)
            {
                await stream.WriteAsync(bArr, 0, size);
                size =await responseStream.ReadAsync(bArr, 0, bArr.Length);
            }
            stream.Close();
            responseStream.Close();
        }

        public static void SetHeaderValue(WebHeaderCollection header, string name, string value)
        {
            var property = typeof(WebHeaderCollection).GetProperty("InnerCollection",
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (property != null)
            {
                var collection = property.GetValue(header, null) as NameValueCollection;
                collection[name] = value;
            }
        }
        public static async System.Threading.Tasks.Task<string> GetNewsDataAsync(string url, Encoding c)
        {
            HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
            hwr.Timeout = 20000;
            SetHeaderValue(hwr.Headers, "Connection", "keep-alive");
            hwr.Headers.Add(HttpRequestHeader.CacheControl, "max-age=0");
            hwr.Headers.Add(HttpRequestHeader.Upgrade, "1");
            hwr.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36";
            SetHeaderValue(hwr.Headers, "Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            hwr.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            hwr.Headers.Add(HttpRequestHeader.Cookie, "uuid=\"w: 474bf851054f473fbb047c3ee88fb1d4\"; UM_distinctid=15bb99c4446e98-053585cc51c06c-b373f68-100200-15bb99c4447a42; tt_webid=6393431827013158402; utm_source=toutiao; csrftoken=89b7dde63a0edebe0a4c232685fae4d0; _ga=GA1.2.2017800297.1488586857; CNZZDATA1259612802=1144631660-1488585496-%7C1493468972; __tasessionId=h1xcwzl5d1493467677496");
            if (He.Settings.isWebProxy) hwr.Proxy = He.proxy;
            var o = await hwr.GetResponseAsync();
            StreamReader sr = new StreamReader(o.GetResponseStream(), c);
            var st = await sr.ReadToEndAsync();
            sr.Dispose();
            return st;
        }

        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Timeout = 20000;
                if (He.Settings.isWebProxy) hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream(), Encoding.UTF8);
                var st = await sr.ReadToEndAsync();
                sr.Dispose();
                return st;
            }
            catch { return ""; }
        }
        public static async System.Threading.Tasks.Task<string> GetWebAsync(string url, Encoding c)
        {
            try
            {
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create(url);
                hwr.Timeout = 20000;
                if (He.Settings.isWebProxy) hwr.Proxy = He.proxy;
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
            Stream writer = await request.GetRequestStreamAsync();
            writer.Write(payload, 0, payload.Length);
            writer.Close();
            System.Net.HttpWebResponse response;
            response = (System.Net.HttpWebResponse)await request.GetResponseAsync();
            System.IO.Stream s;
            s = response.GetResponseStream();
            string StrDate = "";
            string strValue = "";
            StreamReader Reader = new StreamReader(s, Encoding.UTF8);
            while ((StrDate = await Reader.ReadLineAsync()) != null)
            {
                strValue += StrDate + "\r\n";
            }
            return strValue;
        }
        public static async Task<string> PostWebAsync(string url, string idata)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var data = Encoding.ASCII.GetBytes(idata);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = await request.GetRequestStreamAsync())
            {
                await stream.WriteAsync(data, 0, data.Length);
            }

            var response = (HttpWebResponse)await request.GetResponseAsync();

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
                if (He.Settings.isWebProxy) hwr.Proxy = He.proxy;
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
        {
            try
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
        public string ZJ { set; get; }
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

    public class GridHelper
    {

        //请注意：可以通过propa这个快捷方式生成下面三段代码

        public static bool GetShowBorder(DependencyObject obj)
        {
            return (bool)obj.GetValue(ShowBorderProperty);
        }

        public static void SetShowBorder(DependencyObject obj, bool value)
        {
            obj.SetValue(ShowBorderProperty, value);
        }

        public static readonly DependencyProperty ShowBorderProperty =
            DependencyProperty.RegisterAttached("ShowBorder", typeof(bool), typeof(GridHelper), new PropertyMetadata(OnShowBorderChanged));

        private static void OnShowBorderChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var grid = d as Grid;
            if ((bool)e.OldValue)
            {
                grid.Loaded -= (s, arg) => { };
            }
            if ((bool)e.NewValue)
            {
                grid.Loaded += (s, arg) =>
                {
                    var rows = grid.RowDefinitions.Count;
                    var columns = grid.ColumnDefinitions.Count;
                    for (int i = 0; i < rows; i++)
                    {
                        for (int j = 0; j < columns; j++)
                        {
                            var border = new Border() { BorderBrush = new SolidColorBrush(Colors.Gray), BorderThickness = new Thickness(0.5) };
                            Grid.SetRow(border, i);
                            Grid.SetColumn(border, j);

                            grid.Children.Add(border);
                        }
                    }

                };
            }

        }

    }

}
