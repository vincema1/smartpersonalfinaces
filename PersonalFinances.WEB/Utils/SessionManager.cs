using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PersonalFinances.DATA.DataModel;
using PersonalFinances.BUSINESS.ViewModels;
using System.Data.Entity.Core.Objects;

namespace PersonalFinances.WEB.Utils
{
    public static class SessionManager
    {
        private static PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        public static UserModel Userlogged
        {
            get { return HttpContext.Current.Session["userlogged"] as UserModel; }

            set { HttpContext.Current.Session["userlogged"] = value; }
        }

        public static bool IsCurrentUserLogged
        {
            get { return HttpContext.Current.Session["userlogged"] != null; }
        }

        public static DossierDetailsModel CurrentDossier
        {
            get { return HttpContext.Current.Session["CurrentDossier"] as DossierDetailsModel; }

            set { HttpContext.Current.Session["CurrentDossier"] = value; }
        }

        public static void ListExpenses(int dossierId, List<record> list)
        {
            HttpContext.Current.Session["ListExpenses" + dossierId] = list;
        }

        public static List<record> ListRecords(int dossierId)
        {
            return HttpContext.Current.Session["ListExpenses" + dossierId] as List<record>;
        }

        public static void SetListRecordCategories(int dossierId, List<recordCategory> list)
        {
            HttpContext.Current.Session["ListExpenseCategories" + dossierId] = list;
        }

        public static List<recordCategory> GetListRecordCategories(int dossierId)
        {
            return HttpContext.Current.Session["ListRecordCategories" + dossierId] as List<recordCategory>;
        }

        public static void SetListExpenseSubcategories(int dossierId)
        {

            List<recordSubcategory> list = new List<recordSubcategory>();

            //TODO: replace with a generic method with EF implememntation
            ObjectResult<GetrecordSubcategoriesByDossierId_Result> listTmp = _context.GetrecordSubcategoriesByDossierId(dossierId);

            foreach (var item in listTmp)
            {
                recordSubcategory expsc = new recordSubcategory();
                expsc.recordCategoryId = item.recordCategoryId;
                expsc.recordSubcategoryId = item.recordSubcategoryId;
                expsc.description = item.description;

                list.Add(expsc);

            }

            HttpContext.Current.Session["ListExpenseSubcategories" + dossierId] = list;
        }

        public static List<recordSubcategory> GetListExpenseSubcategories(int dossierId)
        {
            return HttpContext.Current.Session["ListExpenseSubcategories" + dossierId] as List<recordSubcategory>;
        }

        //public static List<BalanceSheetLine> SetListBalanceSheetLines(int dossierId,
        //                                                                string beginDate,
        //                                                                string endDate)
        //{
        //    List<BalanceSheetLine> list = new List<BalanceSheetLine>();


        //    //TODO: replace with a generic method with EF implememntation
        //    ObjectResult<CreateReport_BalanceSheet_Result> listTmp = _context.CreateReport_BalanceSheet(dossierId, beginDate, endDate);

        //    foreach (var item in listTmp)
        //    {
        //        BalanceSheetLine bsl = new BalanceSheetLine();
        //        bsl.bitmap = item.bitmap;
        //        bsl.category = item.category;
        //        bsl.subcategory = item.subcategory;
        //        bsl.total = item.total ?? 0;

        //        list.Add(bsl);

        //    }

        //    HttpContext.Current.Session["ListBalanceSheetLines" + dossierId] = list;

        //    return list;


        //}

        public static List<BalanceSheetLine> GetListBalanceSheetLines(int dossierId)
        {
            return HttpContext.Current.Session["ListBalanceSheetLines" + dossierId] as List<BalanceSheetLine>;
        }

     


    }
}