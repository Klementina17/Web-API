using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicWebAPI.Models
{
    public class Contact
    {

        [Key]
        public int ContactId { get; set; }
        [Required]
        public string ContactName { get; set; }

        public int CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        [ValidateNever]
        public Company Company { get; set; }
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        [ValidateNever]
        public Country Country { get; set; }

    }
}
