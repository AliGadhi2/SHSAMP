using FSH.Modules.Accounting.Domain;
using FSH.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Modules.Accounting.Data.Configurations;

public sealed class CashReceiptsConfiguration : IEntityTypeConfiguration<CashReceipt>
{
    public void Configure(EntityTypeBuilder<CashReceipt> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("cash_receipts");
        builder.HasKey(x => x.Id);
        
         builder.Property(receiving => receiving.ReceiptNumber)
            .IsRequired()
            .ValueGeneratedOnAdd();
        builder.Property(x => x.ReceiptDate).IsRequired();
        builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
        builder.Property(x => x.CurrencyId).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(2048);
        builder.Property(x => x.IsDeleted).HasDefaultValue(false);
        builder.Property(x => x.DeletedOnUtc);
        builder.Property(x => x.DeletedBy).HasMaxLength(256);
        builder.HasIndex(x => x.ReceiptNumber)
            .IsUnique()
            .HasDatabaseName("ux_cash_receipt_number");
        builder.HasIndex(x => x.ReceiptDate);
        builder.HasIndex(x => x.CurrencyId);
         builder.HasOne(c => c.ExchangeAccount)
            .WithMany()
            .HasForeignKey(c => c.ExchangeAccountId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.ToAccount)
            .WithMany()
            .HasForeignKey(c => c.ToAccountId)
           .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.ToAccountId);
   builder.Property(x=>x.RowVersion).IsRowVersion();
  builder.HasIndex(x => x.JournalEntry);
          
        builder.Ignore(x => x.DomainEvents);
    }
}

