
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using backend.Models.auth;
using backend.Models.auth_t;
using backend.Sqls.mssql;
using backend.util;
using Microsoft.Extensions.Options;

namespace backend.Middleware.jwt_t
{
    public class Ch09Dao
    
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _messqlConnect;
        public Ch09Dao(IOptions<appSettings> appSettings)
        {
            _appSettings=appSettings.Value;
            _messqlConnect=new MssqlConnect(_appSettings.db);
        }
        #region 註冊
        public void Register(Members model)
        {
            Hashtable ht=new Hashtable();
            string sql=$@"INSERT INTO [Members] ([Account],[Password],[Name],[Email]) VALUES (@Account,@Password,@Name,@Email); ";
            ht.Add(@"@Account",new SQLParameter(model.Account,SqlDbType.VarChar));
            ht.Add(@"@Password",new SQLParameter(model.Password,SqlDbType.VarChar));
            ht.Add(@"@Name",new SQLParameter(model.Name,SqlDbType.NVarChar));
            ht.Add(@"@Email",new SQLParameter(model.Email,SqlDbType.NVarChar));
            _messqlConnect.Execute(sql,ht);
        }
        #endregion
        public List<Members> GetUserList()
        {
            string sql=string.Empty;
            sql=$@"SELECT * FROM [Members]; ";
            List<Members> AccountResult=_messqlConnect.GetDataList<Members>(sql);

            sql=$@"SELECT * FROM [sys_account_role]; ";
            //用範例給的 accountRoleModel
            List<accountRoleModel> RoleModel=_messqlConnect.GetDataList<accountRoleModel>(sql);
            
            List<Members> Result=AccountResult.GroupJoin(RoleModel,Account => Account.Account, Role => Role.account
                ,(Account,Role)=>new Members
                {
                    Account=Account.Account,
                    Password=Account.Password,
                    Name=Account.Name,
                    Email=Account.Email,
                    accountRole=RoleModel.Select(role => role).ToList<accountRoleModel>()
                }).ToList<Members>();
            return Result;
        }
    }
}