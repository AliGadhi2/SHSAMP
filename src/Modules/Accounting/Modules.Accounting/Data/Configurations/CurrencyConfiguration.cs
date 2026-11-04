using FSH.Modules.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FSH.Modules.Accounting.Data.Configurations;
public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.HasKey(c => c.Id);
        builder.ToTable("Currencies");
        builder.HasKey(c => c.Id);
        builder.Property(c => c.CurrencyName).IsRequired().HasMaxLength(100);
        builder.Property(c => c.IsoCode).IsRequired().HasMaxLength(5);
        builder.HasIndex(c => c.CurrencyName).IsUnique();
        builder.Property(c => c.TheNumber).IsRequired().ValueGeneratedOnAdd();
        builder.HasIndex(c => c.IsoCode).IsUnique();
        builder.Property(a=>a.RowVersion).IsRowVersion();
        
    }
}