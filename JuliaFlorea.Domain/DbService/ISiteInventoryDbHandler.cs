﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaFlorea.Domain.DbService
{
   public  interface ISiteInventoryDbHandler
    {
        void UpdateSiteInventory(string destinationSiteId, string requestedDrugCode, int requestedQuantity);
    }
}
