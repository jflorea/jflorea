using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JuliaFlorea.DataModel
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string CountryId { get; set; }
        [Required]
        [Display(Name = "Country Name")]
        public string CountryName { get; set; }
        public virtual Depot Depot { get; set; }
        public virtual ICollection<Site> Sites { get; set; }

        public Country(string countryId, string countryName)
        {
            this.CountryId = countryId;
            this.CountryName = countryName;
            
        }

        public Country()
        {

        }
    }
}