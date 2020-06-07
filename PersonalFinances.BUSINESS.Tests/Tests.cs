using PersonalFinances.BUSINESS.Services.Implementations;
using System;
using Xunit;


namespace PersonalFinances.BUSINESS.Tests
{
    public class Tests
    {

        [Fact]
        public void SimpleTest() {

            Assert.True(1 == 1);
        }

        [Fact]
        public void should_extract_records_from_excel_file()
        {
            RecordExtractorFromExcel recordExtractor = new RecordExtractorFromExcel();

            var results=recordExtractor.ProcessSource(@"E:\IT_PROJECTS_2020\PersonalFinances2020\2020.xlsx");

            Assert.True(results.TotalDiscardedRecord == 0);


        }

        [Fact]
        public void should_write_bulk_import_file()
        {
            RecordExtractorFromExcel recordExtractor = new RecordExtractorFromExcel();

            var results = recordExtractor.ProcessSource(@"E:\IT_PROJECTS_2020\PersonalFinances2020\2020.xlsx");

            var bulkImportFileCreator = new BulkImportFileCreator();
            
            bulkImportFileCreator.CreateBulkImportFile(@"E:\IT_PROJECTS_2020\PersonalFinances2020\2020.txt", results.ValidRecords);

        }

    }


    
}
