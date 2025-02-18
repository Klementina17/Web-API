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

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            List<Company> companyList = _unitOfWork.Company.GetAll().ToList();
            return Ok(companyList); 
        }

        [HttpGet("{id}")]
        public IActionResult GetCompany(int id)
        {
            var company = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (company == null)
            {
                return NotFound(new { message = "Company not found" });
            }
            return Ok(company);
        }

        [HttpPost]
        public IActionResult CreateCompany([FromBody] Company company)
        {
            if (company == null)
            {
                return BadRequest(new { message = "Invalid company data" });
            }

            _unitOfWork.Company.Add(company);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetCompany), new { id = company.CompanyId }, company);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCompany(int id, [FromBody] Company company)
        {
            if (company == null || id != company.CompanyId)
            {
                return BadRequest(new { message = "Invalid company data" });
            }

            var existingCompany = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (existingCompany == null)
            {
                return NotFound(new { message = "Company not found" });
            }

            _unitOfWork.Company.Update(company);
            _unitOfWork.Save();
            return Ok(new { message = "Company updated successfully" });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCompany(int id)
        {
            var companyToDelete = _unitOfWork.Company.Get(u => u.CompanyId == id);
            if (companyToDelete == null)
            {
                return NotFound(new { message = "Company not found" });
            }

            _unitOfWork.Company.Remove(companyToDelete);
            _unitOfWork.Save();
            return Ok(new { message = "Company deleted successfully" });
        }
    }
}
