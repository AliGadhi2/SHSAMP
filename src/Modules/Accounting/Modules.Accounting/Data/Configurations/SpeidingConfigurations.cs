using FSH.Modules.Accounting.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace FSH.Modules.Accounting.Data.Configurations;
public class SpeindingConfigurations:IEntityTypeConfiguration<Spending>
{
    public void Configure(EntityTypeBuilder<Spending> builder)
    {  ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("Spendings");
        builder.HasKey(a => a.Id);
         builder.HasIndex(spending=> spending.IsDepanded);
      builder.HasIndex(a=>a.CurrencyId);
        builder.Property(a => a.Amount)
            .IsRequired().HasColumnType("decimal(18,2)");
        builder.HasOne(c => c.FromAccount)
            .WithMany()
            .HasForeignKey(c => c.FromAccountId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(c => c.ToAccount)
            .WithMany()
            .HasForeignKey(c => c.ToAccountId)
           .OnDelete(DeleteBehavior.Restrict);
        builder.HasIndex(c => c.FromAccountId);
        builder.HasIndex(c => c.ToAccountId);
        builder.Property(a=>a.RowVersion).IsRowVersion();
       
             builder.Property(a=>a.TheNumber).IsRequired().ValueGeneratedOnAdd();
               builder.Property(a=>a.EnterTime).IsRequired().ValueGeneratedOnAdd().HasDefaultValueSql("GETUTCDATE()");
        builder.HasIndex(p => p.IsDeleted);
builder.Property(a=>a.DelevarName).IsRequired().HasMaxLength(500);
builder.HasIndex(c=>c.TheDate);
builder.HasIndex(c=>c.TheNumber);

       
        
    }
}