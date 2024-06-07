using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
