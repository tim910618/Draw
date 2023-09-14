
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

namespace backend.dao
{
    public class PaintingDao
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _MssqlConnect;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _AccountNumber;
        public PaintingDao(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor)
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
        public void Insert(PaintingInsertImportModel model, string path)
        {
            Hashtable ht = new Hashtable();
            string date = common.DateFormat_full(DateTime.Now);

            string sql = $@"INSERT INTO painting(painting_id,kid_id,picture,result,create_time)
                VALUES (NEWID(),@kid_id,@picture,@result,@create_time)";

            ht.Add(@"@kid_id", new SQLParameter(model.kid_id, SqlDbType.UniqueIdentifier));
            ht.Add(@"@picture", new SQLParameter(path, SqlDbType.NVarChar));
            ht.Add(@"@result", new SQLParameter(model.result, SqlDbType.NVarChar));
            ht.Add(@"@create_time", new SQLParameter(model.create_time, SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql,ht);
        }
        #endregion
    }
}