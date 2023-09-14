
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels{
    
    public class KidInsertImportModel{
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string name{get;set;}
        [Required]
        public string birth{get;set;}
        [Required]
        public bool gender{get;set;}
    }
}