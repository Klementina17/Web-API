using Microsoft.AspNetCore.Mvc;
using BasicWebAPI.Repository.IRepository;
using BasicWebAPI.Models;

namespace BasicWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase 
    {
        private readonly IUnitOfWork _unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            List<Country> countries = _unitOfWork.Country.GetAll().ToList();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            var country = _unitOfWork.Country.Get(u => u.CountryId == id);
            if (country == null)
            {
                return NotFound(new { message = "Country not found" });
            }
            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] Country country)
        {
            if (country == null)
            {
                return BadRequest(new { message = "Invalid country data" });
            }

            _unitOfWork.Country.Add(country);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetCountryById), new { id = country.CountryId }, country);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] Country country)
        {
            if (country == null || id != country.CountryId)
            {
                return BadRequest(new { message = "Invalid country data" });
            }

            var existingCountry = _unitOfWork.Country.Get(u => u.CountryId == id);
            if (existingCountry == null)
            {
                return NotFound(new { message = "Country not found" });
            }

            _unitOfWork.Country.Update(country);
            _unitOfWork.Save();
            return Ok(new { message = "Country updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var countryToDelete = _unitOfWork.Country.Get(u => u.CountryId == id);
            if (countryToDelete == null)
            {
                return NotFound(new { message = "Country not found" });
            }

            _unitOfWork.Country.Remove(countryToDelete);
            _unitOfWork.Save();
            return Ok(new { message = "Country deleted successfully" });
        }
    }
}
