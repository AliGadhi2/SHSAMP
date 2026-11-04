using FSH.Framework.Core.Domain;


namespace FSH.Modules.Accounting.Domain;
public sealed class JournalLine: BaseEntity<Guid>, ISoftDeletable
{
    
      public Account Account { get; set; } = null!;
    public Guid AccountId { get; set; }
     public Guid CurrencyId { get; set; }           // ← الرقم (للفلترة/التعديل)
    public Currency Currency { get; set; }=null!;
    public decimal Amount { get; set; }
    public decimal MCAmount { get; set; }
    public decimal ExchangeRate { get; set; }
    public string Notes { get; set; } = null!;
  
    public uint RowVersion { get; set; } 
   
    public bool IsDeleted { get;  set; }
    public DateTimeOffset? DeletedOnUtc { get;  set; }
    public string? DeletedBy { get;  set; }

    public JournalEntry JournalEntry { get; private set; } = default!;
    public Guid LineNumberId { get; private set; }

    private JournalLine() { }
public static JournalLine Create( Guid accountId, Guid currencyId, decimal amount, decimal mcAmount, decimal exchangeRate, string notes, Guid lineNumberId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(notes);

        return new JournalLine
        {
            Id = Guid.CreateVersion7(),
            AccountId = accountId,
            CurrencyId = currencyId,
            Amount = amount,
            MCAmount = mcAmount,
            ExchangeRate = exchangeRate,
            Notes = notes.Trim(),
            LineNumberId = lineNumberId
        };
    }
 
}