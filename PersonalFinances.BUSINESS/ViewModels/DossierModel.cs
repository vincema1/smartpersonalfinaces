using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalFinances.DATA.DataModel;

namespace PersonalFinances.BUSINESS.ViewModels
{
    public class DossierModel
    {
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        public string dossierName { get; set; }

        public static void CreateDossier(DossierModel dossier, string UserName)
        {
            PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
            dossier dos = new dossier();

            string userId = (from u in db.AspNetUsers
                              where u.UserName == UserName
                              select u.Id).Single();

            ////TODO: give real value
            dos.userId = userId;
            
            dos.dossierName = dossier.dossierName;
            dos.creationDate = DateTime.Now;
            db.dossiers.Add(dos);
            db.SaveChanges();

        }
    }


}
