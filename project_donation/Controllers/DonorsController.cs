using Microsoft.AspNetCore.Mvc;

namespace project_donation.Controllers
{
    public class DonorsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
