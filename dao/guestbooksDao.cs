
using backend.Sqls.mssql;
using backend.util;
using backend.Models;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Data;
using System.Linq;
using System;

namespace backend.dao
{
    public class guestbooksDao
    {
        private readonly appSettings _appSettings;
        private readonly MssqlConnect _MssqlConnect;
        public guestbooksDao(IOptions<appSettings> appSettings)
        {
            this._appSettings=appSettings.Value;
            this._MssqlConnect=new MssqlConnect(_appSettings.db);
        }
        #region 全部
        public List<Guestbooks> GetDataList()
        {
            List<Guestbooks> Result=new List<Guestbooks>();
            string sql=@"SELECT * FROM Guestbooks ";
            Result=_MssqlConnect.GetDataList<Guestbooks>(sql);
            return Result;
        }
        #endregion
        #region 單筆
        public Guestbooks GetDataById(string id)
        {
            Hashtable ht=new Hashtable();
            Guestbooks Result=new Guestbooks();
            string sql=$@"SELECT * FROM Guestbooks WHERE Id=@Id; ";
            ht.Add(@"@Id",new SQLParameter(id,SqlDbType.Int));
            Result=_MssqlConnect.GetDataList<Guestbooks>(sql,ht).FirstOrDefault();
            return Result;
        }
        #endregion
        #region 新增
        public void Insert(Guestbooks model)
        {
            Hashtable ht=new Hashtable();
            string sql=$@"INSERT INTO Guestbooks ([Name],[Content],[CreateTime]) VALUES (@Name,@Content,@CreateTime); ";
            ht.Add(@"@Name", new SQLParameter(model.Name,SqlDbType.NVarChar));
            ht.Add(@"@Content",new SQLParameter(model.Content,SqlDbType.NVarChar));
            ht.Add(@"@CreateTime",new SQLParameter(DateTime.Now,SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql,ht);
        }
        #endregion
        #region 修改
        public void Update(Guestbooks model)
        {
            Hashtable ht=new Hashtable();
            string sql=$@"UPDATE Guestbooks SET [Name]=@Name,[Content]=@Content,[CreateTime]=@CreateTime WHERE [Id]=@Id; ";
            ht.Add(@"@Id",new SQLParameter(model.Id,SqlDbType.Int));
            ht.Add(@"@Name",new SQLParameter(model.Name,SqlDbType.NVarChar));
            ht.Add(@"@Content",new SQLParameter(model.Content,SqlDbType.NVarChar));
            ht.Add(@"@CreateTime",new SQLParameter(DateTime.Now,SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql,ht);
        }
        #endregion
        #region 回覆
        public void Reply(Guestbooks model)
        {
            Hashtable ht=new Hashtable();
            string sql=$@"UPDATE Guestbooks SET [Reply]=@Reply,[ReplyTime]=@ReplyTime WHERE [Id]=@Id; ";
            ht.Add(@"@Id",new SQLParameter(model.Id,SqlDbType.Int));
            ht.Add(@"@Reply",new SQLParameter(model.Reply,SqlDbType.NVarChar));
            ht.Add(@"@ReplyTime",new SQLParameter(DateTime.Now,SqlDbType.DateTime2));
            _MssqlConnect.Execute(sql,ht);
        }
        #endregion
        #region 刪除
        public void Delete(string Id)
        {
            Hashtable ht=new Hashtable();
            string sql=$@"DELETE FROM [Guestbooks] WHERE Id=@Id; ";
            ht.Add(@"@Id",new SQLParameter(Id,SqlDbType.Int));
            _MssqlConnect.Execute(sql,ht);
        }
        #endregion
    }
}