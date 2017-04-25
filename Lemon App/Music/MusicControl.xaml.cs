using Lemon_App.Properties;
using static Lemon_App.Uuuhh;
using static Lemon_App.He;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Web.Script.Serialization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lemon_App.IApps.User;
using Un4seen.Bass;
using System.Windows.Interop;

namespace Lemon_App
{
    /// <summary>
    /// MusicControl.xaml 的交互逻辑
    /// </summary>
    public partial class MusicControl : UserControl
    {
     //   MediaPlayer player = new MediaPlayer();
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        int IslistBoxInfo = 1;//0=Search,1=G2d,2=SC,3=DFB
        public MusicControl()
        {
            InitializeComponent();
            t.Interval = 500;
            t.Tick += Tick;
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache") == false)
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache");
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"Download") == false)
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + $@"Download");
            getLT = new GetLyricAndLyricTime();
            lyricShow = new LyricShow(CanvasLyric, StackPanelCommonLyric, CanvasFocusLyric, TBFocusLyricBack, CanvasFocusLyricFore, TBFocusLyricFore);
        }
        /// <summary>
        /// 自定义的歌词秀类
        /// </summary>
        LyricShow lyricShow;
        /// <summary>
        /// 自定义的解析歌词类
        /// </summary>
        GetLyricAndLyricTime getLT;
        private void io()
        {
            try
            {
                t.Stop();
                Bass.BASS_ChannelSetPosition(stream,0);
                Bass.BASS_ChannelStop(stream);
                jd.Value = 0;
                s.Data = Geometry.Parse("M118.2,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6h-36c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H118.2z M46,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6H10c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H46z");
                string i = "";
                if (Move.Text == "循环")
                {
                    if (listBox.SelectedIndex != listBox.Items.Count)
                    {
                        i = (listBox.Items[listBox.SelectedIndex + 1] as MusicItemControl).Content;
                        listBox.SelectedItem = listBox.Items[listBox.SelectedIndex + 1];
                    }
                    else
                    {
                        i = (listBox.Items[0] as MusicItemControl).Content;
                        listBox.SelectedItem = listBox.Items[0];
                    }
                }
                else if (Move.Text == "单曲")
                {
                    i = (listBox.Items[listBox.SelectedIndex] as MusicItemControl).Content;
                    listBox.SelectedItem = listBox.Items[listBox.SelectedIndex];
                    listBox_SelectionChanged(null, null);
                }
              //  listBox_SelectionChanged(null, null);
            }
            catch { }
        }
        int stream;
        private void Fis(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
           // player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"));
      //      player.Play();
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        private void Tick(object sender, EventArgs e)
        {
            try
            {
                if (Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)) == Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)))
                    io();
                LyricShow.refreshLyricShow(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)));
                jd.Maximum = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream));
                jd.Value = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream));
            }
            catch { }
        }
        int ioi = 3;
        List<string> data = new List<string>();
        private void Mou(object sender, MouseButtonEventArgs e)
        {
            data.Remove((sender as Border).ToolTip as string);
            Settings.Default.MusicSearch = JSON.ToJSON(data);
            Settings.Default.Save();
            // MessageBox.Show(JSON.ToJSON(data));
        }
        private async void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {           
                    if(!data.Contains(textBox.Text))
                    {
                        var co = new LZoneItemControl();
                        co.S.MouseDown += Mou;
                        co.QZoneData = textBox.Text;
                        co.BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
                        this.SearchDataContent.Children.Insert(0, co);
                        data.Add(textBox.Text);
                        //   MessageBox.Show(JSON.ToJSON(data));
                        Settings.Default.MusicSearch = JSON.ToJSON(data);
                        Settings.Default.Save();
                    }
                    int i = 0;
                    try
                    {
                        IslistBoxInfo = 0;
                        jz.Visibility = Visibility.Visible;
                        DOWN.Visibility = Visibility.Collapsed;
                        listBox.Visibility = Visibility.Visible;
                        listBox.Items.Clear();
                        int osx = 1;
                        while (osx != 3)
                        {
                            JObject o = JObject.Parse(await GetWebAsync($"http://59.37.96.220/soso/fcgi-bin/client_search_cp?format=json&t=0&inCharset=GB2312&outCharset=utf-8&qqmusic_ver=1302&catZhida=0&p={osx}&n=20&w={textBox.Text}&flag_qc=0&remoteplace=sizer.newclient.song&new_json=1&lossless=0&aggr=1&cr=1&sem=0&force_zonghe=0", Encoding.UTF8));
                            while (i < o["data"]["song"]["list"].Count())
                            {
                                //string f = o["data"]["song"]["list"][i]["f"].ToString().Replace("|", "\r\n");
                                //string[] ContentLines = f.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                                //string Gs = o["data"]["song"]["list"][i]["fsinger"].ToString();
                                //string songname = o["data"]["song"]["list"][i]["fsong"].ToString();
                                //string Zhj = o["data"]["song"]["list"][i]["albumName_hilight"].ToString();
                                //    img = ContentLines[22];
                                Music m = new Music();
                                m.MusicName = o["data"]["song"]["list"][i]["name"].ToString();
                                string Singer = "";
                                for (int osxc = 0; osxc != o["data"]["song"]["list"][i]["singer"].Count(); osxc++)
                                { Singer += o["data"]["song"]["list"][i]["singer"][osxc]["name"] + "/"; }
                                m.Singer = Singer.Substring(0, Singer.LastIndexOf("/"));
                                m.ZJ = o["data"]["song"]["list"][i]["album"]["name"].ToString();
                                m.MusicID = o["data"]["song"]["list"][i]["mid"].ToString();
                                m.ImageID = o["data"]["song"]["list"][i]["album"]["mid"].ToString();
                                m.GC = o["data"]["song"]["list"][i]["id"].ToString();
                                m.Fotmat = o["data"]["song"]["list"][i]["file"]["size_flac"].ToString();
                                m.HQFOTmat = o["data"]["song"]["list"][i]["file"]["size_ogg"].ToString();
                                m.MV = o["data"]["song"]["list"][i]["mv"]["id"].ToString();
                                string Q = "";
                                if (m.Fotmat != "0")
                                    Q = "SQ";
                                if (m.HQFOTmat != "0")
                                    if (m.Fotmat == "0")
                                        Q = "HQ";
                                listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth-10, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                                i++;
                            }
                            osx++;
                            i = 0;
                        }
                        jz.Visibility = Visibility.Collapsed;
                        listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
                    }
                    catch { }
                }
            }
            catch { }
        }
        string img = "";
        string musicurl = "";
        string musicid = "";
        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (((listBox.SelectedItem as MusicItemControl).Music as Music).HQFOTmat != "HQ")
                    if (pz.Text == "HQ") pz.Text = "标准";
                try
                {
                    Bass.BASS_ChannelStop(stream);
                    LyricShow.F = true;
                    isR = true;
                    s.Data = Geometry.Parse("M118.2,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6h-36c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H118.2z M46,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6H10c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H46z");
                    string i = (listBox.SelectedItem as MusicItemControl).Content.Replace("\\", ",").Replace("/", ",");
                    textBlock1.Text = i;
                    lrcname.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicName;
                    zk.Inlines.Clear();
                    zk.Inlines.Add(new Run("歌手:") { Foreground = new SolidColorBrush(Color.FromArgb(255, 189, 189, 189)) });
                    zk.Inlines.Add(new Run(((listBox.SelectedItem as MusicItemControl).Music as Music).Singer));
                    zk.Inlines.Add(new Run("专辑:") { Foreground = new SolidColorBrush(Color.FromArgb(255, 189, 189, 189)) });
                    zk.Inlines.Add(new Run(((listBox.SelectedItem as MusicItemControl).Music as Music).ZJ));
                    img = ((listBox.SelectedItem as MusicItemControl).Music as Music).ImageID;
                    He.on = $"https://y.gtimg.cn/music/photo_new/T002R300x300M000{img}.jpg";
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache /{img}.jpg"))
                    {
                        new WebClient().DownloadFile(new Uri(He.on), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache /{img}.jpg");
                        tx.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache /{img}.jpg", UriKind.Absolute)));
                    }
                    else tx.Background = new ImageBrush(new BitmapImage(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache /{img}.jpg", UriKind.Absolute)));
                    if (!((listBox.SelectedItem as MusicItemControl).Music as Music).IsDF)
                    {
                        musicid = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicID;
                        if (pz.Text == "经济")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a"))
                            {
                                musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                                WebClient dc = new WebClient();
                                dc.Proxy = He.proxy;
                                dc.DownloadFileCompleted += Fi_BZ;
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a");
                                ///等待播放
                                loading.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                //player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a"));
                                //player.Play();
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                string h = await Uuuhh.GetWebAsync($"https://route.showapi.com/213-2?showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&musicid={lrc}");
                                JObject o = JObject.Parse(h);
                                string ijo = System.Web.HttpUtility.HtmlDecode(o["showapi_res_body"]["lyric"].ToString()).Replace("&apos;", "'");
                                if (ijo != "")
                                {
                                    await sw.WriteAsync(ijo);
                                    await sw.FlushAsync();
                                    sw.Close();
                                    fs.Close();
                                    if (LyricShow.IsOpenDeskLyric == false)
                                    {
                                        //deskLyricWin = new DeskLyricWin();
                                        //deskLyricWin.Show();
                                        //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                        LyricShow.HB = 124;
                                        LyricShow.HG = 194;
                                        LyricShow.HR = 49;
                                        LyricShow.CB = 193;
                                        LyricShow.CG = 180;
                                        LyricShow.CR = 180;
                                    }
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    //deskLyricWin = new DeskLyricWin();
                                    //deskLyricWin.Show();
                                    //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 124;
                                    LyricShow.HG = 194;
                                    LyricShow.HR = 49;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                        else if (pz.Text == "标准")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"))
                            {
                                // musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                                string guid = "20D919A4D7700FBC424740E8CED80C5F";
                                string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                                string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                                musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{musicid}.mp3?vkey={vkey}&guid={guid}";

                                WebClient dc = new WebClient()
                                {
                                    Proxy = He.proxy
                                };
                                dc.DownloadFileCompleted += Fi;
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3");
                                ///等待播放
                                loading.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                string h = await Uuuhh.GetWebAsync($"https://route.showapi.com/213-2?showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&musicid={lrc}");
                                JObject o = JObject.Parse(h);
                                string ijo = System.Web.HttpUtility.HtmlDecode(o["showapi_res_body"]["lyric"].ToString()).Replace("&apos;", "'");
                                if (ijo != "")
                                {
                                    await sw.WriteAsync(ijo);
                                    await sw.FlushAsync();
                                    sw.Close();
                                    fs.Close();
                                    if (LyricShow.IsOpenDeskLyric == false)
                                    {
                                        //deskLyricWin = new DeskLyricWin();
                                        //deskLyricWin.Show();
                                        //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                        LyricShow.HB = 124;
                                        LyricShow.HG = 194;
                                        LyricShow.HR = 49;
                                        LyricShow.CB = 193;
                                        LyricShow.CG = 180;
                                        LyricShow.CR = 180;
                                    }
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    //deskLyricWin = new DeskLyricWin();
                                    //deskLyricWin.Show();
                                    //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 124;
                                    LyricShow.HG = 194;
                                    LyricShow.HR = 49;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                        else if (pz.Text == "HQ")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.ogg"))
                            {
                                // musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                                string guid = "20D919A4D7700FBC424740E8CED80C5F";
                                string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                                string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                                musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/O600{musicid}.ogg?vkey={vkey}&guid={guid}";

                                WebClient dc = new WebClient()
                                {
                                    Proxy = He.proxy
                                };
                                dc.DownloadFileCompleted += Fi_Ogg;
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.ogg");
                                ///等待播放
                                loading.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.ogg", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                string h = await Uuuhh.GetWebAsync($"https://route.showapi.com/213-2?showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&musicid={lrc}");
                                JObject o = JObject.Parse(h);
                                string ijo = System.Web.HttpUtility.HtmlDecode(o["showapi_res_body"]["lyric"].ToString()).Replace("&apos;", "'");
                                if (ijo != "")
                                {
                                    await sw.WriteAsync(ijo);
                                    await sw.FlushAsync();
                                    sw.Close();
                                    fs.Close();
                                    if (LyricShow.IsOpenDeskLyric == false)
                                    {
                                        //deskLyricWin = new DeskLyricWin();
                                        //deskLyricWin.Show();
                                        //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                        LyricShow.HB = 124;
                                        LyricShow.HG = 194;
                                        LyricShow.HR = 49;
                                        LyricShow.CB = 193;
                                        LyricShow.CG = 180;
                                        LyricShow.CR = 180;
                                    }
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    //deskLyricWin = new DeskLyricWin();
                                    //deskLyricWin.Show();
                                    //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 124;
                                    LyricShow.HG = 194;
                                    LyricShow.HR = 49;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                    }
                    else
                    {

                        if (pz.Text == "标准")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a"))
                            {
                                musicurl = ((listBox.SelectedItem as MusicItemControl).Music as Music).DFSONGURI;
                                WebClient dc = new WebClient()
                                {
                                    Proxy = proxy
                                };
                                dc.DownloadFileCompleted += Fi_BZ;
                                dc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
                                dc.Headers.Add(HttpRequestHeader.Accept, "ext/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                                dc.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
                                dc.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                                dc.Headers.Add(HttpRequestHeader.Cookie, "tvfe_boss_uuid=308e152dbaa0bd6b; eas_sid=h1D4k7n7h7G3g1N6A6c2a812e7; pac_uid=1_2728578956; _ga=GA1.2.889488099.1474016943; luin=o2728578956; lskey=000100005f25e44c67a9f6af47159fd54f9e23ed418536b3cbe8cfacebfa495259d109938019c06a0f2f9314; pgv_pvi=9043384320; RK=oLOObi2e0M; o_cookie=2728578956; pgv_pvid=9806437357; ptui_loginuin=2728578956; ptcz=92e59f3e2a0a260c0597ef023e0044edb543a10592392101aa43e8640241b28f; pt2gguin=o2728578956; pgv_si=s8448803840; qqmusic_uin=12345678; qqmusic_key=12345678; qqmusic_fromtag=30");
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a");
                                ///等待播放
                                loading.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                string h = await Uuuhh.GetWebAsync($"https://route.showapi.com/213-2?showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&musicid={lrc}");
                                JObject o = JObject.Parse(h);
                                string ijo = System.Web.HttpUtility.HtmlDecode(o["showapi_res_body"]["lyric"].ToString());
                                if (ijo != "")
                                {
                                    await sw.WriteAsync(ijo);
                                    await sw.FlushAsync();
                                    sw.Close();
                                    fs.Close();
                                    if (LyricShow.IsOpenDeskLyric == false)
                                    {
                                        //deskLyricWin = new DeskLyricWin();
                                        //deskLyricWin.Show();
                                        //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                        LyricShow.HB = 204;
                                        LyricShow.HG = 122;
                                        LyricShow.HR = 0;
                                        LyricShow.CB = 193;
                                        LyricShow.CG = 180;
                                        LyricShow.CR = 180;
                                    }
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    //deskLyricWin = new DeskLyricWin();
                                    //deskLyricWin.Show();
                                    //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 204;
                                    LyricShow.HG = 122;
                                    LyricShow.HR = 0;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                        else if (pz.Text == "HQ")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"))
                            {
                                musicurl = ((listBox.SelectedItem as MusicItemControl).Music as Music).DFSONGURI_HQ;
                                WebClient dc = new WebClient()
                                {
                                    Proxy = proxy
                                };
                                dc.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/55.0.2883.87 Safari/537.36");
                                dc.Headers.Add(HttpRequestHeader.Accept, "ext/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
                                dc.Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip, deflate, sdch");
                                dc.Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
                                dc.Headers.Add(HttpRequestHeader.Cookie, "tvfe_boss_uuid=308e152dbaa0bd6b; eas_sid=h1D4k7n7h7G3g1N6A6c2a812e7; pac_uid=1_2728578956; _ga=GA1.2.889488099.1474016943; luin=o2728578956; lskey=000100005f25e44c67a9f6af47159fd54f9e23ed418536b3cbe8cfacebfa495259d109938019c06a0f2f9314; pgv_pvi=9043384320; RK=oLOObi2e0M; o_cookie=2728578956; pgv_pvid=9806437357; ptui_loginuin=2728578956; ptcz=92e59f3e2a0a260c0597ef023e0044edb543a10592392101aa43e8640241b28f; pt2gguin=o2728578956; pgv_si=s8448803840; qqmusic_uin=12345678; qqmusic_key=12345678; qqmusic_fromtag=30");
                                dc.DownloadFileCompleted += Fi;
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3");
                                loading.Visibility = Visibility.Visible;
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                                StreamWriter sw = new StreamWriter(fs);
                                string h = await Uuuhh.GetWebAsync($"https://route.showapi.com/213-2?showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&musicid={lrc}");
                                JObject o = JObject.Parse(h);
                                string ijo = System.Web.HttpUtility.HtmlDecode(o["showapi_res_body"]["lyric"].ToString());
                                if (ijo != "")
                                {
                                    await sw.WriteAsync(ijo);
                                    await sw.FlushAsync();
                                    sw.Close();
                                    fs.Close();
                                    if (LyricShow.IsOpenDeskLyric == false)
                                    {
                                        //deskLyricWin = new DeskLyricWin();
                                        //deskLyricWin.Show();
                                        //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                        LyricShow.HB = 204;
                                        LyricShow.HG = 122;
                                        LyricShow.HR = 0;
                                        LyricShow.CB = 193;
                                        LyricShow.CG = 180;
                                        LyricShow.CR = 180;
                                    }
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    //deskLyricWin = new DeskLyricWin();
                                    //deskLyricWin.Show();
                                    //LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 204;
                                    LyricShow.HG = 122;
                                    LyricShow.HR = 0;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                    }
                }
                catch {  }
            }
        }

        private void Fi_Ogg(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.ogg", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        private void Fi_BZ(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        private void Fi(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        string LanChange(string str)
        {
            Encoding utf8;
            Encoding gb2312;
            utf8 = Encoding.GetEncoding("UTF-8");
            gb2312 = Encoding.GetEncoding("GB2312");
            byte[] gb = gb2312.GetBytes(str);
            gb = Encoding.Convert(gb2312, utf8, gb);
            return utf8.GetString(gb);
        }

        bool isR = false;
        private void textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isR)
            { isR = false; Bass.BASS_ChannelPause(stream); s.Data = Geometry.Parse("M40.2,3.8C36.4,0,30,2.7,30,8v112c0,5.3,6.4,8,10.2,4.2l56-56c2.3-2.3,2.3-6.1,0-8.4L40.2,3.8z"); }
            else { isR = true; Bass.BASS_ChannelPlay(stream, true); Bass.BASS_ChannelSetPosition(stream, jd.Value); s.Data = Geometry.Parse("M118.2,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6h-36c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H118.2z M46,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6H10c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H46z"); }
        }

        private void jd_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LyricShow.refreshLyricShow(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)));
                Bass.BASS_ChannelSetPosition(stream, jd.Value);
            }
        }
        private void textBlock4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DOWN.Visibility = Visibility.Visible;
            listBox.Visibility = Visibility.Collapsed;
            string op = "";
            if (pz.Text == "标准") { op = "m4a"; }
            if (pz.Text == "HQ") { op = "mp3"; }
            if (pz.Text == "SQ") { op = "flac"; }
            WebClient dc = new WebClient()
            {
                Proxy = proxy
            };
            dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicDownload/{textBlock1.Text}.{op}");
            dc.DownloadFileCompleted += OK;
            dc.DownloadProgressChanged += DownloadFileCompleted;
        }
        private void DownloadFileCompleted(object sender, DownloadProgressChangedEventArgs e)
        {
            PB.Value = e.ProgressPercentage;
            textBlock6.Text = e.TotalBytesToReceive + "/" + e.BytesReceived;
        }
        //
        private void OK(object sender, AsyncCompletedEventArgs e)
        {
            string op = "";
            if (pz.Text == "标准") { op = "m4a"; }
            if (pz.Text == "HQ") { op = "mp3"; }
            if (pz.Text == "SQ") { op = "flac"; }
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + ((listBox.SelectedItem as ListBoxItem).Content as string) + op))
                textBlock6.Text = "下载失败";
        }

        private void tx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (h.Visibility == Visibility.Visible)
            {
                h.Visibility = Visibility.Collapsed;
                ZjImAgE.Visibility = Visibility.Collapsed;
                G.Visibility = Visibility.Visible;
                h.Margin = new Thickness(-40, 40, 40, -20);
                ZjImAgE.Margin = new Thickness(-40, 40, 40, -20);
                G.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-40, 40, 40, -20), new Thickness(0, 0, 0, 49), TimeSpan.FromSeconds(0.1)));
            }
            else
            { 
                h.Visibility = Visibility.Visible;
                G.Visibility = Visibility.Collapsed;
                ZjImAgE.Visibility = Visibility.Collapsed;
                G.Margin = new Thickness(-40, 40, 40, -20);
                ZjImAgE.Margin = new Thickness(-40, 40, 40, -20);
                h.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-40, 40, 40, -20), new Thickness(0, 0, 0, 49), TimeSpan.FromSeconds(0.1)));
            }
        }
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
        private void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            ListJson lj = new ListJson();
            if (Settings.Default.MusicList != "") lj = JsonToObject(Settings.Default.MusicList, lj) as ListJson;
            lj.List.Add(new ListItem() { ItemText = ((listBox.SelectedItem as MusicItemControl).Music as Music) });
            Settings.Default.MusicList = ToJSON(lj);
            Settings.Default.Save();
        }

        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            IslistBoxInfo = 2;
            jz.Visibility = Visibility.Visible;
            listBox.Items.Clear();
            ListJson lj = new Lemon_App.ListJson();
            lj = JsonToObject(Settings.Default.MusicList, lj) as ListJson;
            for (int i = 0; i < lj.List.Count; i++)
            {
                string os = "";
                if (lj.List[i].ItemText.Fotmat != "0") { os = "SQ"; }
                if (lj.List[i].ItemText.HQFOTmat != "0")
                    if (lj.List[i].ItemText.Fotmat == "0")
                        os = "HQ";
                listBox.Items.Add(new MusicItemControl() {Width=this.ActualWidth-10 , MusicGS =lj.List[i].ItemText.Singer, MusicName = lj.List[i].ItemText.MusicName, MusicZJ = lj.List[i].ItemText.ZJ,Qt=os, Music = lj.List[i].ItemText , ismv = lj.List[i].ItemText.MV });
            }
            jz.Visibility = Visibility.Collapsed;
            listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (Settings.Default.MusicSearch!= "null")
            {
                data = (List<string>)JSON.JsonToObject(Settings.Default.MusicSearch, data);
                for (int i = 0; i != data.Count; i++)
                {
                    var co = new LZoneItemControl();
                    co.S.MouseDown += Mou;
                    co.grid.MouseDown += delegate (object senders, MouseButtonEventArgs es)
                     {
                         textBox.Text = (senders as Grid).ToolTip.ToString();
                     };
                    co.QZoneData = data[i];
                    this.SearchDataContent.Children.Insert(0, co);
                }
            }

            Window.GetWindow(this).Closed += delegate {
                Bass.BASS_ChannelStop(stream);
                Bass.BASS_StreamFree(stream);
                Bass.BASS_Stop();
                Bass.BASS_Free();
            };
            Window.GetWindow(this).LocationChanged += delegate
            {
                var offset = popup.HorizontalOffset;
                popup.HorizontalOffset = offset + 1;
                popup.HorizontalOffset = offset;
                var offset1 = popup1.HorizontalOffset;
                popup1.HorizontalOffset = offset1 + 1;
                popup1.HorizontalOffset = offset1;
                var offsetetet = popupop.HorizontalOffset;
                popupop.HorizontalOffset = offsetetet + 1;
                popupop.HorizontalOffset = offsetetet;
                var offsetetets = popup2.HorizontalOffset;
                popup2.HorizontalOffset = offsetetets + 1;
                popup2.HorizontalOffset = offsetetets;
            };

            audio.Value = Bass.BASS_GetVolume();
            LyricShow.CFontFamily = this.FontFamily;
            LyricShow.HFontFamily = this.FontFamily;
            Move.SelectedIndex = Settings.Default.sx;

            JObject json = JObject.Parse(await Uuuhh.GetWebAsync("http://59.37.96.220/soso/fcgi-bin/dynamic_content?format=json&outCharset=utf-8", Encoding.UTF8));
            textBox.Text = json["data"]["search_content"].ToString();

            RotateTransform rtf = new RotateTransform();// { CenterX = 0.5,CenterY = 0.5};
            os.RenderTransform = rtf;
            os.RenderTransformOrigin = new Point(0.5, 0.5);
            DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(5))) { RepeatBehavior = RepeatBehavior.Forever };
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);

            Border_MouseDown_3(null, null);
                    //ListJson lj = new Lemon_App.ListJson();
                    //lj = JsonToObject(Settings.Default.MusicList, lj) as ListJson;
                    //for (int i = 0; i < lj.List.Count; i++)
                    //{
                    //    string os = "";
                    //    if (lj.List[i].ItemText.Fotmat != "0") { os = "SQ"; }
                    //    if (lj.List[i].ItemText.HQFOTmat != "0")
                    //        if (lj.List[i].ItemText.Fotmat == "0")
                    //            os = "HQ";
                    //    listBox.Items.Add(new MusicItemControl() {Width=this.ActualWidth, MusicGS = lj.List[i].ItemText.Singer, MusicName = lj.List[i].ItemText.MusicName, MusicZJ = lj.List[i].ItemText.ZJ, Music = lj.List[i].ItemText,Qt=os,ismv = lj.List[i].ItemText.MV});
                    //}
        }

        private void pz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox_SelectionChanged(null, null);
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            LyricShow.refreshLyricShowUIWhenChangeWINOrFontSize();
            foreach (var o in listBox.Items)
            {
                (o as UserControl).Width = this.ActualWidth-10;
            }
            if (this.ActualWidth < 400)
            {
                H.Visibility = Visibility.Collapsed;
                Q.Visibility = Visibility.Collapsed;
                __PAGE.Visibility = Visibility.Collapsed;
            }
            else
            {
                H.Visibility = Visibility.Visible;
                Q.Visibility = Visibility.Visible;
                __PAGE.Visibility = Visibility.Visible;
            }
        }

        private async void Border_MouseDown_3(object sender, MouseButtonEventArgs e)
        {
            try
            {
                IslistBoxInfo = 1;
                if (Uuuhh.Lalala("www.mi.com"))
                {
                    long ox = 0;
                    if (Settings.Default.ZJid != "null" && textBox.Text == string.Empty) { textBox.Text = Settings.Default.ZJid; }
                    else { if (long.TryParse(textBox.Text, out ox)) { Settings.Default.ZJid = textBox.Text; Settings.Default.Save(); } else { textBox.Text = Settings.Default.ZJid; } }
                    listBox.Items.Clear();
                    jz.Visibility = Visibility.Visible;
                    var s = await GetWebAsync($"https://y.qq.com/n/yqq/playlist/{textBox.Text}.html#stat=y_new.profile.create_playlist.click&dirid=6");
                    var j = "{\"list\":" + He.Text(s, "var getSongInfo = ", ";", 0) + "}";
                    JObject o = JObject.Parse(j);
                    int i = 0;
                    while (i != o["list"].Count())
                    {
                        Music m = new Music()
                        {
                            MusicName = o["list"][i]["songname"].ToString(),
                            Singer = o["list"][i]["singer"][0]["name"].ToString(),
                            ZJ = o["list"][i]["albumdesc"].ToString(),
                            GC = o["list"][i]["songid"].ToString(),
                            Fotmat = o["list"][i]["sizeflac"].ToString(),
                            HQFOTmat = o["list"][i]["size320"].ToString(),
                            MusicID = o["list"][i]["songmid"].ToString(),
                            ImageID = o["list"][i]["albummid"].ToString(),
                            MV = o["list"][i]["vid"].ToString()
                        };
                        string Q = "";
                        if (m.Fotmat != "0")
                            Q = "SQ";
                        if (m.HQFOTmat != "0")
                            if (m.Fotmat == "0")
                                Q = "HQ";
                        listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 10, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                        i++;
                    }
                    listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));

                    List<Music> l = new List<Music>();
                    int ic = 0;
                    while (ic != listBox.Items.Count)
                    {
                        l.Add((listBox.Items[ic] as MusicItemControl).Music as Music);
                        ic++;
                    }
                    Settings.Default.MusicGD = ToJSON(l);
                    Settings.Default.Save();
                }
                else
                {
                    List<Music> lj = new List<Music>();
                    lj = JsonToObject(Settings.Default.MusicGD, lj) as List<Music>;
                    for (int i = 0; i < lj.Count; i++)
                    {
                        string os = "";
                        if (lj[i].Fotmat != "0") { os = "SQ"; }
                        if (lj[i].HQFOTmat != "0")
                            if (lj[i].Fotmat == "0")
                                os = "HQ";
                        listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth, MusicGS = lj[i].Singer, MusicName = lj[i].MusicName, MusicZJ = lj[i].ZJ, Music = lj[i], Qt = os, ismv = lj[i].MV });
                    }
                 }
                }
            catch { jz.Visibility = Visibility.Collapsed; List<Music> lj = new List<Music>();
                lj = JsonToObject(Settings.Default.MusicGD, lj) as List<Music>;
                for (int i = 0; i < lj.Count; i++)
                {
                    string os = "";
                    if (lj[i].Fotmat != "0") { os = "SQ"; }
                    if (lj[i].HQFOTmat != "0")
                        if (lj[i].Fotmat == "0")
                            os = "HQ";
                    listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth, MusicGS = lj[i].Singer, MusicName = lj[i].MusicName, MusicZJ = lj[i].ZJ, Music = lj[i], Qt = os, ismv = lj[i].MV });
                }
            }
        }int hs = 0;
        private DeskLyricWin deskLyricWin;

        private void textBlock1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(hs==0)
            {
                h.Visibility = Visibility.Collapsed;
                G.Visibility = Visibility.Collapsed;
                ZjImAgE.Visibility = Visibility.Visible;
                hs = 1;
                ZjImAgE.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-40, 40, 40, -20), new Thickness(0, 0, 0, 49), TimeSpan.FromSeconds(0.1)));
                h.Margin = new Thickness(-40, 40, 40, -20);
                G.Margin = new Thickness(-40, 40, 40, -20);
            }
            else if (hs == 1)
            {
                hs = 0;
                h.Visibility = Visibility.Visible;
                G.Visibility = Visibility.Collapsed;
                ZjImAgE.Visibility = Visibility.Collapsed;
                ZjImAgE.Margin = new Thickness(-40, 40, 40, -20);
                G.Margin = new Thickness(-40, 40, 40, -20);
                h.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(- 40, 40, 40, -20), new Thickness(0, 0, 0, 49), TimeSpan.FromSeconds(0.1)));
            }
        }

        private void Border_MouseDown_4(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedIndex != 0)
            {
                listBox.SelectedItem = listBox.Items[listBox.SelectedIndex - 1];
               // listBox_SelectionChanged(null, null);
            }
        }

        private void Border_MouseDown_6(object sender, MouseButtonEventArgs e)
        {
            if (listBox.SelectedIndex != listBox.Items.Count)
            {
                listBox.SelectedItem = listBox.Items[listBox.SelectedIndex + 1];
              //  listBox_SelectionChanged(null, null);
            }
        }

        private async void DF_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((DF.SelectedItem as ComboBoxItem).ToolTip.ToString() != "巅峰榜")
            {
                IslistBoxInfo = 3;
                jz.Visibility = Visibility.Hidden;
                listBox.Items.Clear();
                int i = 0;
                JObject o = JObject.Parse(await GetWebAsync($"https://route.showapi.com/213-4?&showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&topid={(DF.SelectedItem as ComboBoxItem).ToolTip}"));
                while (i != o["showapi_res_body"]["pagebean"]["songlist"].Count())
                {
                    Music m = new Music()
                    {
                        MusicName = o["showapi_res_body"]["pagebean"]["songlist"][i]["songname"].ToString(),
                        Singer = o["showapi_res_body"]["pagebean"]["songlist"][i]["singername"].ToString(),
                        ZJ = "",
                        IsDF = true,
                        DFSONGURI = o["showapi_res_body"]["pagebean"]["songlist"][i]["url"].ToString(),
                        DFSONGURI_HQ = o["showapi_res_body"]["pagebean"]["songlist"][i]["downUrl"].ToString(),
                        GC = o["showapi_res_body"]["pagebean"]["songlist"][i]["songid"].ToString(),
                        Fotmat = "",
                        HQFOTmat = "",
                        MusicID = o["showapi_res_body"]["pagebean"]["songlist"][i]["albummid"].ToString(),
                        ImageID = o["showapi_res_body"]["pagebean"]["songlist"][i]["albummid"].ToString(),
                        MV =""
                    };
                    string Q = "";
                    listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth-10, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                    i++;
                }
                jz.Visibility = Visibility.Collapsed;
                listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
            }
        }

        private void Move_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Settings.Default.sx = Move.SelectedIndex;
            Settings.Default.Save();
        }
        int downloadindex = 0;
        string datadlwo = "";
        ItemCollection ic;
        private async void Border_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
                if (e.ClickCount >= 2)
            {
                datadlwo = textBox.Text;
                if (DownloadPath == "xxx")
                    DownloadPath = AppDomain.CurrentDomain.BaseDirectory + datadlwo;
                if (Directory.Exists(DownloadPath) == false)
                    Directory.CreateDirectory(DownloadPath);
                downloadindex = 0;
                    popupop.IsOpen = true;
                ic = listBox.Items;
                    download_name.Text = (ic[downloadindex] as MusicItemControl).Content;
                    string guid = "20D919A4D7700FBC424740E8CED80C5F";
                    string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                    string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                    musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{((ic[downloadindex] as MusicItemControl).Music as Music).MusicID}.mp3?vkey={vkey}&guid={guid}";
                dc = new WebClient()
                {
                    Proxy = He.proxy
                };
                dc.DownloadFileCompleted += OsAsync;
                    dc.DownloadProgressChanged += As;
                    dc.DownloadFileAsync(new Uri(musicurl),DownloadPath+(ic[downloadindex] as MusicItemControl).Content.Replace("\\", ",").Replace("/", ",")+".mp3");
                }
            if (e.ClickCount == 1)
            {
                if (!LyricShow.IsOpenDeskLyric)
                {
                    RotateTransform rtf = new RotateTransform();
                    (sender as Border).RenderTransform = rtf;
                    (sender as Border).RenderTransformOrigin = new Point(0.5, 0.5);
                    DoubleAnimation dbAscending = new DoubleAnimation(0, 90, new Duration(TimeSpan.FromSeconds(0.2)));
                    rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
                    deskLyricWin = new DeskLyricWin();
                    deskLyricWin.Show();
                    LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                }
                else
                {
                    RotateTransform rtf = new RotateTransform();
                    (sender as Border).RenderTransform = rtf;
                    (sender as Border).RenderTransformOrigin = new Point(0.5, 0.5);
                    DoubleAnimation dbAscending = new DoubleAnimation(90, 0, new Duration(TimeSpan.FromSeconds(0.2)));
                    rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
                    deskLyricWin.Close();
                    LyricShow.IsOpenDeskLyric = false;
                }
            }
        }
        WebClient dc = new WebClient()
        {
            Proxy = He.proxy
        };
        private void As(object sender, DownloadProgressChangedEventArgs e)
        {
            p.Value = e.ProgressPercentage;
        }

        private async void OsAsync(object sender, AsyncCompletedEventArgs e)
        {
            if (downloadindex != ic.Count)
            {
                downloadindex++;
                    download_name.Text = (ic[downloadindex] as MusicItemControl).Content;
                    string guid = "20D919A4D7700FBC424740E8CED80C5F";
                    string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                    string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                    musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{((ic[downloadindex] as MusicItemControl).Music as Music).MusicID}.mp3?vkey={vkey}&guid={guid}";
                dc.DownloadFileAsync(new Uri(musicurl), DownloadPath + (ic[downloadindex] as MusicItemControl).Content.Replace("\\", ",").Replace("/", ",") + ".mp3");
            }
                else { popupop.IsOpen = false; MessageBox.Show("成功下载全部"); dc.Dispose();ic.Clear(); }
        }

        public bool IsVerticalScrollBarAtButtom(ScrollViewer o)
        {
                bool isAtButtom = false;

                // get the vertical scroll position
                double dVer = o.VerticalOffset;

                //get the vertical size of the scrollable content area
                double dViewport = o.ViewportHeight;

                //get the vertical size of the visible content area
                double dExtent = o.ExtentHeight;

                if (dVer != 0)
                {
                    if (dVer + dViewport == dExtent)
                    {
                        isAtButtom = true;
                    }
                    else
                    {
                        isAtButtom = false;
                    }
                }
                else
                {
                    isAtButtom = false;
                }

                if (o.VerticalScrollBarVisibility == ScrollBarVisibility.Disabled
                    || o.VerticalScrollBarVisibility == ScrollBarVisibility.Hidden)
                {
                    isAtButtom = true;
                }

                return isAtButtom;
        }

        private async void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {if (IslistBoxInfo == 0)
            {
                if (IsVerticalScrollBarAtButtom(sender as ScrollViewer))
                {
                    ioi++;
                    JObject o = JObject.Parse(await GetWebAsync($"http://59.37.96.220/soso/fcgi-bin/client_search_cp?format=json&t=0&inCharset=GB2312&outCharset=utf-8&qqmusic_ver=1302&catZhida=0&p={ioi}&n=20&w={textBox.Text}&flag_qc=0&remoteplace=sizer.newclient.song&new_json=1&lossless=0&aggr=1&cr=1&sem=0&force_zonghe=0", Encoding.UTF8));
                    int i = 0;
                    if (o["code"].ToString() == "0") IslistBoxInfo = -1;
                    while (i < o["data"]["song"]["list"].Count())
                    {
                        //string f = o["data"]["song"]["list"][i]["f"].ToString().Replace("|", "\r\n");
                        //string[] ContentLines = f.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //string Gs = o["data"]["song"]["list"][i]["fsinger"].ToString();
                        //string songname = o["data"]["song"]["list"][i]["fsong"].ToString();
                        //string Zhj = o["data"]["song"]["list"][i]["albumName_hilight"].ToString();
                        //    img = ContentLines[22];
                        Music m = new Music();
                        m.MusicName = o["data"]["song"]["list"][i]["name"].ToString();
                        string Singer = "";
                        for (int osxc = 0; osxc != o["data"]["song"]["list"][i]["singer"].Count(); osxc++)
                        { Singer += o["data"]["song"]["list"][i]["singer"][osxc]["name"] + "/"; }
                        m.Singer = Singer.Substring(0, Singer.LastIndexOf("/"));
                        m.ZJ = o["data"]["song"]["list"][i]["album"]["name"].ToString();
                        m.MusicID = o["data"]["song"]["list"][i]["mid"].ToString();
                        m.ImageID = o["data"]["song"]["list"][i]["album"]["mid"].ToString();
                        m.GC = o["data"]["song"]["list"][i]["id"].ToString();
                        m.Fotmat = o["data"]["song"]["list"][i]["file"]["size_flac"].ToString();
                        m.HQFOTmat = o["data"]["song"]["list"][i]["file"]["size_ogg"].ToString();
                        m.MV = o["data"]["song"]["list"][i]["mv"]["id"].ToString();
                        string Q = "";
                        if (m.Fotmat != "0")
                            Q = "SQ";
                        if (m.HQFOTmat != "0")
                            if (m.Fotmat == "0")
                                Q = "HQ";
                        listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth-10, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                        i++;
                    }
                    jz.Visibility = Visibility.Collapsed;
                    listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
            }
        }

        private void audio_MouseMove(object sender, MouseEventArgs e)
        {
            txl.Text = (audio.Value *100).ToString("F0") + "%";
            Bass.BASS_SetVolume(float.Parse(audio.Value.ToString("F2")));
        }

        private void hsq_MouseLeave(object sender, MouseEventArgs e)
        {
            txl.Text = "倍速/音量";
     //       player.SpeedRatio = hsq.Value;
        }

        private void hsq_MouseMove(object sender, MouseEventArgs e)
        {
            if (hsq.Value >= 0.5 && hsq.Value < 1)
                txl.Text = "滑稽";
            else if (hsq.Value <= 1)
                txl.Text = "正常";
            else if (hsq.Value <= 2.25 && hsq.Value > 1)
                txl.Text = "鬼畜";
            else if (hsq.Value <= 3)
                txl.Text = "鬼大畜";
        }

        private void audio_MouseLeave(object sender, MouseEventArgs e)
        {
            txl.Text = "倍速/音量";
        }

        private void txl_MouseDown(object sender, MouseButtonEventArgs e)
        {
            hsq.Value = 1;
            audio.Value = 0.5;
            txl.Text = "倍速/音量";
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dc.Dispose();
            ic.Clear();
            popupop.IsOpen = false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LyricShow.IsLyFanyi = !LyricShow.IsLyFanyi;
            LyricShow.stopLyricShow();
            LyricShow.TimeAndLyricDictionary.Clear();
            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
            LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);
            LyricShow.refreshDeskLyricUIWhenChangeWINOrFontSize();
        }
        string DownloadPath = "xxx";
        private void Border_MouseDown_7(object sender, MouseButtonEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog fbd = new System.Windows.Forms.FolderBrowserDialog();
            fbd.ShowDialog();
            if (fbd.SelectedPath != string.Empty)
               DownloadPath = fbd.SelectedPath;
        }
    }
}
