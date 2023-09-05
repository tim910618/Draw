using System;
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels
{
    public class SampleUpdateModel
    {
        // 設定防呆條件

        [Required(ErrorMessage = " [id] 為必填欄位 ")]
        [RegularExpression("^[{(]?[0-9A-Za-z]{8}[-]?([0-9A-Za-z]{4}[-]?){3}[0-9A-Za-z]{12}[)}]?$",ErrorMessage =" [id] 格式錯誤 ")]
        [StringLengthAttribute(36, ErrorMessage = " [id] 不能超過36字元 ")]
        public string id{ get; set; }

        [Required]
        [StringLengthAttribute(50, ErrorMessage = " [title] 不能超過50字元 ")]
        public string title { get; set; }

        [Required]
        [StringLengthAttribute(20, ErrorMessage = " [title] 不能超過20字元 ")]
        public string content{ get; set; }

        [StringLengthAttribute(170, ErrorMessage = " [email] 不能超過170字元 ")]
        [EmailAddress(ErrorMessage =" [email] 格式錯誤 ")]
        public string email{ get; set; }

        [RegularExpression("^[0-9]*$",ErrorMessage =" [num] 僅能輸入整數 ")]
        public string num { get; set; }
    }
}