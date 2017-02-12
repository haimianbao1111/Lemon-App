using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
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
    /// LoginCxaml.xaml 的交互逻辑
    /// </summary>
    public partial class LoginCxaml : UserControl
    {
        public LoginCxaml()
        {
            InitializeComponent();
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if((sender as PasswordBox).Password.Length == 4)
            {
                MessageBox.Show("n");
            }
        }
        string ini = "";
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (N.Text != string.Empty)
            {
                Random ra = new Random();
                ini = ra.Next(9999).ToString();
                    MailMessage m = new MailMessage()
                    {
                        From = new MailAddress("lemon.app@qq.com", "Lemon团队")
                    };
                    m.To.Add(new MailAddress(N.Text));
                    m.Subject = "Lemon App";
                    m.SubjectEncoding = Encoding.UTF8;
                    m.Body = He.EmailMessage.Replace("{ninini}", ini);
                    m.BodyEncoding = Encoding.UTF8;
                    m.IsBodyHtml = true;
                    SmtpClient s = new SmtpClient()
                    {
                        Host = "smtp.qq.com",
                        Port = 587,
                        EnableSsl = true,
                        Credentials = new NetworkCredential("lemon.app@qq.com", "qtmiqibczofmddbi")
                    };
                    s.Send(m);
            }
        }
    }
}
