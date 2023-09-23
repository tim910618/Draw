using System;
using System.IO;

namespace backend.util
{
  //分辨階段
  public class Distinguish_age
  {
    //分辨階段
    public static string Distinguish(string birth)
    {
      DateTime kidage = DateTime.Parse(birth);

      DateTime currentDate = DateTime.Now;
      int years = currentDate.Year - kidage.Year;
      int months = currentDate.Month - kidage.Month;
      if (currentDate.Day < kidage.Day)
      {
        months--;
      }
      if (months < 0)
      {
        years--;
        months += 12;
      }
      int age = years * 100 + months;

      if (age < 0006) return "age_0004";
      else if (age < 0009) return "age_0006";
      else if (age < 0100) return "age_0009";
      else if (age < 0103) return "age_0100";
      else if (age < 0106) return "age_0103";
      else if (age < 0200) return "age_0106";
      else if (age < 0206) return "age_0200";
      else if (age < 0300) return "age_0206";
      else if (age < 0306) return "age_0300";
      else if (age < 0400) return "age_0306";
      else if (age < 0500) return "age_0400";
      else if (age < 0600) return "age_0500";
      else return "age_0600";
    }
  }
}