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
    public class DrugTypeController : Controller
    {
        AppDbContext dbContext = new AppDbContext();
        // GET: DrugType
        public ActionResult Index()
        {
            return View(dbContext.DrugTypes.ToList());
        }

        // GET: DrugType/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            DrugType drugType = dbContext.DrugTypes.Find(id);

            if (id == null)
            {
                return HttpNotFound();
            }
            return View(drugType);
        }

        // GET: DrugType/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DrugType/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DrugTypeName,WeightInPounds")]DrugType drugType)
        {
           if(ModelState.IsValid)
            {
                dbContext.Add(drugType);
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drugType);
        }

        // GET: DrugType/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugType drugType = dbContext.DrugTypes.Find(id);
            if (drugType == null)
            {
                return HttpNotFound();
            }
            return View(drugType);
        }

        // POST: DrugType/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DrugTypeName,DrugTypeId,WeightInPounds")] DrugType drugType)
        {
            if (ModelState.IsValid)
            {
                dbContext.Entry(drugType).State = EntityState.Modified;
                dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(drugType);
        }

        // GET: DrugType/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DrugType drugType = dbContext.DrugTypes.Find(id);
            if (drugType == null)
            {
                return HttpNotFound();
            }
            return View(drugType);
        }

        // POST: DrugType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DrugType drugType = dbContext.DrugTypes.Find(id);
            dbContext.DrugTypes.Remove(drugType);
            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
