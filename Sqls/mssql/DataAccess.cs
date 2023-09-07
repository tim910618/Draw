using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

using backend.util;

using Microsoft.Extensions.Options;
using System.Text;

namespace backend.Sqls.mssql
{
    public class DataAccess
    {
        public DataTable GetDataTable(SqlConnection conn, string sql)
        {
            DataTable Result = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                //獲取或終止，產生錯誤之前的等待時間。預設30s
                cmd.CommandTimeout = 0;          
                //提供查詢結果及其對數據庫影響的描述。
                SqlDataReader Reader = cmd.ExecuteReader(CommandBehavior.Default);
                //用 dr 的值填回 DataTabl ，如果已包含將現有數據合併。
                Result.Load(Reader);
                Reader.Close();
                return Result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }

        public DataTable GetDataTable(SqlConnection conn, string sql, Hashtable ht)
        {
            DataTable Result = new DataTable();
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SQLParameter parameters;
                cmd.CommandTimeout = 0;
                if (ht != null)
                {
                    //看不懂
                    foreach (object obj in ht.Keys)
                    {
                        parameters = ht[obj] as SQLParameter;
                        if (parameters.SqlDbType == SqlDbType.NVarChar) 
                        {
                            int l_len = Encoding.UTF8.GetByteCount(parameters.ObjValue.ToString())+3;
                            cmd.Parameters.Add(obj.ToString(), parameters.SqlDbType, l_len).Value = parameters.ObjValue;
                        }
                        else cmd.Parameters.Add(obj.ToString(), parameters.SqlDbType).Value = parameters.ObjValue;
                    }
                }
                SqlDataReader Reader = cmd.ExecuteReader(CommandBehavior.Default);
                Result.Load(Reader);
                Reader.Close();
                return Result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void Execute(SqlConnection conn, string sql)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.CommandTimeout = 0;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
        public void Execute(SqlConnection conn, string sql, Hashtable ht)
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SQLParameter parameters;
                cmd.CommandTimeout = 0;
                if (ht != null)
                {
                    foreach (object obj in ht.Keys)
                    {
                        parameters = ht[obj] as SQLParameter;
                        if (parameters.SqlDbType == SqlDbType.NVarChar) 
                        {
                            int l_len = Encoding.UTF8.GetByteCount(parameters.ObjValue.ToString())+3;
                            cmd.Parameters.Add(obj.ToString(), parameters.SqlDbType, l_len).Value = parameters.ObjValue;
                        }
                        else cmd.Parameters.Add(obj.ToString(), parameters.SqlDbType).Value = parameters.ObjValue;
                    }
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                conn.Close();
            }
        }
    }
}