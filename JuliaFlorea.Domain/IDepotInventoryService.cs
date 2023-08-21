using JuliaFlorea.DataModel;
using System.Collections.Generic;

namespace JuliaFlorea.Domain
{
    internal interface IDepotInventoryService
    {
        void AssociateDrugs(ref List<DrugUnit> drugUnits, string depotId, int startPickNumber, int endPickNumber);

        void DisassociateDrugs(ref List<DrugUnit> drugUnits, int startPickNumber, int endPickNumber);
    }
}