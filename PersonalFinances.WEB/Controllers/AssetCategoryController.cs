using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonalFinances.DATA.DataModel;
using POCO=PersonalFinances.DATA.POCO;
using PersonalFinances.BUSINESS.ViewModels;

namespace PersonalFinances.WEB.Controllers
{
    [Authorize]
    public class AssetCategoryController : Controller
    {
       // private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

        // GET: Asset
        public ActionResult Index(int dossierId)
        {
            ListAssetsModel model = new ListAssetsModel(dossierId, true);
            return View(model);
        }

       
        // GET: AssetCategory/Create
        public ActionResult Create(int dossierId)
        {

            POCO.assetCategory model = new POCO.assetCategory { dossierId = dossierId };

            ViewBag.Action = "Create";
            return View("Details",model);
            //return View();
        }

        // POST: AssetCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "assetCategoryId,dossierId,isAsset,description")] POCO.assetCategory assetCategory)
        {
            if (ModelState.IsValid)
            {
                POCO.assetCategory.AddCategory(assetCategory);
                return RedirectToAction("Index", "AssetCategory", new { dossierId = assetCategory.dossierId });
            }

            return View(assetCategory);
        }


        [HttpGet]
        public ActionResult Edit([Bind(Include = "assetCategoryId,dossierId,isAsset,description")] POCO.assetCategory assetCategory)
        {
            ViewBag.Action = "Edit";
            return View("Details",assetCategory);
        }

        // POST: AssetCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditConfirmed([Bind(Include = "assetCategoryId,dossierId,isAsset,description")] POCO.assetCategory assetCategory)
        {
            if (ModelState.IsValid)
            {
                POCO.assetCategory.UpdateCategory(assetCategory);
                return RedirectToAction("Index", "AssetCategory", new { dossierId = assetCategory.dossierId });
            }
            return View(assetCategory);
        }




        // GET: AssetCategory/Create
        public ActionResult CreateSubcategory(int dossierId, bool isAsset)
        {

            POCO.assetSubcategory model = new POCO.assetSubcategory { dossierId = dossierId };
            ListAssetsModel ass = new ListAssetsModel(dossierId, true);
            model.ListCategories = ass.Categories.Where(c=> c.isAsset== isAsset).ToList();

            ViewBag.Action = "CreateSubcategory";
            return View("DetailsSubcategory",model);
            //return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSubcategory([Bind(Include = "dossierId,assetSubcategoryId,assetCategoryId,description,isAsset")] POCO.assetSubcategory assetSubcategory)
        {
            if (ModelState.IsValid)
            {
                //  POCO.assetSubcategory.AddCategory(assetCategory);

                POCO.assetSubcategory.AddSubCategory(assetSubcategory);

                return RedirectToAction("Index", "AssetCategory", new { dossierId = assetSubcategory.dossierId });
                //return RedirectToAction("Index");
            }

            return View(assetSubcategory);
        }


        [HttpGet]
        public ActionResult EditSubcategory([Bind(Include = "dossierId,assetSubcategoryId,assetCategoryId,description,isAsset")]                                                   POCO.assetSubcategory assetSubcategory)
        {

            ListAssetsModel ass = new ListAssetsModel(assetSubcategory.dossierId, assetSubcategory.isAsset);
            assetSubcategory.ListCategories = ass.Categories.Where(c => c.isAsset == assetSubcategory.isAsset).ToList();

            ViewBag.Action = "EditSubcategory";
            return View("DetailsSubcategory", assetSubcategory);
        }

        // POST: AssetCategory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("EditSubcategory")]
        [ValidateAntiForgeryToken]
        public ActionResult EditSubcategoryConfirmed([Bind(Include = "assetCategoryId,assetSubcategoryId,dossierId,isAsset,description")] POCO.assetSubcategory assetSubcategory)
        {
            if (ModelState.IsValid)
            {
                POCO.assetSubcategory.UpdateSubcategory(assetSubcategory);
                return RedirectToAction("Index", "AssetCategory", new { dossierId = assetSubcategory.dossierId });
            }
            return View(assetSubcategory);
        }


        public ActionResult DeleteCategory(int dossierId, int assetCategoryId)
        {

            POCO.assetCategory.DeleteCategory(dossierId, assetCategoryId);

            return RedirectToAction("Index", new { dossierId = dossierId });
        }
        public ActionResult DeleteSubcategory(int dossierId, int assetSubcategoryId)
        {
            POCO.assetSubcategory.DeleteSubcategory(dossierId, assetSubcategoryId);


            return RedirectToAction("Index", new { dossierId= dossierId });
        }



        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }
    }
}
