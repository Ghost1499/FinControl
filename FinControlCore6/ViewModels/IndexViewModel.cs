using FinControlCore6.Data;
using FinControlCore6.Models.DatabaseModels;
using System.ComponentModel.DataAnnotations;

namespace FinControlCore6.ViewModels
{
    public class IndexViewModel
    {
        //List<TOuPurchase> purchases;
        readonly FinControlDBContext context;
        
        [Required]
        public int? TotalCount { get; set; } = null;
        [Required]
        public int? FilteredCount { get; set; } = null;
        public IndexViewModel(FinControlDBContext context)
        {
            this.context = context;
        }

        public void Load()
        {
            TotalCount = context.TOuPurchases.Count();
            FilteredCount = context.TOuPurchases.Count();
        }

        public List<TOuPurchase> GetTOuPurchasesData(int start, int length)
        {
            var result = context.TOuPurchases.Skip(start).Take(length).ToList();
            return result;
        }
    }
}
