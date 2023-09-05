using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using backend.util.Models;

namespace backend.Utils
{
    class mail
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static List<MailModel> SendMail(string[] to_email, string subject, string mail_content, SMTPModel config_info)
        {
            MailMessage EmailHtmlContent = new MailMessage();
            EmailHtmlContent.BodyEncoding = Encoding.UTF8;      // 郵件內容編碼
            EmailHtmlContent.SubjectEncoding = Encoding.UTF8;   // 郵件標題編碼
            EmailHtmlContent.Priority = MailPriority.Normal;           // 郵件優先級
            EmailHtmlContent.IsBodyHtml = true;                 // 信件內容是否為HTML
            EmailHtmlContent.From = new MailAddress(config_info.SMTP_FROM_MAIL, config_info.SMTP_FROM_NAME, Encoding.UTF8);
            List<MailModel> DataList = new List<MailModel>();
            foreach (string email in to_email)
            {
                MailModel data = new MailModel();
                data.sys_logtime = DateTime.Now;
                data.mail_content = mail_content;
                data.mail_subject = subject;
                data.mailto = email.Trim();
                DataList.Add(data);
                EmailHtmlContent.To.Add(email.Trim());
            }
            if (EmailHtmlContent.To.Count > 0)
            {
                EmailHtmlContent.Subject = subject;
                EmailHtmlContent.Body = mail_content;

                SmtpClient EmailConnection = new SmtpClient(config_info.SMTP_HOST, Convert.ToInt32(config_info.SMTP_PORT));
                if (config_info.SMTP_AUTH.Equals("Y"))
                    EmailConnection.Credentials = new NetworkCredential(config_info.SMTP_USER, config_info.SMTP_PWD);

                if (config_info.SMTP_SSL.Equals("Y"))
                    EmailConnection.EnableSsl = true;

                try
                {
                    EmailConnection.Send(EmailHtmlContent);
                    EmailHtmlContent.Dispose();
                    foreach (var item in DataList)
                    {
                        item.status = true;
                    }
                    return DataList;
                }
                catch (Exception Ex)
                {
                    foreach (var item in DataList)
                    {
                        item.status = false;
                    }
                    return DataList;
                }
            }
            return DataList;
        }
    }
}