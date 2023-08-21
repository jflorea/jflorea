using JuliaFlorea.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaFlorea.Domain
{
   public  interface ISiteDistributionService
    {
        IEnumerable<DrugUnit> GetRequestedDrugUnits(string siteId, string drugCode, int quantity);
    }
}
