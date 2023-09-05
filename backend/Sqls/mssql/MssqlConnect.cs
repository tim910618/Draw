using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using backend.util;

using Microsoft.Extensions.Options;

using backend.Extensions;

namespace backend.Sqls.mssql
{
  public class MssqlConnect
  {
    private readonly string _cnstr;
    private readonly SqlConnection _conn;
    //沒有SQL的Controller
    private readonly DataAccess _access;

    public MssqlConnect(string cnstr)
    {
      _cnstr = cnstr;
      _conn = new SqlConnection(_cnstr);
      _access = new DataAccess();
    }

    public DataTable GetDataTable(string sql)
    {
      DataTable Result = _access.GetDataTable(_conn, sql);
      return Result;
    }
    
    public DataTable GetDataTable(string sql, Hashtable ht)
    {
      DataTable Result = _access.GetDataTable(_conn, sql, ht);
      return Result;
    }

    //Hashtable：是一種基本的"集合"類型，用於存儲鍵值對最後，使用foreach循環遍歷Hashtable並打印所有的值

    //SELECT使用，用來查詢
    //看不懂
    public List<T> GetDataList<T>(string sql) where T : new()
    {
      return (List<T>)DataTableExtension.ToList<T>(GetDataTable(sql));
    }
    public List<T> GetDataList<T>(string sql, Hashtable ht) where T : new()
    {
      return (List<T>)DataTableExtension.ToList<T>(GetDataTable(sql, ht));
    }
    //增刪查改使用，不會有回傳值。
    public void Execute(string sql, Hashtable ht)
    {
      _access.Execute(_conn, sql, ht);
    }
  }
}