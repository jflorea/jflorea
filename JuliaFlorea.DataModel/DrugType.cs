using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuliaFlorea.DataModel
{
    public class DrugType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string DrugTypeId { get; set; }

        [Required]
        [Display(Name = "Drug Type Name")]
        public string DrugTypeName { get; set; }

        [Display(Name = "Weight")]
        public double WeightInPounds { get; set; }

        public virtual ICollection<DrugUnit> DrugUnits { get; set; }

        public DrugType(string drugTypeId, string drugTypeName)
        {
            this.DrugTypeId = drugTypeId;
            this.DrugTypeName = drugTypeName;
        }

        public DrugType()
        {

        }
    }
}