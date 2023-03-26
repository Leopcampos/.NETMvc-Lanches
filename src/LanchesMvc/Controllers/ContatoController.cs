using Microsoft.AspNetCore.Mvc;

namespace LanchesMvc.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}