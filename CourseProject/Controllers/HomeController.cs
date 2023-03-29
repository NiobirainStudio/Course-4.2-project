using CourseProject.Data;
using CourseProject.Data.DBService;
using CourseProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Net;

namespace CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDB _dbManager;

        public HomeController(IDB dbManager)
        {
            _dbManager = dbManager;
        }

        private string GetIp()
        {
            string ipAddr = Dns.GetHostEntry(Dns.GetHostName()).AddressList[2].ToString();

            return ipAddr;
        }

        // Ajax call
        public int[] ChangeRating(int bookId, int newRating)
        {
            _dbManager.CreateEntry(bookId, GetIp(), newRating);

            return _dbManager.GetBookRating(bookId);
        }



        // Starting page
        public IActionResult Index()
        {
            ViewBag.Genres = _dbManager.GetDisplayGenres();

            return View(_dbManager.GetDisplayBooks(GetIp()));
        }

        // Search by terms page
        public IActionResult ByTerm(string searchTerm)
        {
            ViewBag.PickedTerm = searchTerm;
            ViewBag.Genres = _dbManager.GetDisplayGenres();

            return View(_dbManager.GetBooksByName(searchTerm, GetIp()));
        }

        // Search by genre page
        public IActionResult ByGenre(int genreId)
        {
            ViewBag.PickedGenre = _dbManager.GetGenreById(genreId);
            ViewBag.Genres = _dbManager.GetDisplayGenres();

            return View(_dbManager.GetBooksByGenre(genreId, GetIp()));
        }

        // Search by author page
        public IActionResult ByAuthor(int authorId)
        {
            ViewBag.PickedAuthor = _dbManager.GetAuthorById(authorId);
            ViewBag.Genres = _dbManager.GetDisplayGenres();

            return View(_dbManager.GetBooksByAuthor(authorId, GetIp()));
        }



        public IActionResult Book(int id)
        {
            ViewBag.Genres = _dbManager.GetDisplayGenres();
            ViewBag.Chapters = _dbManager.GetDisplayChapters(id);

            return View("DisplayBookPage", _dbManager.GetDisplayBookById(id, GetIp()));
        }

        public IActionResult Read(int bookId, int chapterId)
        {
            ViewBag.Genres = _dbManager.GetDisplayGenres();
            return View(_dbManager.GetDisplayChapter(bookId, chapterId));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}