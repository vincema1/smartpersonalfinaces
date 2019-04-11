
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.ViewModels;
using PersonalFinances.WEB.Utils;
using System.Collections.Generic;

namespace PersonalFinances.WEB.Controllers
{
    [Authorize]
    public class RecordController : Controller
    {
         private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();
        
            // GET: /Record/
         public ActionResult Index(int dossierId,
                                    string BeginDate = "01/01/1900", 
                                    string EndDate = "31/12/9999", 
                                    int CurrentPage = 1,
                                    int ItemsPerPage = 20,
                                    bool isPostBack=false)
         {
            //Extracts List Records fromDB
            string sessionID = dossierId + "_" + HttpContext.Session.SessionID;
            ListRecordsModel model = new ListRecordsModel() { beginDate = BeginDate, endDate = EndDate,IsPostBack= isPostBack, ClientSessionId= sessionID };
            model.ListRecords(dossierId,
                                BeginDate,
                                EndDate,
                                CurrentPage,
                                ItemsPerPage);
            return View(model);
         }

        // GET: /Record/
        public ActionResult FullSearch(int dossierId,
                                        string BeginDate="",
                                        string EndDate="",
                                        int recordCategoryId=0,
                                        int recordSubcategoryId=0,
                                        string description="",
                                        string comment="", 
                                        bool isPostback=false)
        {
            //Extracts List Records fromDB
            string sessionID = dossierId + "_" + HttpContext.Session.SessionID;

            ListRecordsModel model = new ListRecordsModel() { beginDate = BeginDate, endDate = EndDate, IsPostBack = isPostback, ClientSessionId = sessionID };
            model.ListRecordsFullSearch(dossierId,
                                        System.Configuration.ConfigurationManager.ConnectionStrings     ["PersonalFinancesDB"].ConnectionString,
                                        BeginDate,
                                        EndDate,
                                        recordCategoryId,
                                        recordSubcategoryId,
                                        description,
                                        comment);

            return View("Index",model);
        }


        //
        // POST: /Record/Create
        [HttpPost]
        public ActionResult CreateAjax(RecordModel Record)
        {
            if (ModelState.IsValid)
            {
                Record.Create();
                return new EmptyResult();
            }
            else
                return new HttpStatusCodeResult(400, "input error");
        }

        [HttpPost]
        public ActionResult Create(POCO.record Record)
        {
      
            ViewBag.dossierId = new SelectList(db.dossiers, "dossierId", "dossierName", Record.dossierId);
            ViewBag.RecordSubcategoryId = new SelectList(db.recordSubcategories, "RecordSubcategoryId", "description", Record.recordSubcategoryId);
            return View(Record);
        }

        //
        // GET: /Record/Edit/5

        public ActionResult Edit(int recordId)
        {
            RecordModel Record = new RecordModel(recordId);
            if (Record == null)
            {
                return HttpNotFound();
            }
     
            return PartialView("_Edit", Record);
            //return View(Record);
        }


        public ActionResult Categories(int dossierId)
        {
            
            RecordModel rec = new RecordModel();
            rec.GetCategories(dossierId);
            List<POCO.recordCategory> categories = rec.categories;

            if (categories == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Categories", categories);
            
        }

        public ActionResult Subcategories(int dossierId)
        {

            RecordModel rec = new RecordModel();
            rec.GetCategories(dossierId);
            List<POCO.recordSubcategory> subcategories = rec.subcategories;

            if (subcategories == null)
            {
                return HttpNotFound();
            }

            return PartialView("_Subcategories", subcategories);

        }
        

        [HttpPost]
        public ActionResult EditAjax(RecordModel Record)
        {
            if (ModelState.IsValid)
            {
                Record.Update();
                return new EmptyResult(); //evaluate informational json
            }
            else
            {
                //More complex alternative
                /*
                Response.StatusCode = 400; //Notifies an error
                var error = new { ErrorId = 123, Level = 2, Message = "Input error" };
                return Json(error, JsonRequestBehavior.AllowGet);
                */
                return new HttpStatusCodeResult(400, "input error");

            }

        }

       
        public ActionResult DeleteSelection(int dossierId, 
                                            string beginDate, 
                                            string endDate)
        {

            ListRecordsModel.DeleteSelectedRecords(dossierId, beginDate, endDate);
            return RedirectToAction("Index", new { dossierId= dossierId , beginDate=beginDate, endDate=endDate});
        }


        public ActionResult MergeCategories(MergeCategoriesModel model)
        {

            if (ModelState.IsValid)
            {
                switch (model.type)
                {
                    case "mergeCategories":
                        MergeCategoriesModel.MergeCategories(model);
                        break;

                    case "mergeSubcategories":
                        MergeCategoriesModel.MergeSubcategories(model);
                        break;

                    case "moveSubcategory":
                        MergeCategoriesModel.MoveSubcategory(model);
                        break;
                
                }
            }


            return RedirectToAction("Index", new { dossierId = model.dossierId,
                                                   beginDate =model.beginDate,
                                                   endDate = model.endDate });
        }

        public ActionResult DeleteEmptyCategories(int dossierId, 
                                                  string beginDate, 
                                                  string endDate)
        {

            MergeCategoriesModel.DeleteEmptyCategories(dossierId);
            
            return RedirectToAction("Index", new
            {
                dossierId = dossierId,
                beginDate = beginDate,
                endDate = endDate
            });
        }

        //[HttpPost]
        public ActionResult Delete(int recordId, int dossierId)
        {
            record Record = db.records.Find(recordId);
            db.records.Remove(Record);
            db.SaveChanges();
            return RedirectToAction("Index", new { dossierId= dossierId });
        }

        //public ActionResult IncomeStatement(int dossierId, 
        //                                    string beginDate = "01/01/1000", 
        //                                    string endDate = "31/12/9999")
        //{

        //    List<BalanceSheetLine> list= SessionManager.SetListBalanceSheetLines(dossierId, beginDate, endDate);

        //    IncomeStatementTab model = new IncomeStatementTab(dossierId, beginDate, endDate, true);

        //    return PartialView("_IncomeStatementTab", model);
        //}

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}