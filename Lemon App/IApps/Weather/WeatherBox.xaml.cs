using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;



/////////////////////////////////////////////////////////
//By:Twilight  By:小白///////////////////////////////////
////////////////////////////////////////////////////////
//©2016 Twilight all rights reserved.//////////////////
//©2016 xiaobai all rights reserved.//////////////////
//////////////////////////////////////////////////////


namespace Lemon_App
{
    /// <summary>
    /// WeatherBox.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherBox : UserControl
    {
        public WeatherBox()
        {
            InitializeComponent();
        }
        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            GetWeather(He.Settings.WeatherInfo);
            textBox.Text = He.Settings.WeatherInfo;
        }
        private async void GetWeather(string i)
        {//V5
            //Now实况天气
            try
            {
                grid.BeginAnimation(MarginProperty, new ThicknessAnimation(new Thickness(0, 20, 0, -20), new Thickness(0), TimeSpan.FromSeconds(0.2)));
                //空气质量
                JObject p = JObject.Parse(await Uuuhh.GetWebAsync($"https://route.showapi.com/104-29?showapi_sign=cfa206656db244c089be2d1499735bb5&showapi_appid=29086&city={Uri.EscapeUriString(i)}"));
                kqzl.Text = p["showapi_res_body"]["pm"]["quality"]+"  "+ p["showapi_res_body"]["pm"]["aqi"];

                JObject obj = JObject.Parse(await Uuuhh.GetWebAsync($"https://api.heweather.com/v5/now?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d"));
                Biaoti.Text = "天气预报•" + obj["HeWeather5"][0]["basic"]["city"];
                Qiwen.Text = obj["HeWeather5"][0]["now"]["tmp"] + "°";
                Tianqi.Text = obj["HeWeather5"][0]["now"]["cond"]["txt"] + "    相对湿度 " + obj["HeWeather5"][0]["now"]["hum"] + "%   体感:"+ obj["HeWeather5"][0]["now"]["fl"]+"°";
                fengsu.Text = obj["HeWeather5"][0]["now"]["wind"]["dir"] + "    " + obj["HeWeather5"][0]["now"]["wind"]["sc"] + "级";
                allqiyanjd.Text = "气压:" + obj["HeWeather5"][0]["now"]["pres"] + "    能见度" + obj["HeWeather5"][0]["now"]["vis"];
                Icon.Background =new ImageBrush(new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj["HeWeather5"][0]["now"]["cond"]["code"]}.png"))) { Stretch=Stretch.UniformToFill};
                //7天天气预报
                JObject obj1 = JObject.Parse(await Uuuhh.GetWebAsync($"https://api.heweather.com/v5/forecast?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d"));
                //Icon图标
                iconw.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][0]["cond"]["code_d"]}.png"));
                iconww.Background =new ImageBrush( new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][1]["cond"]["code_d"]}.png"))) { Stretch = Stretch.UniformToFill };
                iconwww.Background =new ImageBrush( new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][2]["cond"]["code_d"]}.png"))) { Stretch = Stretch.UniformToFill };
                iconwwww.Background =new ImageBrush( new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][3]["cond"]["code_d"]}.png"))) { Stretch = Stretch.UniformToFill };
                iconwwwww.Background = new ImageBrush(new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][4]["cond"]["code_d"]}.png"))) { Stretch = Stretch.UniformToFill };
                iconwwwwww.Background = new ImageBrush(new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][5]["cond"]["code_d"]}.png"))) { Stretch = Stretch.UniformToFill };
                iconwwwwwww.Background = new ImageBrush(new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][6]["cond"]["code_d"]}.png"))) { Stretch = Stretch.UniformToFill };
                //时间
                shijianw.Text = obj1["HeWeather5"][0]["daily_forecast"][0]["date"].ToString();
                shijianww.Text = obj1["HeWeather5"][0]["daily_forecast"][1]["date"].ToString();
                shijianwww.Text = obj1["HeWeather5"][0]["daily_forecast"][2]["date"].ToString();
                shijianwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][3]["date"].ToString();
                shijianwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][4]["date"].ToString();
                shijianwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][5]["date"].ToString();
                shijianwwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][6]["date"].ToString();
                //气温:最高/最低
                qiwenw.Text = obj1["HeWeather5"][0]["daily_forecast"][0]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][0]["tmp"]["min"] + "°";
                qiwenww.Text = obj1["HeWeather5"][0]["daily_forecast"][1]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][1]["tmp"]["min"] + "°";
                qiwenwww.Text = obj1["HeWeather5"][0]["daily_forecast"][2]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][2]["tmp"]["min"] + "°";
                qiwenwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][3]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][3]["tmp"]["min"] + "°";
                qiwenwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][4]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][4]["tmp"]["min"] + "°";
                qiwenwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][5]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][5]["tmp"]["min"] + "°";
                qiwenwwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][6]["tmp"]["max"] + "℃-" + obj1["HeWeather5"][0]["daily_forecast"][6]["tmp"]["min"] + "°";
            }
            catch { Erro.BeginAnimation(HeightProperty, new DoubleAnimation(0, 37, TimeSpan.FromSeconds(0.2))); }
        }
        private void textBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                if (textBox.Text != "") {
                    GetWeather(textBox.Text);
                    He.Settings.WeatherInfo = textBox.Text;
                    He.SaveSettings();
                }
                    
        }

        private void label2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Erro.BeginAnimation(HeightProperty, new DoubleAnimation(37, 0, TimeSpan.FromSeconds(0.2)));
        }

        private void textBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RotateTransform rtf = new RotateTransform()
            {
                CenterX = 0.5,
                CenterY = 0.5
            };
            (sender as Border).RenderTransform = rtf;
            DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(0.3)));
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
            GetWeather(He.Settings.WeatherInfo);
        }
        MediaPlayer p = new MediaPlayer();
        private void Biaoti_MouseDown(object sender, MouseButtonEventArgs e)
        {
            p.Open(new Uri($"http://fanyi.baidu.com/gettts?lan=zh&text={Uri.EscapeDataString( $"小萌天气为你播报，{Biaoti.Text},{Qiwen.Text},{Tianqi.Text},{fengsu.Text},{allqiyanjd.Text},{kqzl.Text}")}&spd=3&source=web"));
            p.Play();
            p.MediaEnded += delegate { p.Close(); };
        }
    }
}