using FinControlCore6.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FinControlCore6.Components
{
    public class TableFromModel:ViewComponent
    {
        public IViewComponentResult Invoke(IEnumerable<string> model, string tagId)
        {
            ViewBag.TagId = tagId;
            return View(model);
        }
    }
}
