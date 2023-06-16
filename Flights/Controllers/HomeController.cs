using Flights.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Flights.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(Login p)
        {
            if(p.username.ToString()=="admin@gmail.com" && p.password.ToString()=="Admin@123")
            {
                HttpContext.Session.SetString("currentUser",p.username);
                return RedirectToAction("Dashboard","Flight");
            }
            else if(p.username.ToString() == "admin@gmail.com" && p.password.ToString() != "Admin@123")
            {
                ViewBag.Message = "Check the password";
                return View();
            }
            else if(p.username.ToString() != "admin@gmail.com" && p.password.ToString() == "Admin@123")
            {

                ViewBag.Message= "Check the username";
                return View();

            }
            else
            {
                ViewBag.Message = "Enter the correct email id and password";
                return View();
            }
        }
       
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("currentUser");
            return RedirectToAction("Login");
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
    }
}