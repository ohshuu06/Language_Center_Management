using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Language_Center_Management.DataAccessLayer;
using Language_Center_Management.Filters;
using Language_Center_Management.Models;

namespace Language_Center_Management.Controllers
{
    
    public class CourseController : Controller
    {
        // GET: Course
        private Register_Course db = new Register_Course();

        public ActionResult Index()
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }
            string studentId = Session["StudentID"].ToString();

            var courses = db.GetCourseSchedules();
            var registeredCourses = db.GetRegisteredCourses(studentId);

            ViewBag.RegisteredCourses = registeredCourses.Select(c => c.Course_ID).ToList();

            return View(courses);
        }

        public ActionResult Register(string id)
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string studentId = Session["StudentID"].ToString();

            // Kiểm tra xem sinh viên đã có các môn tiên quyết chưa
            bool hasPrerequisites = db.HasCompletedPrerequisites(studentId, id);
            if (!hasPrerequisites)
            {
                TempData["ErrorMessage"] = "You must complete the prerequisite courses before registering for this course.";
                return RedirectToAction("Index");
            }

            bool isRegistered = db.RegisterCourse(studentId, id);
            if (isRegistered)
            {
                TempData["SuccessMessage"] = "You have successfully registered for the course.";
            }
            else
            {
                TempData["ErrorMessage"] = "You are already registered for this course.";
            }

            return RedirectToAction("ViewSchedule");
        }

        public ActionResult RegisteredCourses()
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string studentId = Session["StudentID"].ToString();
            var registeredCourses = db.GetRegisteredCourses(studentId);
            return View("Schedule", registeredCourses);
        }

        public ActionResult Classroom()
        {
            var classrooms = db.GetClassrooms();
            return View(classrooms);
        }

        public ActionResult CourseSchedule()
        {
            if (Session["StudentID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string studentId = Session["StudentID"].ToString();
            var schedules = db.GetRegisteredCourseSchedules(studentId);
            return View("Schedule", schedules);
        }
        public ActionResult ViewSchedule(string id)
        {
            // Kiểm tra nếu chưa đăng nhập
            if (Session["StudentID"] == null && Session["ProtectorID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string studentId;

            // Nếu là Student
            if (Session["StudentID"] != null)
            {
                studentId = Session["StudentID"].ToString();
            }
            // Nếu là Protector
            else if (Session["ProtectorID"] != null)
            {
                studentId = Session["ProtectorID"].ToString();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }

            var schedules = db.GetRegisteredCourseSchedules(studentId);

            if (schedules == null || !schedules.Any())
            {
                TempData["ErrorMessage"] = "No registered courses found!";
                return RedirectToAction("Index");
            }

            return View("Schedule", schedules);

        }
    }
}