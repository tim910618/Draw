using System.Collections;
using System.Collections.Generic;
using System.Data;

using Microsoft.Extensions.Options;

using backend.util;
using backend.Sqls.mssql;
using backend.Models.auth;

namespace backend.dao.auth
{
    public class RoleProcessDao
    {

        private readonly appSettings _appSettings;

        private readonly MssqlConnect _MssqlConnect;

        public RoleProcessDao(IOptions<appSettings> appSettings)
        {
            this._appSettings = appSettings.Value;
            this._MssqlConnect = new MssqlConnect(_appSettings.db);
        }
        public List<roleProcessModel> GetRoleProcessList(string role, string purl, string sysType)
        {
            string sql = @$"
                SELECT A.*,
                       C.[sys_purl]
                FROM [{sysType}].[dbo].[sys_role] AS A
                INNER JOIN [{sysType}].[dbo].[sys_role_process] AS B
                ON A.[sys_rid] = B.[sys_rid]
                INNER JOIN [{sysType}].[dbo].[sys_process] AS C
                ON B.[sys_pid] = C.[sys_pid]
                WHERE A.[sys_rid] = @role
                  AND A.[sys_enable] = 1
                  AND B.[sys_modify] = 1
                  AND C.[sys_purl] = @purl
                  AND C.[sys_enable] = 1
                  AND C.[sys_show] = 1
            ";

            Hashtable ht = new Hashtable();
            ht.Add("@role", new SQLParameter(role, SqlDbType.VarChar));
            ht.Add("@purl", new SQLParameter(purl, SqlDbType.VarChar));

            List<roleProcessModel> Result = _MssqlConnect.GetDataList<roleProcessModel>(sql, ht);

            return Result;
        }

        public List<roleProcessModel> GetRoleProcessList(string role, string sysType)
        {
            string sql = @$"
                SELECT A.*,
                       C.[sys_purl]
                FROM [{sysType}].[dbo].[sys_role] AS A
                INNER JOIN [{sysType}].[dbo].[sys_role_process] AS B
                ON A.[sys_rid] = B.[sys_rid]
                INNER JOIN [{sysType}].[dbo].[sys_process] AS C
                ON B.[sys_pid] = C.[sys_pid]
                WHERE A.[sys_rid] = @role
                  AND A.[sys_enable] = 1
                  AND B.[sys_modify] = 1
                  AND C.[sys_enable] = 1
                  AND C.[sys_show] = 1
            ";

            Hashtable ht = new Hashtable();
            ht.Add("@role", new SQLParameter(role, SqlDbType.VarChar));

            List<roleProcessModel> Result = _MssqlConnect.GetDataList<roleProcessModel>(sql, ht);

            return Result;
        }
    }
}