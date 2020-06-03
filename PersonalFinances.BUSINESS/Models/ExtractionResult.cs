using System.Collections.Generic;

namespace PersonalFinances.BUSINESS.Models
{
    public class ExtractionResult
    {
        private IEnumerable<DATA.POCO.importRecordTmp> _importRecordTmp = new List<DATA.POCO.importRecordTmp> ();


        public IEnumerable<DATA.POCO.importRecordTmp> ValidRecords
        {
            get
            {
                return _importRecordTmp;
            }
            set
            {
                _importRecordTmp = value;
            }
        }

        public int TotalProcessedRecords { get; set; }=0;
        public int TotalDiscardedRecord { get; set; } = 0;

    }
}
