using Microsoft.AspNetCore.Mvc;

namespace DistrictInfoAppv2.Controllers
{
    public class PlacesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(string id)
        {
            return View();
        }
    }
}
