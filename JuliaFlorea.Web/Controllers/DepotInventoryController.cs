using JuliaFlorea.DataModel;
using JuliaFlorea.Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class DepotInventoryController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly DepotInventoryService _depotInventoryService;

        public DepotInventoryController(AppDbContext dbContext, DepotInventoryService depotInventoryService)
        {
            _dbContext = dbContext;
            _depotInventoryService = depotInventoryService;
        }

        public DepotInventoryController()
        {
        }

        public ActionResult Index()
        {
            var depotInventory = _dbContext.DrugUnits.Include(du => du.DrugType).Include(du => du.Depot).ToList();
            ViewBag.Depots = _dbContext.Depots.ToList();
            ViewBag.DrugUnits = depotInventory;
            return View(depotInventory);
        }

        [HttpPost]
        public ActionResult Associate(string associateDepotId, int associateStartPickNumber, int associateEndPickNumber)
        {
            var drugUnits = _dbContext.DrugUnits.ToList();
            var selectedDepot = _dbContext.Depots.FirstOrDefault(d => d.DepotId == associateDepotId);

            if (selectedDepot == null)
            {
                ModelState.AddModelError("", "Selected depot does not exist.");
                ViewBag.Depots = _dbContext.Depots.ToList();
                var depotInventory = _dbContext.DrugUnits.Include(du => du.DrugType).Include(du => du.Depot).ToList();
                return View("Index", depotInventory);
            }

            _depotInventoryService.AssociateDrugs(ref drugUnits, selectedDepot.DepotId, associateStartPickNumber, associateEndPickNumber);
            ViewBag.DrugUnits = drugUnits;
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Disassociate(int disassociateStartPickNumber, int disassociateEndPickNumber)
        {
            var drugUnits = _dbContext.DrugUnits.ToList();
            _depotInventoryService.DisassociateDrugs(ref drugUnits, disassociateStartPickNumber, disassociateEndPickNumber);

            ViewBag.DrugUnits = drugUnits;

            return RedirectToAction("Index");
        }
    }
}