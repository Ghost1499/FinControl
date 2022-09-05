using FinControlCore6.Data;
using FinControlCore6.Extensions;
using FinControlCore6.Models.AuxiliaryModels;
using FinControlCore6.Models.DatabaseModels;
using FinControlCore6.Utils;
using System.ComponentModel.DataAnnotations;

namespace FinControlCore6.ViewModels
{
    public class IndexViewModel
    {
        readonly FinControlDBContext context;
        
        [Required]
        public int? TotalCount { get; set; } = null;
        [Required]
        public int? FilteredCount { get; set; } = null;
        public IEnumerable<TOuPurchase> Purchases { get; set; } = new List<TOuPurchase>();
        public IEnumerable<string> DataPropertiesNames { get; set; } = new List<string>();

        public IndexViewModel(FinControlDBContext context)
        {
            this.context = context;
        }

        private void setCounters(IQueryable<TOuPurchase> filtered)
        {
            TotalCount = context.TOuPurchases.Count();
            FilteredCount = filtered.Count();
        }

        public void LoadTOuPurchasesData(DataTableParameters parameters)
        {
            var result = context.TOuPurchases.AsQueryable();
            var searchQuery = parameters.Search?.Value ?? "";
            //var searchQuery = "Новый";
            string propName = "";
            if (string.IsNullOrEmpty(searchQuery))
            {
                var searchColumn = parameters.Columns?.Where(c => c.Search.Value != "")?.FirstOrDefault();
                searchQuery = searchColumn?.Search?.Value ?? "";
                propName = searchColumn?.Data ?? "";
            }
            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                result = result.WhereDynamic(searchQuery, propName);
            }

            DataTableOrder order = parameters.Order.First();
            result = result.OrderByDynamic(parameters.Columns[order.Column].Data, order.Dir);
            setCounters(result);
            Purchases = result.Skip(parameters.Start).Take(parameters.Length).ToList();
        }
        public void LoadDataPropertiesNames()
        {
            DataPropertiesNames = Utils.Utils.GetPropertiesNames(Purchases.GetType().GetGenericArguments().First());
        }

    }
}
