//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using PersonalFinances.DATA.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PersonalFinances.DATA.POCO
{
    public partial class assetSubcategory
    {
        public int dossierId { get; set; }
        public int assetSubcategoryId { get; set; }
        public int assetCategoryId { get; set; }
        public string categoryDescription { get; set; }
        public string description { get; set; }
        public int numAssets { get; set; }


        public bool isAsset { get; set; }
        public List<POCO.assetCategory> ListCategories { get; set; }
        
        public static void AddSubCategory(assetSubcategory subcat)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

            var catEntity = new PersonalFinances.DATA.DataModel.assetSubcategory
            {
                assetCategoryId = subcat.assetCategoryId,
                description = subcat.description
            };

            db.assetSubcategories.Add(catEntity);
            db.SaveChanges();
        }

        public static assetSubcategory GetSubcategory(int assetSubcategoryId)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

            var aC = db.assetSubcategoryViews.Find(assetSubcategoryId);

            return new POCO.assetSubcategory
            {
                dossierId = aC.dossierId,
                assetCategoryId = aC.assetCategoryId,
                assetSubcategoryId=aC.assetSubcategoryId,
                description = aC.description,
                isAsset = aC.isAsset,
                categoryDescription=aC.categoryDescription,
      
            };

        }

        public static void UpdateSubcategory(POCO.assetSubcategory assetSubcategory)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

            var aC = db.assetSubcategories.Find(assetSubcategory.assetSubcategoryId);

            aC.description = assetSubcategory.description;
            aC.assetCategoryId = assetSubcategory.assetCategoryId;

            db.Entry(aC).State = EntityState.Modified;
            db.SaveChanges();

        }


        public static void DeleteSubcategory(int dossierId, int assetSubcategoryId)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

           // var aSC = db.assetSubcategories.Find(assetSubcategoryId);

            var aSC = (from sc in db.assetSubcategories
                       join c in db.assetCategories on sc.assetCategoryId equals c.assetCategoryId
                       where sc.assetSubcategoryId == assetSubcategoryId
                                           && c.dossierId == dossierId
                       select sc).Single();

            db.assetSubcategories.Remove(aSC);
            db.SaveChanges();

        }

    }
}
