using System;
using System.Security.Cryptography;
using System.Text;

namespace backend.Utils
{
    class sha256Hash
    {
        /// <summary>
        /// 密碼加密
        /// </summary>
        /// <param name="Password"></param>
        /// <param name="Key"></param>
        /// <returns></returns>
        public string getSha256(string Password, string Key)
        {
            var encoding = new System.Text.UTF8Encoding();
            //先轉換成位元組
            byte[] KeyByte = encoding.GetBytes(Key);
            byte[] PasswordBytes = encoding.GetBytes(Password);
            // using 結束就釋放資源，可管理資料、避免洩漏
            using (var hmacSHA256 = new HMACSHA256(KeyByte))
            {
                byte[] hashMessage = hmacSHA256.ComputeHash(PasswordBytes);
                return BitConverter.ToString(hashMessage).Replace("-", "").ToLower();
            }
        }
    }
}