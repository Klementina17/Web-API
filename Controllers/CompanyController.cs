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
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            List<Company> CompanyList = unitOfWork.Company.GetAll().ToList();

            return View(CompanyList);
        }

        [HttpGet("upsert/{id?}")]
        public IActionResult Upsert(int? id)
        {

            if (id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = unitOfWork.Company.Get(u => u.CompanyId == id);
                return View(companyObj);
            }

        }

        [HttpPost("upsert/{id?}")]
        public IActionResult Upsert(Company CompanyObj)
        {
            if (ModelState.IsValid)
            {
                if (CompanyObj.CompanyId == 0)
                {
                    unitOfWork.Company.Add(CompanyObj);
                }
                else
                {

                    unitOfWork.Company.Update(CompanyObj);
                }

                unitOfWork.Save();
                return RedirectToAction("Index");
            }
            else
            {
                return View(CompanyObj);
            }

        }

        #region API CALLS
        [HttpGet("getall")]
        public IActionResult GetAllCompanies()
        {
            List<Company> objCompanyList = unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult Delete(int? id)
        {
            Console.WriteLine(id);

            var companyToBeDeleted = unitOfWork.Company.Get(u => u.CompanyId == id);

            
            if (companyToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            unitOfWork.Company.Remove(companyToBeDeleted);
            unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful " });

        }
        #endregion

    }
}