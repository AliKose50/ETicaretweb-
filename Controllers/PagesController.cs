using Microsoft.AspNetCore.Mvc;
using WebSıtesı.Data;
using WebSıtesı.Models;
using System.Linq;

namespace WebSıtesı.Controllers
{
    public class PagesController : Controller
    {
        private readonly AppDbContext _context;

        public PagesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult Products()
        {
            var Products = _context.Products.ToList();
            return View(Products);
        }

        public IActionResult Contact()
        {
            return View();
        }
    }
}
