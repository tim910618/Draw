using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using System.Data;

using MySql.Data.MySqlClient;

using backend.util;

using Microsoft.Extensions.Options;
using System.Text;

namespace backend.Sqls.mysql
{
    public class DataAccess
    {
        public DataTable GetDataTable(MySqlConnection conn, string sql)
        {
            DataTable Result = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                cmd.CommandTimeout = 0;          
                MySqlDataReader Reader = cmd.ExecuteReader(CommandBehavior.Default);
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

        public DataTable GetDataTable(MySqlConnection conn, string sql, Hashtable ht)
        {
            DataTable Result = new DataTable();
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                SQLParameter parameters;
                cmd.CommandTimeout = 0;
                if (ht != null)
                {
                    foreach (object obj in ht.Keys)
                    {
                        parameters = ht[obj] as SQLParameter;
                        if (parameters.SqlDbType == MySqlDbType.Text) 
                        {
                            int l_len = Encoding.UTF8.GetByteCount(parameters.ObjValue.ToString())+3;
                            cmd.Parameters.Add(obj.ToString(), parameters.SqlDbType, l_len).Value = parameters.ObjValue;
                        }
                        else cmd.Parameters.Add(obj.ToString(), parameters.SqlDbType).Value = parameters.ObjValue;
                    }
                }
                MySqlDataReader Reader = cmd.ExecuteReader(CommandBehavior.Default);
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
        public void Execute(MySqlConnection conn, string sql)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
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
        public void Execute(MySqlConnection conn, string sql, Hashtable ht)
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                SQLParameter parameters;
                cmd.CommandTimeout = 0;
                if (ht != null)
                {
                    foreach (object obj in ht.Keys)
                    {
                        parameters = ht[obj] as SQLParameter;
                        if (parameters.SqlDbType == MySqlDbType.Text) 
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