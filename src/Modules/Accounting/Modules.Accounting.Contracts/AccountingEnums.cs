using System.ComponentModel;
namespace FSH.Modules.Accounting.Contracts;

public enum AccountStatus
{
     None=0,
    Activ=1,
     
    Stop = 2
}
public enum AccountType
{
   Major = 0, Sub = 1
}
public enum DocumentType
{
    None,
    CashPayment = 1,
    CashReceipt = 2,
    Spending = 3,
    JournalEntry = 4
    
}
public enum AccountReference { BalanceSheet = 0,  Statement = 1 }