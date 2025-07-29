using Microsoft.AspNetCore.Mvc;

namespace TrainComponentApi.Controllers
{
    public class ComponentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
