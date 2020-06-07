using PersonalFinances.BUSINESS.Services.Interfaces;
using PersonalFinances.DATA.POCO;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PersonalFinances.BUSINESS.Services.Implementations
{
    public class BulkImportFileCreator : IBulkImportFileCreator
    {
        public void CreateBulkImportFile(string path, IEnumerable<DATA.POCO.importRecordTmp> listRecords)
        {

            using (TextWriter writeFile = new StreamWriter(path, false, Encoding.Unicode))
            {
                StringBuilder sb = new StringBuilder();

                // 01/08/2013§saponetta x 2§0§2,92§casa§cosmetici§
                //https://docs.microsoft.com/en-us/dotnet/standard/base-types/standard-date-and-time-format-strings (date formatting)
                listRecords.ToList().ForEach(r => {
                    writeFile.WriteLine($"{r.date.ToString("O")}§{r.description}§{r.revenue}§{r.expense}§{r.category}§{r.subcategory}§{r.comment}");
                });

                writeFile.Flush();
                writeFile.Close();
               
            }
        }
    }
}
