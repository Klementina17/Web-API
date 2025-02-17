using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BasicWebAPI.Models;
using BasicWebAPI.Repository.IRepository;
using BasicWebAPI.Repository;

namespace BasicWebAPI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public IActionResult Index()
    {
        IEnumerable<Contact> contactList = _unitOfWork.Contact.GetAll();
        return View(contactList);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    #region API CALLS
    [HttpGet("/contacts/getall")]
    public IActionResult GetAllContacts()
    {
        List<Contact> objContactList =_unitOfWork.Contact.GetAll().ToList();
        return Json(new { data = objContactList });
    }

    [HttpDelete]
    [Route("delete/{id}")]
    public IActionResult Delete(int? id)
    {
        Console.WriteLine(id);

        var ContactToBeDeleted = _unitOfWork.Contact.Get(u => u.ContactId == id);


        if (ContactToBeDeleted == null)
        {
            return Json(new { success = false, message = "Error while deleting" });
        }

        _unitOfWork.Contact.Remove(ContactToBeDeleted);
        _unitOfWork.Save();
        return Json(new { success = true, message = "Delete Successful " });

    }
    #endregion

}
