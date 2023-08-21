using JuliaFlorea.DataModel;
using JuliaFlorea.Domain;
using JuliaFlorea.Domain.CorrelationService;
using JuliaFlorea.Domain.DbService;
using System;
using System.Collections.Generic;
using System.Linq;

namespace JuliaFlorea
{
    internal class Program
    {
        //private static SystemDataSet dataSet;

        private static void Main(string[] args)
        {
            AppDbContext dbContext = new AppDbContext();
            DepotInventoryService depotInventoryService = new DepotInventoryService(dbContext);
            DepotCorrelationService depotCorrelationService = new DepotCorrelationService(dbContext);
            SiteDistributionService siteDistributionService = new SiteDistributionService(dbContext);
            SiteInventoryDbHandler siteInventoryDbHandler = new SiteInventoryDbHandler(dbContext, siteDistributionService);
            var drugUnits = dbContext.DrugUnits.ToList();
            var groupedByDrugType = drugUnits.ToGroupedDruGunits();
            var correlationData = depotCorrelationService.CorrelateData();
            var requestedDrugUnits = siteDistributionService.GetRequestedDrugUnits("2", "2", 3);

            //AddEntities(dbContext);
            DisplayCountries(dbContext);
            DisplayDepots(dbContext);
            DisplayDrugTypes(dbContext);
            DisplayDrugUnits(dbContext);
            depotInventoryService.AssociateDrugs(ref drugUnits, "1", 1, 10);
            depotInventoryService.DisassociateDrugs(ref drugUnits, 15, 20);
            DisplayGroupedByDrugType(groupedByDrugType);
            DisplayCorrelationData(correlationData);
            DisplayRequestedDrugUnits(requestedDrugUnits);
            siteInventoryDbHandler.UpdateSiteInventory("2", "2", 3);
        }

        private static void DisplayCountries(AppDbContext dbContext)
        {
            Console.WriteLine("Countries:");
            Console.WriteLine();

            var countries = dbContext.Countries.ToList();
            foreach (var country in countries)
            {
                Console.WriteLine($"Country Id: {country.CountryId}, Name: {country.CountryName}");
            }

            Console.WriteLine();
        }

        private static void DisplayDepots(AppDbContext dbContext)
        {
            Console.WriteLine("Depots:");
            Console.WriteLine();
            var depots = dbContext.Depots.ToList();
            foreach (var depot in depots)
            {
                Console.WriteLine($"Depot Id: {depot.DepotId}, Name: {depot.DepotName}, Address: {depot.DepotAddress},  Storage Capacity: {depot.StorageCapacity}, Country Id: {depot.CountryId}");
            }
            Console.WriteLine();
        }

        private static void DisplayDrugTypes(AppDbContext dbContext)
        {
            Console.WriteLine("Drug Types:");
            Console.WriteLine();
            var drugTypes = dbContext.DrugTypes.ToList();
            foreach (var drugType in drugTypes)
            {
                Console.WriteLine($"Drug Type Id: {drugType.DrugTypeId}, Name: {drugType.DrugTypeName}");
            }
            Console.WriteLine();
        }

        private static void DisplayDrugUnits(AppDbContext dbContext)
        {
            Console.WriteLine("Drug Units:");
            var drugUnits = dbContext.DrugUnits.ToList();
            foreach (var drugUnit in drugUnits)
            {
                Console.WriteLine($"Drug Unit Id: {drugUnit.DrugUnitId}, Pick Number: {drugUnit.PickNumber},  Manufacturing Date: {drugUnit.ManufacturingDate}, Expiry Date: {drugUnit.ExpiryDate},  Manufacturer: {drugUnit.Manufacturer}");
            }
            Console.WriteLine();
        }

        private static void DisplayGroupedByDrugType(Dictionary<string, List<DrugUnit>> groupedByDrugType)
        {
            Console.WriteLine("\nDrug Units Grouped by Drug Type:");
            foreach (var keyValuePair in groupedByDrugType)
            {
                string drugType = keyValuePair.Key;
                List<DrugUnit> drugUnits = keyValuePair.Value;

                Console.WriteLine($"\nDrug Type: {drugType}");
                foreach (var drugUnit in drugUnits)
                {
                    Console.WriteLine($"\n - Drug Unit Id: {drugUnit.DrugUnitId}, Pick Number: {drugUnit.PickNumber},  Manufacturing Date: {drugUnit.ManufacturingDate}, Expiry Date: {drugUnit.ExpiryDate},  Manufacturer: {drugUnit.Manufacturer}");
                }
                Console.WriteLine();
            }
        }

        private static void DisplayCorrelationData(List<DepotCorrelationData> correlationData)
        {
            if (correlationData == null || correlationData.Count == 0)
            {
                Console.WriteLine("No correlation data found.");
                Console.WriteLine();
                return;
            }

            Console.WriteLine("Depot Correlation Data:");
            Console.WriteLine();
            foreach (var data in correlationData)
            {
                Console.WriteLine($"Depot Name: {data.DepotName}");
                Console.WriteLine($"Country Name: {data.CountryName}");
                Console.WriteLine($"Drug Type Name: {data.DrugTypeName}");
                Console.WriteLine($"Drug Unit ID: {data.DrugUnitId}");
                Console.WriteLine($"Pick Number: {data.PickNumber}");
                Console.WriteLine();
            }
        }

        private static void DisplayRequestedDrugUnits(IEnumerable<DrugUnit> drugUnits)
        {
            Console.WriteLine("\nDrug Units In The Requested Quantity : ");

            foreach (var drugUnit in drugUnits)
            {
                Console.WriteLine($"\nDrug Unit Id: {drugUnit.DrugUnitId}, Drug Type Id: {drugUnit.DrugTypeId}, Depot Id: {drugUnit.DepotId}, Pick Number: {drugUnit.PickNumber},  Manufacturing Date: {drugUnit.ManufacturingDate}, Expiry Date: {drugUnit.ExpiryDate},  Manufacturer: {drugUnit.Manufacturer}");
            }
        }

        private static void AddEntities(AppDbContext dbContext)
        {
            Country country1 = new Country("3", "Country 3");
            Country country2 = new Country("4", "Country 4");
            dbContext.Countries.Add(country1);
            dbContext.Countries.Add(country2);

            Depot depot1 = new Depot("3", "Depot 3", "Address 3", 10000, "3");
            Depot depot2 = new Depot("4", "Depot 2", "Address 2", 15000, "4");
            dbContext.Depots.Add(depot1);
            dbContext.Depots.Add(depot2);

            DrugType drugType1 = new DrugType("3", "Type 3");
            DrugType drugType2 = new DrugType("4", "Type 4");
            dbContext.DrugTypes.Add(drugType1);
            dbContext.DrugTypes.Add(drugType2);

            for (int i = 3; i <= 10; i++)
            {
                dbContext.DrugUnits.Add(new DrugUnit(i.ToString(), i, DateTime.Now.AddDays(-i), DateTime.Now.AddMonths(i), "Manufacturer " + i, "1", "1"));
            }

            for (int i = 11; i <= 20; i++)
            {
                dbContext.DrugUnits.Add(new DrugUnit(i.ToString(), i, DateTime.Now.AddDays(-i), DateTime.Now.AddMonths(i), "Manufacturer " + i, "2", "2"));
            }

            Site site1 = new Site("3", "Site 3", "3");
            Site site2 = new Site("4", "Site 4", "4");
            dbContext.Sites.Add(site1);
            dbContext.Sites.Add(site2);

            dbContext.SaveChanges();
        }
    }
}