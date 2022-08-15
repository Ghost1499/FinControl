using System;
using System.Collections.Generic;

namespace FinControlCore6
{
    /// <summary>
    /// Система источник загружаемых данных (кто вызывает веб-сервис)
    /// </summary>
    public partial class TOuPurchaseSrc
    {
        public TOuPurchaseSrc()
        {
            TOuPurchaseOps = new HashSet<TOuPurchaseOp>();
        }

        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Заказчик/Общество
        /// </summary>
        public long? ContractorId { get; set; }
        /// <summary>
        /// Заказчик/Общество
        /// </summary>
        public string? ContractorName { get; set; }
        /// <summary>
        /// Ключ доступа
        /// </summary>
        public string? AccessKey { get; set; }
        /// <summary>
        /// Актуальность
        /// </summary>
        public bool? Actual { get; set; }

        public virtual ICollection<TOuPurchaseOp> TOuPurchaseOps { get; set; }
    }
}
