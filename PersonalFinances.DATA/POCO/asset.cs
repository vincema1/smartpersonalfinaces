using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PersonalFinances.DATA.POCO
{
    public class asset
    {
        public int assetId { get; set; }
        public int dossierId { get; set; }
        public decimal receivable { get; set; }
        public decimal payable { get; set; }
        public string description { get; set; }
        public string externalId { get; set; }
        public int assetSubcategoryId { get; set; }
        public string assetSubcategoryDes { get; set; }
        public string assetCategoryDes { get; set; }

    }
}
