using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;
using System.Net.Mail;

namespace CarDetailingWebApi.Models.Services
{
    public class UtilityService
    {
        private string MyEmail = "CarDetailingApp123@gmail.com";
        private string MyEmailPassword = "TestoweHaslo123-";

        public Result<MailMessage> SendEmail(string from,string password, string to,string message)
        {
            var R = new Result<MailMessage>();
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = "CarWebAppi";
                mail.Body = message;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(from, password);
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                R.info = "pomyślnie wysłany email";
                R.value = mail;
                R.status = true;
                return R;

            }
            catch (Exception ex)
            {
                R.info = ex.ToString();
                R.status = false;
                return R;
            }
        }

        public Result<MailMessage> SendCompanyEmail(string to, string message)
        {
            return SendEmail(this.MyEmail, this.MyEmailPassword, to, message);
        }
        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomPassword(int size = 4)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(size, true));
            builder.Append(RandomNumber(100, 999));
            builder.Append(RandomString(2, false));
            return builder.ToString();
        }

    }
}