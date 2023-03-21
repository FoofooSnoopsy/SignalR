using Microsoft.AspNetCore.Mvc;

namespace SingalR.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
