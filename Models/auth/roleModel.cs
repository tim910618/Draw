using System;
using System.Collections.Generic;

namespace backend.Models.auth
{
    //sys_role
    public class roleModel
    {
        public string sys_rid { get; set; }
        public string sys_rname { get; set; }
        public string sys_rnote { get; set; }
        public bool sys_enable { get; set; }
        public string createid { get; set; }
        public DateTime createat { get; set; }
        public string updateid { get; set; }
        public DateTime updateat { get; set; }
        public List<roleProcessModel> role_process { get; set; }
    }
}