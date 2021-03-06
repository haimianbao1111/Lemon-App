﻿using static Lemon_App.Uuuhh;
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
        System.Windows.Threading.DispatcherTimer t = new System.Windows.Threading.DispatcherTimer();
        int IslistBoxInfo = 1;//0=Search,1=G2d,2=SC,3=DFB
        public MusicControl()
        {
            InitializeComponent();
            t.Interval = TimeSpan.FromSeconds(0.1);
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
                Bass.BASS_ChannelSetPosition(stream, 0);
                Bass.BASS_ChannelStop(stream);
                jd.Value = 0;
                s.Data = Geometry.Parse("M118.2,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6h-36c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H118.2z M46,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6H10c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H46z");
                string i = "";
                if (Move.ToolTip.ToString() == "列表循环")
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
                else if (Move.ToolTip.ToString() == "单曲循环")
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
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            // player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3"));
            //      player.Play();
            t.Start();
            loading.Text = "";
        }

        private void Tick(object sender, EventArgs e)
        {
            if (G.Visibility == Visibility.Visible)
            {
                float[] fft = new float[1024];
                Bass.BASS_ChannelGetData(stream, fft, (int)BASSData.BASS_DATA_FFT1024);
                if (fft[0] >= 0.1 || fft[11] >= 0.1 || fft[22] >= 0.1 || fft[33] >= 0.1)
                    (Resources["D"] as Storyboard).Begin();
            }
            if (Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)) == Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream)))
                io();
            LyricShow.refreshLyricShow(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)));
            jd.Maximum = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetLength(stream));
            jd.Value = Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream));
        }
        int ioi = 3;
        List<string> data = new List<string>();
        private void Mou(object sender, MouseButtonEventArgs e)
        {
            data.Remove((sender as Border).ToolTip as string);
            He.Settings.MusicSearch = JSON.ToJSON(data);
            He.SaveSettings();
            // MessageBox.Show(JSON.ToJSON(data));
        }
        private async void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Key == Key.Enter)
                {
                    if (!data.Contains(textBox.Text))
                    {
                        var co = new LZoneItemControl();
                        co.S.MouseDown += Mou;
                        co.QZoneData = textBox.Text;
                        co.BeginAnimation(OpacityProperty, new System.Windows.Media.Animation.DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
                        this.SearchDataContent.Children.Insert(0, co);
                        data.Add(textBox.Text);
                        //   MessageBox.Show(JSON.ToJSON(data));
                        He.Settings.MusicSearch = JSON.ToJSON(data);
                        He.SaveSettings();
                    }
                    int i = 0;
                    try
                    {
                        IslistBoxInfo = 0;

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
                                Music m = new Music()
                                {
                                    MusicName = o["data"]["song"]["list"][i]["name"].ToString()
                                };
                                string Singer = "";
                                for (int osxc = 0; osxc != o["data"]["song"]["list"][i]["singer"].Count(); osxc++)
                                { Singer += o["data"]["song"]["list"][i]["singer"][osxc]["name"] + "/"; }
                                m.Singer = Singer.Substring(0, Singer.LastIndexOf("/"));
                                m.ZJ = o["data"]["song"]["list"][i]["album"]["name"].ToString();
                                m.MusicID = o["data"]["song"]["list"][i]["mid"].ToString();
                                m.ImageID = o["data"]["song"]["list"][i]["album"]["mid"].ToString();
                                m.SL_128ID= o["data"]["song"]["list"][i]["file"]["media_mid"].ToString();
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
                                if (o["data"]["song"]["list"][i]["file"]["size_320"].ToString() == "0")
                                    m.SL_128 = true;
                                else m.SL_128 = false;
                                listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 15, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                                i++;
                            }
                            osx++;
                            i = 0;
                        }

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

        private async Task<GetLyricAndLyricTime> GetLyricAsync(string McMind)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{McMind}.lrc"))
            {
                WebClient c = new WebClient();
                c.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/57.0.2987.110 Safari/537.36");
                c.Headers.Add("Accept", "*/*");
                c.Headers.Add("Referer", "https://y.qq.com/portal/player.html");
                //      c.Headers.Add("Accept-Encoding", "gzip, deflate, sdch, br");
                c.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8");
                c.Headers.Add("Cookie", "tvfe_boss_uuid=c3db0dcc4d677c60; pac_uid=1_2728578956; qq_slist_autoplay=on; ts_refer=ADTAGh5_playsong; RK=pKOOKi2f1O; pgv_pvi=8927113216; o_cookie=2728578956; pgv_pvid=5107924810; ptui_loginuin=2728578956; ptcz=897c17d7e17ae9009e018ebf3f818355147a3a26c6c67a63ae949e24758baa2d; pt2gguin=o2728578956; pgv_si=s5715204096; qqmusic_fromtag=66; yplayer_open=1; ts_last=y.qq.com/portal/player.html; ts_uid=996779984; yq_index=0");
                c.Headers.Add("Host", "c.y.qq.com");
                string s = Text(c.DownloadString($"https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric_new.fcg?callback=MusicJsonCallback_lrc&pcachetime=1494070301711&songmid={McMind}&g_tk=5381&jsonpCallback=MusicJsonCallback_lrc&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0"), "MusicJsonCallback_lrc(", ")", 0);
                Console.WriteLine(s);
                JObject o = JObject.Parse(s);
                string t = Encoding.UTF8.GetString(Convert.FromBase64String(o["lyric"].ToString())).Replace("&apos;", "\'");
                string x = Encoding.UTF8.GetString(Convert.FromBase64String(o["trans"].ToString())).Replace("&apos;", "\'");
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "MusicCache/" + McMind + ".lrc", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                await sw.WriteAsync(t);
                await sw.FlushAsync();
                fs.Dispose();
                FileStream fss = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "MusicCaChe/" + McMind + "d.lrc", FileMode.Create);
                StreamWriter sws = new StreamWriter(fss);
                await sws.WriteAsync(x);
                await sws.FlushAsync();
                fss.Dispose();
                GetLyricAndLyricTime getLT = new GetLyricAndLyricTime();
                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + "MusicCache/" + McMind + ".lrc");
                GetLyricAndLyricTime getLTD = new GetLyricAndLyricTime();
                getLTD.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + "MusicCaChe/" + McMind + "d.lrc");
                GetLyricAndLyricTime LT = new GetLyricAndLyricTime();
                foreach (var key in getLT.LyricAndTimeDictionary.Keys)
                {
                    if (!LT.LyricAndTimeDictionary.ContainsKey(key))
                        LT.LyricAndTimeDictionary.Add(key, "");
                }
                foreach (var key in getLTD.LyricAndTimeDictionary.Keys)
                {
                    if (!LT.LyricAndTimeDictionary.ContainsKey(key))
                        LT.LyricAndTimeDictionary.Add(key, "");
                }
                foreach (var ele in getLT.LyricAndTimeDictionary)
                {
                    try
                    {
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key] + "^" + ele.Value;
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key].TrimStart('^');
                    }
                    catch { }
                }
                foreach (var ele in getLTD.LyricAndTimeDictionary)
                {
                    try
                    {
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key] + "^" + ele.Value;
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key].TrimStart('^');
                    }
                    catch { }
                }

                return LT;
            }
            else
            {
                GetLyricAndLyricTime getLT = new GetLyricAndLyricTime();
                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + "MusicCache/" + McMind + ".lrc");
                GetLyricAndLyricTime getLTD = new GetLyricAndLyricTime();
                getLTD.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + "MusicCaChe/" + McMind + "d.lrc");
                GetLyricAndLyricTime LT = new GetLyricAndLyricTime();
                foreach (var key in getLT.LyricAndTimeDictionary.Keys)
                {
                    if (!LT.LyricAndTimeDictionary.ContainsKey(key))
                        LT.LyricAndTimeDictionary.Add(key, "");
                }
                foreach (var key in getLTD.LyricAndTimeDictionary.Keys)
                {
                    if (!LT.LyricAndTimeDictionary.ContainsKey(key))
                        LT.LyricAndTimeDictionary.Add(key, "");
                }
                foreach (var ele in getLT.LyricAndTimeDictionary)
                {
                    try
                    {
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key] + "^" + ele.Value;
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key].TrimStart('^');
                    }
                    catch { }
                }

                foreach (var ele in getLTD.LyricAndTimeDictionary)
                {
                    try
                    {
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key] + "^" + ele.Value;
                        LT.LyricAndTimeDictionary[ele.Key] = LT.LyricAndTimeDictionary[ele.Key].TrimStart('^');
                    }
                    catch { }
                }
                return LT;
            }
        }
        private async Task<GetLyricAndLyricTime> GetSLLyricAsync(string McMind)
        {
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{SL_128ID + McMind}.lrc"))
            {
                WebClient c = new WebClient();
                c.Headers.Add(HttpRequestHeader.Cookie,"pgv_pvid=1471514709; pgv_pvi=1420463104; pgv_si=s8512178176; yplayer_open=1; ts_last=y.qq.com/portal/player.html; ts_uid=6022195987; yq_index=0; qqmusic_fromtag=66; player_exist=1");
                c.Headers.Add(HttpRequestHeader.Referer, "https://y.qq.com/portal/player.html");
                c.Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.90 Safari/537.36");
                var data = Text(c.DownloadString("https://c.y.qq.com/lyric/fcgi-bin/fcg_query_lyric_new.fcg?callback=MusicJsonCallback_lrc&pcachetime=1502256745550&songmid=004ZhRKm1nk88f&songtype=111&g_tk=5381&jsonpCallback=MusicJsonCallback_lrc&loginUin=0&hostUin=0&format=jsonp&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0"), "MusicJsonCallback_lrc(", ")", 0);
                JObject o = JObject.Parse(data);
                string t = Encoding.UTF8.GetString(Convert.FromBase64String(o["lyric"].ToString())).Replace("&apos;", "\'");
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + "MusicCache/" +SL_128ID+McMind + ".lrc", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                await sw.WriteAsync(t);
                await sw.FlushAsync();
                await Task.Delay(1000);
                GetLyricAndLyricTime getLT = new GetLyricAndLyricTime();
                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + "MusicCache/" +SL_128ID+ McMind + ".lrc");
                return getLT;
            }
            else
            {
                GetLyricAndLyricTime getLT = new GetLyricAndLyricTime();
                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + "MusicCache/" + SL_128ID + McMind + ".lrc");
                return getLT;
            }
        }
        bool autois = true;
        string  SL_128ID = "";
        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                if (((listBox.SelectedItem as MusicItemControl).Music as Music).SL_128 == false)
                {
                    if ((listBox.SelectedItem as MusicItemControl).RS.Visibility != Visibility.Visible)
                        if ((listBox.SelectedItem as MusicItemControl).RC.Visibility != Visibility.Visible)
                            if (pz.Text == "HQ") pz.Text = "标准";
                    if (autois)
                    {
                        if ((listBox.SelectedItem as MusicItemControl).RS.Visibility == Visibility.Visible)
                            pz.Text = "HQ";
                        if ((listBox.SelectedItem as MusicItemControl).RC.Visibility == Visibility.Visible)
                            pz.Text = "HQ";
                    }
                }
                else pz.Text = "经济";
                LyricShow.refreshLyricShow(0);
                LyricShow.clearLyricShowAllText();
                jd.Maximum = 1;
                jd.Value = 0;

                s.Margin = new Thickness(11);
                jd.Value = 0;
                Bass.BASS_ChannelStop(stream);
                LyricShow.F = true;
                isR = true;
                s.Data = Geometry.Parse("M118.2,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6h-36c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H118.2z M46,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6H10c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H46z");
                string i = (listBox.SelectedItem as MusicItemControl).Content.Replace("\\", ",").Replace("/", ",");
                lrcname.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicName;
                zk.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).Singer;
                musicid = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicID;
                try
                {
                    img = ((listBox.SelectedItem as MusicItemControl).Music as Music).ImageID;
                    on = $"http://y.gtimg.cn/music/photo_new/T002R300x300M000{img}.jpg";
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{img}.jpg"))
                    {
                        new WebClient().DownloadFile(new Uri(He.on), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{img}.jpg");
                        tx.Background = new ImageBrush(new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{img}.jpg").ToImageSource());
                    }
                    else tx.Background = new ImageBrush(new System.Drawing.Bitmap(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{img}.jpg").ToImageSource());
                }
                catch { tx.Background = Resources["Z"] as VisualBrush; }
                Console.WriteLine(musicid);
                if (((listBox.SelectedItem as MusicItemControl).Music as Music).SL_128 == false)
                {
                    issl = false;
                    if (!((listBox.SelectedItem as MusicItemControl).Music as Music).IsDF)
                    {
                        musicid = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicID;
                        if (pz.Text == "经济")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a"))
                            {
                                musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                                WebClient dc = new WebClient()
                                {
                                    Proxy = He.proxy
                                };
                                dc.DownloadFileCompleted += Fi_BZ;
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a");
                                ///等待播放
                                loading.Text = "加载中...";
                            }
                            else
                            {
                                //player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a"));
                                //player.Play();
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                                loading.Text = "";
                            }
                            LyricShow.IsPauseLyricShow = false;
                            var dt = await GetLyricAsync(musicid);
                            LyricShow.initializeLyricUIAsync(dt.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                        }
                        else if (pz.Text == "标准")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3"))
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
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3");
                                ///等待播放
                                loading.Text = "加载中...";
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                                loading.Text = "";
                            }
                            LyricShow.IsPauseLyricShow = false;
                            var dt = await GetLyricAsync(musicid);
                            LyricShow.initializeLyricUIAsync(dt.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                        }
                        else if (pz.Text == "HQ")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg"))
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
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg");
                                ///等待播放
                                loading.Text = "加载中...";
                            }
                            else
                            {
                                if (new FileInfo(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg").Length != 0)
                                {
                                    Bass.BASS_ChannelStop(stream);
                                    stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                    Bass.BASS_ChannelPlay(stream, true);
                                    t.Start();
                                    loading.Text = "";
                                }
                                else
                                {
                                    File.Delete(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg");
                                    loading.Text = "当前通道不稳定，已为你切换到标准品质";
                                    pz.Text = "标准";
                                    autois = false;
                                    await Task.Delay(1000);
                                    listBox_SelectionChanged(null, null);
                                }
                            }
                            LyricShow.IsPauseLyricShow = false;
                            var dt = await GetLyricAsync(musicid);
                            LyricShow.initializeLyricUIAsync(dt.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                        }
                    }
                    else
                    {

                        if (pz.Text == "标准")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a"))
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
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a");
                                ///等待播放
                                loading.Text = "加载中...";
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                                loading.Text = "";
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc", FileMode.Create);
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
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                        else if (pz.Text == "HQ")
                        {
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3"))
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
                                dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3");
                                loading.Text = "加载中...";
                            }
                            else
                            {
                                Bass.BASS_ChannelStop(stream);
                                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                                Bass.BASS_ChannelPlay(stream, true);
                                t.Start();
                                loading.Text = "";
                            }
                            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc"))
                            {
                                string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc", FileMode.Create);
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
                                    LyricShow.IsPauseLyricShow = false;
                                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc");
                                    LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                                }
                                else { }
                            }
                            else
                            {
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc");
                                LyricShow.initializeLyricUIAsync(getLT.LyricAndTimeDictionary);

                            }
                        }
                    }
                }
                else
                {
                    issl = true;
                    SL_128ID = ((listBox.SelectedItem as MusicItemControl).Music as Music).SL_128ID;
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{SL_128ID+musicid}.m4a"))
                    {
                        var sv = $"https://c.y.qq.com/base/fcgi-bin/fcg_music_express_mobile3.fcg?g_tk=5381&loginUin=0&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0&cid=205361747&uin=0&songmid={musicid}&filename=C4L0{SL_128ID}.m4a&guid=2779011578";
                        Console.WriteLine(sv);
                        var st = await Uuuhh.GetWebAsync(sv, Encoding.UTF8);
                        Console.WriteLine(st);
                        JObject obj = JObject.Parse(st);
                        var VKEY = obj["data"]["items"][0]["vkey"];
                        musicurl= $"http://cc.stream.qqmusic.qq.com/C4L00017ZO4B0bI1hZ.m4a?vkey={VKEY}&guid=2779011578&uin=0&fromtag=66";
                        Console.WriteLine(musicurl);
                        WebClient dc = new WebClient()
                        {
                            Proxy = He.proxy
                        };
                        dc.DownloadFileCompleted += Fi_BZQ;
                        dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{SL_128ID+musicid}.m4a");
                        ///等待播放
                        loading.Text = "加载中...";
                    }
                    else
                    {
                        //player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a"));
                        //player.Play();
                        Bass.BASS_ChannelStop(stream);
                        stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{SL_128ID+musicid}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                        Bass.BASS_ChannelPlay(stream, true);
                        t.Start();
                        loading.Text = "";
                    }
                    LyricShow.IsPauseLyricShow = false;
                    var dt = await GetSLLyricAsync(musicid);
                    LyricShow.initializeLyricUIAsync(dt.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                }
                if (pz.Text == "经济")
                    musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                else if (pz.Text == "标准")
                {
                    string guid = "20D919A4D7700FBC424740E8CED80C5F";
                    string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                    string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                    musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{musicid}.mp3?vkey={vkey}&guid={guid}";
                }
                else if (pz.Text == "HQ")
                {
                    string guid = "20D919A4D7700FBC424740E8CED80C5F";
                    string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                    string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                    musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/O600{musicid}.ogg?vkey={vkey}&guid={guid}";
                }
                a = await GetWebAsync($"http://suo.im/api.php?url={Uri.EscapeDataString(musicurl)}");
                q2code.Background = new ImageBrush(new BitmapImage(new Uri($"http://qr.topscan.com/api.php?text={Uri.EscapeDataString(a)}")));
            }
        }
        string a = "";
        private async void Fi_Ogg(object sender, AsyncCompletedEventArgs e)
        {
            if (new FileInfo(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg").Length != 0)
            {
                Bass.BASS_ChannelStop(stream);
                stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
                Bass.BASS_ChannelPlay(stream, true);
                t.Start();
                loading.Text = "";
            }
            else
            {
                File.Delete(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.ogg");
                loading.Text = "当前通道不稳定，已为你切换到标准品质";
                pz.Text = "标准";
                autois = false;
                await Task.Delay(1000);
                listBox_SelectionChanged(null, null);
            }
        }

        private void Fi_BZ(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            t.Start();
            loading.Text = "";
        }
        private void Fi_BZQ(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{SL_128ID+musicid}.m4a", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            t.Start();
            loading.Text = "";
        }
        private void Fi(object sender, AsyncCompletedEventArgs e)
        {
            Bass.BASS_ChannelStop(stream);
            stream = Bass.BASS_StreamCreateFile(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.mp3", 0L, 0L, BASSFlag.BASS_SAMPLE_FLOAT);
            Bass.BASS_ChannelPlay(stream, true);
            t.Start();
            loading.Text = "";
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
            { t.Stop(); isR = false; s.Margin = new Thickness(11, 10, 9, 10); Bass.BASS_ChannelPause(stream); s.Data = Geometry.Parse("M40.2,3.8C36.4,0,30,2.7,30,8v112c0,5.3,6.4,8,10.2,4.2l56-56c2.3-2.3,2.3-6.1,0-8.4L40.2,3.8z"); }
            else { t.Start(); isR = true; s.Margin = new Thickness(11); Bass.BASS_ChannelPlay(stream, true); Bass.BASS_ChannelSetPosition(stream, jd.Value); s.Data = Geometry.Parse("M118.2,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6h-36c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H118.2z M46,125.9c3.3,0,6-2.7,6-6V7.4c0-3.3-2.7-6-6-6H10c-3.3,0-6,2.7-6,6v112.5c0,3.3,2.7,6,6,6H46z"); }
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
            dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicDownload/{musicid}.{op}");
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
                G.Visibility = Visibility.Visible;
                BlurPage.Visibility = Visibility.Visible;
                h.Margin = new Thickness(-40, 40, 40, -20);
                (Resources["START"] as Storyboard).Begin();
                (Window.GetWindow(this).Resources["START"] as Storyboard).Begin();
                BlurPage.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-40, 40, 40, -20), new Thickness(0), TimeSpan.FromSeconds(0.1)));
                G.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-40, 40, 40, -20), new Thickness(0, 0, 0, 70), TimeSpan.FromSeconds(0.1)));
            }
            else
            {
                h.Visibility = Visibility.Visible;
                G.Visibility = Visibility.Collapsed;
                G.Margin = new Thickness(-40, 40, 40, -20);
                BlurPage.Visibility = Visibility.Collapsed;
                (Resources["STOP"] as Storyboard).Begin();
                (Window.GetWindow(this).Resources["STOP"] as Storyboard).Begin();
                h.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(-40, 40, 40, -20), new Thickness(0, 0, 0, 70), TimeSpan.FromSeconds(0.1)));
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
            if (He.Settings.MusicList != "") lj = JsonToObject(He.Settings.MusicList, lj) as ListJson;
            lj.List.Add(new ListItem() { ItemText = ((listBox.SelectedItem as MusicItemControl).Music as Music) });
            He.Settings.MusicList = ToJSON(lj);
            He.SaveSettings();
        }

        private void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            IslistBoxInfo = 2;

            listBox.Items.Clear();
            ListJson lj = new Lemon_App.ListJson();
            lj = JsonToObject(He.Settings.MusicList, lj) as ListJson;
            for (int i = 0; i < lj.List.Count; i++)
            {
                string os = "";
                if (lj.List[i].ItemText.Fotmat != "0") { os = "SQ"; }
                if (lj.List[i].ItemText.HQFOTmat != "0")
                    if (lj.List[i].ItemText.Fotmat == "0")
                        os = "HQ";
                listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 15, MusicGS = lj.List[i].ItemText.Singer, MusicName = lj.List[i].ItemText.MusicName, MusicZJ = lj.List[i].ItemText.ZJ, Qt = os, Music = lj.List[i].ItemText, ismv = lj.List[i].ItemText.MV });
            }

            listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (He.Settings.MusicSearch != "null")
            {
                data = (List<string>)JSON.JsonToObject(He.Settings.MusicSearch, data);
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

            Window.GetWindow(this).Closed += delegate
            {
                Bass.BASS_ChannelStop(stream);
                Bass.BASS_StreamFree(stream);
                Bass.BASS_Stop();
                Bass.BASS_Free();
            };
            PopopHelper.SetPopupPlacementTarget(popup, this);
            Window.GetWindow(this).LocationChanged += delegate
            {
                var offset1 = popup1.HorizontalOffset;
                popup1.HorizontalOffset = offset1 + 1;
                popup1.HorizontalOffset = offset1;
                var offsetetet = popupop.HorizontalOffset;
                popupop.HorizontalOffset = offsetetet + 1;
                popupop.HorizontalOffset = offsetetet;
                var offsetetets = popup2.HorizontalOffset;
                popup2.HorizontalOffset = offsetetets + 1;
                popup2.HorizontalOffset = offsetetets;
                var offsetetetssd = pu.HorizontalOffset;
                pu.HorizontalOffset = offsetetetssd + 1;
                pu.HorizontalOffset = offsetetetssd;
            };

            LyricShow.CFontFamily = this.FontFamily;
            LyricShow.HFontFamily = this.FontFamily;

            GETMID(Settings.ZJid);
            textBox.Text = "搜索";
            foreach (var dt in Settings.MIDLIST)
            {
                var Item = new MIDITEM();
                Item.SETMIDAsync(dt);
                MIDLIST.Items.Add(Item);
            }
            if(MIDLIST.Items.Count==0)
            {
                var Item = new MIDITEM();
                Item.SETMIDAsync("2591355982");
                MIDLIST.Items.Add(Item);
                Settings.MIDLIST.Add("2591355982");
                SaveSettings();
            }
            //ListJson lj = new Lemon_App.ListJson();
            //lj = JsonToObject(He.Settings.MusicList, lj) as ListJson;
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
            if (ActualWidth > 700)
            {
                (Resources["MIN"] as Storyboard).Stop();
                (Resources["MAX"] as Storyboard).Begin();
            }
            else {
                (Resources["MAX"] as Storyboard).Stop();
                (Resources["MIN"] as Storyboard).Begin(); }
            LyricShow.refreshLyricShowUIWhenChangeWINOrFontSize();
            foreach (var o in listBox.Items)
            {
                (o as UserControl).Width = this.ActualWidth - 15;
            }
        }

        private async void GETMID(string MID= "2591355982")
        {
            IslistBoxInfo = 1;
            if (Uuuhh.Lalala("www.mi.com"))
            {
                Settings.ZJid = MID;
                SaveSettings();
                listBox.Items.Clear();
                var s = await GetWebDatacAsync($"https://c.y.qq.com/qzone/fcg-bin/fcg_ucc_getcdinfo_byids_cp.fcg?type=1&json=1&utf8=1&onlysong=0&disstid={MID}&format=json&g_tk=1157737156&loginUin=2728578956&hostUin=0&format=json&inCharset=utf8&outCharset=utf-8&notice=0&platform=yqq&needNewCode=0", Encoding.UTF8);
                JObject o = JObject.Parse(s);
                int i = 0;
                while (i != o["cdlist"][0]["songlist"].Count())
                {
                    Music m = new Music()
                    {
                        MusicName = o["cdlist"][0]["songlist"][i]["songname"].ToString(),
                        Singer = o["cdlist"][0]["songlist"][i]["singer"][0]["name"].ToString(),
                        ZJ = o["cdlist"][0]["songlist"][i]["albumdesc"].ToString(),
                        GC = o["cdlist"][0]["songlist"][i]["songid"].ToString(),
                        Fotmat = o["cdlist"][0]["songlist"][i]["sizeflac"].ToString(),
                        HQFOTmat = o["cdlist"][0]["songlist"][i]["size320"].ToString(),
                        MusicID = o["cdlist"][0]["songlist"][i]["songmid"].ToString(),
                        ImageID = o["cdlist"][0]["songlist"][i]["albummid"].ToString(),
                        MV = o["cdlist"][0]["songlist"][i]["vid"].ToString()
                    };
                    string Q = "";
                    if (m.Fotmat != "0")
                        Q = "SQ";
                    if (m.HQFOTmat != "0")
                        if (m.Fotmat == "0")
                            Q = "HQ";
                    listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 15, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
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
                He.Settings.MusicGD = ToJSON(l);
                He.SaveSettings();
            }
            else
            {
                List<Music> lj = new List<Music>();
                lj = JsonToObject(He.Settings.MusicGD, lj) as List<Music>;
                for (int i = 0; i < lj.Count; i++)
                {
                    string os = "";
                    if (lj[i].Fotmat != "0") { os = "SQ"; }
                    if (lj[i].HQFOTmat != "0")
                        if (lj[i].Fotmat == "0")
                            os = "HQ";
                    listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 15, MusicGS = lj[i].Singer, MusicName = lj[i].MusicName, MusicZJ = lj[i].ZJ, Music = lj[i], Qt = os, ismv = lj[i].MV });
                }
            }
        }
        private DeskLyricWin deskLyricWin;

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
                        MV = ""
                    };
                    string Q = "";
                    listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 15, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                    i++;
                }

                listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
            }
        }
        int downloadindex = 0;
        string datadlwo = "";
        List<Music> ic = new List<Music>();
        private async void Border_MouseDown_5(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount >= 2)
            {
                datadlwo = textBox.Text;
                if (DownloadPath == "xxx")
                    DownloadPath = AppDomain.CurrentDomain.BaseDirectory + "\\" + datadlwo + "\\";
                if (Directory.Exists(DownloadPath) == false)
                    Directory.CreateDirectory(DownloadPath);
                downloadindex = 0;
                popupop.IsOpen = true;
                for (int k = 0; k != listBox.Items.Count; k++)
                {
                    ic.Add((listBox.Items[k] as MusicItemControl).Music as Music);
                }
                download_name.Text = ic[downloadindex].Singer + "-" + ic[downloadindex].MusicName;
                string guid = "20D919A4D7700FBC424740E8CED80C5F";
                string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{ic[downloadindex].MusicID}.mp3?vkey={vkey}&guid={guid}";
                dc = new WebClient()
                {
                    Proxy = He.proxy
                };
                dc.DownloadFileCompleted += OsAsync;
                dc.DownloadProgressChanged += As;
                dc.DownloadFileAsync(new Uri(musicurl), DownloadPath + download_name.Text.Replace("\\", ",").Replace("/", ",") + ".mp3");
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
                download_name.Text = ic[downloadindex].Singer + "-" + ic[downloadindex].MusicName;
                string guid = "20D919A4D7700FBC424740E8CED80C5F";
                string ioo = await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{ic[downloadindex].MusicID}.mp3?vkey={vkey}&guid={guid}";
                dc.DownloadFileAsync(new Uri(musicurl), DownloadPath + (download_name.Text.Replace("\\", ",").Replace("/", ",") + ".mp3"));
            }
            else { popupop.IsOpen = false; MessageBox.Show("成功下载全部"); dc.Dispose(); ic.Clear(); }
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
        {
            if (IslistBoxInfo == 0)
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
                        Music m = new Music()
                        {
                            MusicName = o["data"]["song"]["list"][i]["name"].ToString()
                        };
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
                        listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth - 15, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m, Qt = Q, ismv = m.MV });
                        i++;
                    }

                    listBox.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 93, 0, 0), new Thickness(0, 43, 0, 0), TimeSpan.FromSeconds(0.2)));
                }
            }
        }

        private void CLOSE_MouseDown(object sender, MouseButtonEventArgs e)
        {
            dc.Dispose();
            popupop.IsOpen = false;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LyricShow.IsLyFanyi = !LyricShow.IsLyFanyi;
            LyricShow.stopLyricShow();
            LyricShow.TimeAndLyricDictionary.Clear();
            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{musicid}.lrc");
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

        private void Border_MouseMove(object sender, MouseEventArgs e)
        {
            if (listBox.SelectedIndex != -1)
            {
                pu.IsOpen = true;
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            pu.IsOpen = false;
        }

        private void Move_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (Move.ToolTip.ToString() == "列表循环")
            { Move.ToolTip = "单曲循环"; Move.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2))); MovePath.Data = Geometry.Parse("M23.715,14.546c-0.104,0.172-0.051,0.392,0.115,0.492c0.008,0.004,0.014,0.009,0.018,0.011 l5.969,3.622l4.158-6.165c0.078-0.125,0.068-0.286-0.021-0.401c-0.078-0.115-0.229-0.169-0.367-0.133l-2.969,0.744l-0.152,0.038 C28.193,8.849,24.047,6.443,19.5,6.443c-7,0-12.695,5.695-12.695,12.694S12.5,31.832,19.5,31.832c5.273,0,10.053-3.319,11.896-8.258 c0.354-0.952-0.129-2.012-1.082-2.367c-0.953-0.356-2.012,0.129-2.367,1.081c-1.307,3.508-4.703,5.865-8.447,5.865 c-4.973,0-9.016-4.044-9.016-9.015s4.043-9.015,9.016-9.015c2.863,0,5.502,1.345,7.189,3.582l-2.662,0.669 C23.902,14.372,23.783,14.434,23.715,14.546z"); }
            else { Move.ToolTip = "列表循环"; Move.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2))); MovePath.Data = Geometry.Parse("M22.724,8.446l-3.909,3.909c-0.225,0.226-0.592,0.226-0.818,0l-0.056-0.057V9.42 C13.22,9.75,9.48,13.693,9.48,18.496c0,3.373,1.851,6.454,4.833,8.04c0.751,0.4,1.036,1.333,0.637,2.086 c-0.4,0.752-1.333,1.037-2.085,0.637c-3.99-2.123-6.469-6.247-6.469-10.763c0-6.504,5.123-11.834,11.545-12.168V3.776l0.056-0.056 c0.226-0.226,0.593-0.226,0.818,0l3.909,3.908C22.95,7.854,22.95,8.221,22.724,8.446z M20.497,32.618l-3.908-3.908c-0.226-0.226-0.226-0.592,0-0.818l3.908-3.908 c0.226-0.226,0.593-0.226,0.819,0l0.055,0.057l0.001,2.877c4.72-0.329,8.461-4.272,8.461-9.075c0-3.373-1.852-6.454-4.833-8.04 c-0.752-0.4-1.037-1.334-0.638-2.086c0.401-0.752,1.335-1.038,2.087-0.637c3.989,2.123,6.468,6.247,6.468,10.763 c0,6.504-5.123,11.834-11.545,12.167v2.551l-0.056,0.057C21.09,32.844,20.723,32.844,20.497,32.618z"); }
        }

        private void Border_MouseDown_8(object sender, MouseButtonEventArgs e)
        {
            if (pz.Text == "HQ")
            {
                autois = false;
                pz.Text = "标准";
                pz.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
            }
            else if (pz.Text == "标准")
            {
                autois = false;
                pz.Text = "经济";
                pz.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
            }
            else
            {
                autois = true;
                pz.Text = "HQ";
                pz.BeginAnimation(OpacityProperty, new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2)));
            }
        }

        private void border_MouseDown_9(object sender, MouseButtonEventArgs e)
        {
            Clipboard.SetText(a);
        }

        private void border1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (data.Count != 0)
                popup1.IsOpen = true;
        }
        bool ismove = false;
        private void jd_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton==MouseButtonState.Pressed)
            {
                ismove = true;
                t.Stop();
                Bass.BASS_ChannelPause(stream);
            }else if (ismove)
            {
                ismove = false;
                LyricShow.refreshLyricShow(Bass.BASS_ChannelBytes2Seconds(stream, Bass.BASS_ChannelGetPosition(stream)));
                Bass.BASS_ChannelPlay(stream, true);
                Bass.BASS_ChannelSetPosition(stream, jd.Value);
                t.Start();
            }
        }

        private void Border_MouseDown_10(object sender, MouseButtonEventArgs e)
        {
            var Item = new MIDITEM();
            Item.SETMIDAsync(MID.Text);
            MIDLIST.Items.Add(Item);
            Settings.MIDLIST.Add(MID.Text);
            SaveSettings();
            MID.Text = "音乐ID";
        }
        
        private void MIDLIST_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MIDLIST.SelectedIndex != -1)
                GETMID((MIDLIST.SelectedItem as MIDITEM).MID);
        }
    }
}
