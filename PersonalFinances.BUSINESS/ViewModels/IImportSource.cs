using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PersonalFinances.DATA.DataModel;


namespace PersonalFinances.BUSINESS.ViewModels
{ 
    public interface IImportSource
    {
        List<importRecordTmp> GetRecordTmp();

        string ValidatedFilePath { get;}

        int TotalRecordsProcessed { get; }
        int FailedImports { get; }

    }
}
