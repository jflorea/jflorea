using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuliaFlorea.DataModel
{
    public class DrugUnit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DrugUnitId { get; set; }

        [Required]
        [Display(Name = "Pick Number")]
        public int PickNumber { get; set; }

        [Display(Name = "Manufacturing Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ManufacturingDate { get; set; }

        [Display(Name = "Expiry Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpiryDate { get; set; }

        [Display(Name = "Manufacturer")]
        public string Manufacturer { get; set; }

        public string DepotId { get; set; }

        public virtual Depot Depot { get; set; }

        public string DrugTypeId { get; set; }

        public virtual DrugType DrugType { get; set; }

        public DrugUnit(string drugUnitId, int pickNumber, DateTime manufacturingDate, DateTime expiryDate, string manufacturer,string depotId, string drugTypeId)
        {
            this.DrugUnitId = drugUnitId;
            this.PickNumber = pickNumber;
            this.ManufacturingDate = manufacturingDate;
            this.ExpiryDate = expiryDate;
            this.Manufacturer = manufacturer;
            this.DepotId = depotId;
            this.DrugTypeId = drugTypeId;
        }

        public DrugUnit()
        {

        }
    }
}