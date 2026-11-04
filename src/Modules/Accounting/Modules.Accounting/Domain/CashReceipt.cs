using FSH.Framework.Core.Domain;
 

namespace FSH.Modules.Accounting.Domain;
public sealed class CashReceipt :BaseEntity<Guid>, ISoftDeletable
{
    public string ReceiptNumber { get; private set; } = default!;
    public decimal Amount { get; private set; }
    public Guid CurrencyId { get; private set; }
    public Currency Currency { get; private set; } = default!;
    public Account ToAccount { get; private set; } = default!;
    public Guid ToAccountId { get; private set; }
    public Account ExchangeAccount { get; private set; } = default!;
    public Guid ExchangeAccountId { get; private set; }
    public DateTime ReceiptDate { get; private set; }
    public string? Notes { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public DateTime? UpdatedAtUtc { get; private set; }
 
   public uint RowVersion { get; set; }  
    public bool IsDeleted { get; private set; }
    public DateTimeOffset? DeletedOnUtc { get; private set; }
    public string? DeletedBy { get; private set; }
    public TimeSpan? EnterTime { get; set; }
       public JournalEntry JournalEntry { get; private set; } = default!;

    private CashReceipt() { }

    public static CashReceipt Create(string receiptNumber, decimal amount, DateTime receiptDate, string? notes, Guid currencyId, Guid toAccountId, Guid exchangeAccountId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(receiptNumber);
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        return new CashReceipt
        {     Id=Guid.CreateVersion7(),
           
            ReceiptNumber = receiptNumber.Trim(),
            Amount = amount,
            CurrencyId = currencyId,
            ToAccountId = toAccountId,
            ExchangeAccountId = exchangeAccountId,
            ReceiptDate = receiptDate,
            Notes = notes?.Trim(),
            CreatedAtUtc = DateTime.UtcNow
        };
    }

    public void Update(string receiptNumber, decimal amount, DateTime receiptDate, string? notes, Guid currencyId, Guid toAccountId, Guid exchangeAccountId)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(receiptNumber);
        if (amount <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero.");
        }

        ReceiptNumber = receiptNumber.Trim();
        Amount = amount;
        CurrencyId = currencyId;
        ToAccountId = toAccountId;
        ExchangeAccountId = exchangeAccountId;
        ReceiptDate = receiptDate;
        Notes = notes?.Trim();
        UpdatedAtUtc = DateTime.UtcNow;
    }

    public void Restore()
    {
        if (!IsDeleted) return;
        IsDeleted = false;
        DeletedOnUtc = null;
        DeletedBy = null;
        UpdatedAtUtc = DateTime.UtcNow;
    }
}