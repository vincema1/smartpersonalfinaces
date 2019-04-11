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
    public class ListRecordsModel
    {

        #region private variables
        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        List<POCO.record> _PagedListRecords;
        List<recordCategory> _Categories;
        List<recordSubcategory> _Subcategories;
      
        int _dossierId;

        IncomeStatementTab _ReportExpenses;
        IncomeStatementTab _ReportRevenues;

        string _BeginDate;
        string _EndDate;
        int _ItemsPerPage;
        int _CurrentPage;
        int _TotalNumberOfPages;
        string _ClientSessionId;
        
        bool _IsPostBack = false;
        RecordModel _emptyRecordForEdit;
        List<POCO.record> _listRecordsSess;




        #endregion

        #region public properties

        public string ClientSessionId { get { return _ClientSessionId; } set { _ClientSessionId = value; } }

        public List<POCO.record> PagedListRecords { get { return _PagedListRecords; } }
        public List<recordCategory> Categories { get { return _Categories; } }
        public List<recordSubcategory> Subcategories { get { return _Subcategories; } }

    

        public IncomeStatementTab ReportExpenses { get { return _ReportExpenses; } }
        public IncomeStatementTab ReportRevenues { get { return _ReportRevenues; } }


        [IsValidDate(ErrorMessage = "Insert date as dd/mm/yyyy")]
        public string beginDate
        {
            get { return _BeginDate; }
            set { _BeginDate = value; }
        }

        [IsValidDate(ErrorMessage = "Insert date as dd/mm/yyyy")]
        public string endDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
        }

        public int DossierId { get { return _dossierId; } set { _dossierId = value; } }
        public int ItemsPerPage { get { return _ItemsPerPage; } set { _ItemsPerPage = value; } }
        public int CurrentPage { get { return _CurrentPage; } set { _CurrentPage = value; } }
        public int TotalNumberOfPages { get { return _TotalNumberOfPages; } }
        public RecordModel emptyRecordForEdit { get { return _emptyRecordForEdit; } }
        public bool IsPostBack { get { return _IsPostBack; } set { _IsPostBack = value; } }

        public string CashFlow { get {string ret = (_ReportRevenues.totalDec - _ReportExpenses.totalDec).ToString();
                                      ret = (ret == "0") ? "0,0000" : ret;
                                      return ret.Substring(0, ret.Length - 2); } }

        #endregion

        private void SetParamsForSearch(int dossierId,
                                       string BeginDate,
                                       string EndDate,
                                       int CurrentPage = 1,
                                       int ItemsPerPage = 20)
        {

            _BeginDate = beginDate ?? "01/01/1900";
            _EndDate = endDate ?? "31/12/9999";
            DateTime ret;

            if (_BeginDate == "01/01/1900" || _BeginDate =="")
            {
                ret = _context.records.Where(r => r.dossierId == dossierId).Select(r => r.date).DefaultIfEmpty().Min();
                if (ret.Year == 1)
                    ret = DateTime.Now;

                _BeginDate =Utils.Utils.FormatDate(ret);
            }

            if (_EndDate == "31/12/9999" || _EndDate == "")
            { 
                ret = _context.records.Where(r => r.dossierId == dossierId).Select(r => r.date).DefaultIfEmpty().Max();
                //return ret.ToString("dd /MM/yyyy");
                if (ret.Year == 1)
                    ret = DateTime.Now;

                _EndDate = Utils.Utils.FormatDate(ret);
            }


            _ItemsPerPage = (ItemsPerPage == 0) ? 20 : ItemsPerPage;
            _CurrentPage = (CurrentPage == 0) ? 1 : CurrentPage;
            _dossierId = dossierId;
        }

        public void ListRecords(int dossierId,
                                string BeginDate,
                                string EndDate,
                                int CurrentPage,
                                int ItemsPerPage)
        {



            ObjectCache cache = MemoryCache.Default;

            SetParamsForSearch(dossierId, beginDate, endDate, CurrentPage, ItemsPerPage);
            
            string[] tmp = beginDate.Split('/');
            DateTime begin = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));

            tmp = endDate.Split('/');
            DateTime end = new DateTime(int.Parse(tmp[2]), int.Parse(tmp[1]), int.Parse(tmp[0]));

            // Getting full list from DB or from Session
            if (!IsPostBack)
            {
                
                _listRecordsSess =
                            (from rec in _context.records
                             join subcat in _context.recordSubcategories on rec.recordSubcategoryId equals subcat.recordSubcategoryId
                             join cat in _context.recordCategories on subcat.recordCategoryId equals cat.recordCategoryId
                             where rec.dossierId == _dossierId
                                                     && rec.date >= begin
                                                     && rec.date <= end
                             orderby rec.date descending
                             select new POCO.record
                             {
                                 recordId = rec.recordId,
                                 dossierId = rec.dossierId,
                                 revenue = rec.revenue,
                                 expense = rec.expense,
                                 comment = rec.comment,
                                 description = rec.description,
                                 date = rec.date,
                                 recordSubcategoryId = rec.recordSubcategoryId,
                                 CategoryDescription = cat.description,
                                 SubcategoryDescription = subcat.description
                             }).ToList();

                cache.Remove(_ClientSessionId);
                cache.Add(_ClientSessionId, _listRecordsSess, DateTimeOffset.MaxValue);
            
            }
            else
            {
                _listRecordsSess = (List<POCO.record>) cache[_ClientSessionId];
                //reading object from cache

            }

            SetPagingParams();
            SetPageElems();

        }


        public void ListRecordsFullSearch(int dossierId,
                                          string ConnectionString,
                                          string BeginDate,
                                          string EndDate,
                                          int recordCategoryId,
                                          int recordSubcategoryId,
                                          string description,
                                          string comment)
        {

            ObjectCache cache = MemoryCache.Default;

            SetParamsForSearch(dossierId, beginDate, endDate, CurrentPage, ItemsPerPage);

            
            //TODO: write stored prodecure
            _listRecordsSess = null;


            AdoRepository<POCO.record> AdoRep = new AdoRepository<POCO.record>(ConnectionString);

            

            
            if (!IsPostBack)
            {
                //TODO: Implement a caching system different than Session.
                //Try Object Cache
                _listRecordsSess = AdoRep.recordFullSearch(dossierId,
                                                       beginDate,
                                                       endDate,
                                                       recordCategoryId,
                                                       recordSubcategoryId,
                                                       description,
                                                       comment);



               //var list = _context.sp_records_fullsearch(dossierId, recordSubcategoryId, recordCategoryId, beginDate, endDate, description, comment);
                                                



                cache.Remove(_ClientSessionId);

                cache.Add(_ClientSessionId, _listRecordsSess, DateTimeOffset.MaxValue);


            }
            else
            {
                
                _listRecordsSess = (List<POCO.record>)cache[_ClientSessionId];
                //reading object from cache
            }

            SetPagingParams();
            SetPageElems();

        }


        private void SetPageElems()
        {
            _Categories = _context.recordCategories.Where(ec => ec.dossierId == _dossierId).ToList();
            _Subcategories = GetListExpenseSubcategories(_dossierId);

            _ReportExpenses = new IncomeStatementTab(_dossierId, beginDate, endDate, true);
            _ReportRevenues = new IncomeStatementTab(_dossierId, beginDate, endDate, false);

            _emptyRecordForEdit = new RecordModel(_dossierId);

        }

        private void SetPagingParams()
        {
            // Extracts subset from the full list
            _PagedListRecords = _listRecordsSess.Skip(_ItemsPerPage * (_CurrentPage - 1)).Take(_ItemsPerPage).ToList();
            _TotalNumberOfPages = (_listRecordsSess.Count() / _ItemsPerPage);
            _TotalNumberOfPages += ((_listRecordsSess.Count() % _ItemsPerPage) == 0) ? 0 : 1;
        }

        
        private List<recordSubcategory> GetListExpenseSubcategories(int dossierId)
        {

            List<recordSubcategory> list = new List<recordSubcategory>();

            ObjectResult<GetrecordSubcategoriesByDossierId_Result> listTmp=_context.GetrecordSubcategoriesByDossierId(dossierId);

                //TODO: replace with a generic method with EF implememntation
            //ObjectResult<GetrecordSubcategoriesByDossierId_Result> listTmp = _context.GetrecordSubcategoriesByDossierId(dossierId);

            foreach (var item in listTmp)
            {
                recordSubcategory expsc = new recordSubcategory();
                expsc.recordCategoryId = item.recordCategoryId;
                expsc.recordSubcategoryId = item.recordSubcategoryId;
                expsc.description = item.description;

                list.Add(expsc);

            }

           return  list;
        }

        public static void DeleteSelectedRecords(int dossierId, 
                                                string beginDate ="01/01/1900", 
                                                string endDate = "31/12/9999")
        {
            PersonalFinancesDBEntities context = new PersonalFinancesDBEntities();
            context.sp_deleteRecordSelection(dossierId, beginDate, endDate);
        }

    }
}