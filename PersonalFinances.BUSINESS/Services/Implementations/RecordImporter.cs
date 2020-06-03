using PersonalFinances.BUSINESS.Models;
using PersonalFinances.BUSINESS.Services.Interfaces;
using PersonalFinances.DATA.DataModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalFinances.BUSINESS.Services.Implementations
{
    public class RecordImporter : IRecordsImporter
    {
        private bool _bulkInsert = true;
        private string _ValidatedFilePath = "";
        private IEnumerable<DATA.POCO.importRecordTmp> _validRecords ;
        private int _dossierId;

        //TO_DO: inject dependency when set up
        private PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();


        public RecordImporter(bool BulkInsert)
        {
            _bulkInsert = BulkInsert;
        }
        public ImportResult ImportRecords(int dossierId,IEnumerable<DATA.POCO.importRecordTmp> Records)
        {
            var _importResult = new ImportResult();
            _validRecords = ValidateInputList(Records);

            if (_bulkInsert)
            {
                CreateBulkImportFile(_ValidatedFilePath, _validRecords);
                ImportBulkInsert();
            }
            return _importResult;
        }

        //TO_DO: move to DATA when DI is setup
        private void ImportBulkInsert()
        {
            _context.ImportRecords_BulkInsert(_dossierId, _ValidatedFilePath);
            File.Delete(_ValidatedFilePath);

        }

        private IEnumerable<DATA.POCO.importRecordTmp> ValidateInputList(IEnumerable<DATA.POCO.importRecordTmp> ValidRecords)
        {
            return ValidRecords;
        }

        private void CreateBulkImportFile(string path, IEnumerable<DATA.POCO.importRecordTmp> listRecords)
        {

            using (TextWriter writeFile = new StreamWriter(path, false, Encoding.Unicode)) { 


                StringBuilder sb = new StringBuilder();

                // 01/08/2013§saponetta x 2§0§2,92§casa§cosmetici§
                listRecords.ToList().ForEach(r => {
                    writeFile.WriteLine($"{r.date}§{r.description}§{r.revenue}§{r.expense}§{r.category}§{r.subcategory}§{r.comment}");
                });
      
                writeFile.Flush();
                writeFile.Close();
                //writeFile = null;
            }
        }

    }
}
