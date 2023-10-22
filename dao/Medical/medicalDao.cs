
using backend.Sqls.mssql;
using backend.util;
using backend.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Data;
using System.Linq;
using System;
using backend.Models.auth_t;
using backend.ViewModels;
using backend.ImportModels;

namespace backend.dao
{
    public class medicalDao
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _MssqlConnect;
        public medicalDao(IOptions<appSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
            this._MssqlConnect = new MssqlConnect(_appSettings.db);
        }
        #region 全部
        public List<Medical> GetDataList()
        {
            List<Medical> Result = new List<Medical>();
            string sql = @"SELECT * FROM medical ";
            Result = _MssqlConnect.GetDataList<Medical>(sql);
            return Result;
        }
        #endregion
        #region 單筆
        public Medical GetDataById(MedicalImportModel model)
        {
            Hashtable ht = new Hashtable();
            Medical Result = new Medical();
            string sql = $@"SELECT * FROM medical WHERE name LIKE '%{model.name}%';";
            ht.Add(@"@Id", new SQLParameter(model.name, SqlDbType.NVarChar));
            Result = _MssqlConnect.GetDataList<Medical>(sql, ht).FirstOrDefault();
            return Result;
        }
        #endregion
    }
}