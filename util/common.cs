using System;
using System.IO;

namespace backend.util
{
  public class common
  {
    public static string yieldDatabaseId(string prefix, string current)
    {
      int length = current.Length;
      string id = Convert.ToString(Int32.Parse(current) + 1);
      id = id.PadLeft(length, '0');
      return $"{prefix}{id}";
    }

    public static string DateFormat_full(DateTime date)
    {
      return date.ToString("yyyy/MM/dd HH:mm:ss");
    }

    public static string DateFormat_simple(DateTime date)
    {
      return date.ToString("yyyy/MM/dd");
    }

    public static string DateFormat_report(DateTime date)
    {
      return date.ToString("yyyyMMdd");
    }
    public static void Create_Directory(string path)
    {
      if(!Directory.Exists(path))
      {
          Directory.CreateDirectory(path);
      }
    }
  }
}