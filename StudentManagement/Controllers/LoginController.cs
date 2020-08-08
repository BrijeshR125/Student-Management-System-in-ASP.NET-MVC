using StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(User objchk) 
        {
            if (ModelState.IsValid)
            {
                using (StudentManagementEntities db = new StudentManagementEntities())
                {
                    var obj = db.Users.Where(a=>a.username.Equals(objchk.username) && a.password.Equals(objchk.password)).FirstOrDefault();

                    if (obj != null)
                    {
                        Session["user_id"] = obj.user_id.ToString();
                        Session["firstname"] = obj.firstname.ToString();
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Username of Password is incorrect");
                    }
                }
            }

            return View(objchk);
        }

        public ActionResult Logout() 
        {
            Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}