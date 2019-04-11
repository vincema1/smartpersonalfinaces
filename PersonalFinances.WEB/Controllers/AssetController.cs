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
    public class AssetController : Controller
    {
        private PersonalFinancesDBEntities db = new PersonalFinancesDBEntities();

        // GET: Asset
        public ActionResult Index(int dossierId)
        {
            ListAssetsModel model = new ListAssetsModel(dossierId,false);
            return View(model);
        }

        // GET: Asset/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            asset asset = db.assets.Find(id);
            if (asset == null)
            {
                return HttpNotFound();
            }
            return View(asset);
        }

        // GET: Asset/Create
        public ActionResult Create()
        {
            //POCO.assetCategory model = new POCO.assetCategory { dossierId = dossierId };

            return View();
        }


        // POST: Asset/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "assetId,assetSubcategoryId,dossierId,receivable,payable,description")] AssetModel asset)
        {
            if (ModelState.IsValid)
            {
                //db.assets.Add(asset);
                //db.SaveChanges();
                //AssetModel
                asset.Create();
                return RedirectToAction("Index", new { dossierId= asset.dossierId});
            }

            ViewBag.dossierId = new SelectList(db.dossiers, "dossierId", "dossierName", asset.dossierId);
            return View(asset);
        }

        // GET: Asset/Edit/5
       
        public ActionResult Edit(int dossierId, int assetId)
        {

            AssetModel assetModel = AssetModel.GetAssetModel(dossierId, assetId);
            return View(assetModel);
        }

        // POST: Asset/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "assetId,assetSubcategoryId,dossierId,receivable,payable,description")] AssetModel asset)
        {
            if (ModelState.IsValid)
            {
                asset.Update();
                return RedirectToAction("Index", new { dossierId = asset.dossierId });
            }
            return View(asset);
        }

        // GET: Asset/Delete/5
        public ActionResult Delete(int dossierId, int assetId)
        {

            AssetModel.DeleteAsset(dossierId, assetId);
            return RedirectToAction("Index", new { dossierId = dossierId });
        }

        //// POST: Asset/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    asset asset = db.assets.Find(id);
        //    db.assets.Remove(asset);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
