using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using PersonalFinances.BUSINESS.Models;
using System.Collections.Generic;

namespace PersonalFinances.BUSINESS.Services
{
    public class Report
    {
        public void CreateExcelDoc(string fileName, List<ImportRecord> importRecords)
        {
            using (SpreadsheetDocument document = SpreadsheetDocument.Create(fileName, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = document.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                worksheetPart.Worksheet = new Worksheet();

                Sheets sheets = workbookPart.Workbook.AppendChild(new Sheets());
                Sheet sheet = new Sheet() { Id = workbookPart.GetIdOfPart(worksheetPart), SheetId = 1, Name = "Financial Records" };
                sheets.Append(sheet);
                workbookPart.Workbook.Save();

                SheetData sheetData = worksheetPart.Worksheet.AppendChild(new SheetData());


                // Constructing header
                Row row = new Row();

                row.Append(
                    ConstructCell("date", CellValues.String),
                    ConstructCell("item", CellValues.String),
                    ConstructCell("revenue", CellValues.String),
                    ConstructCell("expense", CellValues.String),
                    ConstructCell("category", CellValues.String),
                    ConstructCell("subcategory", CellValues.String),
                    ConstructCell("comment", CellValues.String)
                    );

                // Insert the header row to the Sheet Data
                sheetData.AppendChild(row);

                // Inserting each record
                foreach (var record in importRecords)
                {
                    row = new Row();

                    row.Append(
                        ConstructCell(record.Date.ToString("dd-MM-yyyy"), CellValues.Date),
                        ConstructCell(record.Description, CellValues.String),
                        ConstructCell(record.Revenue.ToString(), CellValues.Number),
                        ConstructCell(record.Expense.ToString(), CellValues.Number),
                        ConstructCell(record.Category, CellValues.String),
                        ConstructCell(record.Subcategory, CellValues.String),
                        ConstructCell(record.Comment, CellValues.String)
                    );

                    sheetData.AppendChild(row);
                }

                worksheetPart.Worksheet.Save();
            }
        }

        private Cell ConstructCell(string value, CellValues dataType)
        {
            return new Cell()
            {
                CellValue = new CellValue(value),
                DataType = new EnumValue<CellValues>(dataType)
            };
        }
    }
}
