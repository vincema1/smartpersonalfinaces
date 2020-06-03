using PersonalFinances.BUSINESS.Models;
using System.Collections.Generic;

namespace PersonalFinances.BUSINESS.Services
{
    public interface IImportRecordsExtractor
    {
        List<ImportRecord> ExtractImportRecordsFromFile(string path);
    }
}
