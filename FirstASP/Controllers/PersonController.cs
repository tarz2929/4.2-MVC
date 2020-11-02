using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FirstASP.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstASP.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Management()
        {
            People.Add(new Person() { FirstName = "Test", LastName = "Tester" });
            People.Add(new Person() { FirstName = "Person", LastName = "Two" });
            ViewBag.People = People;
            return View();
        }

        public static List<Person> People = new List<Person>();

        // These methods are for data management. The body of the methods will be replaced with EF code tomorrow, but for now, we're just using a static list.
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