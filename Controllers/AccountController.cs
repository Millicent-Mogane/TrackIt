using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using TrackItApp.Models;
using TrackItApp.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TrackItApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _db;

        public AccountController(AppDbContext db)
        {
            _db = db;
        }

        // ========== REGISTER ==========
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(User user)
        {
            if (ModelState.IsValid)
            {
                if (_db.Users.Any(u => u.Email == user.Email))
                {
                    ModelState.AddModelError("Email", "Email already registered.");
                    return View();
                }

                user.PasswordHash = HashPassword(user.PasswordHash);
                _db.Users.Add(user);
                _db.SaveChanges();

                TempData["Success"] = "Registration successful! You can now login.";
                return RedirectToAction("Login");
            }
            return View();
        }

        // ========== LOGIN ==========
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            string hashedPassword = HashPassword(password);
            var user = _db.Users.FirstOrDefault(u => u.Email == email && u.PasswordHash == hashedPassword);

            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("UserRole", user.Role);
                HttpContext.Session.SetString("UserName", user.FullName);

                if (user.Role == "Admin")
                    return RedirectToAction("AdminDashboard", "Dashboard");
                else
                    return RedirectToAction("UserDashboard", "Dashboard");
            }

            ViewBag.Error = "Invalid email or password.";
            return View();
        }

        // ========== LOGOUT ==========
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // ========== PROFILE ==========
        [HttpGet]
        public IActionResult Profile()
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login");

            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
                return RedirectToAction("Login");

            ViewBag.FullName = user.FullName;
            ViewBag.Email = user.Email;
            ViewBag.Role = user.Role;

            return View(new ChangePasswordViewModel());
        }

        // ========== CHANGE PASSWORD ==========
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (HttpContext.Session.GetString("UserId") == null)
                return RedirectToAction("Login");

            if (model.NewPassword != model.ConfirmPassword)
            {
                ViewBag.Error = "New password and confirmation do not match.";
                return View("Profile", model);
            }

            var userId = int.Parse(HttpContext.Session.GetString("UserId"));
            var user = _db.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null || user.PasswordHash != HashPassword(model.OldPassword))
            {
                ViewBag.Error = "Old password is incorrect.";
                return View("Profile", model);
            }

            user.PasswordHash = HashPassword(model.NewPassword);
            _db.SaveChanges();

            ViewBag.Message = "Password updated successfully!";
            ViewBag.FullName = user.FullName;
            ViewBag.Email = user.Email;
            ViewBag.Role = user.Role;

            return View("Profile", new ChangePasswordViewModel());
        }

        // ========== HELPER ==========
        private string HashPassword(string password)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);
                byte[] hashBytes = sha.ComputeHash(inputBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}