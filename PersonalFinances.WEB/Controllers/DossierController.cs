using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalFinances.WEB.Models;
using PersonalFinances.BUSINESS.ViewModels;
using PersonalFinances.DATA.DataModel;
using PersonalFinances.DATA.Utils;
using PersonalFinances.WEB.Utils;
using System.IO;


namespace PersonalFinances.Controllers
{
    
    [Authorize]
    public class DossierController : Controller
    {
        private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

        public enum Importlevel
        {
            expenses,
            revenues,
            expensesRevenues
        }

        //
        // GET: /Dossier/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int dossierId)
        {

            string connectionString=System.Configuration.ConfigurationManager.ConnectionStrings["PersonalFinancesDB"].ConnectionString;
            DossierDetailsModel dossier = new DossierDetailsModel(dossierId,connectionString);

            SessionManager.CurrentDossier = dossier;

            if (dossier == null)
            {
                return HttpNotFound();
            }
            return View(dossier);
        }


        public ActionResult Create()
        {
            
            return View();
        }

        //
        // POST: /Dossier/Create

        [HttpPost]
        public ActionResult Create(DossierModel dossier)
        {
            if (ModelState.IsValid)
            {
                DossierModel.CreateDossier(dossier, User.Identity.Name);
                return RedirectToAction("Index","Home");
            }

            return View(dossier);
        }

        //
        // GET: /Dossier/Edit/5

        public ActionResult Edit(int id = 0)
        {
            dossier dossier = db.dossiers.Find(id);
            if (dossier == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = new SelectList(db.AspNetUsers, "UserId", "Name", dossier.userId);
            return View(dossier);
        }

        //
        // POST: /Dossier/Edit/5

        [HttpPost]
        public ActionResult Edit(dossier dossier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dossier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
     
   //       ViewBag.userId = new SelectList(db.users, "userId", "name", dossier.userId);
            return View(dossier);
        }

        //
        // GET: /Dossier/Delete/5

        public ActionResult Delete(int dossierId)
        {
            try
            {
                StoreProcedures.DeleteDossier(dossierId);
            }
            catch (Exception)
            {
                return RedirectToAction("Details", new { dossierId=dossierId });
            }

            return RedirectToAction("Index", "Home");
               
        }

      
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            dossier dossier = db.dossiers.Find(id);
            db.dossiers.Remove(dossier);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        
        

        public ActionResult PieChartData(int dossierId, 
                                         string beginDate, 
                                         string endDate,
                                         bool isExpense)
        {

            IncomeStatementTab IST = new IncomeStatementTab(dossierId, beginDate, endDate, isExpense);

            ViewBag.FirstDate = beginDate;
            ViewBag.LastDate = endDate;
            ViewBag.Type = (isExpense)?"EXPENSES":"REVENUES";

            List<IncomeStatementLine> model= IST.GetTotalCategories();
            return PartialView("_PieChartData", model);

        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}