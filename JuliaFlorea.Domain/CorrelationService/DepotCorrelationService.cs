using JuliaFlorea.DataModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace JuliaFlorea.Domain.CorrelationService
{
    public class DepotCorrelationService : BaseCorrelationService<List<DepotCorrelationData>>
    {
        private readonly AppDbContext _dbContext;

        public DepotCorrelationService(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override List<DepotCorrelationData> CorrelateData()
        {
            var correlationData = new List<DepotCorrelationData>();

            var depotsWithRelatedData = _dbContext.Depots
                .Include(depot => depot.Country)
                .Include(depot => depot.DrugUnits)
                .ToList();

            foreach (var depot in depotsWithRelatedData)
            {
                var country = depot.Country;
                var drugUnits = depot.DrugUnits.ToList();

                foreach (var drugUnit in drugUnits)
                {
                    var drugType = _dbContext.DrugTypes.FirstOrDefault(dt => dt.DrugTypeId == drugUnit.DrugTypeId);
                    correlationData.Add(new DepotCorrelationData
                    {
                        DepotName = depot.DepotName,
                        CountryName = country?.CountryName,
                        DrugUnitId = drugUnit.DrugUnitId,
                        DrugTypeName = drugType?.DrugTypeName,
                        PickNumber = drugUnit.PickNumber
                    });
                }

                if (drugUnits.Count == 0)
                {
                    correlationData.Add(new DepotCorrelationData
                    {
                        DepotName = depot.DepotName,
                        CountryName = country?.CountryName,
                        DrugUnitId = "No drug unit associated to this depot",
                        DrugTypeName = "No drug type associated",
                        PickNumber = 0
                    });
                }
            }

            return correlationData;
        }

    }
}