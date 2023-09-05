using System;

namespace backend.util.Models
{
    public class SMTPModel
    {
        public string SMTP_FROM_MAIL { get; set; }
        public string SMTP_FROM_NAME { get; set; }
        public string SMTP_HOST { get; set; }
        public string SMTP_PORT { get; set; }
        public string SMTP_USER { get; set; }
        public string SMTP_PWD { get; set; }
        public string SMTP_AUTH { get; set; }
        public string SMTP_SSL { get; set; }
    }
}