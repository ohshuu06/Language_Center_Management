using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Language_Center_Management.Filters
{
    public class TeacherAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Kiểm tra session EmployeeID có tồn tại không
            return httpContext.Session["TeacherID"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Nếu chưa đăng nhập thì chuyển hướng đến trang đăng nhập của nhân viên
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}