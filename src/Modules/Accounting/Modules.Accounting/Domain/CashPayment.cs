using FSH.Framework.Core.Domain;
 
 

namespace FSH.Modules.Accounting.Domain;

public sealed class CashPayment:BaseEntity<Guid>, ISoftDeletable
{
    public long TheNumber { get; set; }

    public Guid CurrencyID { get; set; }
    public Currency Currency { get; set; } = null!;

    public decimal Amount { get; set; }
    public string Delivery { get; set; } = string.Empty;
    public DateTime TheDate { get; set; }
    public Guid EntryID { get; set; }
    public string Notes { get; set; } = string.Empty;
    
    public bool IsDepanded{set;get;}
   
    public Guid ExchangeAccountID { get; set; }
    public Account ExchangeAccount { get; set; } = null!;

    public Guid AccountID { get; set; }
    public Account Account { get; set; } = null!;
    
    public uint RowVersion { get; set; } 
  
    public bool IsDeleted { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public TimeSpan? EnterTime { get; set; }
    

    public DateTimeOffset? DeletedOnUtc{get; set; }

    

    private CashPayment() { }
    public static CashPayment CreateCashPayment(
        long theNumber,
        Guid currencyID,
        decimal amount,
        string delivery,
        DateTime theDate,
        Guid entryID,
        string notes,
        
        bool isDepanded,
        decimal exchangeAmount,
        Guid exchangeCurrencyID,
        Guid exchangeAccountID,
        Guid accountID)
    {
        return new CashPayment
        {Id=Guid.CreateVersion7(),
            TheNumber = theNumber,
            CurrencyID = currencyID,
            Amount = amount,
            Delivery = delivery,
            TheDate = theDate,
            EntryID = entryID,
            Notes = notes,
           
            IsDepanded = isDepanded,
           
            ExchangeAccountID = exchangeAccountID,
            AccountID = accountID
        };
    }
    public void Delete(string? deletedBy)
    {
        if (IsDeleted) return;
        IsDeleted = true;
        DeletedOnUtc = DateTimeOffset.UtcNow;
        DeletedBy = deletedBy;
    }
    public void UpdateCashPayment(
        long theNumber,
        Guid currencyID,
        decimal amount,
        string delivery,
        DateTime theDate,
        Guid entryID,
        string notes,
        int theMethod,
        bool isDepanded,
        decimal exchangeAmount,
        Guid exchangeCurrencyID,
        Guid exchangeAccountID,
        Guid accountID)
    {
        TheNumber = theNumber;
        CurrencyID = currencyID;
        Amount = amount;
        Delivery = delivery;
        TheDate = theDate;
        EntryID = entryID;
        Notes = notes;
   
        IsDepanded = isDepanded;
       
        ExchangeAccountID = exchangeAccountID;
        AccountID = accountID;
    }

  }