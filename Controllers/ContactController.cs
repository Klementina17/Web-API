using BasicWebAPI.Data;
using BasicWebAPI.Models;
using BasicWebAPI.Repository;
using BasicWebAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BasicWebAPI.Controllers
{
    [Route("api/[controller]")]
    public class ContactController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public ContactController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Contact> ContactList = unitOfWork.Contact.GetAll().ToList();

            return View(ContactList);
        }

        [HttpGet("upsert/{id?}")]
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                //create
                return View(new Contact());
            }
            else
            {
                //update
                Contact ContactObj = unitOfWork.Contact.Get(u => u.ContactId == id);
                return View(ContactObj);
            }

        }

        [HttpPost("upsert/{id?}")]
        public IActionResult Upsert(Contact ContactObj)
        {
            if (ModelState.IsValid)
            {
                if (ContactObj.ContactId == 0)
                {
                    unitOfWork.Contact.Add(ContactObj);
                }
                else
                {

                    unitOfWork.Contact.Update(ContactObj);
                }

                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(ContactObj);
            }

        }

        #region API CALLS
        [HttpGet("getall")]
        public IActionResult GetAllContacts()
        {
            List<Contact> objContactList = unitOfWork.Contact.GetAll().ToList();
            return Json(new { data = objContactList });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            Console.WriteLine(id);

            var ContactToBeDeleted = unitOfWork.Contact.Get(u => u.ContactId == id);

            
            if (ContactToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            unitOfWork.Contact.Remove(ContactToBeDeleted);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful " });

        }

        [HttpGet("/contacts/getcontactswithcompanyandcountry")]
        public IActionResult GetContactsWithCompanyAndCountry()
        {
            List<Contact> contacts = unitOfWork.Contact.GetAll(includeProperties: "Company,Country").ToList();

            return Json(new { data = contacts });
        }

        #endregion

    }
}