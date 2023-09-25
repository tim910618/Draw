
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
    public class scaleDao
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _MssqlConnect;
        private readonly IHttpContextAccessor _HttpContextAccessor;
        private string _AccountNumber;
        public scaleDao(IOptions<appSettings> appSettings, IHttpContextAccessor HttpContextAccessor)
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

        #region 修改
        public void Update_stage(ScaleImportModel model,string age_stage,int log)
        {
            Hashtable ht = new Hashtable();
            StringBuilder updateBuilder = new StringBuilder("UPDATE kid SET ");
            StringBuilder fromBuilder = new StringBuilder("=@age_stage WHERE email=@email and kid_id=@kid_id");
            string sql = updateBuilder.ToString() + age_stage + fromBuilder.ToString();
            //string sql = $@"UPDATE kid SET Name=@Name,image=@image WHERE kid_id=@kid_id; ";

            ht.Add(@"@email", new SQLParameter(_AccountNumber, SqlDbType.NVarChar));
            ht.Add(@"@kid_id", new SQLParameter(Guid.Parse(model.kid_id), SqlDbType.UniqueIdentifier));
            ht.Add(@"@age_stage", new SQLParameter(log, SqlDbType.Int));
            _MssqlConnect.Execute(sql, ht);
        }
        #endregion

        #region 新增
        public void Insert(ScaleImportModel model,string age_stage)
        {
            Hashtable ht = new Hashtable();
            string date = common.DateFormat_full(DateTime.Now);
            string sql = $@"INSERT INTO scale_troubles
                        (s_id, kid_id, scale_trouble, disease, disease_other, stage, create_time) 
                        VALUES (
                            @s_id,@kid_id,@scale_trouble,@disease,@disease_other,@stage,@create_time
                        )";
            ht.Add(@"@s_id", new SQLParameter(Guid.NewGuid(), SqlDbType.UniqueIdentifier));
            ht.Add(@"@kid_id", new SQLParameter(Guid.Parse(model.kid_id), SqlDbType.UniqueIdentifier));
            ht.Add(@"@scale_trouble", new SQLParameter(model.scale_trouble, SqlDbType.NVarChar));
            ht.Add(@"@disease", new SQLParameter(model.disease, SqlDbType.NVarChar));
            ht.Add(@"@disease_other", new SQLParameter(model.disease_other, SqlDbType.NVarChar));
            ht.Add(@"@stage", new SQLParameter(age_stage, SqlDbType.NVarChar));
            ht.Add(@"@create_time", new SQLParameter(date, SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql, ht);
        }
        #endregion

        #region 查詢
        public void GetScale(GetScaleImportModel model)
        {
            
        }
        #endregion
    }
}