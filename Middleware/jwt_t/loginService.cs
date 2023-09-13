
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using backend.ImportModels.Register;
using backend.Middleware.jwt;
using backend.Models.auth;
using backend.Models.auth_t;
using backend.util;
using backend.util.Models;
using backend.Utils;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace backend.Middleware.jwt_t
{
    public interface IUserService_T
    {
        AuthenticateResponse Authenticate(loginViewModel model);
        IEnumerable<Members> GetAll();
        Members GetDataById(string Account);
        void Register(RegisterImportModel model);
    }
    public class loginService : IUserService_T
    {
        private readonly appSettings _appSettings;
        private readonly Ch09Dao _dao;
        private readonly List<Members> _user;
        public loginService(IOptions<appSettings> appSettings, Ch09Dao dao)
        {
            _appSettings = appSettings.Value;
            _dao = dao;
            _user = _dao.GetUserList();
        }


        #region 註冊
        /*public void Register(RegisterImportModel model)
        {
            sha256Hash sha256 = new sha256Hash();
            Members Data = new Members
            {
                Account = model.Account,
                Password = sha256.getSha256(model.Password, this._appSettings.hash_key),
                Name = model.Name,
                Email = model.Email,
            };
            _dao.Register(Data);
            //SendMail(Data.Email);
        }*/
        /*public string SendMail(string to_email)
        {
            mail mail=new mail();
            
            SMTPModel data=new SMTPModel
            {
                SMTP_FROM_MAIL="",
                SMTP_FROM_NAME="",
                SMTP_HOST="",
                SMTP_PORT="",
                SMTP_USER="",
                SMTP_PWD="",
                SMTP_AUTH="",
                SMTP_SSL="",
            };
        }*/
        #endregion

        public AuthenticateResponse Authenticate(loginViewModel model)
        {
            sha256Hash sha256 = new sha256Hash();
            //model.Password = sha256.getSha256(model.Password, this._appSettings.hash_key);
            //Members User = _dao.GetMember(model.Email,model.Password);
            var travelUser = _user.SingleOrDefault(m => m.email == model.Email && m.password == sha256.getSha256(model.Password, this._appSettings.hash_key));
            if (travelUser == null) return null;

            Members user = (travelUser != null) ? travelUser : null;

            if (travelUser != null)
            {
                user = new Members
                {
                    name = travelUser.name,
                    phone = travelUser.phone,
                    email = travelUser.email,
                    password = travelUser.password,
                    authcode = travelUser.authcode,
                    //accountRole = travelUser.accountRole.Union(travelUser.accountRole).ToList<accountRoleModel>(),
                };
            }

            /*List<IDictionary<string, string>> roleList = new List<IDictionary<string, string>>();
            foreach (var roleItem in user.accountRole)
            {
                IDictionary<string, string> role = new Dictionary<string, string>();
                //role.Add("role",roleItem.sys_rid);
                //role.Add("type",roleItem.sys_type);
                roleList.Add(role);
            }*/
            //製作Token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user.email.ToString(), token);
        }

        public IEnumerable<Members> GetAll()
        {
            return _user;
        }

        public Members GetDataById(string Email)
        {
            return _user.FirstOrDefault(m => m.email == Email);
        }

        public string generateJwtToken(Members user)
        {
            var header = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._appSettings.jwt_secret);
            var payload = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("email", user.email.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = header.CreateToken(payload);
            return $"Bearer {header.WriteToken(token)}";
        }

        public string GetLine(List<IDictionary<string, string>> dictionartList)
        {
            string result = "[";
            foreach (var dictionaryItem in dictionartList)
            {
                string item = "";
                foreach (KeyValuePair<string, string> pair in dictionaryItem)
                {
                    item += $@"""{pair.Key}"":""{pair.Value}"",";
                }
                result += "{" + $"{item.TrimEnd(',')}" + "},";
            }
            result += "{" + $"{result.TrimEnd(',')}" + "},";
            return result;
        }

        public void Register(RegisterImportModel model)
        {
            throw new NotImplementedException();
        }
    }
}