
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using backend.ImportModels.Register;
using backend.Models.auth_t;
using backend.Sqls.mssql;
using backend.util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace backend.Middleware.jwt_t
{
    public class loginDao

    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _messqlConnect;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _AccountNumber;


        public loginDao(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _messqlConnect = new MssqlConnect(_appSettings.db);
            // 解析Token
            tokenEnCode TokenEnCode = new tokenEnCode(HttpContextAccessor.HttpContext);

            // 抓取Token的PayLoad
            var PayLoad = TokenEnCode.GetPayLoad();

            // 抓取PayLoad中的account
            this._AccountNumber = PayLoad is null ? "" : PayLoad["email"].ToString();
        }
        #region 註冊
        public void Register(Members model)
        {
            Hashtable ht = new Hashtable();
            string sql = $@"INSERT INTO members (name,phone,email,password,authcode) VALUES (@name,@phone,@email,@password,@authcode); ";
            ht.Add(@"@name", new SQLParameter(model.name, SqlDbType.NVarChar));
            ht.Add(@"@phone", new SQLParameter(model.phone, SqlDbType.NVarChar));
            ht.Add(@"@email", new SQLParameter(model.email, SqlDbType.NVarChar));
            ht.Add(@"@password", new SQLParameter(model.password, SqlDbType.NVarChar));
            ht.Add(@"@authcode", new SQLParameter(model.authcode, SqlDbType.NChar));
            //ht.Add(@"@image", new SQLParameter(model.image, SqlDbType.NVarChar));
            _messqlConnect.Execute(sql, ht);
        }

        public void EmailValidate(EmailValidate Data)
        {
            Hashtable ht = new Hashtable();
            string sql = $@"UPDATE MEMBERS SET authcode=@authcode WHERE email=@email ";
            ht.Add(@"@email", new SQLParameter(Data.email, SqlDbType.NVarChar));
            ht.Add(@"@authcode", new SQLParameter(String.Empty, SqlDbType.NChar));
            _messqlConnect.Execute(sql, ht);
        }
        #endregion
        public List<Members> GetUserList()
        {
            string sql = $@"SELECT * FROM members; ";
            List<Members> AccountResult = _messqlConnect.GetDataList<Members>(sql);

            List<Members> Result = AccountResult.ToList<Members>();
            return Result;
        }

        /*public Members GetMember(string email, string password)
        {
            var Result = new Members();
            Hashtable ht = new Hashtable();
            string sql = $@"Select * from Members where email=@email and password=@password";
            ht.Add(@"@email", new SQLParameter(email, SqlDbType.NVarChar));
            ht.Add(@"@password", new SQLParameter(password, SqlDbType.NVarChar));
            Result = _messqlConnect.GetDataList<Members>(sql, ht).FirstOrDefault();
            return Result;
        }*/

        //編輯個資
        public void Edit(Members Data)
        {
            Hashtable ht = new Hashtable();
            string sql = $@"UPDATE members SET name=@name,phone=@phone,password=@password,image=@image WHERE email=@email ";
            ht.Add(@"@name", new SQLParameter(Data.name, SqlDbType.NVarChar));
            ht.Add(@"@phone", new SQLParameter(Data.phone, SqlDbType.NVarChar));
            ht.Add(@"@password", new SQLParameter(Data.password, SqlDbType.NVarChar));
            ht.Add(@"@image", new SQLParameter(Data.image, SqlDbType.NVarChar));
            ht.Add(@"@email", new SQLParameter(_AccountNumber, SqlDbType.NVarChar));
            _messqlConnect.Execute(sql, ht);
        }

        //忘記密碼
        public void ForgetPassword(string password, string email)
        {
            Hashtable ht = new Hashtable();
            string sql = $@"UPDATE MEMBERS SET password=@password WHERE email=@email ";
            ht.Add(@"@password", new SQLParameter(password, SqlDbType.NVarChar));
            ht.Add(@"@email", new SQLParameter(email, SqlDbType.NVarChar));
            _messqlConnect.Execute(sql, ht);
        }
    }
}