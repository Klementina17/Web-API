using System.ComponentModel.DataAnnotations;

namespace BasicWebAPI.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        [Required]
        public string CompanyName { get; set; }

        internal static Dictionary<string, int> ToDictionary(Func<object, object> value1, Func<object, object> value2)
        {
            throw new NotImplementedException();
        }
    }
}
