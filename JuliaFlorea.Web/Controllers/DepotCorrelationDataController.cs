using JuliaFlorea.Domain.CorrelationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace JuliaFlorea.Web.Controllers
{
    public class DepotCorrelationDataController : Controller
    {
        private readonly DepotCorrelationService _depotCorrelationService;

        public DepotCorrelationDataController(DepotCorrelationService depotCorrelationService)
        {
            _depotCorrelationService = depotCorrelationService;

        }

        public DepotCorrelationDataController()
        {

        }



        // GET: DepotCorrelationData
        public ActionResult Index()
        {
            List<DepotCorrelationData> depotCorrelationData = _depotCorrelationService.CorrelateData();
            return View(depotCorrelationData);
        }
    }
}