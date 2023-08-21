using System;
using System.Collections.Generic;

namespace JuliaFlorea.DataModel
{
    public class SystemDataSet
    {
        public List<Country> Countries { get; set; }
        public List<Depot> Depots { get; set; }
        public List<DrugUnit> DrugUnits { get; set; }
        public List<DrugType> DrugTypes { get; set; }
        public List<Site> Sites { get; set; }

        public SystemDataSet()
        {
            Countries = new List<Country>();
            Depots = new List<Depot>();
            DrugTypes = new List<DrugType>();
            DrugUnits = new List<DrugUnit>();
            Sites = new List<Site>();

            Countries.Add(new Country("1", "Country 1"));
            Countries.Add(new Country("2", "Country 2"));

            Depots.Add(new Depot("1", "Depot 1", "Address 1", 10000, "1"));
            Depots.Add(new Depot("2", "Depot 2", "Address 2", 15000, "2"));

            DrugTypes.Add(new DrugType("1", "Type 1"));
            DrugTypes.Add(new DrugType("2", "Type 2"));

            for (int i = 1; i <= 10; i++)
            {
                DrugUnits.Add(new DrugUnit(i.ToString(), i, DateTime.Now.AddDays(-i), DateTime.Now.AddMonths(i), "Manufacturer " + i, "1", "1"));
            }

            for (int i = 11; i <= 20; i++)
            {
                DrugUnits.Add(new DrugUnit(i.ToString(), i, DateTime.Now.AddDays(-i), DateTime.Now.AddMonths(i), "Manufacturer " + i, "2", "2"));
            }

            Sites.Add(new Site("1", "Site 1", "1"));
            Sites.Add(new Site("2", "Site 2", "2"));
        }
    }
}