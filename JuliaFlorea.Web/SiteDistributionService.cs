using JuliaFlorea.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaFlorea.Domain
{
   public class SiteDistributionService : ISiteDistributionService
    {
        private readonly AppDbContext _dbContext;

        public SiteDistributionService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<DrugUnit> GetRequestedDrugUnits(string siteId, string drugCode, int quantity)
        {
           
            Site site = _dbContext.Sites.FirstOrDefault(s => s.SiteId == siteId);
            if(site == null)
            {
                return Enumerable.Empty<DrugUnit>();
            }
            Depot depot = _dbContext.Depots.FirstOrDefault(d => d.CountryId == site.CountryCode);
            if(depot == null)
            {
                return Enumerable.Empty<DrugUnit>();
            }

            DrugType drugType = _dbContext.DrugTypes.FirstOrDefault(dt => dt.DrugTypeId == drugCode);
            if(drugType == null)
            {
                return Enumerable.Empty<DrugUnit>();
            }

            var requestedDrugUnits = _dbContext.DrugUnits.Where(du => du.DepotId == depot.DepotId && du.DrugTypeId == drugType.DrugTypeId)
                                                      .Take(quantity);
            return requestedDrugUnits;


        }
    }
}
