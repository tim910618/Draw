
using System.ComponentModel.DataAnnotations;

namespace backend.Middleware.jwt_t
{
    public class loginViewModel
    {
        [Required]
        public string Email{get;set;}
        [Required]
        public string Password{get;set;}
    }
}