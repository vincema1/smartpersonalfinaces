using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Diagnostics;
using System.Data.Entity.Validation;
using System.Text;

namespace PersonalFinances.BUSINESS.Utils
{ 
    public static class Utils
    {
        public enum KEY { ERROR, ROLE }
        public static void WriteLine(string Message, string Key = "ERROR")
        {
            Debug.WriteLine("{0} : {1}", Key, Message);
        }
        
        public static void WriteLine(string Message, KEY Key = KEY.ERROR)
        {
            Debug.WriteLine("{0} : {1}", Key, Message);
        }

        public static void DisplayErrorDB(DbEntityValidationException ex)
        {
            foreach (DbEntityValidationResult validationResult in ex.EntityValidationErrors)
            {
                string entityName = validationResult.Entry.Entity.GetType().Name;
                foreach (DbValidationError error in validationResult.ValidationErrors)
                {
                    StringBuilder sb = new StringBuilder();
                    sb.Append(entityName).Append(".").Append(error.PropertyName).Append("\n");
                    sb.Append(error.ErrorMessage);

                    Utils.WriteLine(sb.ToString(), "DB_ERROR");
                    //System.Diagnostics.Debug.WriteLine(entityName + "." + error.PropertyName + ": " + error.ErrorMessage);
                }
            }
        }


        public static string FormatDate(DateTime date)
        {
            string day = "0" + date.Day.ToString();
            string month = "0" + date.Month.ToString();

            string ret = string.Format("{0}/{1}/{2}", day.Substring(day.Length - 2, 2), month.Substring(month.Length - 2, 2), date.Year.ToString());

            return ret;
        }
    }
}