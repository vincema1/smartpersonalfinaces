using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.BUSINESS.Utils;
using PersonalFinances.DATA.DataModel;
using POCO = PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.Filters;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Data.Entity;

namespace PersonalFinances.BUSINESS.ViewModels
{

    public class MergeCategoriesModel
    {

        [Required]
        public int dossierId { get; set; }

        [Required]
        public string type { get; set; }

        [Required]
        public int recordCategoryId_TO { get; set; }

        public int? recordSubcategoryId_TO { get; set; }

        [Required]
        public int recordCategoryId_FROM { get; set; }

        public int? recordSubcategoryId_FROM { get; set; }

        private string _beginDate;
        public string beginDate { get {
                                        if (string.IsNullOrEmpty(_beginDate))
                                                                    _beginDate = "01/01/1900";
                                        return _beginDate;
                                      }
                                  set { _beginDate = value; }}

        private string _endDate;
        public string endDate { get{
                                    if (string.IsNullOrEmpty(_endDate))
                                        _endDate = "31/12/9999";
                                    return _endDate;
                                    }
                               set { _endDate = value; }}

        private static PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

        public static void MergeCategories(MergeCategoriesModel model)
        {
            db.sp_mergeCategories(model.dossierId,
                                  model.recordCategoryId_FROM, 
                                  model.recordCategoryId_TO);

        }

        public static void MergeSubcategories(MergeCategoriesModel model)
        {
            db.sp_mergeSubcategories(model.dossierId,
                                     model.recordSubcategoryId_FROM,
                                     model.recordSubcategoryId_TO);

        }

        public static void MoveSubcategory(MergeCategoriesModel model)
        {
            recordSubcategory subCat = db.recordSubcategories.Find(model.recordSubcategoryId_FROM);

            int dossierId = (from c in db.recordCategories
                             join s in db.recordSubcategories on c.recordCategoryId equals s.recordCategoryId
                             where s.recordSubcategoryId == model.recordSubcategoryId_FROM
                             select c.dossierId).Single();

            if (model.dossierId== dossierId)
            { 
                subCat.recordCategoryId = model.recordCategoryId_TO;
                db.Entry(subCat).State = EntityState.Modified;
                db.SaveChanges();
            }
        }

        public static void DeleteEmptyCategories(int dossierId)
        {
            db.sp_deleteEmptyCategories(dossierId);

        }
    }
}
