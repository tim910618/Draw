
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels{
    public class GuestbooksUpdateModel{
        [Required(ErrorMessage ="必填")]
        public string Id {get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50字")]
        public string Name{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string Content{get;set;}
    }
}