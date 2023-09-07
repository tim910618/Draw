using System;

namespace backend.Models.user
{
    public class userinfoModel
    {
        public string account { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public DateTime createat { get; set; }
        public DateTime updateat { get; set; }
        public userinfoModel userinfo { get; set; }
    }
}