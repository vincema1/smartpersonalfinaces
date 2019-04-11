using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PersonalFinances.DATA.POCO
{
    public class yearlyExpensePerCategoryLine
    {
        public decimal total { get; set; }
        public int year { get; set; }
        public string category { get; set; }
    }
}
