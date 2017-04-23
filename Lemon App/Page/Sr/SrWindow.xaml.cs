using Lemon_App.Properties;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
using System.Windows.Shapes;

namespace Lemon_App.Page.Sr
{
    /// <summary>
    /// SrWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SrWindow : Window
    {
        public SrWindow()
        {
            InitializeComponent();
            var c = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(OpacityProperty, c);
            this.FontFamily = new FontFamily(Settings.Default.FontFamilly);
        }
        //[DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        //public static extern int mciSendString(
        // string lpstrCommand,
        // string lpstrReturnString,
        // int uReturnLength,
        // int hwndCallback
        //);
        private async Task<string> GetKey()
        {
           return  JObject.Parse(await Uuuhh.GetWebAsync("https://openapi.baidu.com/oauth/2.0/token?grant_type=client_credentials&client_id=75bl82qIt9Rtly6Na6wqYUmm&client_secret=pMO9ZSQSsZFNvMMnXy5L3GaQbpWG6Fyw"))["access_token"].ToString();
        }
        private async Task<string> PostAsync(string audioFilePath)
        {
            string serverURL = "http://vop.baidu.com/server_api";
            string token = "24.7e89beaa7380e939ffb4890e05427d3d.2592000.1495540990.282335-9474467";//await GetKey("75bl82qIt9Rtly6Na6wqYUmm", "pMO9ZSQSsZFNvMMnXy5L3GaQbpWG6Fyw");
            serverURL += $"?lan=zh&cuid=kwwwvagaa&token=" + token;
            FileStream fs = new FileStream(audioFilePath, FileMode.Open);
            byte[] voice = new byte[fs.Length];
            await fs.ReadAsync(voice, 0, voice.Length);
            fs.Close();
            fs.Dispose();

            HttpWebRequest request = null;

            Uri uri = new Uri(serverURL);
            request = (HttpWebRequest)WebRequest.Create(uri);
            request.Method = "POST";
            request.ContentType = "audio/wav; rate=16000";
            request.ContentLength = voice.Length;
            try
            {
                using (Stream writeStream =await request.GetRequestStreamAsync())
                {
                    await writeStream.WriteAsync(voice, 0, voice.Length);
                    writeStream.Close();
                    writeStream.Dispose();
                }
            }
            catch
            {
                return null;
            }
            string result = string.Empty;
            string result_final = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader readStream = new StreamReader(responseStream, Encoding.UTF8))
                    {
                        string line = string.Empty;
                        StringBuilder sb = new StringBuilder();
                        while (!readStream.EndOfStream)
                        {
                            line =await readStream.ReadLineAsync();
                            sb.Append(line);
                            sb.Append("\r");
                        }
                        readStream.Close();
                        readStream.Dispose();
                        result = sb.ToString();
                        string[] indexs = result.Split(',');
                        foreach (string index in indexs)
                        {
                            string[] _indexs = index.Split('"');
                            if (_indexs[2] == ":[")
                                result_final = _indexs[3];
                        }
                    }
                    responseStream.Close();
                    responseStream.Dispose();
                }
                response.Close();
            }
            return result_final;
        }
        SoundRecorder s;
        private async void t_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_in.Text == "喵喵喵")
            {
                //mciSendString("set wave bitpersample 16", "", 0, 0);
                //mciSendString("set wave samplespersec 16000", "", 0, 0);
                //mciSendString("set wave channels 1", "", 0, 0);
                //mciSendString("set wave format tag pcm", "", 0, 0);
                //mciSendString("open new type WAVEAudio alias movie", "", 0, 0);
                //mciSendString("record movie", "", 0, 0);
                s = new SoundRecorder();
                s.SetFileName(AppDomain.CurrentDomain.BaseDirectory + "on.wav");
                s.RecStart();
                _in.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3)));
                _in.Text = "录音中...";
            }
            else if (_in.Text == "录音中...")
            {
                //mciSendString("stop movie", "", 0, 0);
                //mciSendString("save movie on.wav", "", 0, 0);
                //mciSendString("close movie", "", 0, 0);
                s.RecStop();
                _in.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3)));
                _in.Text = "识别中...";
                s = null;
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "on.wav"))
                {
                    o.Text = await PostAsync(AppDomain.CurrentDomain.BaseDirectory + "on.wav");
                    _in.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3)));
                    _in.Text = "喵喵喵";
                }
            }
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
