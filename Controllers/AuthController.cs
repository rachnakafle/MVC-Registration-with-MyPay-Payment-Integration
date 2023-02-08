using Microsoft.AspNetCore.Mvc;

namespace SchoolRegistrationForm.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
