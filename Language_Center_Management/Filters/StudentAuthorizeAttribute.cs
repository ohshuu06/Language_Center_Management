using System;
using System.Web;
using System.Web.Mvc;

namespace Language_Center_Management.Filters
{
    public class StudentAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            // Kiểm tra session CustomerID có tồn tại không
            return httpContext.Session["StudentID"] != null;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            // Nếu chưa đăng nhập thì chuyển hướng đến trang đăng nhập của khách hàng
            filterContext.Result = new RedirectResult("~/Account/Login");
        }
    }
}
