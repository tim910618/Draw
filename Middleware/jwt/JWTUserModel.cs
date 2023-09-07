using System;
using System.Collections.Generic;
using backend.Models.auth;

namespace backend.Middleware.jwt
{
    public class JWTUserModel
    {
        public string account { get; set; }
        public string password { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public bool status { get; set; }
        public string createid { get; set; }
        public DateTime? createat { get; set; }
        public string updateid { get; set; }
        public DateTime? updateat { get; set; }


        public string sys_type { get; set; }
        //虛擬的
        public virtual List<accountRoleModel> accountRole { get; set; }
    }
}