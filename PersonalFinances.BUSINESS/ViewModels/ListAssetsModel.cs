using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.Filters;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.EntityClient;
using PersonalFinances.DATA;
using System.Runtime.Caching;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class ListAssetsModel
    {

        #region private variables
        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        List<POCO.asset> _listAssets;
       
        int _dossierId;

        BalanceSheetTab _ReportAssets;
        BalanceSheetTab _ReportLiabilities;

        public BalanceSheetTab ReportAssets { get { return _ReportAssets; } }
        public BalanceSheetTab ReportLiabilities { get { return _ReportLiabilities; } }


        //IncomeStatementTab _ReportRevenues;

        public int DossierId { get { return _dossierId; } }


        #endregion

        #region public properties


        List<POCO.assetCategory> _Categories;
        List<POCO.assetSubcategory> _Subcategories;

        public List<POCO.assetCategory> Categories { get { return _Categories; } }
        public List<POCO.assetSubcategory> Subcategories { get { return _Subcategories; } }
        public List<POCO.asset> ListAssets { get { return _listAssets; } }


        #endregion


        public ListAssetsModel(int dossierId, bool CategoriesOnly)
        {

            _dossierId = dossierId;

            if (!CategoriesOnly)
            _listAssets =
                            (from ass in _context.assets
                             join subcat in _context.assetSubcategories on ass.assetSubcategoryId equals subcat.assetSubcategoryId
                             join cat in _context.assetCategories on subcat.assetCategoryId equals cat.assetCategoryId
                             where ass.dossierId == _dossierId
                                                     
                             orderby ass.receivable descending
                             select new POCO.asset
                             {
                                 assetId = ass.assetId,
                                 dossierId = ass.dossierId,
                                 receivable = ass.receivable,
                                 payable = ass.payable,
                                 externalId = ass.externalId,
                                 description = ass.description,
                                
                                 assetSubcategoryId = ass.assetSubcategoryId,
                                 assetSubcategoryDes= subcat.description,
                                 assetCategoryDes = cat.description

                             }).ToList();



            //TODO: Implement a caching system different than Session.
            //Try Object Cache
            _Categories = GetAssetCategories(dossierId);


            _Subcategories = GetAssetSubcategories(dossierId);

            ExtractBalanceSheetTabs();

        }




        private List<POCO.assetSubcategory> GetAssetSubcategories(int dossierId)
        {

            //List<POCO.assetSubcategory> list = new List<POCO.assetSubcategory>();

            return (from aSBC in _context.assetSubcategoryViews
                    where aSBC.dossierId == dossierId
                    select new POCO.assetSubcategory
                    {
                        dossierId = aSBC.dossierId,
                        assetCategoryId = aSBC.assetCategoryId,
                        assetSubcategoryId = aSBC.assetSubcategoryId,
                        categoryDescription = aSBC.categoryDescription,
                        description = aSBC.description,
                        isAsset = aSBC.isAsset,
                        numAssets = _context.assets.Where(a => a.assetSubcategoryId == aSBC.assetSubcategoryId).Count()

                     }).ToList();


            
        }

        private List<POCO.assetCategory> GetAssetCategories(int dossierId)
        {

            return (from aC in _context.assetCategories
                    where aC.dossierId == dossierId
                    select new POCO.assetCategory
                    {
                        dossierId = aC.dossierId,
                        assetCategoryId = aC.assetCategoryId,
                        description = aC.description,
                        isAsset=aC.isAsset,
                        numSubcategories = _context.assetSubcategories.Where(a => a.assetCategoryId ==aC.assetCategoryId).Count()

                    }).ToList();
        }

        private void ExtractBalanceSheetTabs()
        {
            _ReportAssets = new BalanceSheetTab(_dossierId,  true);
            _ReportLiabilities = new BalanceSheetTab(_dossierId, false);
        }

    }
}