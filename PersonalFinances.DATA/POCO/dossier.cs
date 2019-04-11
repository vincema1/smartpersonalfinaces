using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PersonalFinances.DATA.POCO
{
    public class dossier
    {
        public int dossierId { get; set; }
        public string dossierName { get; set; }
        public Nullable<System.DateTime> creationDate { get; set; }
        public string userId { get; set; }

        
    }
}
