using JuliaFlorea.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JuliaFlorea.Domain
{
    public class DepotInventoryService : IDepotInventoryService
    {
        private readonly AppDbContext _dbContext;

        public DepotInventoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AssociateDrugs(ref List<DrugUnit> drugUnits, string depotId, int startPickNumber, int endPickNumber)
        {
            Depot depot = _dbContext.Depots.FirstOrDefault(d => d.DepotId == depotId);
            if (depotId == null)
            {
                Console.WriteLine($"Depot with Id {depotId} not found.");
                return;
            }
            foreach (var drugUnit in drugUnits)
            {
                if (drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                {
                    drugUnit.DepotId = depot.DepotId;
                }
            }

           // Console.WriteLine($"\nAssociated drug units between pick numbers {startPickNumber} and {endPickNumber} with depot {depot.DepotName}.");
            _dbContext.SaveChanges();
        }

        public void DisassociateDrugs(ref List<DrugUnit> drugUnits, int startPickNumber, int endPickNumber)
        {
            foreach (var drugUnit in drugUnits)
            {
                if (drugUnit.PickNumber >= startPickNumber && drugUnit.PickNumber <= endPickNumber)
                {
                    drugUnit.DepotId = null;
                }
            }

            //Console.WriteLine($"\nDisassociated drug units between pick numbers {startPickNumber} and {endPickNumber} from any depot.");
            _dbContext.SaveChanges();
        }
    }
}