using JuliaFlorea.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class WeightController : Controller
    {
        private readonly AppDbContext _dbContext;

        public WeightController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ActionResult Index()
        {
            var depotDrugUnits = _dbContext.Depots
         .Include(d => d.DrugUnits)
         .ThenInclude(du => du.DrugType)
         .ToList();

            return View(depotDrugUnits);

        }
    }
}