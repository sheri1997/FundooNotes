using Microsoft.AspNetCore.Mvc;

namespace FundooNotes.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
