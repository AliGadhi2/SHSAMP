using FSH.Modules.Accounting.Domain;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FSH.Modules.Accounting.Data.Configurations;
public sealed class CashPaymentConfiguration : IEntityTypeConfiguration<CashPayment>
{
    public void Configure(EntityTypeBuilder<CashPayment> builder)
    {  
         ArgumentNullException.ThrowIfNull(builder);
          builder.HasKey(x => x.Id);
        builder.Property(x => x.RowVersion).IsRowVersion();
          builder.Property(x => x.Amount).IsRequired().HasColumnType("decimal(18,2)");
         
            builder.HasIndex(x => x.TheDate);
        builder.HasIndex(x => x.CurrencyID);
         builder.HasOne(c => c.ExchangeAccount)
            .WithMany()
            .HasForeignKey(c => c.ExchangeAccountID)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.Account)
            .WithMany()
            .HasForeignKey(c => c.AccountID)
           .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.AccountID);
    }
}