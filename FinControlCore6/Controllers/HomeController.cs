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
            _indexViewModel.Load();

            ViewBag.modelsList = modelList;
            return View(modelList.First());
        }

        public JsonResult TableDataSource([FromBody] DataTableParameters parameters)
        {
            _indexViewModel.Load();
            if (!ModelState.IsValid)
            {
                throw new Exception("Some model is invalid");
            }
            var dataTableResult = new DataTableResult<TOuPurchase>();
            dataTableResult.Draw = parameters.Draw;
            dataTableResult.RecordsTotal = (int)_indexViewModel.TotalCount;
            dataTableResult.RecordsFiltered = (int)_indexViewModel.FilteredCount;
            dataTableResult.Data = _indexViewModel.GetTOuPurchasesData(parameters.Start, parameters.Length);
            return Json(dataTableResult);
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