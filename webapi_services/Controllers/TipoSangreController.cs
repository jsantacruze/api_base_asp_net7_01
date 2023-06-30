using Microsoft.AspNetCore.Mvc;

namespace webapi_services.Controllers
{
    public class TipoSangreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
