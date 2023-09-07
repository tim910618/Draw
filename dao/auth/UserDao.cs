using System.Collections;
using System.Collections.Generic;
using System.Data;

using Microsoft.Extensions.Options;

using backend.util;
using backend.Sqls.mssql;
using backend.Models.user;
using System.Linq;

namespace backend.dao.user
{
    public class UserDao
    {

        private readonly appSettings _appSettings;

        private readonly MssqlConnect _MssqlConnect;

        public UserDao(IOptions<appSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
            this._MssqlConnect = new MssqlConnect(_appSettings.db);
        }
        public userinfoModel GetUserInfo(string account)
        {
            userinfoModel Result = new userinfoModel();
            string sql = @$"
            SELECT * FROM sys_account WHERE account = @account
            ";
            Hashtable ht = new Hashtable();
            ht.Add("@account", new SQLParameter(account, SqlDbType.VarChar));
            userinfoModel userdata = _MssqlConnect.GetDataList<userinfoModel>(sql, ht).First();
            Result.userinfo = userdata;
            return Result;
        }
    }
}