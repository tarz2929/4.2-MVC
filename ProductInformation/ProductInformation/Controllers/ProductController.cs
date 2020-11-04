using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductInformation.Models;

namespace ProductInformation.Controllers
{
    public class ProductController : Controller
    {
        // Actions (View Endpoints):
        public IActionResult Index()
        {
            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult List()
        {
            ViewBag.Products = GetProducts();
            return View();
        }



        // Data Methods:
        public List<Product> GetProducts()
        {
            List<Product> results;
            using (ProductInfoContext context = new ProductInfoContext())
            {
                results = context.Products.Include(x => x.Category).ToList();
            }
            return results;
        }
    }
}