using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalFinances.DATA.DataModel;
using PersonalFinances.BUSINESS.ViewModels;
//using PersonalFinances.WEB.Models;

namespace PersonalFinances.WEB.Controllers
{
    public class HomeController : Controller
    {
        PersonalFinancesDBEntities _context = new PersonalFinancesDBEntities();
     
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [Authorize]
        public ActionResult UserDossiers()
        {
            var dossiers = new  List<DossierDetailsModel>();

            if (User.Identity.IsAuthenticated)
            {
              

                dossiers = DossierDetailsModel.ListDossierUser(User.Identity.Name);

                

            }

            return PartialView("_UserDossiers", dossiers);
        }

    }
}