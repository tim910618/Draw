using System;

namespace backend.util.Models
{
    public class MailModel
    {
        public Guid sys_logid { get; set; }
        public DateTime sys_logtime { get; set; }
        public string mail_subject { get; set; }
        public string mail_content { get; set; }
        public string mailto { get; set; }
        public bool status { get; set; }
        public string note { get; set; }
        public DateTime createat { get; set; }
    }
}