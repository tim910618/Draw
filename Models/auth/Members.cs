
using System.Collections.Generic;
namespace backend.Models.auth_t
{
    public class Members
    {
        public string name{get;set;}
        public string phone{get;set;}
        public string email{get;set;}
        public string password{get;set;}
        public string authcode{get;set;}
        public string image{get;set;}
        
        //虛擬的
        //public virtual List<accountRoleModel> accountRole { get; set; }
    }
}