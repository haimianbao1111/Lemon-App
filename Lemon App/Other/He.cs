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
using Microsoft.DirectX.DirectSound;
using System.Threading;
using Microsoft.DirectX;
using System.Security;
using System.Diagnostics;
using System.Collections.Specialized;

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
        public static WebProxy proxy = new WebProxy();
        public static string Url = "";
        public static string KMS = "3.5.9.6";
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
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create("http://api.lemonapp.tk/Updata.php");
                hwr.Headers.Add("Cookie", "User=d2eb2545efba96cd3c9b6cf0cd07f135;");
                hwr.Proxy = He.proxy;
                var o = await hwr.GetResponseAsync();
                StreamReader sr = new StreamReader(o.GetResponseStream());
                string html5 = await sr.ReadToEndAsync();
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
            var st = await sr.ReadToEndAsync();
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
            if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
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
                if (Settings.Default.isWebProxy) hwr.Proxy = He.proxy;
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

    public class SoundRecorder
    {
        #region 对外操作函数  
        /// <summary>  
        /// 构造函数,设定录音设备,设定录音格式.  
        /// <summary>  
        public SoundRecorder()
        {
            InitCaptureDevice();
            mWavFormat = CreateWaveFormat();
        }

        /// <summary>  
        /// 创建录音格式,此处使用16bit,16KHz,Mono的录音格式  
        /// <summary>  
        private WaveFormat CreateWaveFormat()
        {
            WaveFormat format = new WaveFormat();
            format.FormatTag = WaveFormatTag.Pcm;   // PCM  
            format.SamplesPerSecond = 16000;        // 采样率：16KHz  
            format.BitsPerSample = 16;              // 采样位数：16Bit  
            format.Channels = 1;                    // 声道：Mono  
            format.BlockAlign = (short)(format.Channels * (format.BitsPerSample / 8));  // 单位采样点的字节数   
            format.AverageBytesPerSecond = format.BlockAlign * format.SamplesPerSecond;
            return format;
            // 按照以上采样规格，可知采样1秒钟的字节数为 16000*2=32000B 约为31K  
        }

        /// <summary>  
        /// 设定录音结束后保存的文件,包括路径  
        /// </summary>  
        /// <param name="filename">保存wav文件的路径名</param>  
        public void SetFileName(string filename)
        {
            mFileName = filename;
        }

        /// <summary>  
        /// 开始录音  
        /// </summary>  
        public void RecStart()
        {
            // 创建录音文件  
            CreateSoundFile();
            // 创建一个录音缓冲区，并开始录音  
            CreateCaptureBuffer();
            // 建立通知消息,当缓冲区满的时候处理方法  
            InitNotifications();
            mRecBuffer.Start(true);
        }


        /// <summary>  
        /// 停止录音  
        /// </summary>  
        public void RecStop()
        {
            mRecBuffer.Stop();      // 调用缓冲区的停止方法，停止采集声音  
            if (null != mNotificationEvent)
                mNotificationEvent.Set();       //关闭通知  
            mNotifyThread.Abort();  //结束线程  
            RecordCapturedData();   // 将缓冲区最后一部分数据写入到文件中  

            // 写WAV文件尾  
            mWriter.Seek(4, SeekOrigin.Begin);
            mWriter.Write((int)(mSampleCount + 36));   // 写文件长度  
            mWriter.Seek(40, SeekOrigin.Begin);
            mWriter.Write(mSampleCount);                // 写数据长度  

            mWriter.Close();
            mWaveFile.Close();
            mWriter = null;
            mWaveFile = null;
        }
        #endregion
        #region 成员数据  
        private Capture mCapDev = null;              // 音频捕捉设备  
        private CaptureBuffer mRecBuffer = null;     // 缓冲区对象  
        private WaveFormat mWavFormat;               // 录音的格式  

        private int mNextCaptureOffset = 0;         // 该次录音缓冲区的起始点  
        private int mSampleCount = 0;               // 录制的样本数目  

        private Notify mNotify = null;               // 消息通知对象  
        public const int cNotifyNum = 16;           // 通知的个数  
        private int mNotifySize = 0;                // 每次通知大小  
        private int mBufferSize = 0;                // 缓冲队列大小  
        private Thread mNotifyThread = null;                 // 处理缓冲区消息的线程  
        private AutoResetEvent mNotificationEvent = null;    // 通知事件  

        private string mFileName = string.Empty;     // 文件保存路径  
        private FileStream mWaveFile = null;         // 文件流  
        private BinaryWriter mWriter = null;         // 写文件  
        #endregion
        #region 对内操作函数  
        /// <summary>  
        /// 初始化录音设备,此处使用主录音设备.  
        /// </summary>  
        /// <returns>调用成功返回true,否则返回false</returns>  
        private bool InitCaptureDevice()
        {
            // 获取默认音频捕捉设备  
            CaptureDevicesCollection devices = new CaptureDevicesCollection();  // 枚举音频捕捉设备  
            Guid deviceGuid = Guid.Empty;

            if (devices.Count > 0)
                deviceGuid = devices[0].DriverGuid;
            else
            {
                System.Windows.MessageBox.Show("系统中没有音频捕捉设备");
                return false;
            }

            // 用指定的捕捉设备创建Capture对象  
            try
            {
                mCapDev = new Capture(deviceGuid);
            }
            catch (DirectXException e)
            {
                System.Windows.MessageBox.Show(e.ToString());
                return false;
            }
            return true;
        }

        /// <summary>  
        /// 创建录音使用的缓冲区  
        /// </summary>  
        private void CreateCaptureBuffer()
        {
            // 缓冲区的描述对象  
            CaptureBufferDescription bufferdescription = new CaptureBufferDescription();
            if (null != mNotify)
            {
                mNotify.Dispose();
                mNotify = null;
            }
            if (null != mRecBuffer)
            {
                mRecBuffer.Dispose();
                mRecBuffer = null;
            }
            // 设定通知的大小,默认为1s钟  
            mNotifySize = (1024 > mWavFormat.AverageBytesPerSecond / 8) ? 1024 : (mWavFormat.AverageBytesPerSecond / 8);
            mNotifySize -= mNotifySize % mWavFormat.BlockAlign;
            // 设定缓冲区大小  
            mBufferSize = mNotifySize * cNotifyNum;
            // 创建缓冲区描述  
            bufferdescription.BufferBytes = mBufferSize;
            bufferdescription.Format = mWavFormat;           // 录音格式  
                                                             // 创建缓冲区  
            mRecBuffer = new CaptureBuffer(bufferdescription, mCapDev);
            mNextCaptureOffset = 0;
        }

        /// <summary>  
        /// 初始化通知事件,将原缓冲区分成16个缓冲队列,在每个缓冲队列的结束点设定通知点.  
        /// </summary>  
        /// <returns>是否成功</returns>  
        private bool InitNotifications()
        {
            if (null == mRecBuffer)
            {
                System.Windows.MessageBox.Show("未创建录音缓冲区");
                return false;
            }
            // 创建一个通知事件,当缓冲队列满了就激发该事件.  
            mNotificationEvent = new AutoResetEvent(false);
            // 创建一个线程管理缓冲区事件  
            if (null == mNotifyThread)
            {
                mNotifyThread = new Thread(new ThreadStart(WaitThread));
                mNotifyThread.Start();
            }
            // 设定通知的位置  
            BufferPositionNotify[] PositionNotify = new BufferPositionNotify[cNotifyNum + 1];
            for (int i = 0; i < cNotifyNum; i++)
            {
                PositionNotify[i].Offset = (mNotifySize * i) + mNotifySize - 1;
                PositionNotify[i].EventNotifyHandle = mNotificationEvent.SafeWaitHandle.DangerousGetHandle();
            }
            mNotify = new Notify(mRecBuffer);
            mNotify.SetNotificationPositions(PositionNotify, cNotifyNum);
            return true;
        }

        /// <summary>  
        /// 接收缓冲区满消息的处理线程  
        /// </summary>  
        private void WaitThread()
        {
            while (true)
            {
                // 等待缓冲区的通知消息  
                mNotificationEvent.WaitOne(Timeout.Infinite, true);
                // 录制数据  
                RecordCapturedData();
            }
        }

        /// <summary>  
        /// 将录制的数据写入wav文件  
        /// </summary>  
        private void RecordCapturedData()
        {
            byte[] CaptureData = null;
            int ReadPos = 0, CapturePos = 0, LockSize = 0;
            mRecBuffer.GetCurrentPosition(out CapturePos, out ReadPos);
            LockSize = ReadPos - mNextCaptureOffset;
            if (LockSize < 0)       // 因为是循环的使用缓冲区，所以有一种情况下为负：当文以载读指针回到第一个通知点，而Ibuffeoffset还在最后一个通知处  
                LockSize += mBufferSize;
            LockSize -= (LockSize % mNotifySize);   // 对齐缓冲区边界,实际上由于开始设定完整,这个操作是多余的.  
            if (0 == LockSize)
                return;

            // 读取缓冲区内的数据  
            CaptureData = (byte[])mRecBuffer.Read(mNextCaptureOffset, typeof(byte), LockFlag.None, LockSize);
            // 写入Wav文件  
            mWriter.Write(CaptureData, 0, CaptureData.Length);
            // 更新已经录制的数据长度.  
            mSampleCount += CaptureData.Length;
            // 移动录制数据的起始点,通知消息只负责指示产生消息的位置,并不记录上次录制的位置  
            mNextCaptureOffset += CaptureData.Length;
            mNextCaptureOffset %= mBufferSize; // Circular buffer  
        }

        /// <summary>  
        /// 创建保存的波形文件,并写入必要的文件头.  
        /// </summary>  
        private void CreateSoundFile()
        {
            // Open up the wave file for writing.  
            mWaveFile = new FileStream(mFileName, FileMode.Create);
            mWriter = new BinaryWriter(mWaveFile);
            /**************************************************************************  
               Here is where the file will be created. A  
               wave file is a RIFF file, which has chunks  
               of data that describe what the file contains.  
               A wave RIFF file is put together like this:  
               The 12 byte RIFF chunk is constructed like this:  
               Bytes 0 - 3 :  'R' 'I' 'F' 'F'  
               Bytes 4 - 7 :  Length of file, minus the first 8 bytes of the RIFF description.  
                                 (4 bytes for "WAVE" + 24 bytes for format chunk length +  
                                 8 bytes for data chunk description + actual sample data size.)  
                Bytes 8 - 11: 'W' 'A' 'V' 'E'  
                The 24 byte FORMAT chunk is constructed like this:  
                Bytes 0 - 3 : 'f' 'm' 't' ' '  
                Bytes 4 - 7 : The format chunk length. This is always 16.  
                Bytes 8 - 9 : File padding. Always 1.  
                Bytes 10- 11: Number of channels. Either 1 for mono,  or 2 for stereo.  
                Bytes 12- 15: Sample rate.  
                Bytes 16- 19: Number of bytes per second.  
                Bytes 20- 21: Bytes per sample. 1 for 8 bit mono, 2 for 8 bit stereo or  
                                16 bit mono, 4 for 16 bit stereo.  
                Bytes 22- 23: Number of bits per sample.  
                The DATA chunk is constructed like this:  
                Bytes 0 - 3 : 'd' 'a' 't' 'a'  
                Bytes 4 - 7 : Length of data, in bytes.  
                Bytes 8 -: Actual sample data.  
              ***************************************************************************/
            // Set up file with RIFF chunk info.  
            char[] ChunkRiff = { 'R', 'I', 'F', 'F' };
            char[] ChunkType = { 'W', 'A', 'V', 'E' };
            char[] ChunkFmt = { 'f', 'm', 't', ' ' };
            char[] ChunkData = { 'd', 'a', 't', 'a' };

            short shPad = 1;                // File padding  
            int nFormatChunkLength = 0x10;  // Format chunk length.  
            int nLength = 0;                // File length, minus first 8 bytes of RIFF description. This will be filled in later.  
            short shBytesPerSample = 0;     // Bytes per sample.  

            // 一个样本点的字节数目  
            if (8 == mWavFormat.BitsPerSample && 1 == mWavFormat.Channels)
                shBytesPerSample = 1;
            else if ((8 == mWavFormat.BitsPerSample && 2 == mWavFormat.Channels) || (16 == mWavFormat.BitsPerSample && 1 == mWavFormat.Channels))
                shBytesPerSample = 2;
            else if (16 == mWavFormat.BitsPerSample && 2 == mWavFormat.Channels)
                shBytesPerSample = 4;

            // RIFF 块  
            mWriter.Write(ChunkRiff);
            mWriter.Write(nLength);
            mWriter.Write(ChunkType);

            // WAVE块  
            mWriter.Write(ChunkFmt);
            mWriter.Write(nFormatChunkLength);
            mWriter.Write(shPad);
            mWriter.Write(mWavFormat.Channels);
            mWriter.Write(mWavFormat.SamplesPerSecond);
            mWriter.Write(mWavFormat.AverageBytesPerSecond);
            mWriter.Write(shBytesPerSample);
            mWriter.Write(mWavFormat.BitsPerSample);

            // 数据块  
            mWriter.Write(ChunkData);
            mWriter.Write((int)0);   // The sample length will be written in later.  
        }
        #endregion
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
