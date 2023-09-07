
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels{
    
    public class GuestbooksImportModel{
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string Name{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string Content{get;set;}
    }
}