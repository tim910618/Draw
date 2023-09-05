using System;

namespace backend.Models.auth
{
    //account_role
    public class accountRoleModel
    {
        public string account { get; set; }
        //系統ID??
        public string sys_rid { get; set; }
        public string createid { get; set; }
        public DateTime createat { get; set; }
        public string updateid { get; set; }
        public DateTime updateat { get; set; }
        public string sys_type { get; set; }
        public processModel process { get; set; }
    }
}