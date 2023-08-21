using JuliaFlorea.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class DrugUnitController : Controller
    {
        AppDbContext dbContext = new AppDbContext();
        // GET: DrugUnit
        public ActionResult Index()
        {
            var drugUnitsWithDepotAndDrugType = dbContext.DrugUnits.Include(du => du.Depot).Include(du => du.DrugType).ToList();
            return View(drugUnitsWithDepotAndDrugType);
        }

        // GET: DrugUnit/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DrugUnit drugUnit = dbContext.DrugUnits.Include(du => du.Depot).Include(du => du.DrugType).SingleOrDefault(d => d.DrugUnitId == id);
            

            if (drugUnit == null)
            {
                return HttpNotFound();
            }

            return View(drugUnit);
        }

        // GET: DrugUnit/Create
        public ActionResult Create()
        {
            ViewBag.DepotList = new SelectList(dbContext.Depots, "DepotId", "DepotName");
            ViewBag.DrugTypeList = new SelectList(dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");


            return View();
        }

        // POST: DrugUnit/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DrugUnitId,PickNumber, ManufacturingDate, ExpiryDate, Manufacturer,DepotId,DrugTypeId")] DrugUnit drugUnit)
        {
            if (ModelState.IsValid)
            {

                drugUnit.ManufacturingDate = DateTime.Now;
                dbContext.Add(drugUnit);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepotList = new SelectList(dbContext.Depots, "DepotId", "DepotName");
            ViewBag.DrugTypeList = new SelectList(dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");

            return View(drugUnit);
        }

        // GET: DrugUnit/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           DrugUnit drugUnit= dbContext.DrugUnits.Find(id);
            if (drugUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepotList = new SelectList(dbContext.Depots, "DepotId", "DepotName");
            ViewBag.DrugTypeList = new SelectList(dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");

            return View(drugUnit);
        }


        // POST: DrugUnit/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DrugUnitId,PickNumber, ManufacturingDate, ExpiryDate, Manufacturer,DepotId,DrugTypeId")] DrugUnit drugUnit)
        {
            if (ModelState.IsValid)
            {
                
                dbContext.Entry(drugUnit).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepotList = new SelectList(dbContext.Depots, "DepotId", "DepotName");
            ViewBag.DrugTypeList = new SelectList(dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");

            return View(drugUnit);
        }
        // GET: DrugUnit/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugUnit drugUnit = dbContext.DrugUnits.Include(du => du.Depot).Include(du => du.DrugType).SingleOrDefault(d => d.DrugUnitId == id);

            if (drugUnit == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepotList = new SelectList(dbContext.Depots, "DepotId", "DepotName");
            ViewBag.DrugTypeList = new SelectList(dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");

            return View(drugUnit);
        }

        // POST: DrugUnit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
           DrugUnit drugUnit = dbContext.DrugUnits.Find(id);
            dbContext.DrugUnits.Remove(drugUnit);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
