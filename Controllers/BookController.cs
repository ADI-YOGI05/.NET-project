using LibraryManagement.Models;
using LibraryManagement.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraryManagement.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class BookController : Controller
    {
        // GET: HomeController1
        public ActionResult Index()
        {
            string s = HttpContext.Session.GetString("Email");
            if (s == null)
            {
                return RedirectToAction("Login", "Staff");
            }
            List<Books> b = Books.GetAllBooks();
            return View(b);
        }

        public ActionResult GetAllBooksByType()
        {
            string s = HttpContext.Session.GetString("Email");
            if (s == null)
            {
                return RedirectToAction("Login", "Staff");
            }
            List<Categories> lstCat = Categories.GetAllCategories();
            return View(lstCat);
        }

        // GET: HomeController1/Details/5
        public ActionResult Details(int BookId)
        {
            string s = HttpContext.Session.GetString("Email");
            if (s == null)
            {
                return RedirectToAction("Login", "Staff");
            }
            Books b = Books.GetSingleBook(BookId);
            return View(b);
        }

        // GET: HomeController1/Create
        public ActionResult Create()
        {
            string s = HttpContext.Session.GetString("Email");
            if (s == null)
            {
                return RedirectToAction("Login", "Staff");
            }
            List<SelectListItem> objCategories = Categories.GetAllCategoriesSelectListItem();
            BookViewModel b = new BookViewModel();
            b.Categories = objCategories;

            return View(b);
        }

        // POST: HomeController1/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(BookViewModel book, Categories c, IFormCollection collection)
        {
            try
            {
                string s = HttpContext.Session.GetString("Email");
                if (s == null)
                {
                    return RedirectToAction("Login", "Staff");
                }
                Books b = new Books();
                b.BookId = book.BookId;
                b.Author = book.Author;
                b.Title = book.Title;
                b.Category = c;
                Books.AddBook(b);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Edit/5
        public ActionResult Edit(int BookId)
        {
            string s = HttpContext.Session.GetString("Email");
            if (s == null)
            {
                return RedirectToAction("Login", "Staff");
            }
            Books book = Books.GetSingleBook(BookId);
            List<SelectListItem> objCategories = Categories.GetAllCategoriesSelectListItem();
            BookViewModel b = new BookViewModel();
            
            b.BookId = book.BookId;
            b.Author = book.Author;
            b.Title = book.Title;
            b.Categories = objCategories;
            return View(b);
        }

        // POST: HomeController1/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(BookViewModel book,Categories c, IFormCollection collection)
        {
            try
            {
                string s = HttpContext.Session.GetString("Email");
                if (s == null)
                {
                    return RedirectToAction("Login", "Staff");
                }
                Books b = new Books();
                b.BookId = book.BookId;
                b.Author = book.Author;
                b.Title = book.Title;
                b.Category = c;
                Books.UpdateWithStoredProcedure(b);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: HomeController1/Delete/5
        public ActionResult Delete(int BookId)
        {
            string s = HttpContext.Session.GetString("Email");
            if (s == null)
            {
                return RedirectToAction("Login", "Staff");
            }
            Books b = Books.GetSingleBook(BookId);
            return View(b);
        }

        // POST: HomeController1/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int BookId, IFormCollection collection)
        {
            try
            {
                string s = HttpContext.Session.GetString("Email");
                if (s == null)
                {
                    return RedirectToAction("Login", "Staff");
                }
                Books.DeleteWithStoredProcedure(BookId);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
