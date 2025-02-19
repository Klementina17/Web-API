using Microsoft.AspNetCore.Mvc;
using BasicWebAPI.Repository.IRepository;
using BasicWebAPI.Models;

namespace BasicWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase 
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ContactController> _logger;

        public ContactController(IUnitOfWork unitOfWork,ILogger<ContactController> logger)
        {
            _unitOfWork = unitOfWork;
            _logger=logger;
        }

        
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            _logger.LogInformation("Fetching all contacts");
            List<Contact> contacts = _unitOfWork.Contact.GetAll().ToList();
            return Ok(contacts);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            _logger.LogInformation("Fetching contact with the ID {ContactId}", id);
            var contact = _unitOfWork.Contact.Get(u => u.ContactId == id);
            if (contact == null)
            {
                _logger.LogWarning("Contact with ID {ContactId} not found.", id);
                return NotFound(new { message = "Contact not found" });
            }
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] Contact contact)
        {
            if (contact == null || string.IsNullOrWhiteSpace(contact.ContactName))
            {
                _logger.LogWarning("Invalid contact data received!");
                return BadRequest(new { message = "Invalid contact data" });
            }

            _unitOfWork.Contact.Add(contact);
            _unitOfWork.Save();
            _logger.LogInformation("Contact created with ID{ContacId}", contact.ContactId);
            return CreatedAtAction(nameof(GetContactById), new { id = contact.ContactId }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null)
            {
                _logger.LogWarning("Received empty request body for updating contact with ID { ContactId}.", id);
                return BadRequest(new { message = "Request body is missing" });
            }

            if (id != contact.ContactId)
            {
                _logger.LogWarning("ID mismatch: URL ID ({UrlId}) does not match body ID ({BodyId}).", id, contact.ContactId);
                return BadRequest(new { message = $"ID mismatch: URL ID ({id}) does not match body ID ({contact.ContactId})" });
            }

            var existingContact = _unitOfWork.Contact.Get(u => u.ContactId == id);
            if (existingContact == null)
            {
                _logger.LogInformation("Contact with ID {ContactId} not found to update.", id);
                return NotFound(new { message = "Contact not found" });
            }

            _unitOfWork.Contact.Update(contact);
            _unitOfWork.Save();
            _logger.LogInformation("Contact with ID {ContactId} updated successfully!", id);
            return Ok(new { message = "Contact updated successfully" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contactToDelete = _unitOfWork.Contact.Get(u => u.ContactId == id);
            if (contactToDelete == null)
            {
                _logger.LogWarning("Contact with ID {ContactId} not found for deletion.", id);
                return NotFound(new { message = "Contact not found" });
            }

            _unitOfWork.Contact.Remove(contactToDelete);
            _unitOfWork.Save();
            _logger.LogInformation("Contact with ID {ContactId} deleted successfully!", id);
            return Ok(new { message = "Contact deleted successfully" });
        }

        [HttpGet("withcompanyandcountry")]
        public IActionResult GetContactsWithCompanyAndCountry()
        {
            _logger.LogInformation("Fetching contacts with company and country details.");
            var contacts = _unitOfWork.Contact.GetAll(includeProperties: "Company,Country").ToList();
            return Ok(contacts);
        }

        [HttpGet("FilterContacts")]
        public IActionResult FilterContacts(int countryId, int companyId)
        {
            _logger.LogInformation("FilterContacts endpoint called with countryId: {CountryId}, companyId: {CompanyId}", countryId, companyId);
            List<Contact> contacts = _unitOfWork.Contact
                .GetAll(c => (countryId==0 || c.CountryId == countryId) &&
                            (companyId==0 || c.CompanyId == companyId))
                .ToList();

            if (contacts.Count == 0)
            {
                _logger.LogWarning("No contacts found with the given filters. countryId: {CountryId}, companyId: {CompanyId}", countryId, companyId);
                return NotFound("No contacts found with the given filters.");
            }
            _logger.LogInformation("{Count} contacts found with the given filters.", contacts.Count);
            return Ok(contacts);
        }
    }
}
