using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            Debug.WriteLine("ACTION - Index Action");

            return RedirectToAction("Management");
        }

        public IActionResult Management()
        {
            Debug.WriteLine("ACTION - Management Action");
            ViewBag.People = GetPeople();
            return View();
        }

        public IActionResult Create(string firstName, string lastName, string dateOfBirth)
        {
            Debug.WriteLine("ACTION - Create Action");

            CreatePerson(firstName, lastName, dateOfBirth);
            return RedirectToAction("Management");
        }

        public IActionResult Delete(string firstName)
        {
            Debug.WriteLine("ACTION - Delete Action");

            DeletePersonByFirstName(firstName);
            return RedirectToAction("Management");
        }

        // These methods are for data management. The body of the methods will be replaced with EF code tomorrow, but for now, we're just using a static list.
        public void CreatePerson(string firstName, string lastName, string dateOfBirth)
        {
            Debug.WriteLine($"DATA - CreatePerson({firstName}, {lastName})");

            using (PersonContext context = new PersonContext())
            {
                context.People.Add(new Person()
                {
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    DateOfBirth = DateTime.Parse(dateOfBirth.Trim())
                });
            }
        }
        public void DeletePersonByFirstName(string firstName)
        {
            Debug.WriteLine($"DATA - DeletePersonByFirstName({firstName})");

            using (PersonContext context = new PersonContext())
            {
                context.People.Remove(GetPersonByFirstName(firstName));
            }
        }

        public Person GetPersonByFirstName(string firstName)
        {
            Debug.WriteLine($"DATA - GetPersonByFirstName({firstName})");
            Person found;
            using (PersonContext context = new PersonContext())
            {
                found = context.People.Where(x => x.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper()).SingleOrDefault();
            }
            return found;
        }

        public List<Person> GetPeople()
        {
            List<Person> all;
            using (PersonContext context = new PersonContext())
            {
                all = context.People.ToList();
            }
            return all;
        }
    }
}