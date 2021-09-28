using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using CourseWeb.Models;
using CourseWeb.Models.DBModels;

namespace CourseWeb.Controllers
{
    public class LogController : Controller
    {
        public static User Current;

        private RECIPIESCONTEXT db = new RECIPIESCONTEXT();
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(User user)
        {
            var userId = db.Users.Where(p => p.Username == user.Username).ToList();
            
            string message = string.Empty;
            switch (userId.Count())
            {
                case 0:
                    db.Users.Add(user);
                    db.SaveChanges();
                    ViewBag.Message = "Registration successful.\\nUser Id: " + user.ID.ToString();
                    return View("Index");
                default:
                    ViewBag.Message = "Username already exists.\\nPlease choose a different username.";
                    break;
            }
            return View("Register");
        }


        [HttpPost]
        [AllowAnonymous]
        public ActionResult Index(User user)
        {

            var userId = db.Users.Where(p=>p.Username == user.Username && p.Password == user.Password).ToList();

            string message = string.Empty;
            switch (userId.Count())
            {
                case 0:
                    message = "Username and/or password is incorrect.";
                    break;
                default:
                    User usercheck = userId[0];
                    Current = usercheck;
                    if (usercheck.Type == 1)
                    {
                        FormsAuthentication.SetAuthCookie(user.Username, false);
                        return Redirect("/RECIPIES/Index");
                    }
                    else return Redirect("/User/Index");
            }
            ViewBag.Message = message;
            return View(user);
        }

    }
}
