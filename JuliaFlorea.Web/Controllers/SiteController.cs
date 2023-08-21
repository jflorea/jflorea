using JuliaFlorea.DataModel;
using JuliaFlorea.Domain;
using JuliaFlorea.Domain.DbService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class SiteController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly SiteInventoryDbHandler _siteInventoryDbHandler;
       

        public SiteController(AppDbContext dbContext, SiteInventoryDbHandler siteInventoryDbHandler)
        {
            _dbContext = dbContext;
            _siteInventoryDbHandler = siteInventoryDbHandler;
           
        }



        // GET: Depot
        public ActionResult Index()
        {
            var siteWithCountry = _dbContext.Sites.Include(s => s.Country).ToList();
            return View(siteWithCountry);
        }

        // GET: Depot/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Site site = _dbContext.Sites.Include(s => s.Country).SingleOrDefault(s => s.SiteId == id);
            ;

            if (site == null)
            {
                return HttpNotFound();
            }

            return View(site);
        }

        // GET: Depot/Create
        public ActionResult Create()
        {
            ViewBag.CountryList = new SelectList(_dbContext.Countries, "CountryId", "CountryName");

            return View();
        }

        // POST: Depot/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SiteName,CountryCode")] Site site)
        {
            if (ModelState.IsValid)
            {

                _dbContext.Add(site);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CountryList = new SelectList(_dbContext.Countries, "CountryId", "CountryName", site.CountryCode);
            return View(site);
        }

        // GET: Depot/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = _dbContext.Sites.Find(id);
            if (site == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryList = new SelectList(_dbContext.Countries, "CountryId", "CountryName", site.CountryCode);
            return View(site);
        }

        // POST: Depot/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SiteId,SiteName,CountryCode")] Site site)
        {
            if (ModelState.IsValid)
            {



                _dbContext.Entry(site).State = EntityState.Modified;
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CountryList = new SelectList(_dbContext.Countries, "CountryId", "CountryName", site.CountryCode);

            return View(site);
        }

        // GET: Depot/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Site site = _dbContext.Sites.Include(s => s.Country).SingleOrDefault(s => s.SiteId == id);
            if (site == null)
            {
                return HttpNotFound();
            }
            return View(site);
        }

        // POST: Depot/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Site site = _dbContext.Sites.Find(id);
            _dbContext.Sites.Remove(site);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }


        public ActionResult RequestDrugs(string siteId)
        {
            if (siteId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Site site = _dbContext.Sites.Find(siteId);
            if (site == null)
            {
                return HttpNotFound();
            }

            ViewBag.DrugTypeList = new SelectList(_dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");

            return View(site);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RequestDrugs(string siteId, string drugCode, int quantity)
        {
            if (string.IsNullOrEmpty(siteId) || string.IsNullOrEmpty(drugCode) || quantity <= 0)
            {
                ModelState.AddModelError("", "Invalid input data.");
                return RedirectToAction("RequestDrugs", new { siteId });
            }

            _siteInventoryDbHandler.UpdateSiteInventory(siteId, drugCode, quantity);

            
            Site updatedSite = _dbContext.Sites
                .Include(s => s.Country.Depot.DrugUnits)
                .FirstOrDefault(s => s.SiteId == siteId);

            ViewBag.DrugTypeList = new SelectList(_dbContext.DrugTypes, "DrugTypeId", "DrugTypeName");
            ViewBag.RequestedDrugUnits = updatedSite?.Country?.Depot?.DrugUnits.Where(du => du.DrugType != null && du.DrugType.DrugTypeId == drugCode).ToList();
            ViewBag.RequestedQuantity = quantity;
            return View(updatedSite);

        }
    }
}
