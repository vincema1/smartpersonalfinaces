using PersonalFinances.DATA.DataModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Caching;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class ImportModel
    {

        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();
        private static PersonalFinancesDBEntities _contextST = new PersonalFinancesDBEntities();

        private IImportSource _ImportSource;

        private List<importRecordTmp> _importRecordTmp;

        public IImportSource MyProperty { get { return _ImportSource; } }


        private string _pathFilesTmp = "";

        private int _dossierId;


        private ImportReport _Report = new ImportReport();
        public ImportReport Report { get { return _Report; } }


        public ImportModel(int dossierId, string pathFile, string pathFilesTmp)
        {
            _dossierId = dossierId;
            IImportSource importSource = new ImportFromTxtFile(pathFile, pathFilesTmp, dossierId);

            _ImportSource = importSource;
            _importRecordTmp = _ImportSource.GetRecordTmp();
            _pathFilesTmp = pathFilesTmp;


            _Report.totalRecordsProcessed = _ImportSource.TotalRecordsProcessed;
            _Report.FailedImports = _ImportSource.FailedImports;
            _Report.SuccessfulImports = _ImportSource.TotalRecordsProcessed - _ImportSource.FailedImports;
            _Report.dossierId = dossierId;
        }

        public void ImportBulkInsert()
        {
            _context.ImportRecords_BulkInsert(_dossierId, _ImportSource.ValidatedFilePath);
            File.Delete(_ImportSource.ValidatedFilePath);

        }

        public int ImportEntity(string sessionID)
        {
            ObjectCache cache = MemoryCache.Default;

            cache.Remove("totalRecordsProcessed_" + sessionID);
            cache.Add("totalRecordsProcessed_" + sessionID, _Report.totalRecordsProcessed, DateTimeOffset.MaxValue);

            int recCount = 0;

            //if (cache["RecCount_" + sessionID] != null)
            //    recCount=(int)cache["RecCount_" + sessionID];


            cache.Remove("RecCount_" + sessionID);
            foreach (var Record in _importRecordTmp)
            {
                recCount++;
                _context.importRecordTmps.Add(Record);
          //      _context.SaveChanges();
                cache.Remove("RecCount_" + sessionID);
                cache.Add("RecCount_" + sessionID, recCount, DateTimeOffset.MaxValue);

            }

            return  _context.SaveChanges();
        }


        public async Task<int> ImportEntityAsync(string sessionID)
        {
            ObjectCache cache = MemoryCache.Default;

            cache.Remove("totalRecordsProcessed_" + sessionID);
            cache.Add("totalRecordsProcessed_" + sessionID, _Report.totalRecordsProcessed, DateTimeOffset.MaxValue);

            int recCount =0 ;

            //if (cache["RecCount_" + sessionID] != null)
            //    recCount=(int)cache["RecCount_" + sessionID];


            cache.Remove("RecCount_" + sessionID);
            foreach (var Record in _importRecordTmp)
            {
                recCount++;
                _context.importRecordTmps.Add(Record);
                await _context.SaveChangesAsync();
                cache.Remove("RecCount_" + sessionID);
                cache.Add("RecCount_" + sessionID, recCount, DateTimeOffset.MaxValue);

            }

            return await _context.SaveChangesAsync();
        }

        public void ImportEntity()
        {
            foreach (var Record in _importRecordTmp)
            {
                _context.importRecordTmps.Add(Record);
            }

            _context.SaveChangesAsync();
        }

        public static void AddToDossier(int dossierId)
        {
            //Creates missing categories and sub categories (Expenses)
            _contextST.CreateCategoriesSubcategoriesFromImport(dossierId, true);
            _contextST.CreateCategoriesSubcategoriesFromImport(dossierId, false);

            //Imports into records from temporary records (Revenues)
            _contextST.CreateRecordsFromImport(dossierId, true);
            _contextST.CreateRecordsFromImport(dossierId, false);

            //empty table Record TMP
            _contextST.DeleteFromRecordTmp(dossierId);
        }

        public static void DeleteFromRecordTmp(int dossierId)
        {
            //empties table Record TMP
            _contextST.DeleteFromRecordTmp(dossierId);
        }
        public static List<int> PollOnImport(string sessionID)
        {
            var list = new List<int>();

            ObjectCache cache = MemoryCache.Default;
            int totalRecordsProcessed = 0;
            if (cache["totalRecordsProcessed_" + sessionID]!=null)
                totalRecordsProcessed = (int)cache["totalRecordsProcessed_" + sessionID];

            int recCount = 0;
            if (cache["RecCount_" + sessionID] != null)
                 recCount = (int)cache["RecCount_" + sessionID];

            list.Add(recCount);
            list.Add(totalRecordsProcessed);

            return list;
        }
    }
}