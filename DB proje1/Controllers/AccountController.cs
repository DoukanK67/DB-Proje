using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SmartCourseSelectorWeb.Models;

namespace SmartCourseSelectorWeb.Controllers
{
    [Controller]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AccountController(ApplicationDbContext context)
        {
            _context = context;
        }

        
        public IActionResult LoginUser()
        {
            return View();
        }

        
        [HttpPost("LoginUser")]

        public async Task<ActionResult> LoginUser(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Username && u.PasswordHash == model.Password && u.Role == model.Role);

                if (model.Username == "admin" && model.Password == "123")
                {
                    return RedirectToAction("GetAdminWindow", "Admin");
                }
                if (user != null)
                {
                    
                    if (user.Role == "Student")
                    {
                        return RedirectToAction("CourseSelection", "Students", new { id = user.RelatedID });
                    }
                    else if (user.Role == "Advisor")
                    {
                        return RedirectToAction("ApproveCourses", "Advisors", new { id = user.RelatedID });
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid username, password, or role.";
                }
            }
            return View(model);
        }

        
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    
                    
                    string resetLink = Url.Action("ResetPassword", "Account", new { email = model.Email }, Request.Scheme);

                    

                    ViewBag.Message = "A password reset link has been sent to your email.";
                }
                else
                {
                    ViewBag.Message = "Email address not found.";
                }
            }

            return View(model);
        }
    }
}
