
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using backend.Models.auth_t;
using backend.Sqls.mssql;
using backend.util;
using Microsoft.Extensions.Options;

namespace backend.Middleware.jwt_t
{
    public class loginDao

    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _messqlConnect;
        public loginDao(IOptions<appSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _messqlConnect = new MssqlConnect(_appSettings.db);
        }
        #region 註冊
        public void Register(Members model)
        {
            Hashtable ht=new Hashtable();
            string sql=$@"INSERT INTO members (name,phone,email,password,authcode) VALUES (@name,@phone,@email,@password,@authcode); ";
            ht.Add(@"@name",new SQLParameter(model.name,SqlDbType.NVarChar));
            ht.Add(@"@phone",new SQLParameter(model.phone,SqlDbType.NVarChar));
            ht.Add(@"@email",new SQLParameter(model.email,SqlDbType.NVarChar));
            ht.Add(@"@password",new SQLParameter(model.password,SqlDbType.NVarChar));
            ht.Add(@"@authcode",new SQLParameter(model.authcode,SqlDbType.NChar));
            _messqlConnect.Execute(sql,ht);
        }
        #endregion
        public List<Members> GetUserList()
        {
            string sql = $@"SELECT * FROM members; ";
            List<Members> AccountResult = _messqlConnect.GetDataList<Members>(sql);

            List<Members> Result = AccountResult.ToList<Members>();
            return Result;
            
            /*string sql=string.Empty;
            sql=$@"SELECT * FROM members; ";
            List<Members> AccountResult=_messqlConnect.GetDataList<Members>(sql);

            sql=$@"SELECT * FROM [sys_account_role]; ";
            //用範例給的 accountRoleModel
            List<accountRoleModel> RoleModel=_messqlConnect.GetDataList<accountRoleModel>(sql);
            
            List<Members> Result=AccountResult.GroupJoin(RoleModel,Account => Account.email, Role => Role.account
                ,(Account,Role)=>new Members
                {
                    name=Account.name,
                    phone=Account.phone,
                    email=Account.email,
                    password=Account.password,
                    accountRole=RoleModel.Select(role => role).ToList<accountRoleModel>()
                }).ToList<Members>();
            return Result;*/
        }

        public Members GetMember(string email, string password)
        {
            var Result = new Members();
            Hashtable ht = new Hashtable();
            string sql = $@"Select * from Members where email=@email and password=@password";
            ht.Add(@"@email", new SQLParameter(email, SqlDbType.NVarChar));
            ht.Add(@"@password", new SQLParameter(password, SqlDbType.NVarChar));
            Result = _messqlConnect.GetDataList<Members>(sql, ht).FirstOrDefault();
            return Result;
        }
    }
}