using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.Services.Interfaces
{
    public interface IBulkImportFileCreator
    {
        void CreateBulkImportFile(string path, IEnumerable<DATA.POCO.importRecordTmp> listRecords);

    }
}
