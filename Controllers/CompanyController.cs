using BasicWebAPI.Models;
using BasicWebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BasicWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactController> _logger;

        public CompanyController(IUnitOfWork unitOfWork, ILogger<ContactController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            _logger.LogInformation("Fetching all companies.");
            List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
            return Ok(companyList); 
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            _logger.LogInformation("Fetching company with the ID {CompanyId}", id);
            var company = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (company == null)
            {
                _logger.LogWarning("Company with ID {CompanyId} not found.", id);
                return NotFound(new { message = "Company not found" });
            }
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] Company company)
        {
            if (company == null)
            {
                _logger.LogWarning("Invalid company data received!");
                return BadRequest(new { message = "Invalid company data" });
            }

            _unitOfWork.Company.Add(company);
            _unitOfWork.Save();
            _logger.LogInformation("Company created with ID{CompanyId}", company.CompanyId);
            return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyId }, company);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] Company company)
        {
            if (company == null || id != company.CompanyId)
            {
                _logger.LogWarning("Received empty request body for updating company with ID { CompanyId}.", id);
                return BadRequest(new { message = "Invalid company data" });
            }

            var existingCompany = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (existingCompany == null)
            {
                _logger.LogInformation("Company with ID {CompanyId} not found to update.", id);
                return NotFound(new { message = "Company not found" });
            }

            _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
            _logger.LogInformation("Company with ID {CompanyId} updated successfully!", id);
            return Ok(new { message = "Company updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var companyToDelete = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (companyToDelete == null)
            {
                _logger.LogWarning("Company with ID {CompanyId} not found for deletion.", id);
                return NotFound(new { message = "Company not found" });
            }

            _unitOfWork.Company.Remove(companyToDelete);
            _unitOfWork.Save();
            _logger.LogInformation("Company with ID {CompanyId} deleted successfully!", id);
            return Ok(new { message = "Company deleted successfully" });
        }
    }
}
