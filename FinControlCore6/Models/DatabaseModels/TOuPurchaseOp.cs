using System;
using System.Collections.Generic;

namespace FinControlCore6.Models.DatabaseModels
{
    public partial class TOuPurchaseOp
    {
        public TOuPurchaseOp()
        {
            TOuPurchases = new HashSet<TOuPurchase>();
        }

        /// <summary>
        /// Идентификатор операции
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Источник
        /// </summary>
        public long SrcId { get; set; }
        /// <summary>
        /// Дата загрузки
        /// </summary>
        public DateTime? LoadDate { get; set; }
        /// <summary>
        /// Заказчик/Общество
        /// </summary>
        public long? ContractorId { get; set; }
        /// <summary>
        /// Заказчик/Общество
        /// </summary>
        public string? ContractorName { get; set; }

        public virtual TOuPurchaseSrc Src { get; set; } = null!;
        public virtual ICollection<TOuPurchase> TOuPurchases { get; set; }
    }
}
