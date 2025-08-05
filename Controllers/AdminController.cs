//using Microsoft.AspNetCore.Mvc;
//using WebSıtesı.Data;
//using WebSıtesı.Models;

//namespace WebSıtesı.Controllers
//{
//    public class AdminController : Controller
//    {
//        private readonly AppDbContext _context;

//        public AdminController(AppDbContext context)
//        {
//            _context = context;
//        }

//        [HttpGet]
//        public IActionResult Login()
//        {
//            return View();
//        }



//        [HttpPost]
//        public async Task<IActionResult> Create(Product model, IFormFile ImageFile)
//        {
//            if (ImageFile != null && ImageFile.Length > 0)
//            {
//                // Dosya adını benzersiz yapmak için timestamp veya GUID kullanabiliriz
//                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
//                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/uploads");

//                // Klasör yoksa oluştur
//                if (!Directory.Exists(uploadsFolder))
//                {
//                    Directory.CreateDirectory(uploadsFolder);
//                }

//                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

//                using (var stream = new FileStream(filePath, FileMode.Create))
//                {
//                    await ImageFile.CopyToAsync(stream);
//                }

//                // Görselin URL yolunu kaydet
//                model.ImageUrl = "/images/" + uniqueFileName;
//            }

//            _context.Products.Add(model);
//            await _context.SaveChangesAsync();

//            return RedirectToAction("Index");
//        }
//        public IActionResult Login(User model)
//        {
//            if (!ModelState.IsValid)
//                return View(model);

//            var user = _context.Users
//                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

//            if (user == null)
//            {
//                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
//                return View(model);
//            }

//            HttpContext.Session.SetString("IsLoggedIn", "true");
//            return RedirectToAction("Index", "Admin");
//        }

//        public IActionResult Logout()
//        {
//            HttpContext.Session.Clear();
//            return RedirectToAction("Login");
//        }

//        public IActionResult Index()
//        {
//            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
//                return RedirectToAction("Login");

//            var products = _context.Products.ToList();
//            return View(products);
//        }

//    }
//}
using Microsoft.AspNetCore.Mvc;
using WebSıtesı.Data;
using WebSıtesı.Models;

namespace WebSıtesı.Controllers
{
    public class AdminController : Controller
    {
        private readonly AppDbContext _context;

        public AdminController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(User model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _context.Users
                .FirstOrDefault(u => u.Username == model.Username && u.Password == model.Password);

            if (user == null)
            {
                ModelState.AddModelError("", "Geçersiz kullanıcı adı veya şifre.");
                return View(model);
            }

            HttpContext.Session.SetString("IsLoggedIn", "true");
            return RedirectToAction("Index", "Admin");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("IsLoggedIn") != "true")
                return RedirectToAction("Login");

            var products = _context.Products.ToList();
            return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product model, IFormFile ImageFile)
        {
            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                model.ImageUrl = "/images/" + uniqueFileName;
            }

            _context.Products.Add(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Product model, IFormFile ImageFile)
        {
            var existingProduct = await _context.Products.FindAsync(model.Id);

            if (existingProduct == null)
                return NotFound();

            existingProduct.Name = model.Name;
            existingProduct.Category = model.Category;
            existingProduct.Price = model.Price;
            existingProduct.Stock = model.Stock;

            if (ImageFile != null && ImageFile.Length > 0)
            {
                var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await ImageFile.CopyToAsync(stream);
                }

                existingProduct.ImageUrl = "/images/" + uniqueFileName;
            }

            _context.Products.Update(existingProduct);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
