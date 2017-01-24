﻿using Lemon_App.Properties;
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

namespace Lemon_App
{
    /// <summary>
    /// MusicControl.xaml 的交互逻辑
    /// </summary>
    public partial class MusicControl : UserControl
    {
        MediaPlayer player = new MediaPlayer();
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        public MusicControl()
        {
            InitializeComponent();
            player.MediaEnded += io;
            t.Interval = 500;
            t.Tick += Tick;
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache") == false)
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache");
            if (Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicDownload") == false)
                Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + $@"MusicDownload");
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
        /// <summary>
        /// 桌面歌词窗体
        /// </summary>
        DeskLyricWin deskLyricWin;
        private async void io(object sender, EventArgs e)
        {
               try {
        //    player.Stop();
            s.Data = Geometry.Parse("M2.432,11.997L13.69,1.714c0.393-0.392,0.393-1.028,0-1.42c-0.393-0.392-1.031-0.392-1.424,0L0.286,11.236c-0.21,0.209-0.299,0.487-0.285,0.76c-0.014,0.274,0.075,0.551,0.285,0.76l11.98,10.942c0.393,0.392,1.031,0.392,1.424,0c0.393-0.392,0.393-1.028,0-1.42L2.432,11.997z");
            string i = "";
            if (listBox.SelectedIndex != listBox.Items.Count)
            {
                i = (listBox.Items[listBox.SelectedIndex + 1] as MusicItemControl).Content;
                listBox.SelectedItem = listBox.Items[listBox.SelectedIndex + 1];
            }
            else {
                i = (listBox.Items[0] as MusicItemControl).Content;
                listBox.SelectedItem = listBox.Items[0];
            }
            isR = true;
                lrcname.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicName;
                zk.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).ZJ;
                img = ((listBox.SelectedItem as MusicItemControl).Music as Music).ImageID;
                He.on = $"https://y.gtimg.cn/music/photo_new/T002R300x300M000{img}.jpg";
                tx.Background = new ImageBrush(new BitmapImage(new Uri(He.on)));
                musicid = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicID;
                string guid = "20D919A4D7700FBC424740E8CED80C6F";
                string ioo =await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{musicid}.mp3?vkey={vkey}&guid={guid}";
            player.Open(new Uri(musicurl));
            player.Play();
            t.Start();
            if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
            {
                    string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                string lrcid = lrc.Substring(lrc.Length - 2, 2);
                //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{i}.lrc", FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);
                string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                if (ijo != "")
                {
                    await sw.WriteAsync(ijo);
                    await sw.FlushAsync();
                    sw.Close();
                    fs.Close();
                    if (LyricShow.IsOpenDeskLyric == false)
                    {
                        deskLyricWin = new DeskLyricWin();
                        deskLyricWin.Show();
                        LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                        LyricShow.HB = 204;
                        LyricShow.HG = 122;
                        LyricShow.HR = 0;
                        LyricShow.CB = 193;
                        LyricShow.CG = 180;
                        LyricShow.CR = 180;
                    }
                    LyricShow.IsPauseLyricShow = false;
                    getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{i}.lrc");
                    LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                }
                else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
            }
            else
            {
                if (LyricShow.IsOpenDeskLyric == false)
                {
                    deskLyricWin = new DeskLyricWin();
                    deskLyricWin.Show();
                    LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                    LyricShow.HB = 204;
                    LyricShow.HG = 122;
                    LyricShow.HR = 0;
                    LyricShow.CB = 193;
                    LyricShow.CG = 180;
                    LyricShow.CR = 180;
                }
                LyricShow.IsPauseLyricShow = false;
                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词        }

            }
             }
                catch { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
        }

        private void Fis(object sender, AsyncCompletedEventArgs e)
        {
            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"));
            player.Play();
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        private void Tick(object sender, EventArgs e)
        {
            try
            {
                LyricShow.refreshLyricShow(player.Position.TotalSeconds);
                jd.Maximum = GetMusicDurationTime().TotalMilliseconds;
                jd.Value = GetPosition().TotalMilliseconds;
            }
            catch { }
        }
        int ioi = 1;
        private async void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    jz.Visibility = Visibility.Visible;
                    DOWN.Visibility = Visibility.Collapsed;
                    listBox.Visibility = Visibility.Visible;
                    listBox.Items.Clear();
                    JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"http://59.37.96.220/soso/fcgi-bin/client_search_logic_cp?format=json&t=50&inCharset=GB2312&outCharset=utf-8&w={textBox.Text}&p=1",Encoding.UTF8));
                    int i = 0;
                    while (i < o["data"]["song"]["list"].Count())
                    {
                        //string f = o["data"]["song"]["list"][i]["f"].ToString().Replace("|", "\r\n");
                        //string[] ContentLines = f.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //string Gs = o["data"]["song"]["list"][i]["fsinger"].ToString();
                        //string songname = o["data"]["song"]["list"][i]["fsong"].ToString();
                        //string Zhj = o["data"]["song"]["list"][i]["albumName_hilight"].ToString();
                        //    img = ContentLines[22];
                        Music m = new Music();
                        m.MusicName = o["data"]["song"]["list"][i]["songname"].ToString();
                        m.Singer = o["data"]["song"]["list"][i]["singer"][0]["name"].ToString();
                        m.ZJ = o["data"]["song"]["list"][i]["albumname"].ToString();
                        m.MusicID = o["data"]["song"]["list"][i]["media_mid"].ToString();
                        m.ImageID= o["data"]["song"]["list"][i]["albummid"].ToString();
                        m.GC= o["data"]["song"]["list"][i]["songid"].ToString();
                        m.Fotmat= o["data"]["song"]["list"][i]["sizeflac"].ToString();
                        m.HQFOTmat= o["data"]["song"]["list"][i]["size320"].ToString();
                        string Q = "";
                        if (m.Fotmat != "0")
                            Q = "SQ";
                        if (m.HQFOTmat != "0")
                            if (m.Fotmat == "0")
                                Q = "HQ";
                        listBox.Items.Add(new MusicItemControl() {Width=this.ActualWidth, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName =m.MusicName, MusicZJ = m.ZJ, Music = m,Qt=Q });
                        i++;
                    }
                    jz.Visibility = Visibility.Collapsed;
                }
                catch { }
            }
        }
        string img = "";
        public TimeSpan GetMusicDurationTime()
        {
                return player.NaturalDuration.TimeSpan;
        }
        public void SetPosition(TimeSpan tp)
        {
            player.Position = tp;
        }
        public TimeSpan GetPosition()
        {
            return player.Position;
        }
        string musicurl = "";
        string musicid = "";
        private async void listBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                player.Stop();
                isR = true;
                s.Data = Geometry.Parse("M2.432,11.997L13.69,1.714c0.393-0.392,0.393-1.028,0-1.42c-0.393-0.392-1.031-0.392-1.424,0L0.286,11.236c-0.21,0.209-0.299,0.487-0.285,0.76c-0.014,0.274,0.075,0.551,0.285,0.76l11.98,10.942c0.393,0.392,1.031,0.392,1.424,0c0.393-0.392,0.393-1.028,0-1.42L2.432,11.997z");
                string i = (listBox.SelectedItem as MusicItemControl).Content;
                textBlock1.Text = i;
                lrcname.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicName;
                zk.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).ZJ;
                img = ((listBox.SelectedItem as MusicItemControl).Music as Music).ImageID;
                He.on = $"https://y.gtimg.cn/music/photo_new/T002R300x300M000{img}.jpg";
                tx.Background = new ImageBrush(new BitmapImage(new Uri(He.on)));
                musicid = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicID;
                if (pz.Text == "标准")
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
                        player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a"));
                        player.Play();
                        t.Start();
                    }
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                    {
                        string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                        string lrcid = lrc.Substring(lrc.Length - 2, 2);
                        //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                        if (ijo != "")
                        {
                            await sw.WriteAsync(ijo);
                            await sw.FlushAsync();
                            sw.Close();
                            fs.Close();
                            if (LyricShow.IsOpenDeskLyric == false)
                            {
                                deskLyricWin = new DeskLyricWin();
                                deskLyricWin.Show();
                                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                LyricShow.HB = 204;
                                LyricShow.HG = 122;
                                LyricShow.HR = 0;
                                LyricShow.CB = 193;
                                LyricShow.CG = 180;
                                LyricShow.CR = 180;
                            }
                            LyricShow.IsPauseLyricShow = false;
                            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                        }
                        else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
                    }
                    else
                    {
                        if (LyricShow.IsOpenDeskLyric == false)
                        {
                            deskLyricWin = new DeskLyricWin();
                            deskLyricWin.Show();
                            LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                            LyricShow.HB = 204;
                            LyricShow.HG = 122;
                            LyricShow.HR = 0;
                            LyricShow.CB = 193;
                            LyricShow.CG = 180;
                            LyricShow.CR = 180;
                        }
                        LyricShow.IsPauseLyricShow = false;
                        getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                        LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);

                    }
                }
                else if (pz.Text == "HQ")
                {
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"))
                    {
                        // musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                        string guid = "20D919A4D7700FBC424740E8CED80C5F";
                        string ioo =await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                        string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                        musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{musicid}.mp3?vkey={vkey}&guid={guid}";
                        //player.Open(new Uri(musicurl));
                        //player.Play();
                        //t.Start();
                        WebClient dc = new WebClient();
                        dc.Proxy = He.proxy;
                        dc.DownloadFileCompleted += Fi;
                        dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3");
                        ///等待播放
                        loading.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"));
                        player.Play();
                        t.Start();
                    }
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                    {
                        string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                        string lrcid = lrc.Substring(lrc.Length - 2, 2);
                        //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                        if (ijo != "")
                        {
                            await sw.WriteAsync(ijo);
                            await sw.FlushAsync();
                            sw.Close();
                            fs.Close();
                            if (LyricShow.IsOpenDeskLyric == false)
                            {
                                deskLyricWin = new DeskLyricWin();
                                deskLyricWin.Show();
                                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                LyricShow.HB = 204;
                                LyricShow.HG = 122;
                                LyricShow.HR = 0;
                                LyricShow.CB = 193;
                                LyricShow.CG = 180;
                                LyricShow.CR = 180;
                            }
                            LyricShow.IsPauseLyricShow = false;
                            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                        }
                        else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
                    }
                    else
                    {
                        if (LyricShow.IsOpenDeskLyric == false)
                        {
                            deskLyricWin = new DeskLyricWin();
                            deskLyricWin.Show();
                            LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                            LyricShow.HB = 204;
                            LyricShow.HG = 122;
                            LyricShow.HR = 0;
                            LyricShow.CB = 193;
                            LyricShow.CG = 180;
                            LyricShow.CR = 180;
                        }
                        LyricShow.IsPauseLyricShow = false;
                        getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                        LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);

                    }
                }
                else if (pz.Text == "SQ")
                {
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac"))
                    {
                        // musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                        string guid = "20D919A4D7700FBC424740E8CED80C5F";//2,6
                        string ioo =await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                        string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                        musicurl = $"http://116.55.235.12/streamoc.music.tc.qq.com/F000{musicid}.flac?vkey={vkey}&guid={guid}";
                        //player.Open(new Uri(musicurl));
                        //player.Play();
                        //t.Start();
                        WebClient dc = new WebClient();
                        dc.Proxy = He.proxy;
                        dc.DownloadFileCompleted += FiSQ;
                        dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac");
                        ///等待播放
                        loading.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac"));
                        player.Play();
                        t.Start();
                    }
                    if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                    {
                        string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                        string lrcid = lrc.Substring(lrc.Length - 2, 2);
                        //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                        FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                        StreamWriter sw = new StreamWriter(fs);
                        string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                        if (ijo != "")
                        {
                            await sw.WriteAsync(ijo);
                            await sw.FlushAsync();
                            sw.Close();
                            fs.Close();
                            if (LyricShow.IsOpenDeskLyric == false)
                            {
                                deskLyricWin = new DeskLyricWin();
                                deskLyricWin.Show();
                                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                LyricShow.HB = 204;
                                LyricShow.HG = 122;
                                LyricShow.HR = 0;
                                LyricShow.CB = 193;
                                LyricShow.CG = 180;
                                LyricShow.CR = 180;
                            }
                            LyricShow.IsPauseLyricShow = false;
                            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                        }
                        else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
                    }
                    else
                    {
                        if (LyricShow.IsOpenDeskLyric == false)
                        {
                            deskLyricWin = new DeskLyricWin();
                            deskLyricWin.Show();
                            LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                            LyricShow.HB = 204;
                            LyricShow.HG = 122;
                            LyricShow.HR = 0;
                            LyricShow.CB = 193;
                            LyricShow.CG = 180;
                            LyricShow.CR = 180;
                        }
                        LyricShow.IsPauseLyricShow = false;
                        getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                        LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);

                    }
                }
            }
            catch { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
        }

        private void Fi_BZ(object sender, AsyncCompletedEventArgs e)
        {
            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a"));
            player.Play();
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        private void Fi(object sender, AsyncCompletedEventArgs e)
        {
            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"));
            player.Play();
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
        #region 上下拖动歌词(移植时,此部分可以基本完全复制黏贴)
        /// <summary>
        /// 歌词拖动鼠标坐标点A
        /// </summary>
        Point LyricPointA;
        /// <summary>
        /// 歌词拖动鼠标坐标点B
        /// </summary>
        Point LyricPointB;
        /// <summary>
        /// 是否开始拖动歌词(默认false)
        /// </summary>
        bool IsStartDragLyric = false;
        /// <summary>
        /// 拖动歌词前的时间
        /// </summary>
        double DragBeforeLyricTime = 0;
        /// <summary>
        /// 拖动歌词后的时间
        /// </summary>
        double DragAfterLyricTime = 0;
        /// <summary>
        /// 鼠标按下准备拖动歌词
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasLyric_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Windows.Input.Cursor csB = new System.Windows.Input.Cursor(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\cursor\\hand-close.cur");
                this.Cursor = csB;
            }
            catch { }
            LyricShow.pauseOrContinueLyricShow(true);//暂停歌词秀，然后拖动
            DragBeforeLyricTime = player.Position.TotalSeconds;
            DragAfterLyricTime = DragBeforeLyricTime;
            IsStartDragLyric = true;
            LyricPointA.X = e.GetPosition(this).X;
            LyricPointA.Y = e.GetPosition(this).Y;
            LyricPointB.X = e.GetPosition(this).X;
            LyricPointB.Y = e.GetPosition(this).Y;
            BaseLine.Visibility = Visibility.Visible;
            tbDragTime.Visibility = Visibility.Visible;
            try
            {
                tbDragTime.Text = string.Format("{0}:{1}", player.Position.Minutes, player.Position.Seconds);
            }
            catch
            {
                IsStartDragLyric = false;
                tbDragTime.Text = "0:00.00";
            }
        }
        /// <summary>
        /// 鼠标拖动歌词
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasLyric_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (IsStartDragLyric == true)
            {
                LyricPointB.X = e.GetPosition(this).X;
                LyricPointB.Y = e.GetPosition(this).Y;
                if (LyricPointB.Y != LyricPointA.Y)
                {
                    if (LyricPointB.Y - LyricPointA.Y > 0)
                    {
                        //向下(每次变化0.12秒，该值可以按需调整.)
                        DragAfterLyricTime -= 0.12;
                    }
                    if (LyricPointB.Y - LyricPointA.Y < 0)
                    {
                        //向上
                        DragAfterLyricTime += 0.12;
                    }
                    tbDragTime.Text = string.Format("{0}:{1}", (int)DragAfterLyricTime / 60, (DragAfterLyricTime % 60).ToString("F2"));
                    LyricShow.refrshLyricShowWhenSkipPlay(DragAfterLyricTime);
                    LyricPointA.Y = LyricPointB.Y;
                }
                else
                {
                    // 没上没下
                }
            }
        }
        /// <summary>
        /// 鼠标释放,结束拖动歌词
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasLyric_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                System.Windows.Input.Cursor csC = new System.Windows.Input.Cursor(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\cursor\\hand-open.cur");
                this.Cursor = csC;
            }
            catch { }
            BaseLine.Visibility = Visibility.Collapsed;
            tbDragTime.Visibility = Visibility.Collapsed;
            if (IsStartDragLyric == true)
            {
                IsStartDragLyric = false;
                if (isR == true)
                {
                    LyricShow.IsPauseLyricShow = false;
                    if (DragAfterLyricTime == DragBeforeLyricTime)
                    {
                        //有按下拖动歌词的意愿,但实际是没有拖动歌词,那么直接刷新不跳播
                        LyricShow.refrshLyricShowWhenSkipPlay(player.Position.TotalSeconds);
                    }
                    else
                    {
                        //刷新,并跳播                        
                        LyricShow.refrshLyricShowWhenSkipPlay(DragAfterLyricTime);
                        player.Position = TimeSpan.FromSeconds(DragAfterLyricTime);
                        player.Play();
                    }
                }
                else
                {
                    LyricShow.IsPauseLyricShow = true;
                    player.Position = TimeSpan.FromSeconds(DragAfterLyricTime);
                    LyricShow.refrshLyricShowWhenSkipPlay(DragAfterLyricTime);
                    //          TBTime.Text = string.Format("0:{0}:{1}", player.Position.Minutes, player.Position.Seconds);
                }
            }
        }
        /// <summary>
        /// 鼠标进入歌词面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasLyric_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                System.Windows.Input.Cursor csA = new System.Windows.Input.Cursor(System.AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\cursor\\hand-open.cur");
                this.Cursor = csA;
            }
            catch { }
        }
        /// <summary>
        /// 鼠标离开歌词面板
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CanvasLyric_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            this.Cursor = System.Windows.Input.Cursors.Arrow;
            BaseLine.Visibility = Visibility.Collapsed;
            tbDragTime.Visibility = Visibility.Collapsed;
            if (IsStartDragLyric == true)
            {
                IsStartDragLyric = false;
                player.Position = TimeSpan.FromSeconds(DragAfterLyricTime);
                LyricShow.refrshLyricShowWhenSkipPlay(DragAfterLyricTime);
                //         TBTime.Text = string.Format("0:{0}:{1}", player.Position.Minutes, player.Position.Seconds);
            }
        }

        #endregion
        bool isR = false;
        private void textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isR)
            { isR = false; player.Pause(); s.Data = Geometry.Parse("M40.2,3.8C36.4,0,30,2.7,30,8v112c0,5.3,6.4,8,10.2,4.2l56-56c2.3-2.3,2.3-6.1,0-8.4L40.2,3.8z"); }
            else { isR = true; player.Play(); s.Data = Geometry.Parse("M2.432,11.997L13.69,1.714c0.393-0.392,0.393-1.028,0-1.42c-0.393-0.392-1.031-0.392-1.424,0L0.286,11.236c-0.21,0.209-0.299,0.487-0.285,0.76c-0.014,0.274,0.075,0.551,0.285,0.76l11.98,10.942c0.393,0.392,1.031,0.392,1.424,0c0.393-0.392,0.393-1.028,0-1.42L2.432,11.997z"); }
        }

        private void jd_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                LyricShow.refreshLyricShow(player.Position.TotalSeconds);
                player.Stop();
                SetPosition(TimeSpan.FromMilliseconds(jd.Value));
                player.Play();
            }
        }

        private async void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (ioi != 1)
            {
                ioi--;
                try
                {
                    jz.Visibility = Visibility.Visible;
                    listBox.Items.Clear();
                    JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"http://59.37.96.220/soso/fcgi-bin/client_search_logic_cp?format=json&t=50&inCharset=GB2312&outCharset=utf-8&w={textBox.Text}&p={ioi}"));
                    int i = 0;
                    while (i < o["data"]["song"]["list"].Count())
                    {
                        //string f = o["data"]["song"]["list"][i]["f"].ToString().Replace("|", "\r\n");
                        //string[] ContentLines = f.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                        //string Gs = o["data"]["song"]["list"][i]["fsinger"].ToString();
                        //string songname = o["data"]["song"]["list"][i]["fsong"].ToString();
                        //string Zhj = o["data"]["song"]["list"][i]["albumName_hilight"].ToString();
                        //    img = ContentLines[22];
                        Music m = new Music();
                        m.MusicName = o["data"]["song"]["list"][i]["songname"].ToString();
                        m.Singer = o["data"]["song"]["list"][i]["singer"][0]["name"].ToString();
                        m.ZJ = o["data"]["song"]["list"][i]["albumname"].ToString();
                        m.MusicID = o["data"]["song"]["list"][i]["media_mid"].ToString();
                        m.ImageID = o["data"]["song"]["list"][i]["albummid"].ToString();
                        m.GC = o["data"]["song"]["list"][i]["songid"].ToString();
                        m.Fotmat = o["data"]["song"]["list"][i]["sizeflac"].ToString();
                        string Q = "";
                        if (m.Fotmat != "0")
                            Q = "SQ";
                        if (m.HQFOTmat != "0")
                            if (m.Fotmat == "0")
                                Q = "HQ";
                        listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m,Qt=Q });
                        i++;
                    }
                    jz.Visibility = Visibility.Collapsed;
                }
                catch { }
            }
        }

        private async void textBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                jz.Visibility = Visibility.Visible;
                h.Visibility = Visibility.Visible;
                G.Visibility = Visibility.Hidden;
                ZjImAgE.Visibility = Visibility.Collapsed;
                ioi++;
                listBox.Items.Clear();
                JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"http://59.37.96.220/soso/fcgi-bin/client_search_logic_cp?format=json&t=50&inCharset=GB2312&outCharset=utf-8&w={textBox.Text}&p={ioi}"));
                int i = 0;
                while (i < o["data"]["song"]["list"].Count())
                {
                    //string f = o["data"]["song"]["list"][i]["f"].ToString().Replace("|", "\r\n");
                    //string[] ContentLines = f.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    //string Gs = o["data"]["song"]["list"][i]["fsinger"].ToString();
                    //string songname = o["data"]["song"]["list"][i]["fsong"].ToString();
                    //string Zhj = o["data"]["song"]["list"][i]["albumName_hilight"].ToString();
                    //    img = ContentLines[22];
                    Music m = new Music();
                    m.MusicName = o["data"]["song"]["list"][i]["songname"].ToString();
                    m.Singer = o["data"]["song"]["list"][i]["singer"][0]["name"].ToString();
                    m.ZJ = o["data"]["song"]["list"][i]["albumname"].ToString();
                    m.MusicID = o["data"]["song"]["list"][i]["media_mid"].ToString();
                    m.ImageID = o["data"]["song"]["list"][i]["albummid"].ToString();
                    m.GC = o["data"]["song"]["list"][i]["songid"].ToString();
                    m.Fotmat = o["data"]["song"]["list"][i]["sizeflac"].ToString();
                    string Q = "";
                    if (m.Fotmat != "0")
                        Q = "SQ";
                    if (m.HQFOTmat != "0")
                        if (m.Fotmat == "0")
                            Q = "HQ";
                    listBox.Items.Add(new MusicItemControl() { Width = this.ActualWidth, BorderThickness = new Thickness(0), MusicGS = m.Singer, MusicName = m.MusicName, MusicZJ = m.ZJ, Music = m ,Qt=Q});
                    i++;
                }
                jz.Visibility = Visibility.Collapsed;
            }
            catch { }
        }

        private void textBlock4_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DOWN.Visibility = Visibility.Visible;
            listBox.Visibility = Visibility.Collapsed;
            string op = "";
            if (pz.Text == "标准") { op = "m4a"; }
            if (pz.Text == "HQ") { op = "mp3"; }
            if (pz.Text == "SQ") { op = "flac"; }
            WebClient dc = new WebClient();
            dc.Proxy = He.proxy;
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
            if (!System.IO.File.Exists(AppDomain.CurrentDomain.BaseDirectory + ((listBox.SelectedItem as ListBoxItem).Content as string) + ".m4a"))
                Toast.SetToastNotion("Lemon App:", "啊哦 下载失败了", "-----来自Lemon Updata").Show();
        }

        private void tx_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (h.Visibility == Visibility.Visible)
            {
                h.Visibility = Visibility.Hidden;
                ZjImAgE.Visibility = Visibility.Collapsed;
                G.Visibility = Visibility.Visible;
            }
            else
            { 
                h.Visibility = Visibility.Visible;
                G.Visibility = Visibility.Hidden;
                ZjImAgE.Visibility = Visibility.Collapsed;
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
                listBox.Items.Add(new MusicItemControl() {Width=this.ActualWidth , MusicGS =lj.List[i].ItemText.Singer, MusicName = lj.List[i].ItemText.MusicName, MusicZJ = lj.List[i].ItemText.ZJ,Qt=os, Music = lj.List[i].ItemText });
            }
            jz.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseDown(object sender, MouseEventArgs e)
        {
            h.Visibility = Visibility.Collapsed;
            G.Visibility = Visibility.Collapsed;
            ZjImAgE.Visibility = Visibility.Visible;
            //   ZjImAgE.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 0, 0, 49), new Thickness(0, 299, 591, 49), TimeSpan.FromSeconds(0.1)));
        }

        private void tx_MouseLeave(object sender, MouseEventArgs e)
        {
            h.Visibility = Visibility.Visible;
            G.Visibility = Visibility.Collapsed;
            ZjImAgE.Visibility = Visibility.Collapsed;
        }

        private async void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            JObject json = JObject.Parse(await Uuuhh.GetWebAsync("http://59.37.96.220/soso/fcgi-bin/dynamic_content?format=json&outCharset=utf-8", Encoding.UTF8));
            textBox.Text = json["data"]["search_content"].ToString();

            RotateTransform rtf = new RotateTransform();// { CenterX = 0.5,CenterY = 0.5};
            os.RenderTransform = rtf;
            os.RenderTransformOrigin = new Point(0.5, 0.5);
            DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(5))) { RepeatBehavior = RepeatBehavior.Forever };
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);

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
                listBox.Items.Add(new MusicItemControl() {Width=this.ActualWidth, MusicGS = lj.List[i].ItemText.Singer, MusicName = lj.List[i].ItemText.MusicName, MusicZJ = lj.List[i].ItemText.ZJ, Music = lj.List[i].ItemText,Qt=os });
            }
        }

        private async void pz_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (isR)
            {
                try
                {
                    player.Stop();
                    isR = true;
                    s.Data = Geometry.Parse("M2.432,11.997L13.69,1.714c0.393-0.392,0.393-1.028,0-1.42c-0.393-0.392-1.031-0.392-1.424,0L0.286,11.236c-0.21,0.209-0.299,0.487-0.285,0.76c-0.014,0.274,0.075,0.551,0.285,0.76l11.98,10.942c0.393,0.392,1.031,0.392,1.424,0c0.393-0.392,0.393-1.028,0-1.42L2.432,11.997z");
                    string i = (listBox.SelectedItem as MusicItemControl).Content;
                    textBlock1.Text = i;
                    lrcname.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicName;
                    zk.Text = ((listBox.SelectedItem as MusicItemControl).Music as Music).ZJ;
                    img = ((listBox.SelectedItem as MusicItemControl).Music as Music).ImageID;
                    He.on = $"https://y.gtimg.cn/music/photo_new/T002R300x300M000{img}.jpg";
                    tx.Background = new ImageBrush(new BitmapImage(new Uri(He.on)));
                    musicid = ((listBox.SelectedItem as MusicItemControl).Music as Music).MusicID;
                    if (pz.Text == "标准")
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
                            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.m4a"));
                            player.Play();
                            t.Start();
                        }
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                        {
                            string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                            string lrcid = lrc.Substring(lrc.Length - 2, 2);
                            //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);
                            string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                            if (ijo != "")
                            {
                                await sw.WriteAsync(ijo);
                                await sw.FlushAsync();
                                sw.Close();
                                fs.Close();
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    deskLyricWin = new DeskLyricWin();
                                    deskLyricWin.Show();
                                    LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 204;
                                    LyricShow.HG = 122;
                                    LyricShow.HR = 0;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                            }
                            else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
                        }
                        else
                        {
                            if (LyricShow.IsOpenDeskLyric == false)
                            {
                                deskLyricWin = new DeskLyricWin();
                                deskLyricWin.Show();
                                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                LyricShow.HB = 204;
                                LyricShow.HG = 122;
                                LyricShow.HR = 0;
                                LyricShow.CB = 193;
                                LyricShow.CG = 180;
                                LyricShow.CR = 180;
                            }
                            LyricShow.IsPauseLyricShow = false;
                            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);

                        }
                    }
                    else if (pz.Text == "HQ")
                    {
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"))
                        {
                            // musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                            string guid = "20D919A4D7700FBC424740E8CED80C5F";
                            string ioo =await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                            string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                            musicurl = $"http://182.247.250.19/streamoc.music.tc.qq.com/M500{musicid}.mp3?vkey={vkey}&guid={guid}";
                            //player.Open(new Uri(musicurl));
                            //player.Play();
                            //t.Start();
                            WebClient dc = new WebClient();
                            dc.Proxy = He.proxy;
                            dc.DownloadFileCompleted += Fi;
                            dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3");
                            ///等待播放
                            loading.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.mp3"));
                            player.Play();
                            t.Start();
                        }
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                        {
                            string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                            string lrcid = lrc.Substring(lrc.Length - 2, 2);
                            //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);
                            string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                            if (ijo != "")
                            {
                                await sw.WriteAsync(ijo);
                                await sw.FlushAsync();
                                sw.Close();
                                fs.Close();
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    deskLyricWin = new DeskLyricWin();
                                    deskLyricWin.Show();
                                    LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 204;
                                    LyricShow.HG = 122;
                                    LyricShow.HR = 0;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                            }
                            else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
                        }
                        else
                        {
                            if (LyricShow.IsOpenDeskLyric == false)
                            {
                                deskLyricWin = new DeskLyricWin();
                                deskLyricWin.Show();
                                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                LyricShow.HB = 204;
                                LyricShow.HG = 122;
                                LyricShow.HR = 0;
                                LyricShow.CB = 193;
                                LyricShow.CG = 180;
                                LyricShow.CR = 180;
                            }
                            LyricShow.IsPauseLyricShow = false;
                            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);

                        }
                    }
                    else if (pz.Text == "SQ")
                    {
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac"))
                        {
                            // musicurl = $"http://cc.stream.qqmusic.qq.com/C100{musicid}.m4a?fromtag=52";
                            string guid = "20D919A4D7700FBC424740E8CED80C5F";
                            string ioo =await Uuuhh.GetWebAsync($"http://59.37.96.220/base/fcgi-bin/fcg_musicexpress2.fcg?version=12&miniversion=92&key=19914AA57A96A9135541562F16DAD6B885AC8B8B5420AC567A0561D04540172E&guid={guid}");
                            string vkey = He.Text(ioo, "key=\"", "\" speedrpttype", 0);
                            musicurl = $"http://116.55.235.12/streamoc.music.tc.qq.com/F000{musicid}.flac?vkey={vkey}&guid={guid}";
                            //player.Open(new Uri(musicurl));
                            //player.Play();
                            //t.Start();
                            WebClient dc = new WebClient();
                            dc.Proxy = He.proxy;
                            dc.DownloadFileCompleted += FiSQ;
                            dc.DownloadFileAsync(new Uri(musicurl), AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac");
                            loading.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac"));
                            player.Play();
                            t.Start();
                        }
                        if (!File.Exists(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc"))
                        {
                            string lrc = ((listBox.SelectedItem as MusicItemControl).Music as Music).GC;
                            string lrcid = lrc.Substring(lrc.Length - 2, 2);
                            //     MessageBox.Show(He.Text(sr.ReadToEnd(), @"<lyric><![CDATA[", "]]></lyric>", 0));
                            FileStream fs = new FileStream(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc", FileMode.Create);
                            StreamWriter sw = new StreamWriter(fs);
                            string ijo = He.Text(await Uuuhh.GetWebUAsync($"http://music.qq.com/miniportal/static/lyric/{lrcid}/{lrc}.xml"), @"<lyric><![CDATA[", "]]></lyric>", 0).Replace("&apos;", "'");
                            if (ijo != "")
                            {
                                await sw.WriteAsync(ijo);
                                await sw.FlushAsync();
                                sw.Close();
                                fs.Close();
                                if (LyricShow.IsOpenDeskLyric == false)
                                {
                                    deskLyricWin = new DeskLyricWin();
                                    deskLyricWin.Show();
                                    LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                    LyricShow.HB = 204;
                                    LyricShow.HG = 122;
                                    LyricShow.HR = 0;
                                    LyricShow.CB = 193;
                                    LyricShow.CG = 180;
                                    LyricShow.CR = 180;
                                }
                                LyricShow.IsPauseLyricShow = false;
                                getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                                LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);//解析歌词->得到歌词时间和歌词       
                            }
                            else { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
                        }
                        else
                        {
                            if (LyricShow.IsOpenDeskLyric == false)
                            {
                                deskLyricWin = new DeskLyricWin();
                                deskLyricWin.Show();
                                LyricShow.openDeskLyric(deskLyricWin.textBlockDeskLyricFore, deskLyricWin.textBlockDeskLyricBack, deskLyricWin.canvasDeskLyricFore);
                                LyricShow.HB = 204;
                                LyricShow.HG = 122;
                                LyricShow.HR = 0;
                                LyricShow.CB = 193;
                                LyricShow.CG = 180;
                                LyricShow.CR = 180;
                            }
                            LyricShow.IsPauseLyricShow = false;
                            getLT.getLyricAndLyricTimeByLyricPath(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.lrc");
                            LyricShow.initializeLyricUI(getLT.LyricAndTimeDictionary);

                        }
                    }
                }
                catch { deskLyricWin.Close(); LyricShow.backInitial(); LyricShow.initializeLyricUI(null); LyricShow.IsPauseLyricShow = true; }
            }
        }

        private void FiSQ(object sender, AsyncCompletedEventArgs e)
        {
            player.Open(new Uri(AppDomain.CurrentDomain.BaseDirectory + $@"MusicCache/{textBlock1.Text}.flac"));
            player.Play();
            t.Start();
            loading.Visibility = Visibility.Collapsed;
        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            foreach (var o in listBox.Items)
            {
                (o as UserControl).Width = this.ActualWidth;
            }
        }
    }
}
