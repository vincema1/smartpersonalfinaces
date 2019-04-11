using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using PersonalFinances.DATA.DataModel;


namespace PersonalFinances.DATA.Utils
{
    public class StoreProcedures
    {
            

        public static int GetMaxYearDossier(int dossierId)
        {
            int? max = 0;
            using (PersonalFinancesDBEntities context = new PersonalFinancesDBEntities()) 
            {
                IEnumerable<int> res = context.Database.SqlQuery<int>(String.Format("SELECT [dbo].[GetMaxYearDossier]({0})", dossierId));
                max = res.ElementAt(0);
            }
           
            /*if you want use executeScalar th SP should have select and not RETURN*/

           //using (SqlConnection con = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["PersonalFinancesDB"].ConnectionString))
           //{
           //    con.Open();   
           //    //SqlCommand cmd = new SqlCommand("GetMaxYearDossier", con);
           //    SqlCommand cmd = new SqlCommand("GetMaxYearDossier", con);
               
           //    cmd.Parameters.Add("@dossierId", SqlDbType.Int);
           //    cmd.Parameters["@dossierId"].Value = dossierId;
           //    cmd.CommandType = CommandType.StoredProcedure;

           //    var cesso = cmd.ExecuteScalar();
           //    max = (cesso == null) ? 0 : (int)cesso;              

           //}

           return max ?? 0;
        }

        public static int GetMinYearDossier(int dossierId)
        {
            int? min = 0;
            using (PersonalFinancesDBEntities context = new PersonalFinancesDBEntities())
            {
                IEnumerable<int> res = context.Database.SqlQuery<int>(String.Format("SELECT [dbo].[GetMinYearDossier]({0})", dossierId));
                min = res.ElementAt(0);
            }

            return min ?? 0;
        }

        public static void CreateCategoriesFromImport(int dossierId, bool isExpense)
        {
            using (PersonalFinancesDBEntities context = new PersonalFinancesDBEntities())
            {
                context.CreateCategoriesSubcategoriesFromImport(dossierId,isExpense);
            }
        }

        public static void CreateRecordsFromImport(int dossierId, bool isExpense)
        {
            using (PersonalFinancesDBEntities context = new PersonalFinancesDBEntities())
            {
                context.CreateRecordsFromImport(dossierId, isExpense);
            }
        }


        public static void DeleteDossier(int dossierId)
        {
            using (PersonalFinancesDBEntities context = new PersonalFinancesDBEntities())
            {
                context.DeleteDossier(dossierId);
            }
        }

    }
}