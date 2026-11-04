using FSH.Modules.Accounting.Domain;
using FSH.Modules.Catalog.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Modules.Catalog.Data.Configurations;

public sealed class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
{
    public void Configure(EntityTypeBuilder<JournalEntry> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("journal_entries");
        builder.HasKey(x => x.Id);
       
        builder.Property(x => x.EntryNumber).ValueGeneratedOnAdd();
            
        builder.Property(x => x.EntryDate).IsRequired();
        builder.Property(x => x.RowNumber).IsRequired();
        builder.Property(x => x.DocumentType).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(500);
          builder.Navigation(x => x.JournalLines).AutoInclude();
        builder.Property(x => x.CreatedAtUtc);
        builder.Property(x => x.UpdatedAtUtc);
        builder.HasMany(x => x.JournalLines)
            .WithOne()
            .HasForeignKey(i => i.LineNumberId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasIndex(x => x.EntryNumber)
            .IsUnique()
            .HasDatabaseName("ux_entry_number");
        builder.HasIndex(x => x.ISDepanded);
         builder.HasIndex(x => x.EntryDate);
        builder.HasKey(x => new { x.DocumentType, x.RowNumber });
       builder.HasIndex(x => new { x.DocumentType, x.RowNumber })
       .IsUnique(); 
        builder.Ignore(x => x.DomainEvents);
    }
}

