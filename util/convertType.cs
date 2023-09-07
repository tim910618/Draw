using System;
using System.Drawing;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace backend.util
{
  public class convertType
  {
    public static void Base64ToSave(string base64, string filePath)
    {
      File.WriteAllBytes($@"./{filePath}", Convert.FromBase64String(base64));
    }
    public static string iFormFileToBase64(IFormFile file)
    {
      string Result = string.Empty;
      if (file.Length > 0)
      {
        using (var ms = new MemoryStream())
        {
          file.CopyTo(ms);
          var fileBytes = ms.ToArray();
          Result = Convert.ToBase64String(fileBytes);
        }
      }
      return Result;
    }

  }
}