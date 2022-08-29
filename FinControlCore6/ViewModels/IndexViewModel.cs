using FinControlCore6.Data;
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

        private void setProperties()
        {
            TotalCount = context.TOuPurchases.Count();
            FilteredCount = context.TOuPurchases.Count();
        }

        public void LoadTOuPurchasesData(int start, int length)
        {
            Purchases = context.TOuPurchases.Skip(start).Take(length).ToList();
            setProperties();
        }
        public void LoadDataPropertiesNames()
        {
            DataPropertiesNames = Utils.Utils.GetPropertiesNames(Purchases.GetType().GetGenericArguments().First());
        }

    }
}
