using PersonalFinances.BUSINESS.Models;
using PersonalFinances.BUSINESS.Services.Interfaces;
using PersonalFinances.DATA.DataModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalFinances.BUSINESS.Services.Implementations
{
    public class RecordsImporter : IRecordsImporter
    {

        //TO_DO: inject dependency when set up
        private PersonalFinancesDBEntities _dbContext; 

        public RecordsImporter(PersonalFinancesDBEntities dbContext)
        {
            _dbContext = dbContext;
        }

        public ImportResult ImportRecords(int dossierId,IEnumerable<DATA.POCO.importRecordTmp> Records)
        {
            var _importResult = new ImportResult();
            var _validRecords = ValidateInputList(Records);
            
            
            return _importResult;
        }


        private IEnumerable<DATA.POCO.importRecordTmp> ValidateInputList(IEnumerable<DATA.POCO.importRecordTmp> ValidRecords)
        {
            return ValidRecords;
        }
               

        public int ImportRecordsBulkInsert(int dossierId, string bulkFilePath)
        {
            var _ret=_dbContext.ImportRecords_BulkInsert(dossierId, bulkFilePath);
            File.Delete(bulkFilePath);

            return _ret;
        }
    }
}
