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
        }
        //[DllImport("winmm.dll", EntryPoint = "mciSendString", CharSet = CharSet.Auto)]
        //public static extern int mciSendString(
        // string lpstrCommand,
        // string lpstrReturnString,
        // int uReturnLength,
        // int hwndCallback
        //);
        private string Post(string audioFilePath)
        {
            string serverURL = "http://vop.baidu.com/server_api";
            string token = "24.287d1caf505f1a3c8ba0bee80b2e343e.2592000.1495502901.282335-9474467";
            serverURL += "?lan=zh&cuid=kwwwvagaa&token=" + token;
            FileStream fs = new FileStream(audioFilePath, FileMode.Open);
            byte[] voice = new byte[fs.Length];
            fs.Read(voice, 0, voice.Length);
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
                using (Stream writeStream = request.GetRequestStream())
                {
                    writeStream.Write(voice, 0, voice.Length);
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
                            line = readStream.ReadLine();
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
        SoundRecorder s = new SoundRecorder();
        private void t_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_in.Text == "喵喵喵")
            {
                //mciSendString("set wave bitpersample 16", "", 0, 0);
                //mciSendString("set wave samplespersec 16000", "", 0, 0);
                //mciSendString("set wave channels 1", "", 0, 0);
                //mciSendString("set wave format tag pcm", "", 0, 0);
                //mciSendString("open new type WAVEAudio alias movie", "", 0, 0);
                //mciSendString("record movie", "", 0, 0);
                s.SetFileName(AppDomain.CurrentDomain.BaseDirectory + "on.wav");
                s.RecStart();
                _in.Text = "录音中...";
            }else if (_in.Text == "录音中...")
            {
                //mciSendString("stop movie", "", 0, 0);
                //mciSendString("save movie on.wav", "", 0, 0);
                //mciSendString("close movie", "", 0, 0);
                s.RecStop();
                _in.Text = "识别中...";
                if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "on.wav"))
                {
                    MessageBox.Show("OK");
                    o.Text = Post(AppDomain.CurrentDomain.BaseDirectory + "on.wav");
                    _in.Text = "喵喵喵";
                }
            }
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (_in.Text == "录音中..." && _in.Text == "识别中...")
            {
                //mciSendString("stop movie", "", 0, 0);
                //mciSendString("close movie", "", 0, 0);
                s.RecStop();
                s = null;
            }
            this.Close();
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }
    }
}
