using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using FirstASP.Models;
using System.ComponentModel;

namespace FirstASP.Controllers
{
    public class HomeController : Controller
    {
        // Controllers are responsible for fetching and returning Views according to URIs as well as performing data manipulation via an EF context.

        // A method that will return a View (whether directly or through a redirect) is referred to as an "Action", and will typically return an IActionResult.

        // A data manipulation method will look more similar to our EF practice done with a console application.

        // With the exception of Home/Index (which renders at the root path), pages render at /ControllerName/ActionName (Home/Privacy for this page's Privacy action). 
        // This is also configurable if you don't like this pattern.

        
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // "return View()" essentially means just get the view associated with this path, and send it to the client.
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult TestPage(string item)
        {
            if (!string.IsNullOrWhiteSpace(item))
            {
                People.Add(item);
            }

            ViewBag.Items = People;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public static List<Person> People = new List<Person>();
        public void CreatePerson(string firstName, string lastName)
        {
            People.Add(new Person()
            {
                FirstName = firstName.Trim(),
                LastName = lastName.Trim()
            });
        }

        public void DeletePersonByFirstName(string firstName)
        {
            People.Remove(GetPersonByFirstName(firstName));
        }

        public Person GetPersonByFirstName(string firstName)
        {
            // This assumes nobody's name is duplicated. If it is, it will return null.
            return People.Where(x => x.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper()).SingleOrDefault();
        }
    }
}
