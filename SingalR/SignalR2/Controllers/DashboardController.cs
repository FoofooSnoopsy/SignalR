using Microsoft.AspNetCore.Mvc;

namespace SignalR2.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
