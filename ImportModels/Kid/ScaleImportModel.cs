
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace backend.ImportModels
{
    public class ScaleImportModel
    {
        public string kid_id { get; set; }
        //錯的問題
        public string question { get; set; }
        //危險因子項目 EX:1,3
        public string disease { get; set; }
        //危險因子項目其他
        public string disease_other { get; set; }
    }
}