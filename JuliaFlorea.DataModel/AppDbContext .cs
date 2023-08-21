using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaFlorea.DataModel
{
   public class AppDbContext : DbContext
    {

        public DbSet<Country> Countries { get; set; }
        public DbSet<Depot> Depots { get; set; }
        public DbSet<DrugType> DrugTypes { get; set; }
        public DbSet<DrugUnit> DrugUnits { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionStringFromConfig());
        }

        private static string GetConnectionStringFromConfig()
        {
            return ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }
    }
}
