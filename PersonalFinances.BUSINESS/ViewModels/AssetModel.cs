using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.BUSINESS.Utils;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.Filters;
using System.Data.Entity.Core.Objects;
using System.Reflection;
using System.Data.Entity;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class AssetModel
    {
        public int assetId { get; set; }
        public int dossierId { get;  set; }
        public int assetSubcategoryId { get; set; }
        public string subcategory { get; set; }
        public string category { get; set; }

        public bool isAsset { get; set; }



        [IsValidCurrency(ErrorMessage = "Insert number as 0.000,00")]
        public string receivable { get; set; }

        [IsValidCurrency(ErrorMessage = "Insert number as 0.000,00")]
        public string payable { get; set; }

        public string description { get; set; }
       
        public List<POCO.assetSubcategory> subcategories { get; set; }

        private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
       
        
        public AssetModel() { }

        public int Create()
        {

            asset assetToCreate = new asset();
            assetToCreate= UpdateFields(assetToCreate);
            
            db.assets.Add(assetToCreate);
            db.SaveChanges();
            
            return assetToCreate.assetId;

        }


        private asset UpdateFields(asset assetToUpdate)
        {


            assetToUpdate.dossierId = this.dossierId;
            assetToUpdate.payable = decimal.Parse(this.payable);
            assetToUpdate.receivable = decimal.Parse(this.receivable);

            bool isAsset = (assetToUpdate.receivable > assetToUpdate.payable);
            assetToUpdate.assetSubcategoryId = this.assetSubcategoryId;
            assetToUpdate.description = (this.description == null) ? "" : this.description;
           
            return assetToUpdate;

        }


        public int Update()
        {

            asset assetToUpdate=db.assets.Find(this.assetId);

            if (assetToUpdate == null)
                return 0;
            
            UpdateFields(assetToUpdate);
            db.Entry(assetToUpdate).State = EntityState.Modified;

            db.SaveChanges();

            return 1;

        }
     
        public static AssetModel GetAssetModel(int dossierId, int assetId = 0)
        {

            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

            var asset = new AssetModel();

            asset.assetId = assetId;
            asset.subcategories = GetCategories(dossierId);





            if (assetId != 0)
            {
                //existing record
                asset assetDTO = db.assets.Find(assetId);
                asset.dossierId = dossierId;
                asset.receivable = decimal.Round(assetDTO.receivable, 2, MidpointRounding.AwayFromZero).ToString(); ;
                asset.payable = decimal.Round(assetDTO.payable, 2, MidpointRounding.AwayFromZero).ToString(); ;

                asset.isAsset = (assetDTO.receivable >= assetDTO.payable);
                asset.description = assetDTO.description;
                asset.assetSubcategoryId = assetDTO.assetSubcategoryId;
         
            }
            else
            {
                //new record
                asset.dossierId = dossierId;
                asset.receivable = "0,00";
                asset.payable = "0,00";
                asset.description = "";
                asset.assetSubcategoryId = 0;

            }

            return asset;
        }
      
        public static List<POCO.assetSubcategory> GetCategories(int dossierId)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

            List<POCO.assetSubcategory> subcategories = (from sc in db.assetSubcategoryViews
                                                        where sc.dossierId == dossierId
                                                        select new POCO.assetSubcategory
                                                        {
                                                            assetSubcategoryId=sc.assetSubcategoryId,
                                                            description= sc.description,
                                                            dossierId = sc.dossierId,
                                                            isAsset = sc.isAsset,
                                                            categoryDescription = sc.categoryDescription,
                                                            assetCategoryId = sc.assetCategoryId
                                                        }).ToList();

            //this.categories = FromListEntityToListPOCO<POCO.recordCategory, recordCategory>(categories);
            return subcategories.OrderBy(s=>s.categoryDescription).ToList();
        }
        public static void DeleteAsset(int dossierId, int assetId)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
            var asset = db.assets.Where(a => a.dossierId == dossierId && a.assetId == assetId).Single();
            db.assets.Remove(asset);
            db.SaveChanges();

        }



            //TODO: move somewhere else
            #region POCO generic methods   


            #endregion
        }

}
