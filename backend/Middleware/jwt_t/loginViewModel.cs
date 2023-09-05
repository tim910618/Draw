
using System.ComponentModel.DataAnnotations;

namespace backend.Middleware.jwt_t
{
    public class loginViewModel
    {
        [Required]
        public string Account{get;set;}
        [Required]
        public string Password{get;set;}
    }
}