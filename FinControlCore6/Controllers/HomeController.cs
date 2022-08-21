using FinControlCore6.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace FinControlCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly FinControlDBContext context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(FinControlDBContext context, ILogger<HomeController> logger)
        {
            this.context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model_list =  context.TOuPurchases.ToList();
            ViewBag.modelsList = model_list;
            return View(model_list.First());
        }

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