using System;
using System.Collections.Generic;

namespace FinControlCore6.Models
{
    /// <summary>
    /// Закупка подразделения
    /// </summary>
    public partial class TOuPurchase
    {
        /// <summary>
        /// Уникальный идентификатор
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Идентификатор операции
        /// </summary>
        public long OperationId { get; set; }
        /// <summary>
        /// Наименование поставщика
        /// </summary>
        public string SupplierName { get; set; } = null!;
        /// <summary>
        /// ИНН Поставщика
        /// </summary>
        public string SupplierInn { get; set; } = null!;
        /// <summary>
        /// ОГРН Поставщика
        /// </summary>
        public string? SupplierOgrn { get; set; }
        /// <summary>
        /// Общая сумма закупки по тексту договора (руб. с НДС)
        /// </summary>
        public decimal FullPriceWithVat { get; set; }
        /// <summary>
        /// Номер договора
        /// </summary>
        public string AgreementNum { get; set; } = null!;
        /// <summary>
        /// Наименование договора
        /// </summary>
        public string AgreementName { get; set; } = null!;
        /// <summary>
        /// Дата заключения договора
        /// </summary>
        public DateTime AgreementDate { get; set; }
        /// <summary>
        /// Срок действия договора
        /// </summary>
        public DateTime? AgreementDateTo { get; set; }
        /// <summary>
        /// Валюта договора
        /// </summary>
        public string AgreementCurrency { get; set; } = null!;
        /// <summary>
        /// Сумма по договору (спецификации)
        /// </summary>
        public decimal AgreementSum { get; set; }
        /// <summary>
        /// Дата поставки по договору
        /// </summary>
        public DateTime? AgreementDeliveryDate { get; set; }
        /// <summary>
        /// Сведения о нарушении условий договора
        /// </summary>
        public string? AgreementCorruptNote { get; set; }
        /// <summary>
        /// Срок платежа по договору
        /// </summary>
        public DateTime? AgreementPayDate { get; set; }
        /// <summary>
        /// Исполнитель/Куратор договора
        /// </summary>
        public string AgreementExec { get; set; } = null!;
        /// <summary>
        /// Основание для заключения договора
        /// </summary>
        public string? AgreementBasis { get; set; }
        /// <summary>
        /// Номер спецификации
        /// </summary>
        public string? SpecificationNum { get; set; }
        /// <summary>
        /// Дата заключения спецификации
        /// </summary>
        public DateTime? SpecificationDate { get; set; }
        /// <summary>
        /// Срок исполнения спецификации
        /// </summary>
        public DateTime? SpecificationDeadline { get; set; }
        /// <summary>
        /// Сумма аванса
        /// </summary>
        public decimal? AdvanceSum { get; set; }
        /// <summary>
        /// Дата оплаты аванса
        /// </summary>
        public DateTime? AdvanceDate { get; set; }
        /// <summary>
        /// Дата оплаты поставки
        /// </summary>
        public DateTime? PayDate { get; set; }
        /// <summary>
        /// Код товара
        /// </summary>
        public string ProductCode { get; set; } = null!;
        /// <summary>
        /// Наименование товара
        /// </summary>
        public string ProductName { get; set; } = null!;
        /// <summary>
        /// Количество
        /// </summary>
        public decimal Qty { get; set; }
        /// <summary>
        /// Единица измерения
        /// </summary>
        public string UomName { get; set; } = null!;
        /// <summary>
        /// Цена, без НДС
        /// </summary>
        public decimal PriceWithoutVat { get; set; }
        /// <summary>
        /// Количество дней отсрочки платежа/условия оплаты
        /// </summary>
        public string? PayNote { get; set; }
        /// <summary>
        /// Категория МТР
        /// </summary>
        public string MtrType { get; set; } = null!;
        /// <summary>
        /// Код категории МТР
        /// </summary>
        public string MtrTypeCode { get; set; } = null!;
        /// <summary>
        /// Дата поставки товара
        /// </summary>
        public DateTime? DeliveryDate { get; set; }
        /// <summary>
        /// Дата приема товара
        /// </summary>
        public DateTime? ReceiptDate { get; set; }
        /// <summary>
        /// Номер акта поставки
        /// </summary>
        public string? DeliveryActNum { get; set; }
        /// <summary>
        /// Номер акта приемки
        /// </summary>
        public string? ReceiptActNum { get; set; }
        /// <summary>
        /// № счет-фактуры
        /// </summary>
        public string? InvoiceNum { get; set; }
        /// <summary>
        /// Дата счет-фактуры
        /// </summary>
        public DateTime? InvoiceDate { get; set; }
        /// <summary>
        /// Склад/Место хранения/Место реализации-размещения товара в производство
        /// </summary>
        public string? Warehouse { get; set; }
        /// <summary>
        /// Категория запаса МРП (материал разового потребления), Аварийный запас, РПМ (регулярно потребляемый материал)
        /// </summary>
        public string? StockCategory { get; set; }
        /// <summary>
        /// Признак удаления
        /// </summary>
        public bool? IsDel { get; set; }
        /// <summary>
        /// Сумма оплаты поставки
        /// </summary>
        public decimal? DeliverySum { get; set; }

        public virtual TOuPurchaseOp Operation { get; set; } = null!;
    }
}
