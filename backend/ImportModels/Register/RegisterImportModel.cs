
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels.Register
{
    public class RegisterImportModel
    {
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過20個字")]
        public string Account{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string Password{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過20個字")]
        public string Name{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過170個字")]
        public string Email{get;set;}
    }
}