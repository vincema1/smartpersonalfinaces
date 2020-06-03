using PersonalFinances.BUSINESS.Models;
using PersonalFinances.BUSINESS.Services.Interfaces;

using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using System.Data;
using System.Linq;
using System.IO;
using PersonalFinances.DATA.POCO;

namespace PersonalFinances.BUSINESS.Services.Implementations
{
    public class RecordExtractorFromExcel : IRecordsExtractor
    {
       enum RecordField
        {
            Date,
            Description,
            Revenue,
            Expense,
            Category,
            Subcategory,
            Comment
        }

   
        public ExtractionResult ProcessSource(string pathExcelFile)
        {
            Stream file = File.OpenRead(pathExcelFile);
            var excelReader2013 = ExcelReaderFactory.CreateOpenXmlReader(file);

            List<List<DataRow>> listDataRows = new List<List<DataRow>>();


            foreach (DataTable table in excelReader2013.AsDataSet().Tables)
            {
                List<DataRow> list = table.AsEnumerable().ToList();
                listDataRows.Add(list);
            }

            excelReader2013.Close();

            return ProcessList(listDataRows);
        }
        
        private ExtractionResult ProcessList(List<List<DataRow>> inputList)
        {

            var _result = new ExtractionResult();

            _result.TotalProcessedRecords = inputList.Count();

            var list = new List<importRecordTmp>();
            foreach (var listDR in inputList)
            {
                //Skipping the header
                for (int i = 1; i < listDR.Count(); i++)
                {
                    //Checking if it is an empty line
                    if (string.IsNullOrEmpty(listDR[i][(int)RecordField.Date].ToString()))
                        break;

                    decimal expense = 0;
                    var ValidValue = decimal.TryParse(listDR[i][(int)RecordField.Expense].ToString(), out expense);

                    decimal revenue = 0;
                    if (ValidValue)
                        ValidValue = decimal.TryParse(listDR[i][(int)RecordField.Revenue].ToString(), out revenue);

                    DateTime date = new DateTime();
                    if (ValidValue)
                        ValidValue = DateTime.TryParse(listDR[i][(int)RecordField.Date].ToString(), out date);

                    var record = new importRecordTmp
                    {
                        
                        description = listDR[i][(int)RecordField.Description].ToString(),
                        category = listDR[i][(int)RecordField.Category].ToString(),
                        subcategory = listDR[i][(int)RecordField.Subcategory].ToString(),
                        expense = expense,
                        revenue = revenue,
                        comment = listDR[i][(int)RecordField.Comment].ToString(),
                        date = date,
                        importError = !ValidValue
                    };

                    list.Add(record);
                }
            }

            _result.TotalDiscardedRecord = list.Where(p => p.importError == true).Select(p => p).ToList().Count();
            _result.ValidRecords = list.Where(i => !i.importError).ToList();

            return _result;

        }

    }
}
