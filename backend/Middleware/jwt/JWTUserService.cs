using backend.util;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend.Utils;
using backend.Models.auth;

namespace backend.Middleware.jwt
{
    // interface 定義一個介面
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        //全部用<>包起來
        IEnumerable<JWTUserModel> GetAll();
        //單筆就不需要<>
        JWTUserModel GetById(string account);
    }
    public class JWTUserService : IUserService
    {
        //讀取appSettings資料
        private readonly appSettings _appSettings;
        //資料庫相關
        private readonly JWTDao _dao;
        private List<JWTUserModel> _users;

        public JWTUserService(IOptions<appSettings> appSettings, JWTDao jWTDao)
        {
            this._appSettings = appSettings.Value;
            this._dao = jWTDao;
            this._users = _dao.GetUserList();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            sha256Hash sha256 = new sha256Hash();
            // SingleOrDefault 回傳值
            //傳回符合條件的"單一"元素
            // m 是用來代表 _users 集合中的每個使用者物件
            var travelUser = _users.SingleOrDefault(m => m.account == model.Account && m.password == sha256.getSha256(model.Password, this._appSettings.hash_key));

            // return null if user not found
            if (travelUser == null) return null;

            // authentication successful so generate jwt token
            JWTUserModel user = (travelUser != null) ? travelUser : null;

            if (travelUser != null)
            {
                user = new JWTUserModel
                {
                    account = travelUser.account,
                    password = travelUser.password,
                    name = travelUser.name,
                    email = travelUser.email,
                    status = travelUser.status,
                    createid = travelUser.createid,
                    createat = travelUser.createat,
                    updateid = travelUser.updateid,
                    updateat = travelUser.updateat,
                    sys_type = travelUser.sys_type,
                    // Union 於合併兩個集合並去除重複元素的操作
                    // ToList<>是他的型別
                    accountRole = travelUser.accountRole.Union(travelUser.accountRole).ToList<accountRoleModel>()
                };
            }
        
            List<IDictionary<string, string>> roleList = new List<IDictionary<string, string>>();

            foreach (var roleItem in user.accountRole)
            {
                IDictionary<string, string> role = new Dictionary<string, string>();
                role.Add("role", roleItem.sys_rid);
                role.Add("sys_type", roleItem.sys_type);
                roleList.Add(role);
            }
            // "role"= Admin
            // "sys_type" = "範例"

            var token = generateJwtToken(user, roleList);

            return new AuthenticateResponse(user.account.ToString(), roleList, token);
        }

        public IEnumerable<JWTUserModel> GetAll()
        {
            return _users;
        }

        public JWTUserModel GetById(string account)
        {
            //傳回第 1 筆資料
            return _users.FirstOrDefault(m => m.account == account);
        }

        private string generateJwtToken(JWTUserModel user, List<IDictionary<string, string>> role)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.jwt_secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                // string.Join(";", myDict.Select(x => x.Key + "=" + x.Value).ToArray());
                Subject = new ClaimsIdentity(new[] { new Claim("account", user.account.ToString()) ,
                                                    new Claim("role", GetLine(role)) }), //取得上面的文字
                Expires = DateTime.UtcNow.AddMinutes(30), // 到期時間 30 分鐘
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return $"Bearer {tokenHandler.WriteToken(token)}";
        }

        private string GetLine(List<IDictionary<string, string>> dictionaryList)
        {
            // Build up each line one-by-one and then trim the end
            string result = "[";
            foreach (var dictionaryItem in dictionaryList)
            {
                string item = "";
                foreach (KeyValuePair<string, string> pair in dictionaryItem)
                {
                    item += $@"""{pair.Key}"":""{pair.Value}"",";  // 將每個鍵值對轉換為 JSON 格式的字串，並連接到 item 字串中
                }
                result += "{" + $"{item.TrimEnd(',')}" + "},";  // 將每一行的字串包裝在 {} 中，並連接到結果字串中
            }

            // Remove the final delimiter
            //去掉最後的逗號
            result = $"{result.TrimEnd(',')}]";
            return result;
        }
    }
}