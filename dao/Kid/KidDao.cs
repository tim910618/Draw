
using backend.Sqls.mssql;
using backend.util;
using backend.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Data;
using System.Linq;
using System;
using backend.ImportModels;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace backend.dao
{
    public class kidDao
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _MssqlConnect;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _AccountNumber;
        public kidDao(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor)
        {
            this._appSettings = appSettings.Value;
            this._MssqlConnect = new MssqlConnect(_appSettings.db);

            this._HttpContextAccessor = HttpContextAccessor ??
                throw new ArgumentNullException(nameof(HttpContextAccessor));

            // 解析Token
            tokenEnCode TokenEnCode = new tokenEnCode(HttpContextAccessor.HttpContext);

            // 抓取Token的PayLoad
            var PayLoad = TokenEnCode.GetPayLoad();

            // 抓取PayLoad中的account
            this._AccountNumber = PayLoad is null ? "" : PayLoad["email"].ToString();
        }

        #region 新增
        public void Insert(KidInsertImportModel model,string FileName)
        {
            Hashtable ht = new Hashtable();
            string sql=$@"INSERT INTO kid(email,kid_id,name,birth,gender,image) VALUES (@email,@kid_id,@name,@birth,@gender,@image); ";
            ht.Add(@"@email", new SQLParameter(_AccountNumber, SqlDbType.NVarChar));
            ht.Add(@"@kid_id", new SQLParameter(Guid.NewGuid(), SqlDbType.UniqueIdentifier));
            ht.Add(@"@name", new SQLParameter(model.name, SqlDbType.NVarChar));
            ht.Add(@"@birth", new SQLParameter(model.birth, SqlDbType.DateTime2));
            ht.Add(@"@gender", new SQLParameter(model.gender, SqlDbType.Bit));
            ht.Add(@"@image", new SQLParameter(FileName, SqlDbType.NVarChar));
            _MssqlConnect.Execute(sql, ht);
        }
        #endregion

        #region 自己的小孩
        public List<Kids> GetDataList()
        {
            List<Kids> Result = new List<Kids>();
            Hashtable ht = new Hashtable();
            string sql = @"SELECT * FROM kid WHERE email=@email ";
            ht.Add(@$"@email", new SQLParameter(_AccountNumber, SqlDbType.NVarChar));
            Result = _MssqlConnect.GetDataList<Kids>(sql, ht);
            return Result;
        }
        #endregion

        #region 單筆
        public Kids GetDataByKid_Id(string kid_id)
        {
            Hashtable ht = new Hashtable();
            Kids Result = new Kids();
            string sql = @"SELECT * FROM kid WHERE email=@email and kid_id=@kid_id";
            ht.Add(@"@email", new SQLParameter(_AccountNumber, SqlDbType.NVarChar));
            ht.Add(@"@kid_id", new SQLParameter(Guid.Parse(kid_id), SqlDbType.UniqueIdentifier));
            Result = _MssqlConnect.GetDataList<Kids>(sql, ht).FirstOrDefault();
            return Result;
        }
        #endregion

        #region 修改
        public void Update(KidEditModel UpdateData, string FileName)
        {
            Hashtable ht = new Hashtable();
            string sql = $@"UPDATE kid SET name=@name,image=@image WHERE kid_id=@kid_id; ";
            ht.Add(@"@name", new SQLParameter(UpdateData.name, SqlDbType.NVarChar));
            ht.Add(@"@image", new SQLParameter(FileName, SqlDbType.NVarChar));
            ht.Add(@"@kid_id", new SQLParameter(Guid.Parse(UpdateData.kid_id), SqlDbType.UniqueIdentifier));
            _MssqlConnect.Execute(sql, ht);
        }
        #endregion
    }
}