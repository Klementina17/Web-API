using Microsoft.AspNetCore.Mvc;
using BasicWebAPI.Repository.IRepository;
using BasicWebAPI.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BasicWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CountryController> _logger;

        public CountryController(IUnitOfWork unitOfWork,ILogger<CountryController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCountries()
        {
            _logger.LogInformation("Fetching all countries.");
            List<Country> countries = _unitOfWork.Country.GetAll().ToList();
            return Ok(countries);
        }

        [HttpGet("{id}")]
        public IActionResult GetCountryById(int id)
        {
            _logger.LogInformation("Fetching country with the ID {CountryId}", id);
            var country = _unitOfWork.Country.Get(u => u.CountryId == id);
            if (country == null)
            {
                _logger.LogWarning("Country with ID {CountryId} not found.", id);
                return NotFound(new { message = "Country not found" });
            }
            return Ok(country);
        }

        [HttpPost]
        public IActionResult CreateCountry([FromBody] Country country)
        {
            if (country == null)
            {
                _logger.LogWarning("Invalid country data received!");
                return BadRequest(new { message = "Invalid country data" });
            }

            _unitOfWork.Country.Add(country);
            _unitOfWork.Save();
            _logger.LogInformation("Country created with ID{CountryId}", country.CountryId);
            return CreatedAtAction(nameof(GetCountryById), new { id = country.CountryId }, country);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCountry(int id, [FromBody] Country country)
        {
            if (country == null || id != country.CountryId)
            {
                _logger.LogWarning("Received empty request body for updating country with ID {CountryId}.", id);
                return BadRequest(new { message = "Invalid country data" });
            }

            var existingCountry = _unitOfWork.Country.Get(u => u.CountryId == id);
            if (existingCountry == null)
            {
                _logger.LogInformation("Country with ID {CountryId} not found to update.", id);
                return NotFound(new { message = "Country not found" });
            }

            _unitOfWork.Country.Update(country);
            _unitOfWork.Save();
            _logger.LogInformation("Country with ID {CountryId} updated successfully!", id);
            return Ok(new { message = "Country updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCountry(int id)
        {
            var countryToDelete = _unitOfWork.Country.Get(u => u.CountryId == id);
            if (countryToDelete == null)
            {
                _logger.LogWarning("Country with ID {CountryId} not found for deletion.", id);
                return NotFound(new { message = "Country not found" });
            }

            _unitOfWork.Country.Remove(countryToDelete);
            _unitOfWork.Save();
            _logger.LogInformation("Country with ID {CountryId} deleted successfully!", id);
            return Ok(new { message = "Country deleted successfully" });
        }
    }
}
