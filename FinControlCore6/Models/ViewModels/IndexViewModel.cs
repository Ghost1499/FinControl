using FinControlCore6.Data;
using FinControlCore6.Extensions;
using FinControlCore6.Models.AuxiliaryModels;
using FinControlCore6.Models.DatabaseModels;
using FinControlCore6.Utils;
using Microsoft.EntityFrameworkCore;
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

        private async Task setCountersAsync(IQueryable<TOuPurchase> filtered)
        {
            TotalCount = await context.TOuPurchases.CountAsync();
            FilteredCount = await filtered.CountAsync();
        }

        public async Task LoadTOuPurchasesDataAsync(DataTableParameters parameters)
        {
            var result = context.TOuPurchases.AsQueryable();
            Dictionary<string,string> columnsSearches = new Dictionary<string,string>();
            string globalSearch = parameters.Search?.Value ?? "";
            if (globalSearch != "")
                columnsSearches[""] = globalSearch;

            IEnumerable<DataTableColumn> searchColumns = parameters.Columns?.Where(c => (c.Search?.Value ?? "") != "") ?? new DataTableColumn[0];
            foreach (DataTableColumn datatableColumn in searchColumns)
            {
                columnsSearches[datatableColumn.Data!] = datatableColumn.Search!.Value!;
            }
            result = result.WhereDynamic(columnsSearches);

            DataTableOrder order = parameters.Order.First();
            result = result.OrderByDynamic(parameters.Columns[order.Column].Data, order.Dir);
            await setCountersAsync(result);
            Purchases = await result.Skip(parameters.Start).Take(parameters.Length).ToListAsync();
        }
        public void LoadDataPropertiesNames()
        {
            DataPropertiesNames = Utils.Utils.GetPropertiesNames(Purchases.GetType().GetGenericArguments().First());
        }

    }
}
