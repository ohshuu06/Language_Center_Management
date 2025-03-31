using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Language_Center_Management.Models;
using Language_Center_Management.DataAccessLayer;

namespace Language_Center_Management.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }
        private Register_Course db = new Register_Course();

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string username, string password)
        {
            var student = db.ValidateUser(username, password);
            if (student != null)
            {
                // Nếu là sinh viên
                Session["StudentID"] = student.Student_ID;
                return RedirectToAction("Index", "Course");
            }

            // Nếu không phải sinh viên, kiểm tra giáo viên
            var teacher = db.ValidateTeacher(username, password);
            if (teacher != null)
            {
                // Nếu là giáo viên
                Session["TeacherID"] = teacher.Teacher_ID;
                return RedirectToAction("TeachingSchedule", "Teacher");
            }
            var protector = db.ValidateProtector(username, password);
            if (protector != null)
            {
                Session["ProtectorID"] = protector.Student_ID;
                return RedirectToAction("ViewSchedule", "Course", new { id = protector.Student_ID });
            }

            // Nếu không phải cả hai
            ViewBag.Message = "Invalid username or password";
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Login", "Account");
        }
    }
}