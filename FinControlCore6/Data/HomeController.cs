using FinControlCore6.Models;
using FinControlCore6.Models.DatabaseModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace FinControlCore6.Data
{
    public class HomeController : Controller
    {
        private readonly FinControlDBContext context;
        private readonly ILogger<HomeController> _logger;

        public List<TOuPurchase> modelList { get; private set; }

        public HomeController(FinControlDBContext context, ILogger<HomeController> logger)
        {
            this.context = context;
            _logger = logger;

            modelList = context.TOuPurchases.ToList();
        }

        public IActionResult Index()
        {
            ViewBag.modelsList = modelList;
            return View(modelList.First());
        }

        public JsonResult TableDataSource(string draw, Dictionary<string, string>[] columns, Dictionary<string, string>[] order, int start, int length, Dictionary<string, string> search)
        {
            int drawInt = int.Parse(Request.Query["draw"]);
            int recordsTotal = modelList.Count;
            var dataList = modelList.Take(start..(start + length));
            int recordsFiltered = dataList.Count();
            var response = new DataTableResponse(drawInt, recordsTotal, recordsFiltered, dataList);
            JsonResult jsonResult = Json(response);
            string? strJsonResult = jsonResult.Value?.ToString();
            return jsonResult;
        }
        public record class DataTableResponse(int draw, int recordsTotal, int recordsFiltered, IEnumerable<TOuPurchase> data, string? error = null);

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