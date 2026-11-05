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
    public Currency(){
    }
    public static Currency Create(string currencyName, string? isoCode = null)
    {
        var currency = new Currency
        {
            Id = Guid.NewGuid(),
            CurrencyName = currencyName,
            IsoCode = isoCode ?? string.Empty,
            TheNumber = 0,
            IsDeleted = false,
            RowVersion = 0
        };
        return currency;
    }
    public  void Update(Guid ID ,string CurrencyName,string? Iso)
    {
        this.CurrencyName = CurrencyName;
        IsoCode=Iso??"";
    }
}