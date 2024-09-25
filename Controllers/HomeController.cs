using Microsoft.AspNetCore.Mvc;
using Projeto_RenalPrime.Web.Models;
using System.Diagnostics;

namespace Projeto_RenalPrime.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Investimento() => View();
        public IActionResult JornadaPaciente() => View();
        public IActionResult Contatos() => View();
        public IActionResult Dialog_Plus() => View();
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
