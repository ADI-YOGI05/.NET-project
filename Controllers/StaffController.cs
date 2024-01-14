using LibraryManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    [ResponseCache(NoStore = true, Duration = 0)]
    public class StaffController : Controller
    {
        // GET: UserController
        public ActionResult Index()
        {
            return View();
        }

        // GET: UserController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserController/Register
        public ActionResult Register()
        {
            Staff obj=new Staff();
            obj.Email = Request.Cookies["Email"] ?? null;
            obj.Password = Request.Cookies["Password"] ?? null;
            if(obj.Email!=null && obj.Password != null)
            {
                Staff staff = Staff.ValidateStaff(obj);
                if (staff == null)
                {

                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    HttpContext.Session.SetString("Email", obj.Email);
                    return RedirectToAction("Index", "Book");
                }
            }
            return View();
        }

        // POST: UserController/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Staff s, IFormCollection collection)
        {
            try
            {
                Staff.AddStaff(s);
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserController/Edit/5
        public ActionResult Login()
        {
            
            return View();
        }

        // POST: UserController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Staff s,string chk1, IFormCollection collection)
        {
            try
            {
                Staff staff = Staff.ValidateStaff(s);
                if (staff == null)
                {

                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    if (chk1 == "true")
                    {
                        CookieOptions option = new CookieOptions();
                        option.Expires = DateTime.Now.AddMinutes(1);

                        Response.Cookies.Append("Email", s.Email, option);
                        Response.Cookies.Append("Password", s.Password, option);
                    }
                    HttpContext.Session.SetString("Email", s.Email);
                    return RedirectToAction("Index", "Book");
                }
            }
            catch
            {
                return View();
            }
        }

        
        public ActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            HttpContext.Session.Clear();
            Response.Cookies.Delete("Email");
            Response.Cookies.Delete("Password");
            return RedirectToAction(nameof(Login));
        }

        // POST: UserController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
