using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Shift_Scheduler.Models;

namespace Shift_Scheduler.Controllers
{
    public class LoginController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Login
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Employee User)
        {
            if (ModelState.IsValid)
            {
                var LoginUser = (from e in db.Employees
                                 where e.Email == User.Email && e.passWord == User.passWord
                                 select e).FirstOrDefault();

                if (LoginUser != null)
                {
                    Session["EmpId"] = LoginUser.employeeId;
                    Session["Roles"] = LoginUser.role;
                    return RedirectToAction("RoleRouting");
                }
            }
            return View(User);
        }

        public ActionResult RoleRouting()
        {
            if (Session["Roles"].ToString().Equals("Manager"))
            {
                return RedirectToAction("Dashboard", "Manager");
            }
            else if (Session["Roles"].ToString().Equals("Employee"))
            {
                return RedirectToAction("Index", "Employee");
            }
            return View(User);
        }

        public ActionResult LogOff()
        {
            Session.Clear();
            return RedirectToAction("Login", "Login");
        }

        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Register()
        {
            if(ModelState.IsValid)
        }
    }
}