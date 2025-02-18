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

        public ContactController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        
        [HttpGet]
        public IActionResult GetAllContacts()
        {
            List<Contact> contacts = _unitOfWork.Contact.GetAll().ToList();
            return Ok(contacts);
        }

        
        [HttpGet("{id}")]
        public IActionResult GetContactById(int id)
        {
            var contact = _unitOfWork.Contact.Get(u => u.ContactId == id);
            if (contact == null)
            {
                return NotFound(new { message = "Contact not found" });
            }
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult CreateContact([FromBody] Contact contact)
        {
            if (contact == null || string.IsNullOrWhiteSpace(contact.ContactName))
            {
                return BadRequest(new { message = "Invalid contact data" });
            }

            _unitOfWork.Contact.Add(contact);
            _unitOfWork.Save();
            return CreatedAtAction(nameof(GetContactById), new { id = contact.ContactId }, contact);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateContact(int id, [FromBody] Contact contact)
        {
            if (contact == null)
            {
                return BadRequest(new { message = "Request body is missing" });
            }

            if (id != contact.ContactId)
            {
                return BadRequest(new { message = $"ID mismatch: URL ID ({id}) does not match body ID ({contact.ContactId})" });
            }

            var existingContact = _unitOfWork.Contact.Get(u => u.ContactId == id);
            if (existingContact == null)
            {
                return NotFound(new { message = "Contact not found" });
            }

            _unitOfWork.Contact.Update(contact);
            _unitOfWork.Save();
            return Ok(new { message = "Contact updated successfully" });
        }


        [HttpDelete("{id}")]
        public IActionResult DeleteContact(int id)
        {
            var contactToDelete = _unitOfWork.Contact.Get(u => u.ContactId == id);
            if (contactToDelete == null)
            {
                return NotFound(new { message = "Contact not found" });
            }

            _unitOfWork.Contact.Remove(contactToDelete);
            _unitOfWork.Save();
            return Ok(new { message = "Contact deleted successfully" });
        }

        [HttpGet("withcompanyandcountry")]
        public IActionResult GetContactsWithCompanyAndCountry()
        {
            var contacts = _unitOfWork.Contact.GetAll(includeProperties: "Company,Country").ToList();
            return Ok(contacts);
        }

        [HttpGet("FilterContacts")]
        public IActionResult FilterContacts(int countryId, int companyId)
        {
            List<Contact> contacts = _unitOfWork.Contact
                .GetAll(c => (countryId==0 || c.CountryId == countryId) &&
                            (companyId==0 || c.CompanyId == companyId))
                .ToList();

            if (contacts.Count == 0)
            {
                return NotFound("No contacts found with the given filters.");
            }

            return Ok(contacts);
        }
    }
}
