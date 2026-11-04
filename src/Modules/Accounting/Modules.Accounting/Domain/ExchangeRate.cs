using FSH.Framework.Core.Domain;
 

namespace FSH.Modules.Accounting.Domain;

public sealed class ExchangeRate:BaseEntity<Guid>,ISoftDeletable
{
    
    public Currency FromCurrency { get; private set; } = default!;
    public Guid FromCurrencyId { get; private set; }
    public Guid ToCurrencyId { get; private set; }
    public Currency ToCurrency { get; private set; } = default!;
 
   public uint RowVersion { get; set; } 
    public decimal Rate { get; private set; }
    public DateTime EffectiveDate { get; private set; }
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedOnUtc { get; private set; }
    public string? DeletedBy { get; private set; }
    public TimeSpan? EnterTime { get; set; }
    public string Note { get; private set; } = string.Empty;

    private ExchangeRate() { }

    public static ExchangeRate Create(string currencyCode, decimal rate, DateTime effectiveDate, Guid fromCurrencyId, Guid toCurrencyId, string note)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

        return new ExchangeRate
        {
            Id = Guid.CreateVersion7(),
            FromCurrencyId = fromCurrencyId,
            ToCurrencyId = toCurrencyId,
            Rate = rate,
            EffectiveDate = effectiveDate,
            Note = note
        };
    }

    public void Update(string currencyCode, decimal rate, DateTime effectiveDate, Guid fromCurrencyId, Guid toCurrencyId, string note)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

        FromCurrencyId = fromCurrencyId;
        ToCurrencyId = toCurrencyId;
        Rate = rate;
        EffectiveDate = effectiveDate;
        Note = note;
    }

    public void Restore()
    {
        if (!IsDeleted) return;
        IsDeleted = false;
        DeletedOnUtc = null;
        DeletedBy = null;
    }
    
}