
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels.Register
{
    public class RegisterImportModel
    {
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string name{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string phone{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過300個字")]
        public string email{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過30個字")]
        public string password{get;set;}
    }
}