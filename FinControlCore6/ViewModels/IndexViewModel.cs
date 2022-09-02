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

        private void setCounters()
        {
            TotalCount = context.TOuPurchases.Count();
            FilteredCount = Purchases.Count();
        }

        public void LoadTOuPurchasesData(DataTableParameters parameters)
        {
            var result = context.TOuPurchases.AsQueryable();
            DataTableOrder order = parameters.Order[0];
            result = result.OrderByDynamic(parameters.Columns[order.Column].Data, order.Dir);
            Purchases = result;
            setCounters();
            Purchases = Purchases.Skip(parameters.Start).Take(parameters.Length).ToList();
        }
        public void LoadDataPropertiesNames()
        {
            DataPropertiesNames = Utils.Utils.GetPropertiesNames(Purchases.GetType().GetGenericArguments().First());
        }

    }
}
