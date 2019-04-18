using PersonalFinances.BUSINESS.Models;
using System;
using System.Collections.Generic;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ExcelDataReader;
using System.Data;
using System.Linq;
using System.IO;

namespace PersonalFinances.BUSINESS.Utils
{
    public class ImportRecordsExtractorFromExcel : IImportRecordsExtractor
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

        public List<ImportRecord> ExtractImportRecordsFromFile(string path) => ExtractImportRecords(path);
       
        private List<ImportRecord> ExtractImportRecords(string pathExcelFile)
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
        private List<ImportRecord> ProcessList(List<List<DataRow>> inputList)
        {
            var list = new List<ImportRecord>();
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

                    var record = new ImportRecord
                    {
                        Description = listDR[i][(int)RecordField.Description].ToString(),
                        Category = listDR[i][(int)RecordField.Category].ToString(),
                        Subcategory = listDR[i][(int)RecordField.Subcategory].ToString(),
                        Expense = expense,
                        Revenue = revenue,
                        Comment = listDR[i][(int)RecordField.Comment].ToString(),
                        Date = date,
                        ImportError = !ValidValue
                    };

                    list.Add(record);
                }
            }

            var errorNum = list.Where(p => p.ImportError == true).Select(p => p).ToList().Count();

            return list;

        }
    }
}
