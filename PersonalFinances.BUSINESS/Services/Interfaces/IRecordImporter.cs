using PersonalFinances.BUSINESS.Models;
using PersonalFinances.DATA.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.Services.Interfaces
{
    public interface IRecordsImporter
    {
        ImportResult ImportRecords(int dossierId, IEnumerable<importRecordTmp> Records);
        int ImportRecordsBulkInsert(int dossierId, string bulkFilePath);

    }
}
