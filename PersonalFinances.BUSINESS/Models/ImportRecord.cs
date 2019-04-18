namespace PersonalFinances.BUSINESS.Models
{
    public class ImportRecord
    {
        public System.DateTime Date { get; set; }
        public decimal Revenue { get; set; }
        public decimal Expense { get; set; }
        public string Category { get; set; }
        public string Subcategory { get; set; }
        public string Description { get; set; }
        public string Comment { get; set; }
        public bool ImportError { get; set; }
    }
}
