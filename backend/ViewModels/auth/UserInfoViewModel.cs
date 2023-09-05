namespace backend.ViewModels.auth.User
{
    public class UserInfoViewModel
    {
        public InfoViewModel info { get; set; }
    }
    public class InfoViewModel
    {
        public string account { get; set; }
        public string name { get; set; }
        public string email { get; set; }
        public string createat { get; set; }
        public string updateat { get; set; }
    }
}