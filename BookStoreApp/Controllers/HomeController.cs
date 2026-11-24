using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BookStoreApp.Models;
using BookStoreApp.Repositories;
using BookStoreApp.ViewModels;

namespace BookStoreApp.Controllers
{
    [Authorize] // Sirf logged-in user hi access kar sakega
    public class HomeController : Controller
    {
        private readonly IBookRepository _bookRepo;
        private readonly IAuthorRepository _authorRepo;
        private readonly IDashboardRepository _dashboardRepo;

        // Dependency Injection of all 3 Repositories
        public HomeController(IBookRepository bookRepo, IAuthorRepository authorRepo, IDashboardRepository dashboardRepo)
        {
            _bookRepo = bookRepo;
            _authorRepo = authorRepo;
            _dashboardRepo = dashboardRepo;
        }

        public IActionResult Index()
        {
            var vm = new HomeViewModel
            {
                Books = _bookRepo.GetAllBooks(),
                Authors = _authorRepo.GetAllAuthors()
            };
            return View(vm);
        }

        // 1. $.get endpoint (Live Stats & Books Data)
        [HttpGet]
        public IActionResult GetDashboardStats()
        {
            return Json(new
            {
                totalBooks = _dashboardRepo.GetTotalBooksCount(),
                totalAuthors = _dashboardRepo.GetTotalAuthorsCount(),
                books = _bookRepo.GetAllBooks()
            });
        }

        // 2. $.post endpoint (Add Author via AJAX)
        [HttpPost]
        public IActionResult AddAuthorAjax(string name, string country)
        {
            if (string.IsNullOrWhiteSpace(name))
                return BadRequest("Author name required");

            _authorRepo.AddAuthor(new Author { Name = name, Country = country });
            return Json(new { success = true, authors = _authorRepo.GetAllAuthors() });
        }

        // 3. $.ajax endpoint (Add Book via AJAX)
        [HttpPost]
        public IActionResult AddBookAjax([FromBody] Book book)
        {
            if (book == null || string.IsNullOrWhiteSpace(book.Title))
                return BadRequest("Invalid book data");

            _bookRepo.AddBook(book);
            return Json(new { success = true });
        }

        // 4. $.ajax endpoint (Delete Book via AJAX)
        [HttpPost]
        public IActionResult DeleteBookAjax(int id)
        {
            _bookRepo.DeleteBook(id);
            return Json(new { success = true });
        }

        public IActionResult About() => View();
        public IActionResult Contact() => View();
    }
}