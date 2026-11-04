
using FSH.Modules.Accounting.Contracts;
using FSH.Modules.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FSH.Modules.Accounting.Data.Configurations;

public sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("accounts");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.AccountNumber).IsRequired();
        
        builder.Property(x => x.AccountName).IsRequired().HasMaxLength(200);
        builder.Property(x => x.AccountFather).IsRequired();
        builder.Property(x => x.Status).HasConversion<int>();
        builder.Property(x => x.Status).HasConversion<int>().HasDefaultValue(AccountStatus.Activ);
        builder.Property(x => x.CreatedAtUtc);
        builder.Property(x => x.AccountType).IsRequired();
     builder.Property(x => x.Email).IsRequired().HasMaxLength(200);
        builder.Property(x => x.Notes).HasMaxLength(2048);

        builder.HasIndex(x => x.AccountName)
           .IsUnique()
           .HasDatabaseName("ux_account_name");
        builder.HasIndex(x => x.AccountNumber)
            .IsUnique()
            .HasDatabaseName("ux_account_number");
        builder.HasIndex(x => x.Status);
      builder.Property(x => x.AccountReference)
       .HasConversion<int>()
       .IsRequired();
        builder.Property(x => x.AccountType)
       .HasConversion<int>()
       .IsRequired();

        builder.Ignore(x => x.DomainEvents);
    }
}
