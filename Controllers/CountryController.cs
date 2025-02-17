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
    public class CountryController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CountryController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Country> CountryList = unitOfWork.Country.GetAll().ToList();

            return View(CountryList);
        }

        [HttpGet("upsert/{id?}")]
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                //create
                return View(new Country());
            }
            else
            {
                //update
                Country countryObj = unitOfWork.Country.Get(u => u.CountryId == id);
                return View(countryObj);
            }

        }

        [HttpPost("upsert/{id?}")]
        public IActionResult Upsert(Country CountryObj)
        {
            if (ModelState.IsValid)
            {
                if (CountryObj.CountryId == 0)
                {
                    unitOfWork.Country.Add(CountryObj);
                }
                else
                {

                    unitOfWork.Country.Update(CountryObj);
                }

                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(CountryObj);
            }

        }

        #region API CALLS
        [HttpGet("getall")]
        public IActionResult GetAllCountries()
        {
            List<Country> objCountryList = unitOfWork.Country.GetAll().ToList();
            return Json(new { data = objCountryList });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            Console.WriteLine(id);

            var CountryToBeDeleted = unitOfWork.Country.Get(u => u.CountryId == id);


            if (CountryToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            unitOfWork.Country.Remove(CountryToBeDeleted);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful " });

        }
        #endregion

    }
}