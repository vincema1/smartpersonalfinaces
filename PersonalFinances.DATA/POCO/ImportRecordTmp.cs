using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.DATA.POCO
{
    public class importRecordTmp
    {
        public int importRecordTmpId { get; set; }
        public int dossierId { get; set; }
        public System.DateTime date { get; set; }
        public Nullable<decimal> revenue { get; set; }
        public Nullable<decimal> expense { get; set; }
        public Nullable<int> duplicates { get; set; }
        public Nullable<int> doubles { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public string description { get; set; }
        public string comment { get; set; }

    }
}
