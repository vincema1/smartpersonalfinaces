using PersonalFinances.BUSINESS.Models;

namespace PersonalFinances.BUSINESS.Services
{
    interface IImportRecordService
    {
        ImportReport ImportRecordsFromFile(int dossierId, string importFilePath, string tmpFilesFolder);
    }
}
