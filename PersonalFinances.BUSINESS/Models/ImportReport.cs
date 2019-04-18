namespace PersonalFinances.BUSINESS.Models
{
    public class ImportReport
    {
        public int dossierId { get; set; }
        public int totalRecordsProcessed { get; set; }
        public int SuccessfulImports { get; set; }
        public int FailedImports { get; set; }

    }
}
