using System;
using System.ComponentModel.DataAnnotations;

namespace backend.ImportModels
{
    public class SampleImportModel
    {
        // 設定防呆條件
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