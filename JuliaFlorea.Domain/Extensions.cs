using JuliaFlorea.DataModel;
using System.Collections.Generic;
using System.Linq;

namespace JuliaFlorea.Domain
{
    public static class Extensions
    {
        public static Dictionary<string, List<DrugUnit>> ToGroupedDruGunits(this IList<DrugUnit> drugUnits)
        {
            var groupedByDrugType = drugUnits.GroupBy(drugUnit => drugUnit.DrugTypeId)
                                             .ToDictionary(group => group.Key, group => group.ToList());
            return groupedByDrugType;
        }
    }
}