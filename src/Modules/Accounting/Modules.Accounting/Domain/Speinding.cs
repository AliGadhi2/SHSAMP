
 
using FSH.Framework.Core.Domain;
namespace FSH.Modules.Accounting.Domain;

public class Spending :BaseEntity<Guid>, ISoftDeletable
{
  
    public long TheNumber { get; set; }
    public Account FromAccount { get; set; } = null!;
    public Guid FromAccountId { get; set; }
    public Account ToAccount { get; set; } = null!;
    public Guid ToAccountId { get; set; }
    public string TenantId { get; set; } = null!;
    public Guid CurrencyId { get; set; }           // ← الرقم (للفلترة/التعديل)
    public Currency Currency { get; set; }=null!;
    public decimal Amount { get; set; }
    public string Notes { get; set; } = null!;
    
    public uint RowVersion { get; set; }  
    public int BranchId { get; set; }
    public string? DelevarName { get; set; } = null!;
    public DateTime TheDate { get; set; }
    public bool IsDepanded{set;get;}
    public bool IsDeleted { get  ; set  ; }
    public DateTime? DeletedAt { get  ; set ; }
    public string? DeletedBy { get  ; set ; }
    public TimeSpan? EnterTime { get; set; }
       public JournalEntry JournalEntry { get; private set; } = default!;

        public DateTimeOffset? DeletedOnUtc { get; set; }

    public static Spending Create(long theNumber, Guid fromAccountId, Guid toAccountId, string tenantId, Guid currencyId, decimal amount, string notes, int branchId, string delevarName, DateTime theDate,bool isDepanded)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(notes);

        return new Spending
        { Id=Guid.CreateVersion7(),
            TheNumber = theNumber,
            FromAccountId = fromAccountId,
            ToAccountId = toAccountId,
            TenantId = tenantId,
            CurrencyId = currencyId,
            Amount = amount,
            Notes = notes.Trim(),
            BranchId = branchId,
            DelevarName = delevarName?.Trim(),
            TheDate = theDate,
            IsDepanded=isDepanded
        };
    }
}