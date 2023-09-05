using System.Collections.Generic;

namespace backend.Middleware.jwt
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse()
        {

        }

        public AuthenticateResponse(string userId, List<IDictionary<string, string>> role, string token)
        {
            this.userId = userId;
            this.userRole = role;
            this.token = token;
        }
        public string userId { get; set; }
        public List<IDictionary<string, string>> userRole { get; set; }
        public string token { get; set; }
    }
}