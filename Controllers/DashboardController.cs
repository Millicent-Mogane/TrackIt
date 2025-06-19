using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace TrackItApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult AdminDashboard()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            // Check if the user is an Admin
            if (HttpContext.Session.GetString("UserRole") != "Admin")
                return RedirectToAction("Login", "Account");

            ViewBag.User = HttpContext.Session.GetString("UserName");
            return View();
        }

        public IActionResult UserDashboard()
        {
            // Check if the user is logged in
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login", "Account");

            // Check if the user is a User
            if (HttpContext.Session.GetString("UserRole") != "User")
                return RedirectToAction("Login", "Account");

            ViewBag.User = HttpContext.Session.GetString("UserName");
            return View();
        }
    }
}
