using PersonalFinances.BUSINESS.Services.Interfaces;
using PersonalFinances.BUSINESS.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.Services.Implementations
{
    public class ExcelImportProcessor : IImportProcessor
    {
        IRecordsExtractor _recordsExctractor;
        IBulkImportFileCreator _bulkImportFileCreator;
        IRecordsImporter _recordsImporter;

        public ExcelImportProcessor()
        {
            _recordsExctractor = new RecordExtractorFromExcel();
            _bulkImportFileCreator = new BulkImportFileCreator();
            _recordsImporter = new RecordImporter();
        }
        public ImportReport Process(int dossierId, string filePath, string fileName)
        {
           
            //extracts list from excel
            var extractionResults= _recordsExctractor.ProcessSource(Path.Combine(filePath, fileName));
            
            //transforms List into bulk excel input file
            string bulkImportFilePath = Path.Combine(filePath, Guid.NewGuid().ToString());
            _bulkImportFileCreator.CreateBulkImportFile(bulkImportFilePath, extractionResults.ValidRecords);

            //Executes Bulk Insert
            _recordsImporter.ImportRecordsBulkInsert(dossierId, bulkImportFilePath);

            //Fills report data 
            return new ImportReport { totalRecordsProcessed= extractionResults.TotalProcessedRecords,
                                        FailedImports= extractionResults.TotalDiscardedRecord,
                                        SuccessfulImports= extractionResults.ValidRecords.Count(),
                                        dossierId=dossierId};
        }
    }
}
