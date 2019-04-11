using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PersonalFinances.DATA.DataModel;
using System.Text;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class ImportFromTxtFile: IImportSource
    {
        private StreamReader _ImportFile;
        private string _ValidatedFilePath;
        private List<importRecordTmp> _listRecordsTmp = new List<importRecordTmp>();
        private int _dossierId;
        private char _separator = '§';

        private int _totalRecordsProcessed = 0;
        private int _failedImports = 0;


        public string ValidatedFilePath { get { return _ValidatedFilePath; } }

        private enum DateField
        {
            day,
            month,
            year
        }

        private enum RecordField
        {
            Date,
            Description,
            Revenue,
            Expense,
            Category,
            Subcategory,
            Type
        }


        public char Separator { get { return _separator; } set { _separator = value; } }

        public int TotalRecordsProcessed { get { return _totalRecordsProcessed; } }
        public int FailedImports { get { return _failedImports; } }

        public List<importRecordTmp> GetRecordTmp()
        {
            return _listRecordsTmp;
        }

        private void FillListRecordsTmp()
        {
            string line = "";
            importRecordTmp importRecordTmp;


            System.IO.TextWriter writeFile = new StreamWriter(_ValidatedFilePath, false, Encoding.Unicode);
           
            int count = 0;
            using (_ImportFile)
            {
                while ((line = _ImportFile.ReadLine()) != null)
                {
                    _totalRecordsProcessed++;
                    if (ProcessRecordline(line, 
                                           out importRecordTmp))
                    {
                        count++;
                        _listRecordsTmp.Add(importRecordTmp);
                        writeFile.WriteLine(line);
                    }
                }
            }

            writeFile.Flush();
            writeFile.Close();
            writeFile = null;

        }

        private bool ProcessRecordline(string line, 
                                        out importRecordTmp importRecordTmp)
        {
            
            importRecordTmp = new importRecordTmp();
            String[] RecordLine = line.Split(_separator);

            if (RecordLine.Count() != 7)
            {
                //TODO: report error to USER
                _failedImports++;
                return false;
            }

            string [] dateTmp = RecordLine[(int)RecordField.Date].Split('/');

            if (dateTmp.Count() != 3)
            {
                //TODO: report error to USER
                _failedImports++;
                return false;
            }

            importRecordTmp.date = new DateTime(year: Int32.Parse(dateTmp[(int)DateField.year]), 
                                                 month:Int32.Parse(dateTmp[(int)DateField.month]), 
                                                 day: Int32.Parse(dateTmp[(int)DateField.day]));
                    
            importRecordTmp.description = RecordLine[(int)RecordField.Description];

            //string numTmp = RecordLine[(int)RecordField.Amount].Replace(',', '.');
            //importRecordTmp.expense = decimal.Parse(numTmp);

            importRecordTmp.expense = decimal.Parse(RecordLine[(int)RecordField.Expense]);
            importRecordTmp.revenue = decimal.Parse(RecordLine[(int)RecordField.Revenue]);

            if (importRecordTmp.expense>0 && 
                importRecordTmp.revenue>0)
            {
                _failedImports++;
                return false;
            }

            importRecordTmp.category =RecordLine[(int)RecordField.Category];
            importRecordTmp.subcategory = RecordLine[(int)RecordField.Subcategory];
            importRecordTmp.dossierId = _dossierId;

            return true;
        }
        //Contructors
        public ImportFromTxtFile(string ImportFilePath, string TmpFilesPath, int dossierId)
        {
            //StreamReader is created from path

            _ValidatedFilePath= TmpFilesPath + string.Format(@"{0}.txt", Guid.NewGuid());
            _dossierId = dossierId;
            _ImportFile = new StreamReader(ImportFilePath);
             FillListRecordsTmp();
        }

        public ImportFromTxtFile(StreamReader ImportFile, int dossierId)
        {
            //StreamReader is fed as a parameter
            _dossierId = dossierId;
            _ImportFile = ImportFile;
            FillListRecordsTmp();
        }
    }
}