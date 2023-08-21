using JuliaFlorea.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class DepotController : Controller
    {
        AppDbContext dbContext = new AppDbContext();

        // GET: Depot
        public ActionResult Index()
        {
            var depotsWithCountry = dbContext.Depots.Include(d => d.Country).ToList();
            return View(depotsWithCountry);
        }

        // GET: Depot/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Depot depot = dbContext.Depots.Include(d => d.Country).SingleOrDefault(d => d.DepotId == id);

            if (depot == null)
            {
                return HttpNotFound();
            }

            return View(depot);
        }

        // GET: Depot/Create
        public ActionResult Create()
        {
            ViewBag.CountryList = new SelectList(dbContext.Countries, "CountryId", "CountryName");
           
            return View();
        }

        // POST: Depot/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepotName,DepotAddress,StorageCapacity,CountryId")] Depot depot)
        {
            if (ModelState.IsValid)
            {
                bool isCountryAssociated = dbContext.Depots.Any(d => d.CountryId == depot.CountryId);
                if(isCountryAssociated)
                {
                    ModelState.AddModelError("CountryId", "The selected country is already associated with another depot.");
                    ViewBag.CountryList = new SelectList(dbContext.Countries, "CountryId", "CountryName", depot.CountryId);
                    return View(depot);
                }
                dbContext.Add(depot);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryList = new SelectList(dbContext.Countries, "CountryId", "CountryName", depot.CountryId);
            return View(depot);
        }

        // GET: Depot/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depot depot = dbContext.Depots.Find(id);
            if (depot == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryList = new SelectList(dbContext.Countries, "CountryId", "CountryName", depot.CountryId);
            return View(depot);
        }

        // POST: Depot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepotId,DepotName,DepotAddress,StorageCapacity,CountryId")] Depot depot)
        {
            if (ModelState.IsValid)
            {

                bool isCountryAssociated = dbContext.Depots.Any(d => d.CountryId == depot.CountryId && d.DepotId != depot.DepotId);
                if (isCountryAssociated)
                {
                    ModelState.AddModelError("CountryId", "The selected country is already associated with another depot.");
                    ViewBag.CountryList = new SelectList(dbContext.Countries, "CountryId", "CountryName", depot.CountryId);

                    return View(depot);
                }


                dbContext.Entry(depot).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryList = new SelectList(dbContext.Countries, "CountryId", "CountryName", depot.CountryId);
            
            return View(depot);
        }

        // GET: Depot/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Depot depot = dbContext.Depots.Include(d => d.Country).SingleOrDefault(d => d.DepotId == id);
            if (depot == null)
            {
                return HttpNotFound();
            }
            return View(depot);
        }

        // POST: Depot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Depot depot = dbContext.Depots.Find(id);
            dbContext.Depots.Remove(depot);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
