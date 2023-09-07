using System.ComponentModel.DataAnnotations;

namespace backend.Middleware.jwt
{
    public class AuthenticateRequest
    {
        [Required]
        public string Account{get;set;}

        [Required]
        public string Password{get;set;}
    }
}