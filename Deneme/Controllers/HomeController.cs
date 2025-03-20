using DemoWebSite.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoWebSite.Controllers
{

    public class HomeController : Controller
    {
 
        public IActionResult Index()
        {
            return View(); 
        }

      
        public IActionResult About()
        {
            ViewBag.Message = "Hakkýmýzda...";
            return View(); 
        }

     
        public IActionResult Contact()
        {
            ViewData["Email"] = "info@example.com";
            return View(); 
        }
    }

}
