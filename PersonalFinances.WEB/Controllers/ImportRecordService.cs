namespace PersonalFinances.WEB.Controllers
{
    internal class ImportRecordService
    {
        private int dossierId;
        private string path;
        private string v;

        public ImportRecordService(int dossierId, string path, string v)
        {
            this.dossierId = dossierId;
            this.path = path;
            this.v = v;
        }
    }
}