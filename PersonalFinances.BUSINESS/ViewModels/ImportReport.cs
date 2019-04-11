using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.BUSINESS.Utils;
using PersonalFinances.DATA.DataModel;
using PersonalFinances.BUSINESS.Filters;
using System.Data.Entity.Core.Objects;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class ImportReport
    {
        public int dossierId { get; set; }
        public int totalRecordsProcessed { get; set; }
        public int SuccessfulImports { get; set; }
        public int FailedImports { get; set; }

    }
}
