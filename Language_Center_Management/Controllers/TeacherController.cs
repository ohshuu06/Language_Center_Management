using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Language_Center_Management.Models;
using Language_Center_Management.DataAccessLayer;
using Language_Center_Management.Filters;

namespace Language_Center_Management.Controllers
{
    [TeacherAuthorize]
    public class TeacherController : Controller
    {
        // GET: Teacher
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TeachingSchedule()
        {
            if (Session["TeacherID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string teacherId = Session["TeacherID"].ToString();
            Register_Course registerCourse = new Register_Course();
            List<CourseSchedule> schedules = registerCourse.GetTeachingSchedule(teacherId);

            return View(schedules);
        }
        public ActionResult GradeSchedule()
        {
            if (Session["TeacherID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            string teacherId = Session["TeacherID"].ToString();
            Register_Course registerCourse = new Register_Course();
            List<Grade> schedules = registerCourse.GetAllGradesByTeacher(teacherId);

            // Kiểm tra xem danh sách có rỗng không
            if (schedules == null)
            {
                schedules = new List<Grade>();
            }

            return View(schedules);
        }
    }
}