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
                var LoginUser = db.Employees.Where(a => a.Email.Equals(User.Email));
                if (LoginUser != null)
                {
                    Session["EmpId"] = User.employeeId;
                    Session["UserName"] = User.Email;
                    return RedirectToAction("Index", "Employee");
                }
            }
            return View(User);
        }
    }
}