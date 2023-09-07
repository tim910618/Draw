using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;

using Microsoft.Extensions.Configuration;

using backend.dao;
using backend.util;
using backend.Sqls.mssql;
using Microsoft.Extensions.Options;
using backend.Models.auth;

namespace backend.Middleware.jwt
{
    public class JWTDao
    {

        private readonly appSettings _appSettings;
        //SQL連線
        private readonly MssqlConnect _MssqlConnect;

        //IOptions 用來選 appSettings 的東西
        public JWTDao(IOptions<appSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _MssqlConnect = new MssqlConnect(_appSettings.db);
        }
        public List<JWTUserModel> GetUserList()
        {
            string sql = string.Empty;
            sql = @$"
                SELECT *, '範例' AS [sys_type]  
                FROM [sys_account]
            ";//新增加一個欄位'範例'
            List<JWTUserModel> AccountResult = _MssqlConnect.GetDataList<JWTUserModel>(sql);

            sql = @$"
                SELECT *, '範例' AS [sys_type]
                FROM [sys_account_role]
            ";
            List<accountRoleModel> RoleResult = _MssqlConnect.GetDataList<accountRoleModel>(sql);

            // GroupJoin 群組加入
            //看不懂
                                        //( e1 ) . GroupJoin( e2 , 左側集合的聯結條件 , 右側集合的聯結條件 , 聯結後的結果 )
            List<JWTUserModel> Result = AccountResult.GroupJoin(RoleResult,Account => Account.account,Role => Role.account,
                                            (Account, Role) =>
                                            new JWTUserModel
                                            {
                                            account = Account.account,
                                            password = Account.password,
                                            name = Account.name,
                                            email = Account.email,
                                            status = Account.status,
                                            createid = Account.createid,
                                            createat = Account.createat,
                                            updateid = Account.updateid,
                                            updateat = Account.updateat,
                                            sys_type = Account.sys_type,
                                            //每個元素弄一個新表單
                                            //Select 方法遍歷集合中的每個元素，並根據指定的轉換邏輯將每個元素轉換成新的值。
                                            accountRole = Role.Select(role => role).ToList<accountRoleModel>()  //看不懂
                                            }).ToList();

            return Result;
        }
    }
}