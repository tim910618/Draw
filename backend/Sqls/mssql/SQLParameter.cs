using System;
using System.Data;



namespace backend.Sqls.mssql
{
  //序列化是將對象的狀態轉換為可以持久或傳輸的形式的過程。序列化的補充是反序列化，它將流轉換為對象。這些過程共同實現數據的存儲和傳輸。
  //2進位
  [Serializable]
  public class SQLParameter
  {
    public SQLParameter(object ObjValue, SqlDbType SqlDbType)
    {
      this.ObjValue = ObjValue;
      this.SqlDbType = SqlDbType;
    }

    object _objValue;

    public object ObjValue
    {
      get { return _objValue; }
      set
      {
        if ((value == null))
          _objValue = DBNull.Value;
        else _objValue = value;
      }
    }

    SqlDbType _SqlDbType;

    public SqlDbType SqlDbType
    {
      get { return _SqlDbType; }
      set { _SqlDbType = value; }
    }
  }
}