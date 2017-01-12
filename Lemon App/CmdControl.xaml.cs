using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// CmdControl.xaml 的交互逻辑
    /// </summary>
    public partial class CmdControl : UserControl
    {
        public CmdControl()
        {
            InitializeComponent();
            this.p.StartInfo.FileName = "cmd.exe";
            this.p.StartInfo.RedirectStandardInput = true;
            this.p.StartInfo.RedirectStandardOutput = true;
            this.p.StartInfo.UseShellExecute = false;
            this.p.StartInfo.RedirectStandardError = true;
            this.p.StartInfo.CreateNoWindow = true;
            this.p.Start();
        }
      //  Thread t;
        private void textBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            textBlock1.Text = "";
            //this.t = new Thread(new ThreadStart(this.S));
            ////   this.t.IsBackground = false;
            ////  this.t.Start();
            this.p.StandardInput.WriteLine(textBox.Text);
            this.p.StandardInput.WriteLine("exit");
            StreamReader standardOutput = p.StandardOutput;
            while (!standardOutput.EndOfStream)
            {
                textBlock1.Text = textBlock1.Text + "\r\n" + standardOutput.ReadLine();
            }
            standardOutput.Dispose();
        }
         Process p = new Process();
         void S()
        {
            if (this.p.HasExited)
            {
                this.p.Start();
            }
            this.p.StandardInput.WriteLine(textBox.Text);
            this.p.StandardInput.WriteLine("exit");
            StreamReader standardOutput = p.StandardOutput;
            while (!standardOutput.EndOfStream)
            {
                textBlock1.Text =textBlock1.Text + "\r\n" + standardOutput.ReadLine();
            }
            standardOutput.Dispose();
        }
    }
}
