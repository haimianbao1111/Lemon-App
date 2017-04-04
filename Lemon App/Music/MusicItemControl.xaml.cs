using Lemon_App.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
    /// MusicItemControl.xaml 的交互逻辑
    /// </summary>
    public partial class MusicItemControl : UserControl
    {
        public MusicItemControl()
        {
            InitializeComponent();
        }
        public string MusicName
        {
            get {return Nm.Text; } set { Nm.Text = value; }
        }
        public new string Content
        {
            get { return MusicName + "-" + MusicGS; }
        }
        public string MusicGS
        {
            get { return GS.Text; } set { GS.Text = value+MusicZJ; }
        }
        public string MusicZJ
        {
            get { return k; }
            set { GS.Text =MusicGS+ "  "+value; }
        }
        public string Qt
        {
          get { return Q.Text; }
            set { if (value != "") if (value == "SQ") { Q.Text = value; RS.Visibility = Visibility.Visible; } else if (value == "HQ") { QC.Text = value; RC.Visibility = Visibility.Visible; } }
        }
        public string mvid = "";
        public string ismv
        {
            get { return MC.Text; }
            set { if (value != "") { mv.Visibility = Visibility.Visible; mvid = value; } }
        }
        public Object M;
        public object Music { get { return M; } set { M = value; } }
        public object t = "";
     //   public new object ToolTip{get { return t; }set { t = value; if (t.ToString() == "True") { IsOpen.Visibility = Visibility.Visible; } else IsOpen.Visibility = Visibility.Collapsed;  } }
        private void mv_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start($"https://y.qq.com/portal/mv/v/{mvid}.html");
        }
        string k = "";
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            ListJson lj = new ListJson();
            if (Settings.Default.MusicList != "") lj = MusicControl.JsonToObject(Settings.Default.MusicList, lj) as ListJson;
            lj.List.Add(new ListItem() { ItemText = (this.Music as Music) });
            Settings.Default.MusicList = MusicControl.ToJSON(lj);
            Settings.Default.Save();
        }
    }
}
