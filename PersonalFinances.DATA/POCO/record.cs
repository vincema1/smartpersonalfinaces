using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PersonalFinances.DATA.POCO
{
    public class record
    {
        public int recordId { get; set; }
        public int dossierId { get; set; }
        public Nullable<int> recordSubcategoryId { get; set; }
        public Nullable<int> recordCategoryId { get; set; }
        public string CategoryDescription { get; set; }
        public string SubcategoryDescription { get; set; }
        public System.DateTime date { get; set; }
        
        [DataType(DataType.Currency)]
        public Nullable<decimal> revenue { get; set; }
        [DataType(DataType.Currency)]
        public Nullable<decimal> expense { get; set; }
        public string description { get; set; }
        public string comment { get; set; }
    }
}
