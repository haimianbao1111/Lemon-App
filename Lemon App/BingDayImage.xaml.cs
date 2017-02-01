using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
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
    /// BingDayImage.xaml 的交互逻辑
    /// </summary>
    public partial class BingDayImage : UserControl
    {
        private int BingimageMS;

        public BingDayImage()
        {
            InitializeComponent();
        }
        string downuri = "";
        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"));
                string url = "http://cn.bing.com" + obj["images"][0]["url"].ToString();
                bingDailyPicture.Background =new ImageBrush( new BitmapImage(new Uri(url)));
                textBlock.Text = obj["images"][0]["copyright"].ToString();
                downuri = obj["images"][0]["copyrightlink"].ToString();
                BingimageMS = 0;
            }
            catch { }
        }
        private async void Border_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (BingimageMS < 17)
                {
                    BingimageMS++;
                    JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"));
                    string url = "http://cn.bing.com" + obj["images"][0]["url"].ToString();
                    bingDailyPicture.Background = new ImageBrush(new BitmapImage(new Uri(url)));
                    textBlock.Text = obj["images"][0]["copyright"].ToString();
                    downuri = obj["images"][0]["copyrightlink"].ToString();
                }
            }
            catch { }
        }

        private async void Border_MouseDown_2(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (BingimageMS > 0)
                {
                    BingimageMS--;
                    JObject obj = JObject.Parse(await Uuuhh.GetWebAsync("http://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=1&mkt=zh-CN"));
                    string url = "http://cn.bing.com" + obj["images"][0]["url"].ToString();
                    bingDailyPicture.Background = new ImageBrush(new BitmapImage(new Uri(url)));
                    textBlock.Text = obj["images"][0]["copyright"].ToString();
                    downuri = obj["images"][0]["copyrightlink"].ToString();
                }
            }
            catch { }
        }
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SystemParametersInfo")]
        public static extern int SystemParametersInfo(
     int uAction,
     int uParam,
     string lpvParam,
     int fuWinIni
     );
        private void label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BitmapImage image =(BitmapImage) ((bingDailyPicture.Background as ImageBrush).ImageSource);
                BmpBitmapEncoder encoder = new BmpBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                FileStream fileStream = new FileStream(AppDomain.CurrentDomain.BaseDirectory+@"BingImage.bmp", FileMode.Create, FileAccess.ReadWrite);
                encoder.Save(fileStream);
                fileStream.Close();
                SystemParametersInfo(20, 0, AppDomain.CurrentDomain.BaseDirectory+ @"BingImage.bmp", 0x2);
            }catch { }
        }

        private void textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (downuri != "")
                Process.Start(downuri);
        }
    }
}
