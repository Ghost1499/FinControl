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
        private readonly IndexViewModel _indexViewModel;

        public HomeController(FinControlDBContext context,  ILogger<HomeController> logger)
        {
            _indexViewModel = new IndexViewModel(context);
            _logger = logger;
        }

        public IActionResult Index()
        {
            _indexViewModel.LoadDataPropertiesNames();
            return View(_indexViewModel);
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
            var dataTableResult = new DataTableResult<TOuPurchase>();
            _indexViewModel.LoadTOuPurchasesData(parameters);
            dataTableResult.Draw = parameters.Draw;
            dataTableResult.RecordsTotal = (int)_indexViewModel.TotalCount;
            dataTableResult.RecordsFiltered = (int)_indexViewModel.FilteredCount;
            dataTableResult.Data = _indexViewModel.Purchases;
            JsonResult jsonResult = Json(
                dataTableResult
                );
            return jsonResult;
            //return Json("");
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