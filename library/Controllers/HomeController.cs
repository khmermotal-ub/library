using library.Data;
using library.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace library.Controllers
{
    public class HomeController : Controller
    {
        // ASP.NET Core hands in the repository, because Program.cs registered it.
        private readonly BookRepository _books;

        public HomeController(BookRepository books)
        {
            _books = books;
        }

        // async: the action waits for the database without holding up the server.
        public async Task<IActionResult> Index()
        {
            ViewBag.BookCount = await _books.CountAsync();   // one number from the database
            return View();                                   // show Views/Home/Index.cshtml
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

