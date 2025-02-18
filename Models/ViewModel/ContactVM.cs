using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BasicWebAPI.Models.ViewModel
{
    public class ContactVM
    {
        public Contact Contact { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> CompanyList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }

    }
}
