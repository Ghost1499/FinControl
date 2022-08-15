using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FinControlCore6
{
    public partial class FinControlDBContext : DbContext
    {
        public FinControlDBContext()
        {
        }

        public FinControlDBContext(DbContextOptions<FinControlDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TOuPurchase> TOuPurchases { get; set; } = null!;
        public virtual DbSet<TOuPurchaseOp> TOuPurchaseOps { get; set; } = null!;
        public virtual DbSet<TOuPurchaseSrc> TOuPurchaseSrcs { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=FinControlDB;Username=postgres;Password=password");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TOuPurchase>(entity =>
            {
                entity.ToTable("t_ou_purchase");

                entity.HasComment("Закупка подразделения");

                entity.HasIndex(e => new { e.SupplierName, e.SupplierInn, e.AgreementNum, e.AgreementName, e.SpecificationNum, e.ProductCode, e.ProductName }, "t_ou_purchase_un1")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('s_imp_seq'::regclass)")
                    .HasComment("Уникальный идентификатор");

                entity.Property(e => e.AdvanceDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("advance_date")
                    .HasComment("Дата оплаты аванса");

                entity.Property(e => e.AdvanceSum)
                    .HasColumnName("advance_sum")
                    .HasComment("Сумма аванса");

                entity.Property(e => e.AgreementBasis)
                    .HasMaxLength(4000)
                    .HasColumnName("agreement_basis")
                    .HasComment("Основание для заключения договора");

                entity.Property(e => e.AgreementCorruptNote)
                    .HasMaxLength(4000)
                    .HasColumnName("agreement_corrupt_note")
                    .HasComment("Сведения о нарушении условий договора");

                entity.Property(e => e.AgreementCurrency)
                    .HasMaxLength(2000)
                    .HasColumnName("agreement_currency")
                    .HasComment("Валюта договора");

                entity.Property(e => e.AgreementDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("agreement_date")
                    .HasComment("Дата заключения договора");

                entity.Property(e => e.AgreementDateTo)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("agreement_date_to")
                    .HasComment("Срок действия договора");

                entity.Property(e => e.AgreementDeliveryDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("agreement_delivery_date")
                    .HasComment("Дата поставки по договору");

                entity.Property(e => e.AgreementExec)
                    .HasMaxLength(4000)
                    .HasColumnName("agreement_exec")
                    .HasComment("Исполнитель/Куратор договора");

                entity.Property(e => e.AgreementName)
                    .HasMaxLength(2000)
                    .HasColumnName("agreement_name")
                    .HasComment("Наименование договора");

                entity.Property(e => e.AgreementNum)
                    .HasMaxLength(2000)
                    .HasColumnName("agreement_num")
                    .HasComment("Номер договора");

                entity.Property(e => e.AgreementPayDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("agreement_pay_date")
                    .HasComment("Срок платежа по договору");

                entity.Property(e => e.AgreementSum)
                    .HasColumnName("agreement_sum")
                    .HasComment("Сумма по договору (спецификации)");

                entity.Property(e => e.DeliveryActNum)
                    .HasMaxLength(4000)
                    .HasColumnName("delivery_act_num")
                    .HasComment("Номер акта поставки");

                entity.Property(e => e.DeliveryDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("delivery_date")
                    .HasComment("Дата поставки товара");

                entity.Property(e => e.DeliverySum)
                    .HasColumnName("delivery_sum")
                    .HasComment("Сумма оплаты поставки");

                entity.Property(e => e.FullPriceWithVat)
                    .HasColumnName("full_price_with_vat")
                    .HasComment("Общая сумма закупки по тексту договора (руб. с НДС)");

                entity.Property(e => e.InvoiceDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("invoice_date")
                    .HasComment("Дата счет-фактуры");

                entity.Property(e => e.InvoiceNum)
                    .HasMaxLength(4000)
                    .HasColumnName("invoice_num")
                    .HasComment("№ счет-фактуры");

                entity.Property(e => e.IsDel)
                    .HasColumnName("is_del")
                    .HasComment("Признак удаления");

                entity.Property(e => e.MtrType)
                    .HasMaxLength(4000)
                    .HasColumnName("mtr_type")
                    .HasComment("Категория МТР");

                entity.Property(e => e.MtrTypeCode)
                    .HasMaxLength(4000)
                    .HasColumnName("mtr_type_code")
                    .HasComment("Код категории МТР");

                entity.Property(e => e.OperationId)
                    .HasColumnName("operation_id")
                    .HasComment("Идентификатор операции");

                entity.Property(e => e.PayDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("pay_date")
                    .HasComment("Дата оплаты поставки");

                entity.Property(e => e.PayNote)
                    .HasMaxLength(4000)
                    .HasColumnName("pay_note")
                    .HasComment("Количество дней отсрочки платежа/условия оплаты");

                entity.Property(e => e.PriceWithoutVat)
                    .HasColumnName("price_without_vat")
                    .HasComment("Цена, без НДС");

                entity.Property(e => e.ProductCode)
                    .HasMaxLength(2000)
                    .HasColumnName("product_code")
                    .HasComment("Код товара");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(2000)
                    .HasColumnName("product_name")
                    .HasComment("Наименование товара");

                entity.Property(e => e.Qty)
                    .HasColumnName("qty")
                    .HasComment("Количество");

                entity.Property(e => e.ReceiptActNum)
                    .HasMaxLength(4000)
                    .HasColumnName("receipt_act_num")
                    .HasComment("Номер акта приемки");

                entity.Property(e => e.ReceiptDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("receipt_date")
                    .HasComment("Дата приема товара");

                entity.Property(e => e.SpecificationDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("specification_date")
                    .HasComment("Дата заключения спецификации");

                entity.Property(e => e.SpecificationDeadline)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("specification_deadline")
                    .HasComment("Срок исполнения спецификации");

                entity.Property(e => e.SpecificationNum)
                    .HasMaxLength(2000)
                    .HasColumnName("specification_num")
                    .HasComment("Номер спецификации");

                entity.Property(e => e.StockCategory)
                    .HasMaxLength(4000)
                    .HasColumnName("stock_category")
                    .HasComment("Категория запаса МРП (материал разового потребления), Аварийный запас, РПМ (регулярно потребляемый материал)");

                entity.Property(e => e.SupplierInn)
                    .HasMaxLength(2000)
                    .HasColumnName("supplier_inn")
                    .HasComment("ИНН Поставщика");

                entity.Property(e => e.SupplierName)
                    .HasMaxLength(2000)
                    .HasColumnName("supplier_name")
                    .HasComment("Наименование поставщика");

                entity.Property(e => e.SupplierOgrn)
                    .HasMaxLength(2000)
                    .HasColumnName("supplier_ogrn")
                    .HasComment("ОГРН Поставщика");

                entity.Property(e => e.UomName)
                    .HasMaxLength(4000)
                    .HasColumnName("uom_name")
                    .HasComment("Единица измерения");

                entity.Property(e => e.Warehouse)
                    .HasMaxLength(4000)
                    .HasColumnName("warehouse")
                    .HasComment("Склад/Место хранения/Место реализации-размещения товара в производство");

                entity.HasOne(d => d.Operation)
                    .WithMany(p => p.TOuPurchases)
                    .HasForeignKey(d => d.OperationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_ou_purchase_fk1");
            });

            modelBuilder.Entity<TOuPurchaseOp>(entity =>
            {
                entity.ToTable("t_ou_purchase_op");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('s_imp_seq'::regclass)")
                    .HasComment("Идентификатор операции");

                entity.Property(e => e.ContractorId)
                    .HasColumnName("contractor_id")
                    .HasComment("Заказчик/Общество");

                entity.Property(e => e.ContractorName)
                    .HasMaxLength(4000)
                    .HasColumnName("contractor_name")
                    .HasComment("Заказчик/Общество");

                entity.Property(e => e.LoadDate)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("load_date")
                    .HasComment("Дата загрузки");

                entity.Property(e => e.SrcId)
                    .HasColumnName("src_id")
                    .HasComment("Источник");

                entity.HasOne(d => d.Src)
                    .WithMany(p => p.TOuPurchaseOps)
                    .HasForeignKey(d => d.SrcId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("t_ou_purchase_op_fk1");
            });

            modelBuilder.Entity<TOuPurchaseSrc>(entity =>
            {
                entity.ToTable("t_ou_purchase_src");

                entity.HasComment("Система источник загружаемых данных (кто вызывает веб-сервис)");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('s_imp_seq'::regclass)")
                    .HasComment("Идентификатор записи");

                entity.Property(e => e.AccessKey)
                    .HasMaxLength(2000)
                    .HasColumnName("access_key")
                    .HasComment("Ключ доступа");

                entity.Property(e => e.Actual)
                    .HasColumnName("actual")
                    .HasComment("Актуальность");

                entity.Property(e => e.ContractorId)
                    .HasColumnName("contractor_id")
                    .HasComment("Заказчик/Общество");

                entity.Property(e => e.ContractorName)
                    .HasMaxLength(4000)
                    .HasColumnName("contractor_name")
                    .HasComment("Заказчик/Общество");
            });

            modelBuilder.HasSequence("s_imp_seq").IncrementsBy(1000);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
