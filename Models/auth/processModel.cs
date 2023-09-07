using System;

namespace backend.Models.auth
{
    //sys_process
    public class processModel
    {
        public string sys_mid { get; set; }
        public string sys_pid { get; set; }
        public string sys_pname { get; set; }
        public string sys_purl { get; set; }
        public int sys_pseq { get; set; }
        public bool sys_enable { get; set; }
        public bool sys_show { get; set; }
        public string createid { get; set; }
        public DateTime createat { get; set; }
        public string updateid { get; set; }
        public DateTime updateat { get; set; }
        public processModel process { get; set; }
    }
}