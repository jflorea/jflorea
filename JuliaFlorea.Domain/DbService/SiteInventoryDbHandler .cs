using JuliaFlorea.DataModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace JuliaFlorea.Domain.DbService
{
    public class SiteInventoryDbHandler : ISiteInventoryDbHandler
    {


        private readonly AppDbContext _dbContext;
        private readonly ISiteDistributionService _siteDistributionService;

        public SiteInventoryDbHandler(AppDbContext dbContext, ISiteDistributionService siteDistributionService)
        {
            _dbContext = dbContext;
            _siteDistributionService = siteDistributionService;
        }

    

        public void UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity)
        {
            var requestedDrugUnits = _siteDistributionService.GetRequestedDrugUnits(destinationSiteId, requestedDrugCode, requestedQuantity);

            var site = _dbContext.Sites.Include(s => s.Country).FirstOrDefault(s => s.SiteId == destinationSiteId);
            if (site == null || site.Country == null)
            {
                return;
            }

            var depot = site.Country.Depot;
           
            foreach (var drugUnit in requestedDrugUnits)
            {
                drugUnit.DepotId = depot.DepotId;
                drugUnit.Depot = depot;
                _dbContext.DrugUnits.Update(drugUnit);
            }
           
            _dbContext.SaveChanges();
        }
    }
}