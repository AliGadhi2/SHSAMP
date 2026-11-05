using FSH.Framework.Core.Domain;
 using FSH.Modules.Accounting.Contracts;

namespace FSH.Modules.Accounting.Domain;
/// <summary>
/// An Account for tree Acounts for tanent Accounting Department
/// </summary>
public sealed class Account : AggregateRoot<Guid>,ISoftDeletable
{
 

    public long AccountNumber { get; private set; } = default!;
    public string AccountName { get; private set; } = default!;
    public AccountReference  AccountReference  {  get; private set; } = default!;

    public Account AccountFather {  set; get; } = null!;
    public Guid AccountFatherId {  set; get; }
     public int BranchID { private set; get; } = 1;
    public AccountStatus Status { get; private set; }
    public DateTime CreatedAtUtc { get; private set; }
    public AccountType AccountType { get; private set; }
   
    public string? Notes { get; private set; }

    public TimeSpan? EnterTime { get; set; }
    
    public uint RowVersion { get; set; }  

    public bool IsDeleted {set; get; } 

    public DateTimeOffset? DeletedOnUtc{set; get; }

    public string? DeletedBy {set; get; }
    public string Email {set; get; } = string.Empty;

    private Account() { }

    public static Account CreateAccount(
        long accountNumber,
        DateTime createdAtUtc,
        string accountName,
        Guid accountFather,string notes ,Guid userid,AccountType accountType,AccountReference accountRefrense)
        => CreateDraft(accountNumber, createdAtUtc, accountName, accountFather,
            notes, userid, accountType, accountRefrense);

    public static Account CreateDraft(
        long accountNumber,
        DateTime createdAtUtc,
        
        string accountName,

        Guid accountFather,
        string Notes,Guid userid, AccountType accountType,AccountReference  accountRefrense)
    {


        return new Account
        {
            Id = Guid.CreateVersion7(),
            AccountNumber = accountNumber,
            CreatedAtUtc = createdAtUtc,
            AccountName = accountName,
           
            AccountType = accountType,
            AccountReference = accountRefrense,
            AccountFatherId= accountFather,
            Notes = Notes,
            Status = AccountStatus.Activ,

        };
    }

     

    public void Issue(DateTime? dueAtUtc = null)
    {
        RequireStatus(AccountStatus.Activ);
        Status = AccountStatus.Activ;
     
           
    }
 

 

    public void SetNotes(string? notes)
    {
        
        Notes = notes;
    }

    private void RequireStatus(AccountStatus expected)
    {
        if (Status != expected)
        {
            throw new InvalidOperationException($"Operation requires invoice status {expected} but was {Status}.");
        }
    }
    public void MarkStop()
    {
        Status = AccountStatus.Stop; 
    }
    public void MarkActive()
    {
        Status = AccountStatus.Activ;
    }
    public void Depanding()
    {
        Status = AccountStatus.Activ;
    }
    public void UpdateAccountinfo(string accountName,
        long accountNumber,
        Guid accountFatherID,
        AccountType accountType,
        string notes,
        AccountReference  accountRefrense)
    {
        AccountName = accountName ?? throw new ArgumentNullException(nameof(accountName));
        AccountNumber = accountNumber;
        AccountReference  = accountRefrense;
        AccountType = accountType;
        Notes = notes;
        AccountFatherId = accountFatherID;

    }


}
