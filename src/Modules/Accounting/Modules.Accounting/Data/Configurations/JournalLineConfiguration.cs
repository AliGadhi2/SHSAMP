using FSH.Modules.Accounting.Domain;
using FSH.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Modules.Catalog.Data.Configurations;

public sealed class JournalLineConfiguration : IEntityTypeConfiguration<JournalLine>
{
    public void Configure(EntityTypeBuilder<JournalLine> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("journal_lines");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.LineNumberId).IsRequired();
        builder.Property(x => x.AccountId).IsRequired();
        builder.Property(x => x.Amount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.MCAmount).HasColumnType("decimal(18,2)");
        builder.Property(x => x.CurrencyId).IsRequired();
        builder.Property(x => x.Notes).HasMaxLength(500);
      
        builder.Property(x => x.ExchangeRate).HasColumnType("decimal(18,6)");
        builder.HasIndex(x => x.LineNumberId);
        builder.HasIndex(x => x.AccountId);
        builder.Property(x=>x.RowVersion).IsRowVersion();

        builder.Ignore(x => x.DomainEvents);
    }
}

