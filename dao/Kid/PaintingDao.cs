
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
    public class paintingDao
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _MssqlConnect;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _AccountNumber;
        public paintingDao(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor)
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
        public void Insert(Guid painting_id,PaintingInsertImportModel model, string FileName, string result)
        {
            Hashtable ht = new Hashtable();
            string date = common.DateFormat_full(DateTime.Now);

            string sql = $@"INSERT INTO painting(painting_id,kid_id,picture,result,create_time)
                VALUES (@painting_id,@kid_id,@picture,@result,@create_time)";

            ht.Add(@"@painting_id", new SQLParameter(painting_id, SqlDbType.UniqueIdentifier));
            ht.Add(@"@kid_id", new SQLParameter(Guid.Parse(model.kid_id), SqlDbType.UniqueIdentifier));
            ht.Add(@"@picture", new SQLParameter(FileName, SqlDbType.NVarChar));
            ht.Add(@"@result", new SQLParameter(result, SqlDbType.NVarChar));
            ht.Add(@"@create_time", new SQLParameter(date, SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql, ht);
        }
        #endregion
        #region 搜尋
        public Kids GetDataById(string kid_id)
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
        #region 檢查
        public Kids kid_check(string kid_id, string age_stage)
        {
            Hashtable ht = new Hashtable();
            Kids Result = new Kids();
            StringBuilder selectBuilder = new StringBuilder("SELECT ");
            StringBuilder fromBuilder = new StringBuilder(" FROM kid WHERE email=@email and kid_id=@kid_id and ");
            string sql = selectBuilder.ToString() + age_stage + fromBuilder.ToString() + age_stage + " IN (1, 2)";

            //string sql = @"SELECT * FROM kid WHERE email=@email and kid_id=@kid_id";
            ht.Add(@"@email", new SQLParameter(_AccountNumber, SqlDbType.NVarChar));
            ht.Add(@"@kid_id", new SQLParameter(Guid.Parse(kid_id), SqlDbType.UniqueIdentifier));
            Result = _MssqlConnect.GetDataList<Kids>(sql, ht).FirstOrDefault();
            return Result;
        }
        #endregion
        #region 歷史紀錄
        public List<Painting> History(PaintingHistoryImportModel model)
        {
            List<Painting> Result = new List<Painting>();
            Hashtable ht = new Hashtable();
            string sql = @"SELECT * FROM painting WHERE kid_id=@kid_id ";
            ht.Add(@$"@kid_id", new SQLParameter(Guid.Parse(model.kid_id), SqlDbType.UniqueIdentifier));
            Result = _MssqlConnect.GetDataList<Painting>(sql, ht);
            return Result;
        }
        #endregion
        #region 歷史紀錄單筆
        public Painting GetHistoryById(PaintingHistoryByIdImportModel model)
        {
            Painting Result = new Painting();
            Hashtable ht = new Hashtable();
            string sql = @"SELECT * FROM painting WHERE painting_id=@painting_id ";
            ht.Add(@$"@painting_id", new SQLParameter(Guid.Parse(model.painting_id), SqlDbType.UniqueIdentifier));
            Result = _MssqlConnect.GetDataList<Painting>(sql, ht).FirstOrDefault();
            return Result;
        }
        #endregion
    }
}