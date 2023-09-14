using System.Collections.Generic;

namespace backend.Middleware.jwt
{
    public class AuthenticateResponse
    {
        public AuthenticateResponse()
        {

        }

        public AuthenticateResponse(string account,string name, string token)
        {
            this.account = account;
            this.name=name;
            this.token = token;
        }
        public string account { get; set; }
        public string name { get; set; }
        //public List<IDictionary<string, string>> userRole { get; set; }
        public string token { get; set; }
    }
}