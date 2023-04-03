using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace Taxi
{
    public partial class MailForm : Form
    {
        public MailForm()
        {
            InitializeComponent();
        }

        private void MailForm_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SmtpClient Client = new SmtpClient();
            Client.Credentials = new NetworkCredential("komrad.sergei-goncharov@yandex.ru", "kjogfmdgyjzqdlaa");
            Client.Host = "smtp.yandex.ru";
            Client.Port = 587;
            Client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("komrad.sergei-goncharov@yandex.ru");
            mail.To.Add(new MailAddress(textBox1.Text));
            mail.Subject = "" + textBox4.Text;
            mail.IsBodyHtml = true;
            mail.Body = "" + textBox3.Text;
            Client.Send(mail);
        }
    }
}
