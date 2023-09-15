using System.Collections.Generic;
using backend.dao;
using backend.ViewModels;
using backend.Models;
using backend.Extensions;
using backend.ImportModels;
using System;
using System.Linq;
using System.Net.Mail;

namespace backend.Services
{
    public class MailService
    {
        private readonly guestbooksDao _guestbooksDao;
        public MailService(guestbooksDao guestbooksDao)
        {
            this._guestbooksDao = guestbooksDao;
        }

        private string gmail_account = "s1410931048@gms.nutc.edu.tw";
        private string gmail_password = "gexarlffubzkdyzr";
        private string gmail_mail = "s1410931048@gms.nutc.edu.tw";

        public string GetValidateCode()
        {
            string[] Code ={ "A", "B", "C", "D", "E", "F", "G", "H", "I","J", "K", "L", "M", "N", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" ,
                            "a", "b", "c", "d", "e", "f", "g", "h", "i","j", "k", "l", "m", "n", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" ,
                            "1", "2", "3", "4", "5", "6", "7", "8", "9"};
            string VaildateCode = string.Empty;
            Random rd = new Random();
            for (int i = 0; i < 10; i++)
            {
                VaildateCode += Code[rd.Next(Code.Count())];
            }
            return VaildateCode;
        }
        public string GetRegisterMailBody(string TempString, string UserName, string ValidateUrl)
        {
            TempString = TempString.Replace("{{UserName}}", UserName);
            TempString = TempString.Replace("{{ValidateUrl}}", ValidateUrl);
            return TempString;
        }
        public void SendRegisterMail(string MailBody, string ToEmail)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            SmtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(gmail_mail);
            mail.To.Add(ToEmail);
            mail.Subject = " 繪心醫效 註冊確認信 ";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            SmtpServer.Send(mail);
        }
        public string GetForgetPasswordMailBody(string TempString, string UserName, string password)
        {
            TempString = TempString.Replace("{{UserName}}", UserName);
            TempString = TempString.Replace("{{NewPassword}}", password);
            return TempString;
        }
        public void SendForgetPasswordMail(string MailBody, string ToEmail)
        {
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential(gmail_account, gmail_password);
            SmtpServer.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(gmail_mail);
            mail.To.Add(ToEmail);
            mail.Subject = " 繪心醫效 註冊確認信 ";
            mail.Body = MailBody;
            mail.IsBodyHtml = true;
            SmtpServer.Send(mail);
        }

    }
}