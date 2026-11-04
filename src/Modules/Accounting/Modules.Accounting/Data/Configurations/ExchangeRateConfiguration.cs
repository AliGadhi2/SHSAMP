using FSH.Modules.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FSH.Modules.Accounting.Data.Configurations;
public sealed class ExchangeRateConfiguration : IEntityTypeConfiguration<ExchangeRate>
{
    public void Configure(EntityTypeBuilder<ExchangeRate> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(x => x.Id);
        builder.ToTable("ExchangeRates");

        builder.HasKey(x => x.Id);

        

        builder.Property(x => x.FromCurrencyId)
            .IsRequired();

        builder.Property(x => x.ToCurrencyId)
            .IsRequired();

        builder.Property(x => x.Rate)
            .IsRequired()
            .HasPrecision(18, 6);

        builder.Property(x => x.EffectiveDate)
            .IsRequired();

      

        builder.Property(x => x.IsDeleted)
            .IsRequired();

        builder.Property(x => x.DeletedOnUtc);

        builder.Property(x => x.DeletedBy)
            .HasMaxLength(256);
          
        builder.HasOne(c => c.FromCurrency)
            .WithMany()
            .HasForeignKey(c => c.FromCurrencyId)
           .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.ToCurrency)
            .WithMany()
            .HasForeignKey(c => c.ToCurrencyId)
           .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.ToCurrency)
            .WithMany()
            .HasForeignKey(c => c.ToCurrencyId)
           .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.FromCurrencyId);
        builder.HasIndex(c => c.ToCurrencyId);
              
               builder.HasKey(x => new { x.FromCurrencyId, x.ToCurrencyId });
       builder.HasIndex(x => new { x.FromCurrencyId, x.ToCurrencyId })
       .IsUnique(); 
              builder.Property(x=>x.RowVersion).IsRowVersion();

    }
}