using JuliaFlorea.DataModel;
using JuliaFlorea.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class ToGroupedDrugUnitsController : Controller
    {
        AppDbContext dbContext = new AppDbContext();

        public ToGroupedDrugUnitsController()
        {

        }
        
        public ActionResult GroupedDrugUnits()
        {
            var allDrugUnits = dbContext.DrugUnits.Include(du => du.DrugType).ToList();
            var groupedDrugUnits = allDrugUnits.ToGroupedDruGunits();
            return View(groupedDrugUnits);
        }
    }
}