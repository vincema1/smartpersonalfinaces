using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonalFinances.BUSINESS.ViewModels
{
    
    public class BalanceSheetLine
    {
        public int bitmap { get; set; }
        public string category { get; set; }
        public string subcategory { get; set; }
        public decimal total { get; set; }
    }
    
}