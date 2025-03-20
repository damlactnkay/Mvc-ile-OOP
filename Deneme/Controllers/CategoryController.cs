using BusinessLayer.Concrete;
using BusinessLayer.FluentValidation;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebSite.Controllers
{
    public class CategoryController : Controller
    {
        CategoryManager categoryManager = new CategoryManager (new EfCategoryDal());
        public IActionResult Index()
        {
            var values = categoryManager.GetList();
            return View(values);
        }
        [HttpGet]
        public IActionResult AddCategory()
        {

            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            CategoryValidator validationRules = new CategoryValidator();
            ValidationResult results = validationRules.Validate(p);
            if (results.IsValid)
            {

                categoryManager.TInsert(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in results.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();
        }
        public IActionResult DeleteCategory(int id)
        {
            var value = categoryManager.GetById(id);
            categoryManager.TDelete(value);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var value = categoryManager.GetById(id);
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category p)
        {
            // var value = productManager.GetById(p.ProductId);
            categoryManager.TUpdate(p);
            return RedirectToAction("Index");
        }
    }
}

