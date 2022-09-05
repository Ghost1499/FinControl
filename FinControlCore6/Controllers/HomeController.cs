using FinControlCore6.Data;
using FinControlCore6.Models;
using FinControlCore6.Models.AuxiliaryModels;
using FinControlCore6.Models.DatabaseModels;
using FinControlCore6.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;

namespace FinControlCore6.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IndexViewModel indexViewModel;

        public HomeController(FinControlDBContext context,  ILogger<HomeController> logger)
        {
            indexViewModel = new IndexViewModel(context);
            _logger = logger;
        }

        public IActionResult Index()
        {
            indexViewModel.LoadDataPropertiesNames();
            return View(indexViewModel);
        }

        public JsonResult TableDataSource([FromBody] DataTableParameters parameters)
        {

            if (!ModelState.IsValid)
            {
                string errors = $"Количество ошибок: {ModelState.ErrorCount}. Ошибки в свойствах: ";
                foreach (var prop in ModelState.Keys)
                {
                    errors = $"{errors}{prop}; ";
                }
                throw new Exception(errors);
            }
            indexViewModel.LoadTOuPurchasesData(parameters);
            var dataTableResult = new DataTableResult<TOuPurchase>(parameters.Draw, (int)indexViewModel.TotalCount, (int)indexViewModel.FilteredCount, indexViewModel.Purchases);
            JsonResult jsonResult = Json(
                dataTableResult
                );
            return jsonResult;
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