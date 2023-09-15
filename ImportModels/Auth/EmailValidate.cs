
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels.Register
{
    public class EmailValidate
    {
        public string email { get; set; }
        public string authcode { get; set; }
    }
}