using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.ViewModels;
using PersonalFinances.WEB.Utils;
using static PersonalFinances.Controllers.DossierController;
using System.IO;
using PersonalFinances.DATA.Utils;
using System.Threading.Tasks;
using PersonalFinances.DATA;

namespace PersonalFinances.WEB.Controllers
{
    [Authorize]
    public class RecordTmpController : Controller
    {
        private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

      
        [HttpGet]
        public ActionResult GetListDoubles(int dossierId, int importRecordTmpId)
        {

            List<POCO.record> listRec= RecordTmpModel.GetDoubles(dossierId, importRecordTmpId);
            return PartialView("_ListDoubles", listRec);

        }


        public ActionResult Index(int dossierId,
                                  int CurrentPage = 1,
                                  int ItemsPerPage = 20)
        {

            ListRecordsTmpModel model = new ListRecordsTmpModel(dossierId, CurrentPage, ItemsPerPage);
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> ImportAsync(HttpPostedFileBase importFile, int dossierId, string type)
        {
            if (importFile != null)
            {
                var fileName = importFile.FileName;
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), importFile.FileName);
                importFile.SaveAs(path);

                ImportModel ImportModel = new ImportModel(dossierId, path, Server.MapPath("~/App_Data/FilesTmp/"));

                //Extracts List Records fromDB
                string sessionID = dossierId + "_" + HttpContext.Session.SessionID;

                if (type == "bulk")
                    ImportModel.ImportBulkInsert();
                else
                    await ImportModel.ImportEntityAsync(sessionID);


                return View("Import", ImportModel.Report);
            }
            else
                return RedirectToAction("index", new { dossierId= dossierId });
        }

        //[HttpPost]
        //public async Task<ActionResult> ImportEntity(HttpPostedFileBase importFile, int dossierId, string type)
        //{
        //    if (importFile != null)
        //    {
        //        var fileName = importFile.FileName;
        //        var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), importFile.FileName);
        //        importFile.SaveAs(path);

        //        ImportModel ImportModel = new ImportModel(dossierId, path, Server.MapPath("~/App_Data/FilesTmp/"));

        //        //Extracts List Records fromDB
        //        string sessionID = dossierId + "_" + HttpContext.Session.SessionID;

        //        await ImportModel.ImportEntityAsync(sessionID);


        //        return View("Import", ImportModel.Report);
        //    }
        //    else
        //        return RedirectToAction("index", new { dossierId = dossierId });
        //}

        [HttpPost]
        public ActionResult ImportEntity(HttpPostedFileBase importFile, int dossierId, string type)
        {
            if (importFile != null)
            {
                var fileName = importFile.FileName;
                var path = Path.Combine(Server.MapPath("~/App_Data/uploads"), importFile.FileName);
                importFile.SaveAs(path);

                ImportModel ImportModel = new ImportModel(dossierId, path, Server.MapPath("~/App_Data/FilesTmp/"));

                //Extracts List Records fromDB
                string sessionID = dossierId + "_" + HttpContext.Session.SessionID;

                ImportModel.ImportEntity(sessionID);

                return View("Import", ImportModel.Report);
            }
            else
                return RedirectToAction("index", new { dossierId = dossierId });
        }

        //      [HttpPost]
        public ActionResult ImportEntitySync()
        {

            var value1 = Request["importFile"];
            var value2 = Request["dossierId"];
            var value3 = Request["type"];

            return View();
        }


         public ActionResult AddToDossier(int dossierId)
        {
            ImportModel.AddToDossier(dossierId);
            return RedirectToAction("Index", new { dossierId= dossierId });

        }

        public ActionResult DeleteRecordsTmp(int dossierId)
        {
            //Deletes all temporary records;
            ImportModel.DeleteFromRecordTmp(dossierId);
            return RedirectToAction("Index",new { dossierId= dossierId });
        }

                             
        public ActionResult Delete(int dossierId, int importRecordTmpId)
        {
            //Deletes individual temporary record;
            RecordTmpModel.Delete(dossierId, importRecordTmpId);
            return RedirectToAction("Index", new { dossierId= dossierId });
        }

        public ActionResult PollOnImport(int dossierId)
        {
            string sessionID = dossierId + "_" + HttpContext.Session.SessionID;
            List<int> values= ImportModel.PollOnImport(sessionID);

            return PartialView("_PollOnImport", values);
        }

 


        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}