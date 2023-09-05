using System;
namespace backend.Models
{
    public class sys_sample // 與TABLE_NAME相同
    {
        // DB schema Model 
        public Guid id { get; set; }
        public string title { get; set; }
        public string content{ get; set; }
        public string email{ get; set; }
        public int num { get; set; }
        public string createid{ get; set; }
        public DateTime createat{ get; set; }
        public string updateid{ get; set; }
        public DateTime updateat{ get; set; }
    }
}