using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class NavigationData
    {
        public string beginDate { get; set; }
        public string endDate { get; set; }
        public int DossierId { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalNumberOfPages { get; set; }
    }
}
