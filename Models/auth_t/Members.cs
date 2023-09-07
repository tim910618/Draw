
using System.Collections.Generic;
using backend.Models.auth;

namespace backend.Models.auth_t
{
    public class Members
    {
        public string Account{get;set;}
        public string Password{get;set;}
        public string Name{get;set;}
        public string Email{get;set;}
        
        //虛擬的
        public virtual List<accountRoleModel> accountRole { get; set; }
    }
}