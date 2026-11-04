using FSH.Framework.Core.Domain;
 

namespace FSH.Modules.Accounting.Domain;
public class Currency:BaseEntity<Guid>,ISoftDeletable
{
            // ← للاستخدام في العمليات
    public string CurrencyName { get; set; }="";   // ← للعرض
    public int TheNumber { get; set; } 
    public bool IsDeleted { get ; set ; }
    public DateTime? DeletedAt { get  ; set ; }
    public string? DeletedBy { get  ; set ; }
   
    public uint RowVersion { get; set; } 
    public string IsoCode { get; set; } = string.Empty;
    public TimeSpan? EnterTime { get; set; }

    public DateTimeOffset? DeletedOnUtc{get; set; }
}