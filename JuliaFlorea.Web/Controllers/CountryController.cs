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
    public class CountryController : Controller
    {

        private readonly AppDbContext dbContext = new AppDbContext();

        // GET: Country
        public ActionResult Index()
        {
            return View(dbContext.Countries.ToList());
        }

        // GET: Country/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = dbContext.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }


        // GET: Country/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Country/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CountryName")] Country country)
        {
            if(ModelState.IsValid)
            {
                dbContext.Add(country);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(country);
        }

        // GET: Country/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = dbContext.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Country/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CountryName,CountryId")] Country country)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(country).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
                
            return View(country);
        }

        // GET: Country/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Country country = dbContext.Countries.Find(id);
            if (country == null)
            {
                return HttpNotFound();
            }
            return View(country);
        }

        // POST: Country/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Country country = dbContext.Countries.Find(id);

            bool isCountryAssociated = dbContext.Depots.Any(d => d.CountryId == country.CountryId);
            if (isCountryAssociated)
            {
                ModelState.AddModelError(string.Empty, "The selected country cannot be deleted because it is associated with a depot.");
                return View("Delete", country);
            }

            dbContext.Countries.Remove(country);
            dbContext.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
