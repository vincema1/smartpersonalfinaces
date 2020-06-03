using PersonalFinances.DATA.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.Models
{
    public class ImportResult
    {
        public IEnumerable<importRecordTmp> ImportedRecords { get; set; }
        public IEnumerable<importRecordTmp> DiscardedRecords { get; set; }

    }
}
