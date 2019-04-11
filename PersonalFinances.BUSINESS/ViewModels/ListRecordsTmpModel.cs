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

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class ListRecordsTmpModel
    {

        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();

        List<POCO.importRecordTmp> _PagedListRecords;

        private int _dossierId;
        private string _dossierName;


        private string _BeginDate;
        private string _EndDate;
        private int _ItemsPerPage;
        private int _CurrentPage;
        private int _TotalNumberOfPages;

        private bool _IsPostBack =false;

        RecordTmpModel _emptyRecordForEdit;
        public List<POCO.importRecordTmp> PagedListRecords { get { return _PagedListRecords; } }
        
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

        public int DossierId { get { return _dossierId; } }
        public string DossierName { get { return _dossierName; } }

        public int ItemsPerPage { get { return _ItemsPerPage; }  }
        public int CurrentPage { get { return _CurrentPage; } }
        public int TotalNumberOfPages { get {return _TotalNumberOfPages; } }

        public RecordTmpModel emptyRecordForEdit { get { return _emptyRecordForEdit; } }


        public bool IsPostBack { get { return _IsPostBack; } set { _IsPostBack = value; } }

       
        public ListRecordsTmpModel(int dossierId,
                                   int CurrentPage=1,
                                   int ItemsPerPage=20)
        {

            _ItemsPerPage = (ItemsPerPage==0)?20: ItemsPerPage;
            _CurrentPage = (CurrentPage == 0) ? 1 : CurrentPage;

            _dossierId = dossierId;
            _dossierName = _context.dossiers.Find(_dossierId).dossierName;

            List<POCO.importRecordTmp> _listRecordsSess;

            // Getting full list from DB or from Session
            _listRecordsSess = (from 
                                    rec in _context.sp_getImportRecordTmp(_dossierId) //_context.importRecordTmps
                                where 
                                    rec.dossierId == _dossierId
                                select 
                                    new POCO.importRecordTmp {importRecordTmpId=rec.importRecordTmpId,
                                                                dossierId=rec.dossierId,
                                                                revenue = rec.revenue,
                                                                expense = rec.expense,
                                                                comment=rec.comment,
                                                                description=rec.description,
                                                                date=rec.date,
                                                                category=rec.category,
                                                                subcategory=rec.subcategory,
                                                                duplicates= rec.duplicates,
                                                                doubles = rec.doubles
                                                                }).ToList();


        


            if (!IsPostBack)
            {
                //TODO: Implement a caching system different than Session.
                //Try Object Cache
            }

            // data for paging
            _PagedListRecords = _listRecordsSess.Skip(_ItemsPerPage * (_CurrentPage-1)).Take(_ItemsPerPage).ToList();
            _TotalNumberOfPages = (_listRecordsSess.Count() / _ItemsPerPage);
            _TotalNumberOfPages += ((_listRecordsSess.Count() % _ItemsPerPage) == 0) ? 0 : 1;

            _emptyRecordForEdit = new RecordTmpModel(_dossierId);
        }
     
    }
}