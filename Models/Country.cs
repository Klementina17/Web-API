using System.ComponentModel.DataAnnotations;

namespace BasicWebAPI.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        [Required]
        public string CountryName { get; set; }
    }
}
