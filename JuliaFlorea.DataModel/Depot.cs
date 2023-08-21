using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuliaFlorea.DataModel
{
    public class Depot
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DepotId { get; set; }

        [Required]
        [Display(Name = "Depot Name")]
        public string DepotName { get; set; }

        [Display(Name = "Depot Address")]
        public string DepotAddress { get; set; }

        [Display(Name = "Storage Capacity")]
        public int StorageCapacity { get; set; }

        public string CountryId { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<DrugUnit> DrugUnits { get; set; }

        public Depot(string depotId, string depotName, string depotAddress, int storageCapacity, string countryId)
        {
            this.DepotId = depotId;
            this.DepotName = depotName;
            this.DepotAddress = depotAddress;
            this.StorageCapacity = storageCapacity;
            this.CountryId = countryId;
        }

        public Depot()
        {
        }
    }
}