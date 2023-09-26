
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Web;
using backend.ImportModels.Register;
using backend.Middleware.jwt;
using backend.Models.auth_t;
using backend.Services;
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
        bool checkMember(string email);
        void EmailValidate(EmailValidate Data);
        void Edit(EditModel EditData);
        void ForgetPassword(ForgetPasswordImportModel Data);
    }
    public class loginService : IUserService_T
    {
        private readonly appSettings _appSettings;
        private readonly loginDao _dao;
        private readonly MailService _mailService;
        private readonly List<Members> _user;
        public loginService(IOptions<appSettings> appSettings, loginDao dao, MailService mailService)
        {
            _appSettings = appSettings.Value;
            _dao = dao;
            _user = _dao.GetUserList();
            _mailService = mailService;
        }


        #region 註冊
        public void Register(RegisterImportModel model)
        {
            sha256Hash sha256 = new sha256Hash();
            Members Data = new Members
            {
                name = model.name,
                phone = model.phone,
                email = model.email,
                password = sha256.getSha256(model.password, this._appSettings.hash_key),
                authcode = _mailService.GetValidateCode(),
            };
            _dao.Register(Data);

            string filePath = @"Views/RegisterEmail.html";
            string TempString = System.IO.File.ReadAllText(filePath);
            string mailBody = _mailService.GetRegisterMailBody(TempString, Data.name, Data.authcode);
            _mailService.SendRegisterMail(mailBody, model.email);
        }
        public void EmailValidate(EmailValidate Data)
        {
            _dao.EmailValidate(Data);
        }
        #endregion

        public AuthenticateResponse Authenticate(loginViewModel model)
        {
            sha256Hash sha256 = new sha256Hash();
            var User = _user.SingleOrDefault(m => m.email == model.Email && m.password == sha256.getSha256(model.Password, this._appSettings.hash_key));
            if (User == null) return null;

            Members user = (User != null) ? User : null;
            if (User != null)
            {
                user = new Members
                {
                    name = User.name,
                    phone = User.phone,
                    email = User.email,
                    password = User.password,
                    authcode = User.authcode,
                    image = User.image
                };
            }

            //製作Token
            var token = generateJwtToken(user);
            return new AuthenticateResponse(user.email.ToString(), user.name.ToString(), token);
        }

        public IEnumerable<Members> GetAll()
        {
            return _user;
        }
        public Members GetDataById(string email)
        {
            return _user.FirstOrDefault(m => m.email == email);
        }

        public bool checkMember(string email)
        {
            if (_user.FirstOrDefault(m => m.email == email) != null)
            {
                return false;
            }
            else
            {
                return true;
            }
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

        //編輯個資
        public void Edit(EditModel EditData)
        {
            sha256Hash sha256 = new sha256Hash();
            var FileName = Guid.NewGuid().ToString() + Path.GetExtension(EditData.image.FileName);
            var folderPath = Path.Combine(this._appSettings.UploadPath, "ParentHead");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            var path = Path.Combine(folderPath, FileName);

            Members Data = new Members
            {
                name = EditData.name,
                phone = EditData.phone,
                password = sha256.getSha256(EditData.password, this._appSettings.hash_key),
                image = FileName,
            };
            _dao.Edit(Data);

            //存到路徑裡面
            using (var stream = new FileStream(path, FileMode.Create))
            {
                EditData.image.CopyTo(stream);
            }
        }

        //忘記密碼
        public void ForgetPassword(ForgetPasswordImportModel Data)
        {
            sha256Hash sha256 = new sha256Hash();
            Members members = GetDataById(Data.email);
            string password = _mailService.GetValidateCode();
            string sha256password = sha256.getSha256(password, this._appSettings.hash_key);
            _dao.ForgetPassword(sha256password, members.email);

            string filePath = @"Views/ForgetPassword.html";
            string TempString = System.IO.File.ReadAllText(filePath);
            // var validateUrl = new
            // {
            //     name = members.name,
            //     password = password,
            // };
            string mailBody = _mailService.GetForgetPasswordMailBody(TempString, members.name, password);
            _mailService.SendForgetPasswordMail(mailBody, Data.email);
        }
    }
}