using System.Data;
using System.Linq;
using System.Collections;
using System;
using System.Collections.Generic;
using backend.Models;
using backend.Sqls.mssql;
using backend.util;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using backend.ImportModels;

namespace backend.dao
{
    public class sampleDao
    {
        private readonly appSettings _appSettings;
        //應該算自己寫好的套件
        private readonly MssqlConnect _MssqlConnect;

        private readonly IHttpContextAccessor _HttpContextAccessor;

        private string _AccountNumber;

        public sampleDao(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor)
        {
            _appSettings = appSettings.Value;
            _MssqlConnect = new MssqlConnect(_appSettings.db);

            /* 取得 JWT 中資訊 */
            this._HttpContextAccessor = HttpContextAccessor ??
                throw new ArgumentNullException(nameof(HttpContextAccessor));

            // 解析Token
            tokenEnCode TokenEnCode = new tokenEnCode(HttpContextAccessor.HttpContext);

            // 抓取Token的PayLoad
            var PayLoad = TokenEnCode.GetPayLoad();

            // 抓取PayLoad中的account
            this._AccountNumber = PayLoad is null ? "" : PayLoad["account"].ToString();
        }

        public List<sys_sample> GetDataList()
        {
            List<sys_sample> Result = new List<sys_sample>();
            string sql = @"
                select *
                from sys_sample
            ";
            Result = _MssqlConnect.GetDataList<sys_sample>(sql);
            return Result;
        }

        public sys_sample GetDataById(Guid id)
        {
            Hashtable ht = new Hashtable();
            sys_sample Result = new sys_sample();
            string sql = @"
                select *
                from sys_sample
                where id = @id
            ";
            ht.Add(@"@id", new SQLParameter(id, SqlDbType.UniqueIdentifier));
            Result = _MssqlConnect.GetDataList<sys_sample>(sql, ht).FirstOrDefault();
            return Result;
        }

        public void Insert(sys_sample model)
        {
            Hashtable ht = new Hashtable();
            string sql = @"
                INSERT INTO [sys_sample]
                        ([id]
                        ,[title]
                        ,[content]
                        ,[email]
                        ,[num]
                        ,[createid]
                        ,[createat]
                        ,[updateid]
                        ,[updateat])
                    VALUES
                    (
                        @id,
                        @title,
                        @content,
                        @email,
                        @num,
                        @createid,
                        @createat,
                        @updateid,
                        @updateat
                    )
            ";
            ht.Add(@"@id", new SQLParameter(model.id, SqlDbType.UniqueIdentifier));
            ht.Add(@"@title", new SQLParameter(model.title, SqlDbType.NVarChar));
            ht.Add(@"@content", new SQLParameter(model.content, SqlDbType.NVarChar));
            ht.Add(@"@email", new SQLParameter(model.email, SqlDbType.VarChar));
            ht.Add(@"@num", new SQLParameter(model.num, SqlDbType.Int));
            ht.Add(@"@createid", new SQLParameter(this._AccountNumber, SqlDbType.VarChar));
            ht.Add(@"@createat", new SQLParameter(DateTime.Now, SqlDbType.DateTime2));
            ht.Add(@"@updateid", new SQLParameter(this._AccountNumber, SqlDbType.VarChar));
            ht.Add(@"@updateat", new SQLParameter(DateTime.Now, SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql, ht);
        }

        public void Update(sys_sample model)
        {
            Hashtable ht = new Hashtable();
            string sql = @"
                UPDATE [sys_sample]
                SET [title] = @title
                    ,[content] = @content
                    ,[email] = @email
                    ,[num] = @num
                    ,[updateid] = @updateid 
                    ,[updateat] = @updateat
                WHERE id = @id
            ";
            ht.Add(@"@id", new SQLParameter(model.id, SqlDbType.UniqueIdentifier));
            ht.Add(@"@title", new SQLParameter(model.title, SqlDbType.NVarChar));
            ht.Add(@"@content", new SQLParameter(model.content, SqlDbType.NVarChar));
            ht.Add(@"@email", new SQLParameter(model.email, SqlDbType.VarChar));
            ht.Add(@"@num", new SQLParameter(model.num, SqlDbType.Int));
            ht.Add(@"@updateid", new SQLParameter(this._AccountNumber, SqlDbType.VarChar));
            ht.Add(@"@updateat", new SQLParameter(DateTime.Now, SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql, ht);
        }

        public void Delete(Guid id)
        {
            Hashtable ht = new Hashtable();
            string sql = @"
                DELETE FROM [sys_sample] WHERE id = @id
            ";
            ht.Add(@"@id", new SQLParameter(id, SqlDbType.UniqueIdentifier));
            _MssqlConnect.Execute(sql, ht);
        }
    }
}