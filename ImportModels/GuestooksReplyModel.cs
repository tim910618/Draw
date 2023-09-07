using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels
{
    public class GuestbooksReplyModel
    {
        public string Id{get;set;}
        [Required]
        [StringLengthAttribute(50,ErrorMessage ="不可超過50個字")]
        public string Reply{get;set;}
    }
}