using OntapASP.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.Web.Mvc;

namespace OntapASP.Controllers
{
    public class LoginController : Controller
    {
        private Company db = new Company();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]                       
        public ActionResult Login(string username, string password)
        {
            var user = db.Accouts.Where(u => u.username == username && u.password == password).FirstOrDefault();
            if(user == null)
            {
                ViewBag.errorMsg = "Sai tên đăng nhập hoặc mật khẩu";
                return View("Login");
            }
            else
            {
                Session["username"] = username;
                return RedirectToAction("Index", "Management");
            }
        }
        public ActionResult Logout()
        {
            Session["username"] = null;
            return RedirectToAction("Login","Login");
        }
    }
}