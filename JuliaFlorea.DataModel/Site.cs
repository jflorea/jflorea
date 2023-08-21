using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuliaFlorea.DataModel
{
    public class Site
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string SiteId { get; set; }

        [Required]
        [Display(Name = "Site Name")]
        public string SiteName { get; set; }

        [ForeignKey("Country")]
        public string CountryCode { get; set; }

        public virtual Country Country { get; set; }


        public Site(string siteId, string siteName, string countryCode)
        {
            this.SiteId = siteId;
            this.SiteName = siteName;
            this.CountryCode = countryCode;
        }

        public Site()
        {

        }
    }
}
