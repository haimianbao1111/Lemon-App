using Lemon_App.Properties;
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
            GetWeather(Settings.Default.WeatherInfo);
        }
        private async void GetWeather(string i)
        {//V5
            //Now实况天气
            try
            {
                JObject obj = JObject.Parse(await Uuuhh.GetWebAsync($"https://api.heweather.com/v5/now?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d"));
                Biaoti.Text = "天气预报•" + obj["HeWeather5"][0]["basic"]["city"];
                Qiwen.Text = obj["HeWeather5"][0]["now"]["tmp"] + "°";
                Tianqi.Text = obj["HeWeather5"][0]["now"]["cond"]["txt"] + "    相对湿度 " + obj["HeWeather5"][0]["now"]["tmp"] + "%";
                fengsu.Text = obj["HeWeather5"][0]["now"]["wind"]["dir"] + "    " + obj["HeWeather5"][0]["now"]["wind"]["sc"] + "级";
                allqiyanjd.Text = "气压:" + obj["HeWeather5"][0]["now"]["pres"] + "    能见度" + obj["HeWeather5"][0]["now"]["vis"];
                Icon.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj["HeWeather5"][0]["now"]["cond"]["code"]}.png"));
                //7天天气预报
                JObject obj1 = JObject.Parse(await Uuuhh.GetWebAsync($"https://api.heweather.com/v5/forecast?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d"));
                //Icon图标
                iconw.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][0]["cond"]["code_d"]}.png"));
                iconww.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][1]["cond"]["code_d"]}.png"));
                iconwww.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][2]["cond"]["code_d"]}.png"));
                iconwwww.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][3]["cond"]["code_d"]}.png"));
                iconwwwww.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][4]["cond"]["code_d"]}.png"));
                iconwwwwww.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][5]["cond"]["code_d"]}.png"));
                iconwwwwwww.Source = new BitmapImage(new Uri($"http://files.heweather.com/cond_icon/{obj1["HeWeather5"][0]["daily_forecast"][6]["cond"]["code_d"]}.png"));
                //时间
                shijianw.Text = obj1["HeWeather5"][0]["daily_forecast"][0]["date"].ToString();
                shijianww.Text = obj1["HeWeather5"][0]["daily_forecast"][1]["date"].ToString();
                shijianwww.Text = obj1["HeWeather5"][0]["daily_forecast"][2]["date"].ToString();
                shijianwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][3]["date"].ToString();
                shijianwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][4]["date"].ToString();
                shijianwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][5]["date"].ToString();
                shijianwwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][6]["date"].ToString();
                //气温:最高/最低
                qiwenw.Text = obj1["HeWeather5"][0]["daily_forecast"][0]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][0]["tmp"]["min"] + "°";
                qiwenww.Text = obj1["HeWeather5"][0]["daily_forecast"][1]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][1]["tmp"]["min"] + "°";
                qiwenwww.Text = obj1["HeWeather5"][0]["daily_forecast"][2]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][2]["tmp"]["min"] + "°";
                qiwenwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][3]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][3]["tmp"]["min"] + "°";
                qiwenwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][4]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][4]["tmp"]["min"] + "°";
                qiwenwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][5]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][5]["tmp"]["min"] + "°";
                qiwenwwwwwww.Text = obj1["HeWeather5"][0]["daily_forecast"][6]["tmp"]["max"] + "°  " + obj1["HeWeather5"][0]["daily_forecast"][6]["tmp"]["min"] + "°";
                //降水量
                jcglw.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][0]["pcpn"];
                jcglww.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][1]["pcpn"];
                jcglwww.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][2]["pcpn"];
                jcglwwww.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][3]["pcpn"];
                jcglwwwww.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][4]["pcpn"];
                jcglwwwwww.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][5]["pcpn"];
                jcglwwwwwww.Content = "💧" + obj1["HeWeather5"][0]["daily_forecast"][6]["pcpn"];
                //每小时天气
                JObject o = JObject.Parse(await Uuuhh.GetWebAsync($"https://api.heweather.com/v5/hourly?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d"));
                hourlyTimew.Content = o["HeWeather5"][0]["hourly_forecast"][0]["date"].ToString();
                hourlyTimeww.Content = o["HeWeather5"][0]["hourly_forecast"][1]["date"].ToString();
                hourlyTimewww.Content = o["HeWeather5"][0]["hourly_forecast"][2]["date"].ToString();
                hourlyTimewwww.Content = o["HeWeather5"][0]["hourly_forecast"][3]["date"].ToString();
                hourlyTimewwwww.Content = o["HeWeather5"][0]["hourly_forecast"][4]["date"].ToString();
                hourlyQww.Content = o["HeWeather5"][0]["hourly_forecast"][0]["tmp"] + "℃";
                hourlyQwww.Content = o["HeWeather5"][0]["hourly_forecast"][1]["tmp"] + "℃";
                hourlyQwwww.Content = o["HeWeather5"][0]["hourly_forecast"][2]["tmp"] + "℃";
                hourlyQwwwww.Content = o["HeWeather5"][0]["hourly_forecast"][3]["tmp"] + "℃";
                hourlyQwwwwww.Content = o["HeWeather5"][0]["hourly_forecast"][4]["tmp"] + "℃";
                hourlyfsw.Content = "☁" + o["HeWeather5"][0]["hourly_forecast"][0]["wind"]["dir"] + "    " + o["HeWeather5"][0]["hourly_forecast"][0]["wind"]["sc"];
                hourlyfsww.Content = "☁" + o["HeWeather5"][0]["hourly_forecast"][1]["wind"]["dir"] + "    " + o["HeWeather5"][0]["hourly_forecast"][1]["wind"]["sc"];
                hourlyfswww.Content = "☁" + o["HeWeather5"][0]["hourly_forecast"][2]["wind"]["dir"] + "    " + o["HeWeather5"][0]["hourly_forecast"][2]["wind"]["sc"];
                hourlyfswwww.Content = "☁" + o["HeWeather5"][0]["hourly_forecast"][3]["wind"]["dir"] + "    " + o["HeWeather5"][0]["hourly_forecast"][3]["wind"]["sc"];
                hourlyfswwwww.Content = "☁" + o["HeWeather5"][0]["hourly_forecast"][4]["wind"]["dir"] + "    " + o["HeWeather5"][0]["hourly_forecast"][4]["wind"]["sc"];
                //生活指数
                JObject b = JObject.Parse(await Uuuhh.GetWebAsync($"https://api.heweather.com/v5/suggestion?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d"));
                CY.Text = "穿衣指数:" + b["HeWeather5"][0]["suggestion"]["drsg"]["brf"];
                CY1.ToolTip = b["HeWeather5"][0]["suggestion"]["drsg"]["txt"].ToString();
                ZYX.Text = "紫外线指数:" + b["HeWeather5"][0]["suggestion"]["uv"]["brf"];
                ZWX.ToolTip = b["HeWeather5"][0]["suggestion"]["uv"]["txt"].ToString();
                XC.Text = "洗车指数:" + b["HeWeather5"][0]["suggestion"]["cw"]["brf"];
                XC1.ToolTip = b["HeWeather5"][0]["suggestion"]["cw"]["txt"].ToString();
                LY.Text = "旅游指数:" + b["HeWeather5"][0]["suggestion"]["trav"]["brf"];
                LY1.ToolTip = b["HeWeather5"][0]["suggestion"]["trav"]["txt"].ToString();
                GM.Text = "感冒指数:" + b["HeWeather5"][0]["suggestion"]["flu"]["brf"];
                GM1.ToolTip = b["HeWeather5"][0]["suggestion"]["flu"]["txt"].ToString();
                YD.Text = "感冒指数:" + b["HeWeather5"][0]["suggestion"]["sport"]["brf"];
                YD1.ToolTip = b["HeWeather5"][0]["suggestion"]["sport"]["txt"].ToString();
            }
            catch { }
        }
        private void GetAndSetWeather(string i)
        {
            try
            {
                //V3
                HttpWebRequest hwr = (HttpWebRequest)WebRequest.Create($"https://api.heweather.com/x3/weather?city={Uri.EscapeUriString(i)}&key=f97e6a6ad4cd49babd0538747c86b88d");
                StreamReader sr = new StreamReader(hwr.GetResponse().GetResponseStream());
                string html5 = sr.ReadToEnd().Replace("HeWeather data service 3.0", "Weather");
                JObject obj = JObject.Parse(html5);
                Biaoti.Text = "天气预报•" + obj["Weather"][0]["basic"]["city"];
                Qiwen.Text = obj["Weather"][0]["now"]["tmp"] + "°";
                Icon.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{obj["Weather"][0]["now"]["cond"]["code"]}.png"));
                Tianqi.Text = obj["Weather"][0]["now"]["cond"]["txt"] + "    相对湿度 " + obj["Weather"][0]["now"]["tmp"] + "%";
                fengsu.Text = obj["Weather"][0]["now"]["wind"]["dir"] + "    " + obj["Weather"][0]["now"]["wind"]["sc"] + "级";
                shijianw.Text = obj["Weather"][0]["daily_forecast"][1]["date"].ToString();
                shijianww.Text = obj["Weather"][0]["daily_forecast"][2]["date"].ToString();
                shijianwww.Text = obj["Weather"][0]["daily_forecast"][3]["date"].ToString();
                shijianwwww.Text = obj["Weather"][0]["daily_forecast"][4]["date"].ToString();
                shijianwwwww.Text = obj["Weather"][0]["daily_forecast"][5]["date"].ToString();
                iconw.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{obj["Weather"][0]["daily_forecast"][1]["cond"]["code_d"]}.png"));
                iconww.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{obj["Weather"][0]["daily_forecast"][2]["cond"]["code_d"]}.png"));
                iconwww.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{obj["Weather"][0]["daily_forecast"][3]["cond"]["code_d"]}.png"));
                iconwwww.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{obj["Weather"][0]["daily_forecast"][4]["cond"]["code_d"]}.png"));
                iconwwwww.Source = new BitmapImage(new Uri($"http://www.heweather.com/weather/images/icon/{obj["Weather"][0]["daily_forecast"][5]["cond"]["code_d"]}.png"));
                qiwenw.Text = obj["Weather"][0]["daily_forecast"][1]["tmp"]["max"] + "°  " + obj["Weather"][0]["daily_forecast"][1]["tmp"]["min"] + "°";
                qiwenww.Text = obj["Weather"][0]["daily_forecast"][2]["tmp"]["max"] + "°  " + obj["Weather"][0]["daily_forecast"][2]["tmp"]["min"] + "°";
                qiwenwww.Text = obj["Weather"][0]["daily_forecast"][3]["tmp"]["max"] + "°  " + obj["Weather"][0]["daily_forecast"][3]["tmp"]["min"] + "°";
                qiwenwwww.Text = obj["Weather"][0]["daily_forecast"][4]["tmp"]["max"] + "°  " + obj["Weather"][0]["daily_forecast"][4]["tmp"]["min"] + "°";
                qiwenwwwww.Text = obj["Weather"][0]["daily_forecast"][5]["tmp"]["max"] + "°  " + obj["Weather"][0]["daily_forecast"][5]["tmp"]["min"] + "°";
                hourlyTimew.Content = obj["Weather"][0]["hourly_forecast"][0]["date"].ToString();
                hourlyTimeww.Content = obj["Weather"][0]["hourly_forecast"][1]["date"].ToString();
                hourlyQww.Content = obj["Weather"][0]["hourly_forecast"][0]["tmp"] + "℃";
                hourlyQwww.Content = obj["Weather"][0]["hourly_forecast"][1]["tmp"] + "℃";
                hourlyfsw.Content = "☁" + obj["Weather"][0]["hourly_forecast"][0]["wind"]["dir"] + "    " + obj["Weather"][0]["hourly_forecast"][0]["wind"]["sc"];
                hourlyfsww.Content = "☁" + obj["Weather"][0]["hourly_forecast"][1]["wind"]["dir"] + "    " + obj["Weather"][0]["hourly_forecast"][1]["wind"]["sc"];
                jcglw.Content = "💧" + obj["Weather"][0]["daily_forecast"][1]["pcpn"];
                jcglww.Content = "💧" + obj["Weather"][0]["daily_forecast"][2]["pcpn"];
                jcglwww.Content = "💧" + obj["Weather"][0]["daily_forecast"][3]["pcpn"];
                jcglwwww.Content = "💧" + obj["Weather"][0]["daily_forecast"][4]["pcpn"];
                jcglwwwww.Content = "💧" + obj["Weather"][0]["daily_forecast"][5]["pcpn"];
                CY.Text = "穿衣指数:" + obj["Weather"][0]["suggestion"]["drsg"]["brf"];
                CY1.ToolTip = obj["Weather"][0]["suggestion"]["drsg"]["txt"].ToString();
                ZYX.Text = "紫外线指数:" + obj["Weather"][0]["suggestion"]["uv"]["brf"];
                ZWX.ToolTip = obj["Weather"][0]["suggestion"]["uv"]["txt"].ToString();
                XC.Text = "洗车指数:" + obj["Weather"][0]["suggestion"]["cw"]["brf"];
                XC1.ToolTip = obj["Weather"][0]["suggestion"]["cw"]["txt"].ToString();
                LY.Text = "旅游指数:" + obj["Weather"][0]["suggestion"]["trav"]["brf"];
                LY1.ToolTip = obj["Weather"][0]["suggestion"]["trav"]["txt"].ToString();
                GM.Text = "感冒指数:" + obj["Weather"][0]["suggestion"]["flu"]["brf"];
                GM1.ToolTip = obj["Weather"][0]["suggestion"]["flu"]["txt"].ToString();
                YD.Text = "感冒指数:" + obj["Weather"][0]["suggestion"]["sport"]["brf"];
                YD1.ToolTip = obj["Weather"][0]["suggestion"]["sport"]["txt"].ToString();
                allqiyanjd.Text = "气压:" + obj["Weather"][0]["now"]["pres"] + "    能见度" + obj["Weather"][0]["now"]["vis"];
            }
            catch {Erro.BeginAnimation(HeightProperty, new DoubleAnimation(0, 37, TimeSpan.FromSeconds(0.2))); }
        }
        private void textBox_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key==Key.Enter)
                if (textBox.Text != "") {
                    GetWeather(textBox.Text);
                    Settings.Default.WeatherInfo = textBox.Text;
                    Settings.Default.Save();
                }
                    
        }

        private void label2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Erro.BeginAnimation(HeightProperty, new DoubleAnimation(37, 0, TimeSpan.FromSeconds(0.2)));
        }

        private void textBlock2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RotateTransform rtf = new RotateTransform();
            rtf.CenterX = 0.5;
            rtf.CenterY = 0.5;
            (sender as Border).RenderTransform = rtf;
            DoubleAnimation dbAscending = new DoubleAnimation(0, 360, new Duration(TimeSpan.FromSeconds(0.3)));
            rtf.BeginAnimation(RotateTransform.AngleProperty, dbAscending);
            GetWeather(Settings.Default.WeatherInfo);
        }
    }
}