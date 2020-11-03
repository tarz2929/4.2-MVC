using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using FirstASP.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstASP.Controllers
{
    public class PersonController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Management");
        }

        public IActionResult Management()
        {
           ViewBag.People = GetPeople();
            return View();
        }

        public IActionResult Details(string id)
        {
            try
            {
                ViewBag.Person = GetPersonByID(id);
            }
            catch
            {

            }
            return View();
        }

        public IActionResult AddEmail(string id, string address)
        {
            AddEmailToPersonByID(id, address);
            return RedirectToAction("Details", new Dictionary<string, string>() { { "id", id } });
        }

        public IActionResult Create(string firstName, string lastName, string dateOfBirth)
        {
            CreatePerson(firstName, lastName, dateOfBirth);
            return RedirectToAction("Management");
        }

        public IActionResult Delete(string firstName)
        {
            DeletePersonByFirstName(firstName);
            return RedirectToAction("Management");
        }

        // These methods are for data management. The body of the methods will be replaced with EF code tomorrow, but for now, we're just using a static list.
        public void CreatePerson(string firstName, string lastName, string dateOfBirth)
        {
            using (PersonContext context = new PersonContext())
            {
                context.People.Add(new Person()
                {
                    FirstName = firstName.Trim(),
                    LastName = lastName.Trim(),
                    DateOfBirth = DateTime.Parse(dateOfBirth.Trim())
                });
                context.SaveChanges();

            }
        }
        public void DeletePersonByFirstName(string firstName)
        {
            using (PersonContext context = new PersonContext())
            {
                context.People.Remove(GetPersonByFirstName(firstName));
                context.SaveChanges();
            }
        }

        public void AddEmailToPersonByID(string id, string address)
        {
            using (PersonContext context = new PersonContext())
            {
                context.EMailAddresses.Add(new EMailAddress()
                {
                    PersonID = int.Parse(id),
                    Address = address.Trim()
                });
                context.SaveChanges();
            }
        }

        public Person GetPersonByFirstName(string firstName)
        {
            Person found;
            using (PersonContext context = new PersonContext())
            {
                found = context.People.Where(x => x.FirstName.Trim().ToUpper() == firstName.Trim().ToUpper()).SingleOrDefault();
            }
            return found;
        }

        public Person GetPersonByID(string id)
        {
            Person found;
            using (PersonContext context = new PersonContext())
            {
                // Loading Option 1 - Eager:
                // .Include() allows data from associated tables to be loaded as part of an initial query. By default, only the table requested via the context property (People, in this case) is loaded.
                // found = context.People.Where(x => x.ID == int.Parse(id)).Include(x => x.EMailAddresses).SingleOrDefault();

                // Loading Option 2 - Explicit:
                found = context.People.Where(x => x.ID == int.Parse(id)).SingleOrDefault();

                // With explicit loading, we can load after the initial query. Use Reference() for singular navigation properties, and Collection() for plural.
                context.Entry(found).Collection(x => x.EMailAddresses).Load();
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