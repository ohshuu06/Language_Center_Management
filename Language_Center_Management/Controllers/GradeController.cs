using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Language_Center_Management.Models;
using Language_Center_Management.DataAccessLayer;

namespace Language_Center_Management.Controllers
{
    public class GradeController : Controller
    {
        private Register_Course db = new Register_Course();

        // GET: Grade

        public ActionResult Index()
        {
            // Kiểm tra nếu không có ProtectorID và StudentID trong Session
            if (Session["ProtectorID"] == null && Session["StudentID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Nếu có ProtectorID thì dùng ID đó
            string studentId = Session["ProtectorID"] != null ? Session["ProtectorID"].ToString() : Session["StudentID"].ToString();

            var grades = db.GetAllGradesByStudent(studentId);

            if (grades == null || !grades.Any())
            {
                TempData["ErrorMessage"] = "No grades found!";
                return RedirectToAction("Index", "Course");
            }

            return View("Index", grades); 
        }
    }
}